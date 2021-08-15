using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using simple_note.User;

namespace simple_note.Guards
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthGuard : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var account = (UserEntity)context.HttpContext.Items["User"];
            if (account == null)
            {
                context.Result = new UnauthorizedResult();
                //context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }

}
