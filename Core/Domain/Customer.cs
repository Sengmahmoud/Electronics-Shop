namespace Core.Domain
{
    public class Customer : Entity<Guid>
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
