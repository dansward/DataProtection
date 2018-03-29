using Microsoft.AspNetCore.DataProtection.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Microsoft.AspNetCore.DataProtection.EntityFrameworkCore
{
    /// <summary>
    /// An <see cref="IXmlRepository"/> backed by an Entity Framework datastore.
    /// </summary>
    public class EntityFrameworkCoreXmlRepository : IXmlRepository
    {
        private readonly Func<KeyStore> _databaseFactory;

        /// <summary>
        /// Creates a new instance of the <see cref="EntityFrameworkCoreXmlRepository"/>.
        /// </summary>
        /// <param name="databaseFactory">The delegate used to create <see cref="KeyStore"/> instances.</param>
        public EntityFrameworkCoreXmlRepository(Func<KeyStore> databaseFactory)
            => _databaseFactory = databaseFactory ?? throw new ArgumentNullException(nameof(databaseFactory));

        /// <inheritdoc />
        public IReadOnlyCollection<XElement> GetAllElements()
        {
            var context = _databaseFactory() ?? throw new InvalidOperationException("The KeyStore database factory method returned null.");
            return context.Keys.Select(key => XElement.Parse(key.Xml)).ToList().AsReadOnly();
        }

        /// <inheritdoc />
        public void StoreElement(XElement element, string friendlyName)
        {
            var context = _databaseFactory() ?? throw new InvalidOperationException("The KeyStore database factory method returned null.");
            var newKey = new Key() { FriendlyName = friendlyName, Xml = element.ToString(SaveOptions.DisableFormatting) };
            var oldKey = context.Keys.Find(friendlyName);
            if (oldKey != null)
            {
                context.Entry(oldKey).CurrentValues.SetValues(newKey);
            }
            else
            {
                context.Add(newKey);
            }
            context.SaveChanges();
        }
    }
}