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
// --- FIM DA SEÇÃO DO BANCO ---


// Add services to the container.
builder.Services.AddControllers();

// LINHAS DO SWAGGER QUE FALTAVAM (SERVIÇOS)
builder.Services.AddEndpointsApiExplorer(); // <-- NOVO
builder.Services.AddSwaggerGen(); // <-- NOVO


var app = builder.Build();

// LINHAS DO SWAGGER QUE FALTAVAM (APLICAÇÃO)
// Estas linhas DEVEM vir antes de app.UseAuthorization()
app.UseSwagger(); // <-- NOVO
app.UseSwaggerUI(); // <-- NOVO


app.UseAuthorization();
app.MapControllers();
app.Run();