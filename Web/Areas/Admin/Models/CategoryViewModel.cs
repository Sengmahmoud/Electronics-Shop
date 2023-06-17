namespace Web.Areas.Admin.Models
{
    public class CategoryViewModel:ViewModelBase
    {
        public IReadOnlyList<CategoryDto> Categories { get; set; }
        public CategoryInputDto Category { get; set; }
    }
}
