using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
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
                .AddDbContext<DataProtectionKeyContext>()
                .AddDataProtection()
                .PersistKeysToDbContext<DataProtectionKeyContext>()
                .Services
                .BuildServiceProvider())
            {
                // Run a sample payload
                var protector = services.GetDataProtector("sample-purpose");
                var protectedData = protector.Protect("Hello world!");
                Console.WriteLine(protectedData);
            }
        }
    }
}
