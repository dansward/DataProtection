using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.DataProtection.EntityFrameworkCore
{
    /// <summary>
    /// The DbContext used by <see cref="EntityFrameworkCoreXmlRepository"/>.
    /// </summary>
    public class KeyStore : DbContext
    {
        /// <summary>
        /// Creates a new instance of the <see cref="KeyStore"/>.
        /// </summary>
        /// <param name="options">The instance of <see cref="DbContextOptions{KeyStore}"/> used to configure this instance of the <see cref="KeyStore"/>.</param>
        public KeyStore(DbContextOptions<KeyStore> options) : base(options) { }

        /// <summary>
        /// A collection of <see cref="Key"/> instances.
        /// </summary>
        public DbSet<Key> Keys { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Key>().HasKey(m => m.FriendlyName);
            base.OnModelCreating(modelBuilder);
        }
    }
}
