namespace Core.Dtos.Roles
{
    public record RoleDto:DtoBase<Guid>
    {
      
        public string Name { get; set; }
        public  ICollection<RoleClaimDto> RoleClaims { get; set; }
      
    }
}
