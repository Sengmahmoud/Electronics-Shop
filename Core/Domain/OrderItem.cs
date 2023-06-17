using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class OrderItem:Entity<Guid>
    {
        public Guid OrderId { get; set; }
         public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Qantity { get; set; }
    }
}
