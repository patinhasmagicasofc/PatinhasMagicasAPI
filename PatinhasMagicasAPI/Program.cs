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

// ?? Chave secreta JWT
var secretKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key not configured");

// ?? Banco de dados
builder.Services.AddDbContext<PatinhasMagicasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ?? Repositórios e serviços
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();
builder.Services.AddScoped<IItemPedidoRepository, ItemPedidoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IStatusAgendamentoRepository, StatusAgendamentoRepository>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<ITipoPagamentoRepository, TipoPagamentoRepository>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<ITipoServicoRepository, TipoServicoRepository>();
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<PedidoService, PedidoService>();
builder.Services.AddHttpClient<CepService>();

// ? CORS — configurado para cobrir todos os cenários locais
builder.Services.AddCors(options =>
{
    // Permite as origens específicas e remove o AllowAnyOrigin para funcionar com AllowCredentials
    builder.WithOrigins("http://127.0.0.1:5501", "http://localhost:5260")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials(); // Crucial para o JWT
}));

//Services
builder.Services.AddScoped<PedidoService, PedidoService>();
builder.Services.AddScoped<ITokenService, TokenService>();
    options.AddPolicy("MyPolicy", policy =>
    {
        policy
            .WithOrigins(
                "http://127.0.0.1:5500",
                "http://localhost:5500",
                "http://localhost:5260"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// ?? JWT
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

        // ?? Corrige erro de preflight (OPTIONS) em rotas com JWT
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Permite leitura do token de Authorization ou cookie
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                if (string.IsNullOrEmpty(token) && context.Request.Cookies.ContainsKey("jwt"))
                    token = context.Request.Cookies["jwt"];

                context.Token = token;
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ?? Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ? Ordem correta (importantíssimo)
app.UseCors("MyPolicy");           
app.UseHttpsRedirection();       
app.UseAuthentication();           
app.UseAuthorization();           
app.MapControllers();              

app.Run();
