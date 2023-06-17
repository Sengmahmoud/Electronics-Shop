using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Dtos
{
    public record DtoBase<T>
    {
        public T Id { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? CreationDate { get; set; }
        public Guid? CreatorId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
    }
}
