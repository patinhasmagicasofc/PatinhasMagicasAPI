using Microsoft.EntityFrameworkCore;
using PatinhasMagicasAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Adicionar a conex�o com o banco de dados SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PatinhasMagicasDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories

//Services

//configura��o do Cors
//n�o esquecer de colocar enabledCors nas controllers
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
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();