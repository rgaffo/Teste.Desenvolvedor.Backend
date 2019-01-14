using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Superdigital.Backend.ContaCorrente.Security
{
    public class LoginFilter : IAuthorizationFilter
    {
        private readonly string domain;

        public LoginFilter(string domain = null)
        {
            this.domain = domain;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                // Get the encoded username and password
                var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();

                // Decode from Base64 to string
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                // Split username and password
                var username = decodedUsernamePassword.Split(':', 2)[0];
                var password = decodedUsernamePassword.Split(':', 2)[1];
                
                // Check if login is correct
                if (IsAuthorized(username, password))
                {
                    return;
                }
            }
            
            // Return authentication type (causes browser to show login dialog)
            context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic";
            
            // Add realm if it is not null
            if (!string.IsNullOrWhiteSpace(domain))
            {
                context.HttpContext.Response.Headers["WWW-Authenticate"] += $" realm=\"{domain}\"";
            }
            
            // Return unauthorized
            context.Result = new UnauthorizedResult();
        }

        public bool IsAuthorized(string username, string password)
        {
            return username.Equals("superdigital", StringComparison.InvariantCultureIgnoreCase) && password.Equals("123456*");
        }
    }
}