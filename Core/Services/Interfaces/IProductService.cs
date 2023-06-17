namespace Core.Services.Interfaces
{
    public interface IProductService
    {
        IPagedList<ProductDto> GetProducts(int? categoryId, int page, int pageSize);
        void AddProduct(ProductInputDto product);
        void UpdateProduct(ProductInputDto product);
        ProductInputDto GetProduct(Guid id);
        void Delete(Guid id);
    }
}
