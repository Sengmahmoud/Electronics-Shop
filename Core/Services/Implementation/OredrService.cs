using Core.Dtos.Order;
using Microsoft.AspNetCore.Http;

namespace Core.Services.Implementation
{
    public class OredrService : IOredrService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public OredrService(IApplicationDbContext context, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public void BuyForCustomer(OrderInputDto orderInput)
        {
            Guard.Against.Null(orderInput, nameof(orderInput));
            var entity = _mapper.Map<Order>(orderInput);

            var id = _httpContextAccessor.HttpContext?.User?.FindFirst("Id");
            if (id != null)
            {
              
                var user = _userManager.FindByIdAsync(id.Value.ToString());
                entity.Customer = new Customer()
                {
                    Name = user.Result.Name,
                    Email = user.Result.Email,
                    Mobile = user.Result.Mobile
                };
            }
            entity.Items = new List<OrderItem>()
            {
                new OrderItem
                {
                    ProductId=orderInput.ProductId,
                    Qantity=orderInput.Qantity
                }
            };
            var product = _context.Products.Find(orderInput.ProductId);

            entity.Total = (orderInput.Qantity >= 2 ? product.FinalPrice * orderInput.Qantity : product.Price * orderInput.Qantity);
            _context.Orders.Add(entity);
            _context.SaveChanges();
        }

        public void BuyProduct(OrderInputDto orderInput)
        {
            Guard.Against.Null(orderInput, nameof(orderInput));
            var entity = _mapper.Map<Order>(orderInput);
            entity.Customer = new Customer()
            {
                Name = orderInput.CusomerName,
                Email = orderInput.CusomerEmail,
                Mobile = orderInput.CusomerMobile
            };
            entity.Items = new List<OrderItem>()
            {
                new OrderItem
                {
                    ProductId=orderInput.ProductId,
                    Qantity=orderInput.Qantity
                }
            };
            var product = _context.Products.Find(orderInput.ProductId);

            entity.Total = (orderInput.Qantity >= 2 ? product.FinalPrice * orderInput.Qantity : product.Price * orderInput.Qantity);
            _context.Orders.Add(entity);
            _context.SaveChanges();
        }

        public IPagedList<OrderDto> GetOrders(int page, int pageSize)
        {
            var data = _context.Orders.Include(o => o.Items).ThenInclude(i => i.Product).Include(o => o.Customer)
                .Select(o => new OrderDto
                {
                    CustomerName = o.Customer.Name,
                    CustomerEmail = o.Customer.Email,
                    CustomerMobile = o.Customer.Mobile,
                    ProductName = o.Items.FirstOrDefault().Product.Name,
                    Quantity = o.Items.FirstOrDefault().Qantity,
                    Total=o.Total
                }).ToPagedList(page, pageSize);
            return data;
        }
    }
}
