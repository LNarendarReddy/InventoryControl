//-----------------------------------------------------------------------
// <copyright file="AppOAuthProvider.cs" company="None">
//     Copyright (c) Allow to distribute this code.
// </copyright>
// <author>Asma Khalid</author>
//-----------------------------------------------------------------------

namespace NSRetail.Helper_Code.OAuth2
{
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using NSRetail.Helper_Code.Wrapper;
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Text;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using NSRetail.Models;

    /// <summary>
    /// Application OAUTH Provider class.
    /// </summary>
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        #region Private Properties

        /// <summary>
        /// Public client ID property.
        /// </summary>
        private readonly string _publicClientId;

        
        #endregion

        #region Default Constructor method.

        /// <summary>
        /// Default Constructor method.
        /// </summary>
        /// <param name="publicClientId">Public client ID parameter</param>
        public AppOAuthProvider(string publicClientId)
        {
            //TODO: Pull from configuration
            if (publicClientId == null)
            {
                throw new ArgumentNullException(nameof(publicClientId));
            }

            // Settings.
            _publicClientId = publicClientId;
        }

        #endregion
        
        #region Grant resource owner credentials override method.

        /// <summary>
        /// Grant resource owner credentials overload method.
        /// </summary>
        /// <param name="context">Context parameter</param>
        /// <returns>Returns when task is completed</returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Initialization.
            string usernameVal = context.UserName;
            string passwordVal = context.Password;
            NSRetail.Controllers.UsersController usercontrol = new Controllers.UsersController();
            if (string.IsNullOrEmpty(usernameVal) || string.IsNullOrEmpty(passwordVal))
            {
                context.SetError("invalid_request", "Parameter(s) are missing");
                return;
            }
            List<TBLUSER> user = usercontrol.Login(usernameVal, passwordVal).ToList();
            // Verification.
            if (user == null || user.Count() <= 0)
            {
                ApiError apiError = null;
                APIResponse apiResponse = null;
                HttpRequestMessage api = new HttpRequestMessage();
                apiError = new ApiError("The user name or password is incorrect.", "501");
                apiResponse = new APIResponse(null, apiError);
                // Settings.
                //context.SetError(JsonConvert.SerializeObject(apiResponse));
                context.SetError("invalid_request", "The user name or password is incorrect");
                // Retuen info.
                return;
            }

            // Initialization.
            var claims = new List<Claim>();
            var userInfo = user.FirstOrDefault();

            // Setting
            claims.Add(new Claim(ClaimTypes.Name, userInfo.EMAIL));

            // Setting Claim Identities for OAUTH 2 protocol.
            ClaimsIdentity oAuthClaimIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesClaimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);

            // Setting user authentication.
            AuthenticationProperties properties = CreateProperties(Convert.ToString(userInfo.USERID));
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthClaimIdentity, properties);

            // Grant access to authorize user.
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesClaimIdentity);
        }

        #endregion

        #region Token endpoint override method.

        /// <summary>
        /// Token endpoint override method
        /// </summary>
        /// <param name="context">Context parameter</param>
        /// <returns>Returns when task is completed</returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                // Adding.
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            // Return info.
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Validate Client authntication override method

        /// <summary>
        /// Validate Client authntication override method
        /// </summary>
        /// <param name="context">Contect parameter</param>
        /// <returns>Returns validation of client authentication</returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                // Validate Authoorization.
                context.Validated();
            }

            // Return info.
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Validate client redirect URI override method

        /// <summary>
        /// Validate client redirect URI override method
        /// </summary>
        /// <param name="context">Context parmeter</param>
        /// <returns>Returns validation of client redirect URI</returns>
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            // Verification.
            if (context.ClientId == _publicClientId)
            {
                // Initialization.
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                // Verification.
                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    // Validating.
                    context.Validated();
                }
            }

            // Return info.
            return Task.FromResult<object>(null);
        }

        #endregion

        #region Create Authentication properties method.

        /// <summary>
        /// Create Authentication properties method.
        /// </summary>
        /// <param name="userName">User name parameter</param>
        /// <returns>Returns authenticated properties.</returns>
        public static AuthenticationProperties CreateProperties(string UserId)
        {
            // Settings.
            IDictionary<string, string> data = new Dictionary<string, string>
                                               {
                                                   { "UserId", UserId }
                                               };

            // Return info.
            return new AuthenticationProperties(data);
        }

        #endregion
    }
    public class CustomAuthorization : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            ApiError apiError = null;
            APIResponse apiResponse = null;
            HttpRequestMessage api = new HttpRequestMessage();
            apiError = new ApiError("User unauthorized to access this resource", "501");
            apiResponse = new APIResponse(null, apiError);
            //HttpResponseMessage message = api.CreateResponse(HttpStatusCode.OK, apiResponse);
            //actionContext.Response = message;
            actionContext.Response = new HttpResponseMessage
            {

                //return Request.CreateResponse(HttpStatusCode.OK, apiResponse);
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent(JsonConvert.SerializeObject(apiResponse), Encoding.UTF8, "application/json")
            };
        }
    }
}