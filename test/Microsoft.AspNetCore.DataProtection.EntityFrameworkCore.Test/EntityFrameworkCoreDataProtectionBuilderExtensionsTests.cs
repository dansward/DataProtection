using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.Test
{
    public class EntityFrameworkCoreDataProtectionBuilderExtensionsTests
    {
        [Fact]
        public void PersistKeysToEntityFrameworkCore_UsesEntityFrameworkXmlRepository()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var builder = serviceCollection.AddDataProtection();

            // Act
            builder.PersistKeysToEntityFrameworkCore(() => BuildKeyStore(nameof(PersistKeysToEntityFrameworkCore_UsesEntityFrameworkXmlRepository)));
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
