using Ardalis.GuardClauses;
using Core.Common.Exceptions;
using Core.CustomGuards;

namespace Core.Services.Implementation;

public class SecurityService:ISecurityService
{
    
    private readonly IApplicationDbContext _context;
    private readonly IMapper Mapper;
    private readonly UserManager<User> _usermanager;
    private readonly RoleManager<Role> _roleManager;


    public SecurityService(IApplicationDbContext context, IMapper mapper, UserManager<User> usermanager, RoleManager<Role> roleManager)
        {
        _context = context;
      
              Mapper = mapper;
              _usermanager = usermanager;
              _roleManager = roleManager;
    }

    public async Task AddRole(RoleInputDto roleInput)
    {
        if (roleInput is null)
            throw new ArgumentNullException(nameof(roleInput));
        if (_context.Roles.Any(r => r.Name == roleInput.Name))
            throw new BusinessValidationException("DuplicatedRoleName");
        var roles = _context.Roles.ToList();
        Guard.Against.Null(roleInput, nameof(roleInput));
        Guard.Against.DublicateRole(roles, roleInput.Name);

        if (roleInput.RoleClaims != null && roleInput.RoleClaims.Any())
        {
            roleInput.RoleClaims = roleInput.RoleClaims.Where(c => c != null).ToList();
        }

        var entity = Mapper.Map<Role>(roleInput);

        var result = await _roleManager.CreateAsync(entity);
        if (!result.Succeeded)
            throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
        
    }
    public async Task AddRoleOnly(RoleInputWithoutClaimsDto roleInput)
    {
      

        var roles = _context.Roles.ToList();
        Guard.Against.Null(roleInput, nameof(roleInput));
        Guard.Against.DublicateRole(roles, roleInput.Name);
         var entity = Mapper.Map<Role>(roleInput);
        var result = await _roleManager.CreateAsync(entity);
        if (!result.Succeeded)
            throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));

    }
    public async Task AddUser(UserInputDto user)
    {
        Guard.Against.Null(user, nameof(user));

         var entity = Mapper.Map<User>(user);
       
        entity.UserName = user.Email;
        var result = await _usermanager.CreateAsync(entity, user.Password);

        if (!result.Succeeded)
            throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
    }

    public async Task EditRole(RoleInputDto roleInput)
    {
        Guard.Against.Null(roleInput, nameof(roleInput));
        var entity = await _roleManager.FindByIdAsync(roleInput.Id.ToString());
        Guard.Against.EntityNotFound(roleInput.Id.ToString(), entity,nameof(entity));
     
        entity.Name = roleInput.Name;
        entity.NormalizedName = roleInput.Name.ToUpper();
      
        var result = await _roleManager.UpdateAsync(entity);
        if (!result.Succeeded)
            throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
          await _roleManager.AddPerminssionToRole(entity,roleInput);

     }

    public async Task EditUser(UserInputDto userInput)
    {        
        Guard.Against.Null(userInput, nameof(userInput));
        var entity = await _usermanager.FindByIdAsync(userInput.Id.ToString());

        Guard.Against.EntityNotFound(userInput.Id.ToString(), entity, nameof(entity));

        entity.Name = userInput.Name;
        entity.UserName = userInput.Email;
        entity.Mobile = userInput.Mobile;
        entity.Active = userInput.Active;
        entity.Email = userInput.Email;
        entity.NormalizedUserName = userInput.Email.ToUpper();
        var result =await  _usermanager.UpdateAsync(entity);
    
        if (!result.Succeeded)
            throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));

        var userRoles = await _usermanager.GetRolesAsync(entity);
        foreach (var role in userRoles)
        {
            var removeResult = await _usermanager.RemoveFromRoleAsync(entity, role);
            if (!removeResult.Succeeded)
                throw new Exception(string.Join(",",
                    removeResult.Errors.Select(e => e.Description)));
        }

        foreach (var role in userInput.UserRoles)
        {
            var roleEntity = _context.Roles.Find(role.RoleId);
            var addResult = await _usermanager.AddToRoleAsync(entity, roleEntity.Name);
            if (!addResult.Succeeded)
                throw new Exception(string.Join(",",
                    addResult.Errors.Select(e => e.Description)));
        }

    }

    public async Task<RoleInputDto> GetRole(Guid id)
    {
        var role = await _context.Roles.Include(r => r.RoleClaims).FirstOrDefaultAsync(r => r.Id == id);
        Guard.Against.EntityNotFound(id.ToString(), role, nameof(role));
        var entity = Mapper.Map<RoleInputDto>(role);
        return entity;
    }

    public IPagedList<RoleDto> GetRoles(int page, int pageSize)
    {
        var data =  _context.Roles.Include(r=>r.RoleClaims).ToPagedList(page, pageSize);
        return Mapper.Map<IPagedList<Role>, PagedList<RoleDto>>(data);
    }

    public async Task<UserInputDto> GetUser(Guid id)
    {
        var user = await _context.Users.Include(u=>u.UserRoles).SingleOrDefaultAsync(u=>u.Id== id);
       Guard.Against.EntityNotFound(id.ToString(),user, nameof(user));
        var entity = Mapper.Map<UserInputDto>(user);
        return entity;

    }

    public IPagedList<UserDto> GetUsers(int page, int pageSize)
    {
        var data = _context.Users.ToPagedList(page, pageSize);
        return Mapper.Map<IPagedList<User>, PagedList<UserDto>>(data);
    }

    public async Task RemoveUser(Guid id)
    {
        var user =await _usermanager.FindByIdAsync(id.ToString());
        Guard.Against.EntityNotFound(id.ToString(), user, nameof(user));

        var result = await _usermanager.DeleteAsync(user);
        if (!result.Succeeded)
            throw new Exception(string.Join(",",
                    result.Errors.Select(e => e.Description)));

    }
    public async Task RemoveRole(Guid id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        Guard.Against.EntityNotFound(id.ToString(), role, nameof(role));

        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
            throw new Exception(string.Join(",",
                    result.Errors.Select(e => e.Description)));

    }

}
