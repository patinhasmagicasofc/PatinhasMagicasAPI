using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Mapping;
using PatinhasMagicasAPI.Repositories;
using PatinhasMagicasAPI.Services;
using PatinhasMagicasAPI.Services.Interfaces;
using System.Text;




var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var secretKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key not configured");

// Add services to the container.
// Adicionar a conexão com o banco de dados SQL Server
builder.Services.AddDbContext<PatinhasMagicasDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro do AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ApplicationProfile>();
});


// Repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IStatusAgendamentoRepository, StatusAgendamentoRepository>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped<ITipoPagamentoRepository, TipoPagamentoRepository>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<ITipoServicoRepository, TipoServicoRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();


//Services
builder.Services.AddScoped<IEnderecoService, EnderecoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITokenService, TokenService>();


// Configuração do JWT (Authentication)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"], // Configurar no appsettings.json
            ValidAudience = configuration["Jwt:Audience"], // Configurar no appsettings.json
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

//configuração do Cors
//não esquecer de colocar enabledCors nas controllers
//builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
//{
//    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithOrigins("http://127.0.0.1:5500", "http://localhost:5501").AllowCredentials(); // Se estiver usando cookies ou autenticação
//}));


builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));



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

//ativar o CORS
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();