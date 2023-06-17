using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Core.Persistence
{
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<RoleClaim> RoleClaims { get; set; }
        DbSet<UserClaim> UserClaims { get; set; }
        DbSet<UserLogin> UserLogins { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<UserToken> UserTokens { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DatabaseFacade Database { get; }
        DbSet<T> Set<T>()
           where T : class;

    }
}
