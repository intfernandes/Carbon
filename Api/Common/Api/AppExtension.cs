namespace Api.Common.Api;

public static class AppExtension
{
    public static void ConfigureDevEnvironment(this WebApplication app)
    {
        app.UseDeveloperExceptionPage();
        
        app.UseSwagger(); 
        
        app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
        // app.MapSwagger().RequireAuthorization();
    }
}