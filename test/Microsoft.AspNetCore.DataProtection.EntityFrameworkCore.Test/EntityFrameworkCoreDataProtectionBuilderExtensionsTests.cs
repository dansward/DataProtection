using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.Test
{
    public class EntityFrameworkCoreDataProtectionBuilderExtensionsTests
    {
        [Fact]
        public void PersistKeysToRedis_UsesRedisXmlRepository()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var builder = serviceCollection.AddDataProtection();

            // Act
            builder.PersistKeysToEntityFrameworkCore(() => BuildKeyStore(nameof(PersistKeysToRedis_UsesRedisXmlRepository)));
            var services = serviceCollection.BuildServiceProvider();

            // Assert
            var options = services.GetRequiredService<IOptions<KeyManagementOptions>>();
            Assert.IsType<EntityFrameworkCoreXmlRepository>(options.Value.XmlRepository);
        }

        private DbContextOptions<KeyStore> BuildDbContextOptions(string databaseName)
            => new DbContextOptionsBuilder<KeyStore>().UseInMemoryDatabase(databaseName: databaseName).Options;

        private KeyStore BuildKeyStore(string databaseName)
            => new KeyStore(BuildDbContextOptions(databaseName));
    }
}
