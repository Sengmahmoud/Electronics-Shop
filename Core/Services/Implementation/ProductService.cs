
namespace Core.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddProduct(ProductInputDto product)
        {
            Guard.Against.Null(product, nameof(product));
            var entity = _mapper.Map<Product>(product);
            entity.FinalPrice = CalculateFinalPrice(product);
            _context.Products.Add(entity);
            _context.SaveChanges();

        }

        public void Delete(Guid id)
        {
            Guard.Against.Null(id, nameof(id));
            var product = _context.Products.Find(id);
            Guard.Against.Null(product, nameof(product));
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public ProductInputDto GetProduct(Guid id)
        {
            Guard.Against.Null(id, nameof(id));
            var product = _context.Products.AsNoTracking().Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            Guard.Against.Null(product, nameof(product));
            return _mapper.Map<ProductInputDto>(product);
        }

        public IPagedList<ProductDto> GetProducts(int? categoryId, int page, int pageSize)
        {
            var data = _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(p=>!categoryId.HasValue || categoryId.Value==p.CategoryId)
                .ToPagedList(page, pageSize);
            return _mapper.Map<IPagedList<Product>, PagedList<ProductDto>>(data);
        }

        public void UpdateProduct(ProductInputDto product)
        {

            Guard.Against.Null(product, nameof(product));
            var entity = _context.Products.Find(product.Id);
            _mapper.Map(product, entity);
            entity.FinalPrice = CalculateFinalPrice(product);
            _context.SaveChanges();
        }
        private decimal CalculateFinalPrice(ProductInputDto product) => (product.Price - (product.Price * (product.Discount / 100)));
    }
}
