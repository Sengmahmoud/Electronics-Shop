

using Core.Dtos.Roles;

namespace Core
{
    public static class Seed
    {
        public static List<Role> SetData()
        {
            var roles = new List<Role>
                {
                    new Role
                    {
                       Id=Guid.NewGuid(),
                       Name="Test"
                    },
                    new Role
                    {
                       Id=Guid.NewGuid(),
                       Name="Test2"
                    }
                };
            return roles;
        }
        public static async Task AddPerminssionToRole(this RoleManager<Role> roleManager, Role role,RoleInputDto roleInput)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            //foreach (var type in typeof(Permissions).GetNestedTypes())
            //{

            //    foreach (var field in type.GetFields(BindingFlags.Static | BindingFlags.Public).Where(y => y.IsLiteral))
            //    {
            //        if (!allClaims.Any(r => r.Value == field.GetValue(null).ToString()))
            //        {
            //            var result = await roleManager.AddClaimAsync(role, new Claim(field.GetValue(null).ToString(), field.GetValue(null).ToString()));
            //            if (!result.Succeeded)
            //                throw new Exception(string.Join(",",
            //                    result.Errors.Select(e => e.Description)));
            //        }
            //    }
            //}
            foreach (var item in allClaims)
            {
                if (!roleInput.RoleClaims.Where(c=>c !=null).Any(c=>c.ClaimValue==item.Value))
                {
                    var removeResult = await roleManager.RemoveClaimAsync(role, item);
                    if (!removeResult.Succeeded)
                        throw new Exception(string.Join(",",
                            removeResult.Errors.Select(e => e.Description)));

                }
            }

            foreach (var claim in roleInput.RoleClaims.Where(c => c != null))
            {
                if (!allClaims.Any(c => c.Value == claim.ClaimValue))
                {
                    var addResult = await roleManager.AddClaimAsync(role, new Claim(claim.ClaimValue, claim.ClaimValue));
                    if (!addResult.Succeeded)
                        throw new Exception(string.Join(",",
                            addResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
