using Microsoft.AspNetCore.Identity;

namespace Core.Domain
{
    public class UserClaim: IdentityUserClaim<Guid>
    {
        public User User { get; set; }
    }
}
