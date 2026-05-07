using Siemens.Internship2026.GradeBook.Data;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Repositories;
using Siemens.Internship2026.GradeBook.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddHttpClient<IDataContext, HttpDataContext>();

builder.Services.AddScoped<IItemReader, ItemRepository>();

builder.Services.AddScoped<IItemValidatorService, ItemValidatorService>();

builder.Services.AddScoped<IItemService, ItemService>();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();