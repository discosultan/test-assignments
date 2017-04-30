using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Varus.Stopwatch.Web.Infrastructure.SignalR
{
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