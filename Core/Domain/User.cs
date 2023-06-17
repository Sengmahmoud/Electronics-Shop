using Core.Common.Domain;
using Microsoft.AspNetCore.Identity;


namespace Core.Domain
{
    public class User:IdentityUser<Guid>,IAuditedEntity
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Mobile { get; set; }
        public virtual ICollection<UserClaim>? Claims { get; set; }
        public virtual ICollection<UserLogin>? Logins { get; set; }
        public virtual ICollection<UserToken>? Tokens { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
        public DateTime? CreationDate { get; set ; }

        public DateTime? CreationTime { get; set; }

        public Guid? CreatorId { get ; set; }
        public DateTime? LastModificationTime { get; set ; }
        public Guid? LastModifierId { get; set ; }
        public User()
        {
            CreationDate = DateTime.Now;
        }
      
    }
}
