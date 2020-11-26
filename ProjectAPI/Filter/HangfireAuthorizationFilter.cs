using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Filter
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private static readonly string HangFireCookieName = "HangFireCookie";
        private static readonly int CookieExpirationMinutes = 60;
        private TokenValidationParameters tokenValidationParameters;
        private string role;

        public HangfireAuthorizationFilter(TokenValidationParameters tokenValidationParameters, string role = null)
        {
            this.tokenValidationParameters = tokenValidationParameters;
            this.role = role;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            var access_token = String.Empty;
            var setCookie = false;

            // try to get token from query string
            if (httpContext.Request.Query.ContainsKey("access_token"))
            {
                access_token = httpContext.Request.Query["access_token"].FirstOrDefault();
                setCookie = true;
            }
            else
            {
                access_token = httpContext.Request.Cookies[HangFireCookieName];
            }

            if (String.IsNullOrEmpty(access_token))
            {
                return false;
            }

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(access_token);
                var tokenS = handler.ReadToken(access_token) as JwtSecurityToken;
                string tokenRole = tokenS.Claims.First(claim => claim.Type == "role").Value;
                if (!String.IsNullOrEmpty(this.role) && !this.role.Equals(tokenRole))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            if (setCookie)
            {
                httpContext.Response.Cookies.Append(HangFireCookieName,
                access_token,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(CookieExpirationMinutes)
                });
            }
            return true;
        }
    }
}
