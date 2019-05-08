﻿using Serilog;
using System.Threading.Tasks;

namespace ConsoleResourceOwnerFlowRefreshToken
{
    public class Program
    {
        
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        static async Task MainAsync()
        {
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Verbose()
              .Enrich.WithProperty("App", "ConsoleResourceOwnerFlowRefreshToken")
              .Enrich.FromLogContext()
              .WriteTo.Seq("http://localhost:5341")
              .WriteTo.ColoredConsole()
              .WriteTo.RollingFile("../../Logs/ConsoleResourceOwnerFlowRefreshToken")
              .CreateLogger();

            var response = IdentityServer4Client.LoginAsync("damienbod", "damienbod").Result;

            Log.Logger.Information("GOT TOKENS FROM IDENTITYSERVER4: {AccessToken}", response.AccessToken);

            await ResourceDataClient.GetDataAndDisplayInConsoleAsync(response.AccessToken, "https://localhost:44365/api/DataEventRecords");
            await ResourceDataClient.GetDataAndDisplayInConsoleAsync(response.AccessToken, "https://localhost:44376/api/DataEventRecords");

            Log.Logger.Information("GOT DATA FROM THE RESOURCE SERVER");

            // Run an loop which gets refreshes the token every 3000 milliseconds
            await IdentityServer4Client.RunRefreshAsync(response, 3000);
        }
    }
}