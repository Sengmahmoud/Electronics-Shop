namespace Core.Domain
{
    public class Category : Entity<int>
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
