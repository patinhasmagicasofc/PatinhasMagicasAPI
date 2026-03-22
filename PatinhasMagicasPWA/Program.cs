using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PatinhasMagicasPWA;
using PatinhasMagicasPWA.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5260/")
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UsuarioService>();

await builder.Build().RunAsync();
