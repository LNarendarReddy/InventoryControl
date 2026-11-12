using Serilog;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NSRetailPOS.Logging
{
    public static class AppLog
    {
        public static void Info(string message)
        {
            Log.Information(message);
        }

        public static void Warn(string message)
        {
            Log.Warning(message);
        }

        public static void Error(Exception ex, string message = "")
        {
            Log.Error(ex, message);
        }

        public static void Debug(string message)
        {
            Log.Debug(message);
        }

        public static string PaymentRequest(string gateway, Uri url, string body)
        {
            string message = gateway + " POST " + SanitizePaymentMessage(url.AbsoluteUri)
                + " request body: " + SanitizePaymentMessage(body);
            Log.Information("{PaymentMessage}", message);
            return message;
        }

        public static string PaymentResponse(string gateway, string action, HttpResponseMessage response, string body)
        {
            string correlation = "";
            foreach (string name in new[] { "CF-Ray", "X-Request-ID", "Request-ID", "Server" })
                if (response.Headers.TryGetValues(name, out var values))
                    correlation += " " + name + "=" + SanitizePaymentMessage(string.Join(",", values));
            string message = gateway + " " + action + " HTTP " + (int)response.StatusCode
                + " Content-Type=" + response.Content?.Headers.ContentType + correlation
                + " response body: " + SanitizePaymentMessage(body);
            Log.Information("{PaymentMessage}", message);
            return message;
        }

        public static string SanitizePaymentMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return message ?? "";
            string result = message;
            int start = message.IndexOf('{');
            if (start < 0) start = message.IndexOf('[');
            if (start >= 0)
            {
                try
                {
                    JToken json = JToken.Parse(message.Substring(start));
                    RedactPaymentFields(json);
                    result = message.Substring(0, start) + json.ToString(Formatting.None);
                }
                catch (JsonException) { }
            }
            // Fallback for non-JSON provider messages and credentials embedded in URLs.
            result = Regex.Replace(result,
                @"(?i)(""[^""]*(?:password|token|secret|privatekey)[^""]*""\s*:\s*)""(?:\\.|[^""\\])*""", "$1\"[REDACTED]\"");
            result = Regex.Replace(result, @"(?i)\bBearer\s+[A-Za-z0-9._~+/=-]+", "Bearer [REDACTED]");
            result = Regex.Replace(result, @"(?i)([?&](?:password|token|secret|key|client_secret)=)[^&#\s]*", "$1[REDACTED]");
            result = result.Replace("\r", " ").Replace("\n", " ");
            return result.Length > 8192 ? result.Substring(0, 8192) + " [truncated]" : result;
        }

        private static bool IsSensitivePaymentField(string name)
        {
            string normalized = (name ?? "").Replace("_", "").Replace("-", "").Replace(" ", "").ToLowerInvariant();
            return normalized.Contains("password") || normalized.Contains("token") || normalized.Contains("secret")
                || normalized.Contains("privatekey") || normalized == "key" || normalized == "authorization"
                || normalized == "checksum" || normalized == "pan" || normalized.Contains("cardnumber")
                || normalized.Contains("cardholder") || normalized == "cvv" || normalized.StartsWith("track")
                || normalized == "qrdata" || normalized == "image" || normalized == "uservpa" || normalized == "usercell";
        }

        private static void RedactPaymentFields(JToken token)
        {
            if (token is JObject obj)
            {
                string tag = Convert.ToString(obj["Tag"] ?? obj["tag"]);
                foreach (var property in obj.Properties().ToList())
                {
                    if (IsSensitivePaymentField(property.Name)
                        || (property.Name.Equals("value", StringComparison.OrdinalIgnoreCase) && IsSensitivePaymentField(tag)))
                        property.Value = "[REDACTED]";
                    else RedactPaymentFields(property.Value);
                }
            }
            else if (token is JArray array)
                foreach (var item in array) RedactPaymentFields(item);
        }
    }
}
