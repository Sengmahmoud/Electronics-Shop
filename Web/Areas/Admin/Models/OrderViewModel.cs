using Core.Dtos.Order;

namespace Web.Areas.Admin.Models
{
    public class OrderViewModel:ViewModelBase
    {
        public IPagedList<OrderDto>Orders { get; set; }
    }
}
