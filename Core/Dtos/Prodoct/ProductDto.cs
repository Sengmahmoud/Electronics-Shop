namespace Core.Dtos.Prodoct
{
    public record ProductDto : DtoBase<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }

        public decimal FinalPrice { get; private set; }

    }
}
