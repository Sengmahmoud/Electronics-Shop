namespace Core.Dtos.Order
{
  public record  OrderDto : DtoBase<Guid>
    {
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
