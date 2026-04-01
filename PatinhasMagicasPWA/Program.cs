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
        BaseAddress = new Uri ("https://zwk6xtsh-5026.brs.devtunnels.ms")
        //BaseAddress = new Uri("http://localhost:5026")
        ////BaseAddress = new Uri("https://patinhasmagicasapi.onrender.com")
    };
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<AgendamentoService>();
builder.Services.AddScoped<ServicoService>();
builder.Services.AddScoped<AnimalService>();
builder.Services.AddScoped<TamanhoAnimalService>();
builder.Services.AddScoped<EspecieService>();
builder.Services.AddScoped<EnderecoService>();
builder.Services.AddScoped<CepService>();
builder.Services.AddScoped<PushNotificationService>();
builder.Services.AddScoped<DeviceFeedbackService>();
builder.Services.AddScoped<TipoPagamentoService>();


await builder.Build().RunAsync();
