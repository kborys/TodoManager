using TodoManager.Api;
using TodoManager.Api.Helpers;
using TodoManager.Core;
using TodoManager.Infrastructure.Dapper;

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

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors(opts =>
{
    opts.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
