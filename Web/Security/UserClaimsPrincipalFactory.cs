using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Web.Security
{
    public class UserClaimsPrincipalFactory:UserClaimsPrincipalFactory<User,Role>
    {
        public UserClaimsPrincipalFactory
            (UserManager<User> userManager,RoleManager<Role> roleManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Id", user.Id.ToString()));
            identity.AddClaim(new Claim("Name", user.Name ?? ""));
            return identity;
          
        }
    }
}
