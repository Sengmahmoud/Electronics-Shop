using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class UserLogin:IdentityUserLogin<Guid>
    {
        public User User { get; set; }
    }
}
