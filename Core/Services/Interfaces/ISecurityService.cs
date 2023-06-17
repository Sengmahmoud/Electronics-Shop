using Core.Domain;
using Core.Dtos;
using Core.Dtos.Roles;
using X.PagedList;

namespace Core.Services.Interfaces
{
    public interface ISecurityService
    {
         Task AddUser(UserInputDto user);
        Task  RemoveUser(Guid id);
        Task<UserInputDto> GetUser(Guid id);
        public Task EditUser(UserInputDto userInputDto);
        IPagedList<UserDto> GetUsers(int page, int pageSize);
        IPagedList<RoleDto> GetRoles(int page, int pageSize);
        public Task AddRole(RoleInputDto roleInput);
        Task AddRoleOnly(RoleInputWithoutClaimsDto roleInput);
        public Task<RoleInputDto> GetRole(Guid id);
        public  Task EditRole(RoleInputDto roleInput);
        Task RemoveRole(Guid id);
    }
}
