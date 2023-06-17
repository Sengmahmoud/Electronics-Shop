
using Core.Common.Dtos;

namespace Core.Dtos.Roles
{
    public record RoleInputWithoutClaimsDto : DtoBase<Guid>
    {

        public string Name { get; set; }
      
    }
}
