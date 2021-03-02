using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebCore5Lab1.ActionFilters
{
    public class RequiredClaimFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            IIdentity identity = context.HttpContext.User.Identity;
            var claims = context.HttpContext.User.Claims;

            if (identity.Name.ToLower() != "gelis")
            {
                context.Result = new ForbidResult();
            }
        }
    }

    public class RequiredClaimAttribute : TypeFilterAttribute
    {
        public RequiredClaimAttribute(string claimType, string claimValue) : base(typeof(RequiredClaimFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }
}
