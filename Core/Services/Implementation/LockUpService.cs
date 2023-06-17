namespace Core.Services.Implementation
{
    public class LockUpService : ILockUpService 
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public LockUpService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<TDto> Get<TDto, TEntiy, TId>()
            where TDto : DtoBase<TId>
            where TEntiy : Entity<TId>
        {
            var data = _context.Set<TEntiy>().ToList();
            return _mapper.Map<IEnumerable<TDto>>(data);
        }
    }
}
