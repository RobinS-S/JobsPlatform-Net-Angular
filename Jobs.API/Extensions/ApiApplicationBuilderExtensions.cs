using Asp.Versioning.ApiExplorer;
using Jobs.Infrastructure.Extensions;

namespace Jobs.API.Extensions
{
    public static class ApiApplicationBuilderExtensions
    {
        public static async Task UseApiCorePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                await app.InitializeDatabaseAsync();

                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    foreach (var desc in app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName);
                    }
                });
            }

            app.UseCors(Constants.CorsPolicyName);
            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
