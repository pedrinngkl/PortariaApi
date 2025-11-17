using Microsoft.EntityFrameworkCore;
using PortariaApi.Data; // Importa o nosso DbContext

var builder = WebApplication.CreateBuilder(args);

// --- INÍCIO DA ADIÇÃO ---

// 1. Obtém a Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Adiciona o DbContext como um serviço
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString) // Deteta a versão do MySQL
    )
);

// --- FIM DA ADIÇÃO ---


// Add services to the container.
builder.Services.AddControllers();
// ... resto do ficheiro (Swagger/OpenAPI, etc.)

var app = builder.Build();

// ... resto do ficheiro

app.UseAuthorization();
app.MapControllers();
app.Run();