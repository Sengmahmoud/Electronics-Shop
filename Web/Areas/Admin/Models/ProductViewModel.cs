using Core.Dtos.Prodoct;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Models
{
    public class ProductViewModel:ViewModelBase
    {
        #region Search
        public int? CategoryId { get; set; }
        #endregion
        public IPagedList<ProductDto>Products { get; set; }
        public ProductInputDto Product { get; set; }
        public IReadOnlyList<SelectListItem> CategoriesList { get; set; }
    }
}
