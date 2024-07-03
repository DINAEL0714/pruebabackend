using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Cargar la configuración
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configurar los servicios
builder.Services.AddSingleton<SqlConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new SqlConnection(connectionString);
});
builder.Services.AddControllers();

var app = builder.Build();

// Probar la conexión a la base de datos
try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        Console.WriteLine("Conexión a la base de datos exitosa.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
