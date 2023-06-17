using Core.Dtos.Order;

namespace Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(Entity<Guid>), typeof(DtoBase<Guid>))
              .ReverseMap();
            CreateMap(typeof(PagedList<Entity<Guid>>), typeof(PagedList<DtoBase<Guid>>)).ReverseMap();

            CreateMap(typeof(User), typeof(UserInputDto)).ReverseMap();
            CreateMap(typeof(Role), typeof(RoleInputDto)).ReverseMap();
            CreateMap(typeof(Role), typeof(RoleInputWithoutClaimsDto)).ReverseMap();
            CreateMap(typeof(RoleClaim), typeof(RoleClaimDto)).ReverseMap();
            CreateMap(typeof(Role), typeof(RoleDto)).ReverseMap();
            CreateMap(typeof(User), typeof(UserDto)).ReverseMap();
            CreateMap(typeof(UserRole), typeof(UserRoleDto)).ReverseMap();

            CreateMap(typeof(Product), typeof(ProductDto)).ReverseMap();
            CreateMap(typeof(Product), typeof(ProductInputDto)).ReverseMap();

            CreateMap(typeof(Category), typeof(CategoryDto)).ReverseMap();
            CreateMap(typeof(Category), typeof(CategoryInputDto)).ReverseMap();
            CreateMap(typeof(Order), typeof(OrderInputDto)).ReverseMap();
            CreateMap(typeof(OrderItem), typeof(ItemInputDto)).ReverseMap();
            //pageLists
            CreateMap<PagedList<User>, PagedList<UserDto>>()
                .ConvertUsing<PagedListGenericConverter<User, UserDto>>();
            CreateMap<PagedList<Role>, PagedList<RoleDto>>()
                .ConvertUsing<PagedListGenericConverter<Role, RoleDto>>();

            CreateMap<PagedList<Product>, PagedList<ProductDto>>()
             .ConvertUsing<PagedListGenericConverter<Product, ProductDto>>();

        }
    }
}
