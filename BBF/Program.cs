using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BBF;
public class Program
{
    public static void Main(string[] args)
    {
        WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
        })
        .UseStartup<Startup>()
        .Build().Run();
    }
}