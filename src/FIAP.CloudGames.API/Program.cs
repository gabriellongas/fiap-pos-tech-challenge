using FIAP.CloudGames.Domain.Interfaces;
using FIAP.CloudGames.Infrastructure.Configuration.Auth;
using FIAP.CloudGames.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = $"FIAP.CloudGames - API (ambiente: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")})", Version = "v1" });

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


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


#region JWT Authentication Configuration

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();

    options.AddPolicy("AdminOnly", policy =>
    policy.RequireRole("Admin"));
});

#endregion


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.RequireAuthorization("AdminOnly")
.WithOpenApi();

app.MapPost("/api/auth/login", async (string email, string userId, IJwtTokenGenerator tokenGenerator) =>
{
    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(userId))
        return Results.BadRequest(new { Error = "Email e senha são obrigatórios." });

    var token = tokenGenerator.GenerateToken(userId, email, "Usuario");

    if (string.IsNullOrWhiteSpace(token))
        return Results.StatusCode(500);

    return Results.Ok(new { Token = token });
})
.WithName("Auth")
.AllowAnonymous();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
