using TodoManager.Api;
using TodoManager.Api.Helpers;
using TodoManager.Application;
using TodoManager.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddStandardServices()
    .AddCoreModules()
    .AddInfrastructure(builder.Configuration)
    .AddAuthServices()
    .AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
        .UseSwaggerUI();
}

app
    .UseDefaultFiles()
    .UseStaticFiles()
    .UseHttpsRedirection();

app.UseCors(opts =>
{
    opts.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app
    .UseAuthentication()
    .UseAuthorization()
    .UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();
app.Run();