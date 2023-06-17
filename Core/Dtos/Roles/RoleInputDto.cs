
using Core.Common.Dtos;

namespace Core.Dtos.Roles
{
    public record RoleInputDto:DtoBase<Guid>
    {

        public string Name { get; set; }
        public List<RoleClaimDto>  RoleClaims{ get; set; }
        public RoleInputDto()
        {
            RoleClaims = new List<RoleClaimDto>();
        }
    }
}
