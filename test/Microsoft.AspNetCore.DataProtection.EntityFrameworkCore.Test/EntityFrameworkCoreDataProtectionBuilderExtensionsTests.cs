using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
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
        public void PersistKeysToEntityFrameworkCore_UsesEntityFrameworkCoreXmlRepository()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<DataProtectionKeyContext>()
                .AddDataProtection()
                .PersistKeysToDbContext<DataProtectionKeyContext>();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var keyManagementOptions = serviceProvider.GetRequiredService<IOptions<KeyManagementOptions>>();
            Assert.IsType<EntityFrameworkCoreXmlRepository<DataProtectionKeyContext>>(keyManagementOptions.Value.XmlRepository);
        }
    }
}
