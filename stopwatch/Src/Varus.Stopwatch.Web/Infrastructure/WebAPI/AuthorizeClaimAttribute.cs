using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Varus.Stopwatch.Web.Infrastructure.WebAPI
{
    public class AuthorizeClaimAttribute : AuthorizeAttribute
    {
        private readonly string[] _allowedAuthenticationTypes;

        public AuthorizeClaimAttribute(params string[] allowedAuthenticationTypes)
        {
            _allowedAuthenticationTypes = allowedAuthenticationTypes
                ?? throw new ArgumentNullException(nameof(allowedAuthenticationTypes));
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal is ClaimsPrincipal user &&
                _allowedAuthenticationTypes.Any(type => user.HasClaim("AuthorizationType", type)))
            {
                base.OnAuthorization(actionContext);
            }
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}