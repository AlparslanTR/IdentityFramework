using Microsoft.AspNetCore.Authorization;

namespace IdentityFrameworkWepApp.Requirements
{
    public class ExchangeExpireRequirement:IAuthorizationRequirement
    {
    }

    public class ExchangeExpirationRequirementHandler : AuthorizationHandler<ExchangeExpireRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExchangeExpireRequirement requirement)
        {
            var hasExChangeClaim = context.User.HasClaim(x => x.Type == "ExchangeExpireDate");
            if (hasExChangeClaim==false)
            {
               context.Fail();
                return Task.CompletedTask;
            }
            var ExchangeExpireDate = context.User.FindFirst("ExchangeExpireDate");
            if (DateTime.Now>Convert.ToDateTime(ExchangeExpireDate.Value))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
