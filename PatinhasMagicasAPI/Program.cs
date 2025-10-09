using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Repositories;
using PatinhasMagicasAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// L� a chave secreta do appsettings.json
var secretKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key not configured");

// Add services to the container.
// Adicionar a conex�o com o banco de dados SQL Server
builder.Services.AddDbContext<PatinhasMagicasDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Reposit�rios e Servi�os (Consolidado e Corrigido)
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(); // Mantido uma vez
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>(); // Mantido uma vez
builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IStatusAgendamentoRepository, StatusAgendamentoRepository>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<ITipoPagamentoRepository, TipoPagamentoRepository>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<ITipoServicoRepository, TipoServicoRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<ITokenService, TokenService>(); // CORRE��O do erro de DI 500
builder.Services.AddScoped<PedidoService, PedidoService>(); // Service
builder.Services.AddHttpClient<CepService>();

// Configura��o do Cors (CORRE��O do erro de CORS)
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    // Permite as origens espec�ficas e remove o AllowAnyOrigin para funcionar com AllowCredentials
    builder.WithOrigins("http://127.0.0.1:5501", "http://localhost:5260")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials(); // Crucial para o JWT
}));

//Services
builder.Services.AddScoped<PedidoService, PedidoService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Configura��o do JWT (Authentication)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });


builder.Services.AddControllers();
// Troca do openAPI pelo swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting(); // Deve vir antes do UseCors se voc� usar rotas espec�ficas

// Ativar o CORS (POSI��O CORRETA)
app.UseCors("MyPolicy");

// Autentica��o (deve vir antes da Autoriza��o)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();