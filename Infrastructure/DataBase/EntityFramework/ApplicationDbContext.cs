using Core.Common.Domain;
using Core.Domain;
using Core.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Infrastructure.DataBase.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,Guid,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       
        public  DbSet<User> Users { get; set; }
        public  DbSet<Role> Roles { get; set; }
        public  DbSet<RoleClaim> RoleClaims  { get; set; }
        public  DbSet<UserClaim>UserClaims { get; set; }
        public  DbSet<UserLogin> UserLogins { get; set; }
        public  DbSet<UserRole> UserRoles { get; set; }
        public  DbSet<UserToken> UserTokens { get; set; }
         public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get ; set ; }
        public DbSet<Order> Orders { get ; set ; }
        public DbSet<OrderItem> OrderItems { get ; set ; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
  
            foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationTime = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModificationTime = DateTime.Now;
                        Entry(entry.Entity).Property(c => c.CreationTime).IsModified = false;
                        Entry(entry.Entity).Property(c => c.CreatorId).IsModified = false;
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            //AuditEntity();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
          //  AuditEntity();
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
