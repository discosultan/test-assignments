using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Varus.Stopwatch.Web.Infrastructure
{
    public static class Authenticate
    {
        public static Task<IEnumerable<Claim>> Basic(string username, string password)
        {
            // Any user gets always authenticated. Password is not checked.
            return Task.FromResult(new[]
            {
                new Claim("User", username),
                new Claim("AuthorizationType", "Basic")
            }.AsEnumerable());
        }

        public static Task<IEnumerable<Claim>> APIKey(string key)
        {
            // Any API key is valid.
            return Task.FromResult(new[]
            {
                new Claim("AuthorizationType", "API-Key")
            }.AsEnumerable());
        }
    }
}