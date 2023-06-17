namespace Core.Domain
{
    public class Product : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string ImagePath { get; set; }

        public decimal FinalPrice { get;  set; }
      
    }
}
