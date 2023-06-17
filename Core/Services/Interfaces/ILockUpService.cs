using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ILockUpService
    {
        IEnumerable<TDto> Get<TDto , TEntiy ,TId>()
            where TDto : DtoBase<TId>
            where TEntiy : Entity<TId>;
    }
}
