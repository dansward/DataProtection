using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Xml.Linq;
using Xunit;

namespace Microsoft.AspNetCore.DataProtection
{
    public class DataProtectionEntityFrameworkTests
    {
        [Fact]
        public void CreateRepository_ThrowsIf_DatabaseFactoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new EntityFrameworkCoreXmlRepository(null));
        }

        [Fact]
        public void GetAllElements_ThrowsIf_DatabaseFactoryReturnsNull()
        {
            var repository = new EntityFrameworkCoreXmlRepository(DatabaseFactory_ReturnsNull);
            Assert.Throws<InvalidOperationException>(() => repository.GetAllElements());
        }

        [Fact]
        public void StoreElement_ThrowsIf_DatabaseFactoryReturnsNull()
        {
            var repository = new EntityFrameworkCoreXmlRepository(DatabaseFactory_ReturnsNull);
            Assert.Throws<InvalidOperationException>(() => repository.GetAllElements());
        }

        [Fact]
        public void StoreElement_PersistData()
        {
            var element = XElement.Parse("<Element1/>");
            var friendlyName = "Element1";
            var key = new Key() { FriendlyName = friendlyName, Xml = element.ToString() };
            using (var context = BuildKeyStore(nameof(StoreElement_PersistData)))
            {
                var service = new EntityFrameworkCoreXmlRepository(() => context);
                service.StoreElement(element, friendlyName);
            }
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = BuildKeyStore(nameof(StoreElement_PersistData)))
            {
                Assert.Equal(1, context.Keys.Count());
                Assert.Equal(key.FriendlyName, context.Keys.Single()?.FriendlyName);
                Assert.Equal(key.Xml, context.Keys.Single()?.Xml);
            }
        }

        [Fact]
        public void StoreElement_UpdatesExisting()
        {
            var friendlyName = "Element";
            var oldElement = XElement.Parse("<Element/>");
            var newElement = XElement.Parse("<Element>Test</Element>");
            var oldKey = new Key() { FriendlyName = friendlyName, Xml = oldElement.ToString() };
            var newKey = new Key() { FriendlyName = friendlyName, Xml = newElement.ToString() };
            using (var context = BuildKeyStore(nameof(StoreElement_UpdatesExisting)))
            {
                var service = new EntityFrameworkCoreXmlRepository(() => context);
                service.StoreElement(oldElement, friendlyName);
            }
            // Use a separate instance of the context to update data
            using (var context = BuildKeyStore(nameof(StoreElement_UpdatesExisting)))
            {
                var service = new EntityFrameworkCoreXmlRepository(() => context);
                service.StoreElement(newElement, friendlyName);
            }
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = BuildKeyStore(nameof(StoreElement_UpdatesExisting)))
            {
                Assert.Equal(1, context.Keys.Count());
                Assert.Equal(newKey.Xml, context.Keys.Single()?.Xml);
            }
        }

        [Fact]
        public void GetAllElements_ReturnsAllElements()
        {
            var element1 = XElement.Parse("<Element1/>");
            var element2 = XElement.Parse("<Element2/>");
            using (var context = BuildKeyStore(nameof(GetAllElements_ReturnsAllElements)))
            {
                var service = new EntityFrameworkCoreXmlRepository(() => context);
                service.StoreElement(element1, "element1");
                service.StoreElement(element2, "element2");
            }
            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = BuildKeyStore(nameof(GetAllElements_ReturnsAllElements)))
            {
                var service = new EntityFrameworkCoreXmlRepository(() => context);
                var elements = service.GetAllElements();
                Assert.Equal(2, elements.Count);
            }
        }

        private KeyStore DatabaseFactory_ReturnsNull() => null;

        private DbContextOptions<KeyStore> BuildDbContextOptions(string databaseName)
            => new DbContextOptionsBuilder<KeyStore>().UseInMemoryDatabase(databaseName: databaseName).Options;

        private KeyStore BuildKeyStore(string databaseName)
            => new KeyStore(BuildDbContextOptions(databaseName));
    }
}
