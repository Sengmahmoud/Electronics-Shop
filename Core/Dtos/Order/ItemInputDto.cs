namespace Core.Dtos.Order
{
    public record ItemInputDto : DtoBase<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Qantity { get; set; }
    }
}
