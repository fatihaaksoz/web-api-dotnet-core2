using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDemo.CustomerMiddlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            string authHeader = httpContext.Request.Headers["Authorization"];

            if (authHeader==null)
            {
                await _next(httpContext);
                return;
            }

            //basic engin:12345
            //authHeader.StartsWith("basic",StringComparison.OrdinalIgnoreCase basic ile başlıyorsa kontrolü

            if (authHeader!=null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring(6).Trim();
                var credentialString = "";

                
                credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                
                   
                

                var credentials = credentialString.Split(":");

                if (credentials[0] == "engin" && credentials[1] == "12345")
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Role,"Admin") // Role verilmektedir.
                    };

                    var identity = new ClaimsIdentity(claims, "basic");
                    httpContext.User = new ClaimsPrincipal(identity);
                }
                else
                {
                    httpContext.Response.StatusCode = 401;
                }
            }


            await _next(httpContext);
        }

    }
}
