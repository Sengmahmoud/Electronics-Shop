using Core.Dtos.Roles;

namespace Core.Dtos
{
    public class UserRoleDto
    {
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}