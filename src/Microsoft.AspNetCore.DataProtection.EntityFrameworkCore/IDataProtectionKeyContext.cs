using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.DataProtection.EntityFrameworkCore
{
    /// <summary>
    /// Interface used to store instances of <see cref="DataProtectionKey"/> in a <see cref="DbContext"/>
    /// </summary>
    public interface IDataProtectionKeyContext
    {
        /// <inheritdoc />
        DbSet<DataProtectionKey> DataProtectionKeys { get; }
    }
}
