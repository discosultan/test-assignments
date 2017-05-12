using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace Varus.Stopwatch.Web.Infrastructure.WebAPI
{
    // This attribute can be used on Web API controller method parameters to
    // indicate the the parameter should be automaticall extracted from user claims.
    public class FromClaimAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            if (parameter.ParameterType != typeof(string))
                throw new ArgumentException("Only string values can be extracted from claims.");

            return new ClaimAttributeBinding(parameter);
        }
    }

    public class ClaimAttributeBinding : HttpParameterBinding
    {
        public ClaimAttributeBinding(HttpParameterDescriptor descriptor) : base(descriptor)
        { }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            if (principal == null)
                throw new InvalidOperationException($"Principal of type {nameof(ClaimsPrincipal)} not found.");

            // Lowercase parameter name is mapped to claim type by uppercasing first letter.
            string claimType = char.ToUpperInvariant(Descriptor.ParameterName.First()) + Descriptor.ParameterName.Substring(1);

            string value = principal.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;
            if (value == null)
                throw new InvalidOperationException($"Claim of type {claimType} not found.");

            SetValue(actionContext, value);

            return Task.FromResult<object>(null);
        }
    }

}