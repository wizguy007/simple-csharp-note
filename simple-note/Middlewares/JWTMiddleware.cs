using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using simple_note.Modules.Auth;
using simple_note.Modules.User;

namespace simple_note.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private JwtService _jwtService;
        private UserService _userService;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
          
        }

        public async Task Invoke(HttpContext httpContext, JwtService jwtService, UserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;

            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if(token != null)
            {
                attachAccountToContext(httpContext, token);
            }

            await _next(httpContext);

            //return _next(httpContext);
        }

        private void attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var jwtToken = _jwtService.Verify(token);
                int userId = int.Parse(jwtToken.Issuer);
                context.Items["User"] = _userService.FindById(userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JWTMiddlewareExtensions
    {
        public static IApplicationBuilder UseJWTMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JWTMiddleware>();
        }
    }
}
