using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Varus.Stopwatch.Web.Infrastructure;

namespace Varus.Stopwatch.Web.Infrastructure
{
    public class APIKeyAuthenticationOptions : AuthenticationOptions
    {
        public APIKeyAuthenticationOptions(Func<string, Task<IEnumerable<Claim>>> authenticateAsync)
            : base("API-Key")
        {
            AuthenticateAsync = authenticateAsync;
        }

        public Func<string, Task<IEnumerable<Claim>>> AuthenticateAsync { get; }
    }

    public class APIKeyAuthenticationMiddleware : AuthenticationMiddleware<APIKeyAuthenticationOptions>
    {
        public APIKeyAuthenticationMiddleware(OwinMiddleware next, APIKeyAuthenticationOptions options)
            : base(next, options)
        { }

        protected override AuthenticationHandler<APIKeyAuthenticationOptions> CreateHandler()
            => new APIKeyAuthenticationHandler();
    }

    public class APIKeyAuthenticationHandler : AuthenticationHandler<APIKeyAuthenticationOptions>
    {
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            const string authValuePrefix = "API-Key ";

            string authValue = Request.Headers.Get("Authorization");

            if (authValue == null || !authValue.StartsWith(authValuePrefix))
                return null;

            string key = authValue.Substring(authValuePrefix.Length).Trim();

            IEnumerable<Claim> claims = await Options.AuthenticateAsync(key);

            // The request is authenticated if claims and therefore ticket is not null.
            AuthenticationTicket ticket = null;
            if (claims != null)
            {
                ticket = new AuthenticationTicket(
                    new ClaimsIdentity(claims, Options.AuthenticationType),
                    new AuthenticationProperties());
            }
            return ticket;
        }
    }
}

namespace Owin
{
    public static class APIKeyAuthenticationExtensions
    {
        public static void UseAPIKeyAuthentication(this IAppBuilder appBuilder,
            Func<string, Task<IEnumerable<Claim>>> authenticateAsync)
        {
            appBuilder.Use<APIKeyAuthenticationMiddleware>(new APIKeyAuthenticationOptions(authenticateAsync));
        }
    }
}