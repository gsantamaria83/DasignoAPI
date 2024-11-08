using Dasigno.Application.Interfaces;
using Dasigno.Application.Services;
using Dasigno.Repositories.Interfaces;
using Dasigno.Repositories.Services;
using System.Data.SqlClient;
using System.Reflection;
using System.Resources;

var builder = WebApplication.CreateBuilder(args);
EnsureDatabaseCreated();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())

{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void EnsureDatabaseCreated()
{
    string connectionString = builder.Configuration.GetConnectionString("Connection");

    // Obtener el nombre de la base de datos desde la cadena de conexión
    var builderWithDatabase = new SqlConnectionStringBuilder(connectionString);
    string databaseName = builderWithDatabase.InitialCatalog;

    // Crear una cadena de conexión sin especificar la base de datos
    var builderWithoutDatabase = new SqlConnectionStringBuilder(connectionString)
    {
        InitialCatalog = "" // Vacía el nombre de la base de datos para conectarse al servidor
    };

    using var connection = new SqlConnection(builderWithoutDatabase.ToString());
    connection.Open();

    // Verificar si la base de datos existe
    var checkDatabaseExistsCommand = new SqlCommand(
        $"IF DB_ID('{databaseName}') IS NULL SELECT 0 ELSE SELECT 1", connection);
    var databaseExists = (int)checkDatabaseExistsCommand.ExecuteScalar() == 1;

    if (!databaseExists)
    {
        // Obtener el script SQL desde el archivo .resx y reemplazar el nombre de la base de datos
        var resourceManager = new ResourceManager("DasignoAPI.Resources.Resource", Assembly.GetExecutingAssembly());
        string databaseScriptCreation = resourceManager.GetString("DatabaseCreation");
        string databaseScriptTable = resourceManager.GetString("TableCreation");
        string databaseSPInsertar = resourceManager.GetString("CreateProcedureInsertarUsuario");
        string databaseSPModificar = resourceManager.GetString("CreateProcedureModificarUsuario");
        string databaseSPEliminar = resourceManager.GetString("CreateProcedureEliminarUsuario");
        string databaseSPSeleccionarPorId = resourceManager.GetString("CreateProcedureSeleccionarUsuarioPorId");
        string databaseSPSeleccionarPorPrimerNombreApellido = resourceManager.GetString("CreateProcedureSeleccionarUsuarioPorPrimerNombreApellido");

        
        databaseScriptCreation = databaseScriptCreation.Replace("@BaseDatosImplementacion", databaseName);
        databaseScriptTable = databaseScriptTable.Replace("@BaseDatosImplementacion", databaseName);
        databaseSPInsertar = databaseSPInsertar.Replace("@BaseDatosImplementacion", databaseName);
        databaseSPModificar = databaseSPModificar.Replace("@BaseDatosImplementacion", databaseName);
        databaseSPEliminar = databaseSPEliminar.Replace("@BaseDatosImplementacion", databaseName);
        databaseSPSeleccionarPorId = databaseSPSeleccionarPorId.Replace("@BaseDatosImplementacion", databaseName);
        databaseSPSeleccionarPorPrimerNombreApellido = databaseSPSeleccionarPorPrimerNombreApellido.Replace("@BaseDatosImplementacion", databaseName);

        // Ejecutar el script para crear la base de datos y tablas
        
        var createDatabaseCommand = new SqlCommand(databaseScriptCreation, connection);
        createDatabaseCommand.ExecuteNonQuery();
        Console.WriteLine("Se creo la base de datos" + databaseName);

        var createTableCommand = new SqlCommand(databaseScriptTable, connection);
        createTableCommand.ExecuteNonQuery();
        Console.WriteLine("Se creo la tabla Usuario");

        var databaseSPInsertarCommand = new SqlCommand(databaseSPInsertar, connection);
        databaseSPInsertarCommand.ExecuteNonQuery();
        Console.WriteLine("Se creo el sp de insercion de usuario");

        var databaseSPModificarCommand = new SqlCommand(databaseSPModificar, connection);
        databaseSPModificarCommand.ExecuteNonQuery();
        Console.WriteLine("Se creo el sp de modificacion de usuario");

        var databaseSPEliminarCommand = new SqlCommand(databaseSPEliminar, connection);
        databaseSPEliminarCommand.ExecuteNonQuery();
        Console.WriteLine("Se creo el sp de eliminacion de usuario");

        var databaseSPSeleccionarPorIdCommand = new SqlCommand(databaseSPSeleccionarPorId, connection);
        databaseSPSeleccionarPorIdCommand.ExecuteNonQuery();
        Console.WriteLine("Se creo el sp de consulta de usuario por Id");

        var databaseSPSeleccionarPorPrimerNombreApellidoCommand = new SqlCommand(databaseSPSeleccionarPorPrimerNombreApellido, connection);
        databaseSPSeleccionarPorPrimerNombreApellidoCommand.ExecuteNonQuery();
        Console.WriteLine("Se creo el sp de consulta de usuario por primer nombre y primer apellido");
    }
    else
    {
        Console.WriteLine("La base de datos ya existe.");
    }
}
