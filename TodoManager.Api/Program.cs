using TodoManager.Api;
using TodoManager.Api.Helpers;
using TodoManager.Core;
using TodoManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddStandardServices();
builder.Services.AddCoreModules();
builder.Services.AddInfrastructure();

builder.AddAuthServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
