namespace Core.Dtos.Roles
{
    public class RoleClaimDto
    {
        public int Id { get; set; }
        public string ClaimType { get;init; }
        public string ClaimValue { get; set; }
        public RoleClaimDto()
        {
            ClaimType = ClaimValue;
        }
    }
}