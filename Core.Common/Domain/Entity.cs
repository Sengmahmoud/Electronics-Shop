using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Domain
{
    public class Entity<TKey>:IAuditedEntity
    {
        public TKey Id { get; set; }
        public DateTime? CreationTime { get ; set  ; }

        public Guid? CreatorId { get ; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? LastModifierId { get; set; }
    }
}
