var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configManager = builder.Configuration;

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
var loggerConfig = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level}] {Message:lj}{NewLine}{Exception}");
if (!string.IsNullOrWhiteSpace(configManager["LogLocation"]))
{
    loggerConfig = loggerConfig.WriteTo.File(configManager["LogLocation"] + "/RecipeAPI.log", outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Day);
}
var logger = loggerConfig.CreateLogger();
builder.Services.AddSingleton<ILogger>(logger);

logger.Information("Starting up");

//log all unhandled exceptions
AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) => logger.Fatal(eventArgs.ExceptionObject.ToString());

var dbHost = configManager["DBHOST"];
if (string.IsNullOrWhiteSpace(dbHost))
{
    logger.Error("DBHOST not set");
    throw new Exception("DBHOST not set");
}

var dbPort = configManager["DBPORT"];
if (string.IsNullOrWhiteSpace(dbPort))
{
    logger.Warning("DBPORT not set. Using default port 3306");
    dbPort = "3306";
}

var dbUser = configManager["DBUSER"];
if (string.IsNullOrWhiteSpace(dbUser))
{
    logger.Error("DBUSER not set");
    throw new Exception("DBUSER not set");
}

var dbPassword = configManager["DBPASS"];
if (string.IsNullOrWhiteSpace(dbPassword))
{
    logger.Error("DBPASS not set");
    throw new Exception("DBPASS not set");
}

// Configure database mysql context
var connectionstring = $"server={dbHost};port={dbPort};database=RecipeApp;user={dbUser};password={dbPassword}";

builder.Services.AddDbContext<RecipeContext>(options =>
    options.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring)));

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
