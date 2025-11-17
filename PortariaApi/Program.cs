using Microsoft.EntityFrameworkCore;
using PortariaApi.Data;

var builder = WebApplication.CreateBuilder(args);

// --- SEÇÃO DO BANCO DE DADOS ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString)
    )
);

// --- ADICIONA O SERVIÇO DE CORS ---
builder.Services.AddCors(options => // <-- NOVO
{
    options.AddPolicy("AllowReactApp", // <-- NOVO
        policy => policy.WithOrigins("http://localhost:3000") 
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection(); // Você pode ter esta linha

app.UseCors("AllowReactApp"); // <-- NOVO (Diz ao app para USAR a política)

app.UseAuthorization();
app.MapControllers();
app.Run();