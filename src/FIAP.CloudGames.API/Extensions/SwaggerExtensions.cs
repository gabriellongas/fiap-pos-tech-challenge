using Microsoft.OpenApi.Models;

namespace FIAP.CloudGames.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"FIAP.CloudGames - API (ambiente: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")})",
                    Version = "v1",
                    Description = "A Api FIAP Cloud Games (FCG) é uma plataforma de venda de jogos digitais e gestão de servidores para partidas online.",
                    Contact = new OpenApiContact
                    {
                        Name = "FIAP Cloud Games Team",
                        Url = new Uri("https://github.com/gabriellongas/fiap-pos-tech-challenge")
                    }
                });                

                setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Informe o token JWT no formato: Bearer {seu_token}",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            In = ParameterLocation.Header,
                            Name = "Authorization" // mais preciso aqui
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}

