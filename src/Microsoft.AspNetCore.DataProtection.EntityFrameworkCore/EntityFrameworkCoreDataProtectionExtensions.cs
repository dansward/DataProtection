using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.DataProtection.EntityFrameworkCore
{
    /// <summary>
    /// Extension method class for configuring instances of <see cref="EntityFrameworkCoreXmlRepository{TContext}"/>
    /// </summary>
    public static class EntityFrameworkCoreDataProtectionExtensions
    {
        /// <summary>
        /// Configures the data protection system to persist keys to an EntityFrameworkCore datastore
        /// </summary>
        /// <typeparam name="TContext">The EntityFrameworkCore <see cref="DbContext"/> type used to access instances of <see cref="DataProtectionKey"/></typeparam>
        /// <param name="builder">The <see cref="IDataProtectionBuilder"/> instance to modify</param>
        /// <returns>The value <paramref name="builder"/>.</returns>
        public static IDataProtectionBuilder PersistKeysToDbContext<TContext>(this IDataProtectionBuilder builder)
            where TContext : DbContext, IDataProtectionKeyContext
        {
            builder.Services.Configure<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new EntityFrameworkCoreXmlRepository<TContext>(builder.Services.BuildServiceProvider().GetRequiredService<TContext>());
            });
            return builder;
        }
    }
}
