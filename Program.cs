using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaymentAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PaymentDetailContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

var app = builder.Build();

//builder.Services.AddLogging(logging =>
//{
//    logging.ClearProviders(); // Clear the default providers
//    logging.AddConsole(); // Add Console provider
//    //logging.AddFilter("path/to/log/file.txt");
//});


// Database migration
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var dbContext = services.GetRequiredService<PaymentDetailContext>();
//    dbContext.Database.Migrate(); // Apply migrations
//}

//app.MapGet("/mini", () => "Hello ur api is working.");
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
//app.UseHttpLogging();

app.Run();