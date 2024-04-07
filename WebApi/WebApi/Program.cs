using MiApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// A�adir servicios a la aplicaci�n
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Configurar DbContext para usar SQL Server y especificar el ensamblado de migraciones
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("WebApi")));  

// Configuraci�n opcional de CORS, ajusta seg�n las necesidades
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
        builder.WithOrigins("https://localhost:7203") // Cambia esto al origen correcto
               .AllowAnyMethod()
               .AllowAnyHeader());
});

// A�adir soporte para Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configurar el HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

app.MapControllers();

// Mapear Razor Pages
app.MapRazorPages();

// Aplicar migraciones autom�ticamente al iniciar la aplicaci�n, opcional
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // Usa Migrate en lugar de EnsureCreated para aplicar migraciones
}

app.Run();
