using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Varus.Stopwatch.Web.Infrastructure;

namespace Varus.Stopwatch.Web.Infrastructure
{
    // Basic authentication implementations is based on:
    // https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/basic-authentication
    // https://github.com/IdentityModel/IdentityModel.Owin.BasicAuthentication

    public class BasicAuthenticationOptions : AuthenticationOptions
    {
        public BasicAuthenticationOptions(string realm, Func<string, string, Task<IEnumerable<Claim>>> authenticateAsync)
            : base("Basic")
        {
            Realm = realm;
            AuthenticateAsync = authenticateAsync;
        }

        public string Realm { get; }
        public Func<string, string, Task<IEnumerable<Claim>>> AuthenticateAsync { get; }
    }

    public class BasicAuthenticationMiddleware : AuthenticationMiddleware<BasicAuthenticationOptions>
    {
        public BasicAuthenticationMiddleware(OwinMiddleware next, BasicAuthenticationOptions options)
            : base(next, options)
        { }

        protected override AuthenticationHandler<BasicAuthenticationOptions> CreateHandler()
            => new BasicAuthenticationHandler();
    }

    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            const string authValuePrefix = "Basic ";

            string authValue = Request.Headers.Get("Authorization");

            if (authValue == null || !authValue.StartsWith(authValuePrefix))
                return null;

            // Token is base 64 encoded in the format <username>:<password>.
            string token = authValue.Substring(authValuePrefix.Length).Trim();
            string[] usernameAndPassword = Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(':');

            IEnumerable<Claim> claims = await Options.AuthenticateAsync(usernameAndPassword[0], usernameAndPassword[1]);

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

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                string challenge = $"{Options.AuthenticationType} realm=\"{Options.Realm}\"";
                Response.Headers.Append("WWW-Authenticate", challenge);
            }
            return Task.FromResult<object>(null);
        }
    }
}

namespace Owin
{
    public static class BasicAuthenticationExtensions
    {
        public static void UseBasicAuthentication(this IAppBuilder appBuilder,
            string realm, Func<string, string, Task<IEnumerable<Claim>>> authenticateAsync)
        {
            appBuilder.Use<BasicAuthenticationMiddleware>(new BasicAuthenticationOptions(realm, authenticateAsync));
        }
    }
}