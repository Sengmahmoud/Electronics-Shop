
namespace Core.Services.Interfaces
{
    public interface ICategoryService
    {
        IReadOnlyList<CategoryDto> GetCategories();
        void AddCategory(CategoryInputDto category);
        void Delete(int id);
        CategoryInputDto GetCategory(int id);
        void UpdateCategory(CategoryInputDto category);
    }
}
