using FindMyPG.Core.Entities;
using FindMyPG.Core.Entities.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FindMyPG.Data
{
    public class PGDBContext : IdentityDbContext<User, Role, long>, IDbContext
    {
        public PGDBContext(DbContextOptions<PGDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var Configurations = Assembly.GetExecutingAssembly().GetTypes()
                   .Where(t => t.GetInterfaces().Any(x => x.IsGenericType && 
                    x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var configuration in Configurations)
            {
                dynamic instance = Activator.CreateInstance(configuration);
                builder.ApplyConfiguration(instance);
            }

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }

        public new DbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }
    }
}
