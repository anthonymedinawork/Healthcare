using HealthcareApp.Contracts.Exceptions;
using System.Security.Claims;

namespace HealthcareApp.Contracts.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetRequesterId(this IHttpContextAccessor http)
        {

            var result = http.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString();

            if (result is null)
            {
                throw new IncorrectAccountIdException(nameof(result));
            }

            return result;
        }
    }
}
