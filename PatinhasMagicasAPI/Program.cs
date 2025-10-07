using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Services;
using PatinhasMagicasAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
// Program.cs



var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var secretKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key not configured");


// Add services to the container.
//base de dados
builder.Services.AddDbContext<PatinhasMagicasDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IStatusAgendamentoRepository, StatusAgendamentoRepository>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();


// Adicionar a conexão com o banco de dados SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PatinhasMagicasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositórios
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();

//Services
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
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithOrigins("http://127.0.0.1:5501", "http://localhost:5501").AllowCredentials(); // Se estiver usando cookies ou autenticação
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