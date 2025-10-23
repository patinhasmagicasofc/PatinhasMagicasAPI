using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PatinhasMagicasAPI.Data;
using PatinhasMagicasAPI.Interfaces;
using PatinhasMagicasAPI.Mapping;
using PatinhasMagicasAPI.Middleware;
using PatinhasMagicasAPI.Repositories;
using PatinhasMagicasAPI.Services;
using PatinhasMagicasAPI.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// JWT Key
var secretKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key not configured");

// Add services to the container
builder.Services.AddDbContext<PatinhasMagicasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<ApplicationProfile>());

// Repositórios
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
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ITamanhoAnimalRepository, TamanhoAnimalRepository>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IAgendamentoServicoRepository, AgendamentoServicoRepository>();
builder.Services.AddScoped<IServicoTamanhoRepository, ServicoTamanhoRepository>();
builder.Services.AddScoped<IEspecieRepository, EspecieRepository>();
//builder.Services.AddScoped<IStatusPagamentoRepository, StatusPagamentoRepository>();
//builder.Services.AddScoped<IStatusPedidoRepository, StatusPedidoRepository>();


// Services
builder.Services.AddScoped<IServicoService, ServicoService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<PedidoService, PedidoService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<ITamanhoAnimalService, TamanhoAnimalService>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
builder.Services.AddHttpClient<CepService>();

// CORS
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins("http://127.0.0.1:5500", "http://localhost:5260")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
}));

// JWT Authentication
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

// Controllers
builder.Services.AddControllers();

// Swagger com suporte a JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PatinhasMagicasAPI", Version = "v1" });

    // Definição do esquema JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT gerado após login"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Middleware de tratamento de erro
app.UseMiddleware<ErrorHandlingMiddleware>();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Pipeline
app.UseRouting();
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
