using Ardalis.GuardClauses;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CustomGuards
{
    public static class RoleGuard
    {

      
        public static void DublicateRole(this IGuardClause guardClause,IList<Role> roles,string roleName)
        {
            if (roles.Any(r => r.Name == roleName))
                throw new BusinessValidationException("DuplicatedRoleName");
        }
        public static T EntityNotFound<T>(this IGuardClause guardClause, [NotNull][ValidatedNotNull] string key, [NotNull][ValidatedNotNull] T input, string parameterName)
        {
            guardClause.NullOrEmpty(key, "key");
            if (input == null)
            {
                throw new EntityNotFoundException("Entity Not Found");
            }

            return input;
        }
    }
}
