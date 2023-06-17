using Infrastructure.DataBase.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ioc
{
    public class ApplicationScopedFactory : IDbContextFactory<ApplicationDbContext>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _pooledFactory;
        public ApplicationScopedFactory(IDbContextFactory<ApplicationDbContext> pooledFactory)
        {
            _pooledFactory = pooledFactory;
        }
       
        public ApplicationDbContext CreateDbContext()
        {
            var context = _pooledFactory.CreateDbContext();
            return context;
        }
    }
}
