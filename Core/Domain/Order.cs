namespace Core.Domain
{
    public class Order : Entity<Guid>
    {
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }

    }
}
