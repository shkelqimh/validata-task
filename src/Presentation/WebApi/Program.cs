using Application;
using Infrastructure.Persistence;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("SQLiteConnection");

builder.Services
    .AddInfrastructureLayer(connectionString!)
    .AddApplicationLayer();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.UseDbMigration();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
