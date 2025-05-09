using Decathlon.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Register services for DI
builder.Services.AddScoped<IInputParser, CsvInputParser>();
builder.Services.AddScoped<IOutputWriter, OutputWriterService>();
builder.Services.AddScoped<IRankingService, RankingService>();
builder.Services.AddScoped<ICalculationService, CalculationService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();