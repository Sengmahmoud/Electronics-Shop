

namespace Web.Areas.Admin.Models
{
    public class UserViewModel:ViewModelBase
    {
        public UserInputDto? User { get; set; }
        public IPagedList<UserDto>? Users { get; set; }
        public IEnumerable<RoleDto>? Roles{ get; set; }
    }
}
