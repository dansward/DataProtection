using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.DataProtection
{
    /// <summary>
    /// Contains EntityFrameworkCore-specific extension methods for modifying a <see cref="IDataProtectionBuilder"/>.
    /// </summary>
    public static class EntityFrameworkCoreDataProtectionBuilderExtensions
    {
        /// <summary>
        /// Configures the data protection system to persist keys to the specified Entity Framework <see cref="KeyStore"/>.
        /// </summary>
        /// <param name="builder">The <see cref="IDataProtectionBuilder" /> instance to modify.</param>
        /// <param name="databaseFactory">The delegate used to create <see cref="KeyStore"/> instances.</param>
        /// <returns>A reference to the <see cref="IDataProtectionBuilder" /> instance that was modified.</returns>
        public static IDataProtectionBuilder PersistKeysToEntityFrameworkCore(this IDataProtectionBuilder builder, Func<KeyStore> databaseFactory)
        {
            builder.Services.Configure<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new EntityFrameworkCoreXmlRepository(databaseFactory);
            });
            return builder;
        }
    }
}
