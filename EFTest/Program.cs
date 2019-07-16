using System;
using System.Linq;
using EFTest.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EFTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MyDbContext>();
                    var query = context.PersonName
                        .Include(q => q.PersonConns)
                        .ThenInclude(q => q.PersonSurname);
                    // Why is there ORDER BY in query?
                    var data = query.ToArray();

                    query = context.PersonName
                        .OrderBy(q => q.Name)
                        .OrderBy(q => q.City)
                        .Include(q => q.PersonConns)
                        .ThenInclude(q => q.PersonSurname);

                    var sortedData = query.ToArray();
                    // There is ORDER BY id in the middle
                    // result (wrong '2  London b' and '3  London a' should be switched):
                    // id city   name
                    // 2  London b
                    // 3  London a
                    // 1  Prague c


                    query = context.PersonName
                        .OrderBy(q => q.City)
                        .ThenBy(q => q.Name)
                        .Include(q => q.PersonConns)
                        .ThenInclude(q => q.PersonSurname);

                    var sortedThenByData = query.ToArray();
                    // There is still ORDER BY id but at the end so it doesn't influence the result
                    // result (Ok):
                    // id city   name
                    // 3  London a
                    // 2  London b
                    // 1  Prague c
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
