var configManager = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
#if RELEASE
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
#else
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
#endif
    .Build();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure database mysql context
var connectionstring = configManager.GetConnectionString("DefaultConnection");

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
