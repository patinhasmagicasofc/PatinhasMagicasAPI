using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PatinhasMagicasPWA;
using PatinhasMagicasPWA.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<TokenStorageService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthTokenHandler>();

builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthTokenHandler>();
    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("http://localhost:5260")
        //BaseAddress = new Uri("https://patinhasmagicasapi.onrender.com")
    };
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<AgendamentoService>();

await builder.Build().RunAsync();
