using dotnet_webapi_example.Options;
using dotnet_webapi_example.StartupConfigurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServicetoAssembly(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var swaggerOptions = new SwaggerOptions();
    app.Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

    app.UseSwagger(options =>
    {
        options.RouteTemplate = swaggerOptions.JsonRoute;
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
