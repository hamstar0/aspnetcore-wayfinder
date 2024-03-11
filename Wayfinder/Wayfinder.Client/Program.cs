using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Runtime.CompilerServices;
using Wayfinder.Client.Data;


namespace Wayfinder.Client;


public class Program {
    public static async Task Main( string[] args ) {
        var builder = WebAssemblyHostBuilder.CreateDefault( args );

        builder.Services.AddScoped<ClientDataAccess>();
        builder.Services.AddScoped( http => new HttpClient {
            BaseAddress = new Uri( builder.HostEnvironment.BaseAddress )
        } );
        //builder.Services.AddHttpClient<IEmployeeService, EmployeeService>( client =>
        //{
        //    client.BaseAddress = new Uri( builder.HostEnvironment.BaseAddress );
        //} );

        ///builder.RootComponents.Add<HeadOutlet>("head::after");

        await builder.Build().RunAsync();
    }
}
