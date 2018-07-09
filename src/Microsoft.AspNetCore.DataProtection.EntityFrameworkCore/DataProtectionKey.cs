using System.ComponentModel.DataAnnotations;

namespace Microsoft.AspNetCore.DataProtection.EntityFrameworkCore
{
    /// <summary>
    /// Code first model used by <see cref="EntityFrameworkCoreXmlRepository{TContext}"/>.
    /// </summary>
    public class DataProtectionKey
    {
        /// <summary>
        /// The friendly name of the <see cref="DataProtectionKey"/>.
        /// </summary>
        [Key]
        public string FriendlyName { get; set; }

        /// <summary>
        /// The XML representation of the <see cref="DataProtectionKey"/>.
        /// </summary>
        public string Xml { get; set; }
    }
}
