using Ardalis.GuardClauses;

namespace Core.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(IApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddCategory(CategoryInputDto category)
        {
            Guard.Against.Null(category, nameof(category));
            var entity = _mapper.Map<Category>(category);
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Guard.Against.Null(id, nameof(id));
            var category = _context.Categories.Find(id);
            Guard.Against.Null(category, nameof(category));
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public IReadOnlyList<CategoryDto> GetCategories()
        {
            var data = _context.Categories.AsNoTracking().ToList();
            return _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDto>>(data);
        }

        public CategoryInputDto GetCategory(int id)
        {
            Guard.Against.Null(id, nameof(id));
            var category = _context.Categories.Find(id);
            Guard.Against.Null(category, nameof(category));
            return _mapper.Map<CategoryInputDto>(category);
        }

        public void UpdateCategory(CategoryInputDto category)
        {
            Guard.Against.Null(category, nameof(category));
            var entity = _context.Categories.Find(category.Id);
            _mapper.Map(category, entity);
            _context.SaveChanges();
        }
    }
}
