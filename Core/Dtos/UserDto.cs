
namespace Core.Dtos
{
    public record UserDto : DtoBase<Guid>
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Mobile { get; set; }
       public string Email { get; set; }

    }
}
