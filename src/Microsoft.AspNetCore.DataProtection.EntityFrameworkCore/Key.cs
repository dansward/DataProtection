namespace Microsoft.AspNetCore.DataProtection.EntityFrameworkCore
{
    /// <summary>
    /// Code first model used by <see cref="EntityFrameworkCoreXmlRepository"/>.
    /// </summary>
    public class Key
    {
        /// <summary>
        /// The friendly name of the <see cref="Key"/>.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// The XML representation of the <see cref="Key"/>.
        /// </summary>
        public string Xml { get; set; }
    }
}
