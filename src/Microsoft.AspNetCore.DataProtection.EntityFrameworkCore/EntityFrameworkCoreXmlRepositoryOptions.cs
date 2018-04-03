using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microsoft.AspNetCore.DataProtection
{
    /// <summary>
    /// Options used to configure a <see cref="EntityFrameworkCoreXmlRepository"/>.
    /// </summary>
    public class EntityFrameworkCoreXmlRepositoryOptions
    {
        /// <summary>
        /// The setup action used to configure the <see cref="KeyStore"/>.
        /// </summary>
        public Action<DbContextOptionsBuilder<KeyStore>> KeyStoreOptionsBuilderAction { get; set; }
    }
}