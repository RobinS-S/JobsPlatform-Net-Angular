using Asp.Versioning;
using Jobs.Application.Pagination;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Jobs.API.Extensions
{
    public static class ApiServiceCollectionExtensions
    {
        public static void AddApiCoreServices(this IServiceCollection services)
        {
            // Controllers and JSON settings for enums
            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(null)));

            // API versioning
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader());
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Swagger
            services.AddOpenApi();
            services.AddSwaggerGen(s =>
            {
                foreach (var asm in new[] { Assembly.GetExecutingAssembly(), typeof(PaginationParameters).Assembly })
                {
                    var xml = Path.Combine(AppContext.BaseDirectory, $"{asm.GetName().Name}.xml");
                    if (File.Exists(xml))
                    {
                        s.IncludeXmlComments(xml);
                    }
                }

                s.SwaggerDoc("v1", new()
                {
                    Title = "Jobs API",
                    Version = "v1",
                    Description = "API for managing companies and job postings. Provides CRUD operations, pagination, and filtering for companies and job postings.",
                });
            });

            // FluentValidation in swagger
            services.AddFluentValidationRulesToSwagger();
        }
    }
}
