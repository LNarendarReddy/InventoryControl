﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System;


namespace NSRetail.Helper_Code.Wrapper
{
    public class WrappingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (IsSwagger(request))
            {
                return await base.SendAsync(request, cancellationToken);
            }
            else
            {
                var response = await base.SendAsync(request, cancellationToken);
                return BuildApiResponse(request, response);
            }
        }

        private static HttpResponseMessage BuildApiResponse(HttpRequestMessage request, HttpResponseMessage response)
        {

            dynamic content = null;
            object data = null;
            object data2 = null;
            string errorMessage = null;
            ApiError apiError = null;

            var code = (int)response.StatusCode;

            if (response.TryGetContentValue(out content) && !response.IsSuccessStatusCode)
            {
                HttpError error = content as HttpError;

                //handle exception
                if (error != null)
                {
                    content = null;

                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                        apiError = new ApiError("The specified URI does not exist. Please verify and try again.", "502");
                    }
                    else if (response.StatusCode == HttpStatusCode.NoContent)
                        apiError = new ApiError("The specified URI does not contain any content.");
                    else
                    {
                        errorMessage = error.Message;

#if DEBUG
                        errorMessage = string.Concat(errorMessage, error.ExceptionMessage, error.StackTrace);
#endif

                        apiError = new ApiError(errorMessage);
                    }

                    data = new APIResponse(null, apiError);

                }
                else
                    data = content;
            }
            else
            {
                if (response.TryGetContentValue(out content))
                {
                    Type type;
                    type = content?.GetType();

                    if (type.Name.Equals("APIResponse"))
                    {
                        //response.StatusCode = Enum.Parse(typeof(HttpStatusCode), content.StatusCode.ToString());
                        data = content;
                        //data2 = new APIErrorDetail((int)content.StatusCode, content.Message);
                        //content = (object)data2;
                    }
                    else if (type.Name.Equals("SwaggerDocument"))
                        data = content;
                    else
                        data = new APIResponse(null, content);
                }
                else
                {
                    if (code == 200)
                        apiError = new ApiError("No Data Found", "201");
                    else
                        apiError = new ApiError("User unauthorized to access this resource", "501");
                    //if (response.IsSuccessStatusCode)
                    data = new APIResponse(null, apiError);
                }
            }

            var newResponse = request.CreateResponse(response.StatusCode, data);

            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }

        private bool IsSwagger(HttpRequestMessage request)
        {
            return request.RequestUri.PathAndQuery.StartsWith("/swagger");
        }
    }
   
}
