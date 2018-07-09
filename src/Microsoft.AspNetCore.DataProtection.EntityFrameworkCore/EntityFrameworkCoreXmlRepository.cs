using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Microsoft.AspNetCore.DataProtection.EntityFrameworkCore
{
    /// <summary>
    /// An <see cref="IXmlRepository"/> backed by an EntityFrameworkCore datastore.
    /// </summary>
    public class EntityFrameworkCoreXmlRepository<TContext> : IXmlRepository
        where TContext : DbContext, IDataProtectionKeyContext
    {
        private readonly TContext _context;

        /// <summary>
        /// Creates a new instance of the <see cref="EntityFrameworkCoreXmlRepository{TContext}"/>.
        /// </summary>
        /// <param name="context">The context used to store instances of <see cref="DataProtectionKey"/></param>
        public EntityFrameworkCoreXmlRepository(TContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <inheritdoc />
        public IReadOnlyCollection<XElement> GetAllElements()
            => _context.DataProtectionKeys.Select(key => XElement.Parse(key.Xml)).ToList().AsReadOnly();

        /// <inheritdoc />
        public void StoreElement(XElement element, string friendlyName)
        {
            var newKey = new DataProtectionKey()
            {
                FriendlyName = friendlyName,
                Xml = element.ToString(SaveOptions.DisableFormatting)
            };
            var oldKey = _context.DataProtectionKeys.Find(friendlyName);
            if (oldKey == null)
            {
                _context.DataProtectionKeys.Add(newKey);
            }
            _context.SaveChanges();
        }
    }
}
