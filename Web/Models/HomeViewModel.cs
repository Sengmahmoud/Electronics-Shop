using Core.Dtos.Order;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Models
{
    public class HomeViewModel:ViewModelBase
    {
        public int? CategoryId { get; set; }
        public IPagedList<ProductDto> Products { get; set; }
        public OrderInputDto OrderInput  { get; set; }
        public IReadOnlyList<SelectListItem> CategoriesList { get; set; }



    }
}
