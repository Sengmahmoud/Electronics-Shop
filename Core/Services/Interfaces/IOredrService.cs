using Core.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IOredrService
    {
        void BuyProduct(OrderInputDto orderInput);
        void BuyForCustomer(OrderInputDto orderInput);
        IPagedList<OrderDto> GetOrders(int page, int pageSize);
    }
}
