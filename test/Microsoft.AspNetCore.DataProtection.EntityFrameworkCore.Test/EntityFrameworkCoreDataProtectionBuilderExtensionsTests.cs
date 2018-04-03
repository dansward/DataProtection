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
            var dpBuilder = serviceCollection.AddDataProtection();

            // Act
            dpBuilder.PersistKeysToEntityFrameworkCore(options =>
            {
                options.KeyStoreOptionsBuilderAction = builder =>
                {
                    builder.UseInMemoryDatabase(databaseName: nameof(PersistKeysToEntityFrameworkCore_UsesEntityFrameworkXmlRepository));
                };
            });
            var services = serviceCollection.BuildServiceProvider();

            // Assert
            var keyManagementOptions = services.GetRequiredService<IOptions<KeyManagementOptions>>();
            Assert.IsType<EntityFrameworkCoreXmlRepository>(keyManagementOptions.Value.XmlRepository);
        }
    }
}
