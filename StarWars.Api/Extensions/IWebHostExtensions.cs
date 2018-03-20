using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Hosting
{
    public static class IWebHostExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(
            this IWebHost webhost,
            Action<TContext> seed) where TContext : DbContext
        {
            using (var scope = webhost.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<TContext>();
                context.Database.Migrate();
                seed(context);
                context.SaveChanges();
            }

            return webhost;
        }
    }
}
