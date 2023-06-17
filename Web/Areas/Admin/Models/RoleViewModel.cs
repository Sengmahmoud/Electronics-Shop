using Core.Dtos.Roles;
using X.PagedList;

namespace Web.Areas.Admin.Models
{
    public class RoleViewModel:ViewModelBase
    {
        public IPagedList<RoleDto>? Roles { get; set; }
        public RoleInputDto? RoleInput { get; set; }
    }
}
