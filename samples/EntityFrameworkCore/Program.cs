using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace EntityFrameworkCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configure
            using (var services = new ServiceCollection()
                .AddLogging(o => o.AddConsole().SetMinimumLevel(LogLevel.Debug))
                .AddDataProtection()
                .PersistKeysToEntityFrameworkCore(() => BuildKeyStore("DataProtection-Keys"))
                .Services
                .BuildServiceProvider())
            {
                // Run a sample payload
                var protector = services.GetDataProtector("sample-purpose");
                var protectedData = protector.Protect("Hello world!");
                Console.WriteLine(protectedData);
            }
        }

        private static DbContextOptions<KeyStore> BuildDbContextOptions(string databaseName)
            => new DbContextOptionsBuilder<KeyStore>().UseInMemoryDatabase(databaseName: databaseName).Options;

        private static KeyStore BuildKeyStore(string databaseName)
            => new KeyStore(BuildDbContextOptions(databaseName));
    }
}
