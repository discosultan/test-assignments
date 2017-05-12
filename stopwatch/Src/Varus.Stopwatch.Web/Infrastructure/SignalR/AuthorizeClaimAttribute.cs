using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Varus.Stopwatch.Web.Infrastructure.SignalR
{
    // A SignalR controller filter requiring at least one of the specified
    // authorization type claims.
    // For example, specifying 'Basic' and 'API-Key' means that the user is
    // authorized for the action if any of those claims is present.
    public class AuthorizeClaimAttribute : AuthorizeAttribute
    {
        private readonly string[] _allowedAuthenticationTypes;

        public AuthorizeClaimAttribute(params string[] allowedAuthenticationTypes)
        {
            _allowedAuthenticationTypes = allowedAuthenticationTypes
                ?? throw new ArgumentNullException(nameof(allowedAuthenticationTypes));
        }

        public override bool AuthorizeHubMethodInvocation(
            IHubIncomingInvokerContext hubIncomingInvokerContext, bool appliesToMethod)
        {
            if (hubIncomingInvokerContext.Hub.Context.User is ClaimsPrincipal user &&
                _allowedAuthenticationTypes.Any(type => user.HasClaim("AuthorizationType", type)))
            {
                return base.AuthorizeHubMethodInvocation(hubIncomingInvokerContext, appliesToMethod);
            }
            return false;
        }
    }
}