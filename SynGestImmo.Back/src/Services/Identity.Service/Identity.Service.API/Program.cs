using Identity.Service.API.Handler;
using Identity.Service.Application;
using Identity.Service.Application.Common;
using Identity.Service.Infrastructure.Handlers;
using Identity.Service.Infrastructure.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // globaliztion
        var cultureInfo = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        // Add services to the container.
        builder.Services.AddInfrastructure();
        builder.Services.AddApplication();

        builder.Services.AddScoped<ISqlExceptionTranslator, SqlExceptionsHandler>();

        // JWt Token
        builder.Services.AddScoped<ITokenService, JwtTokenService>();

        // authentification
        builder.Services
            .AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                        ),
                    NameClaimType=JwtRegisteredClaimNames.Sub,
                    RoleClaimType=ClaimTypes.Role
                };
            });


        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });



        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddAuthorization(opts =>
        {
            opts.InvokeHandlersAfterFailure = false;
            opts.AddPolicy("PasswordChanged", Policy => Policy.Requirements.Add(new PasswordChangeRequirement()));
        });

        builder.Services.AddSingleton<IAuthorizationHandler, PasswordChangeHandler>();
        builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler,PasswordChangedResultHandler>();


        // cors
        builder.Services.AddCors(opts =>
        {
            opts.AddPolicy("Frontend", policy =>
            {
                policy
                    .WithOrigins("http://localhost:4200")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.UseCors("Frontend");
        app.UseHttpsRedirection();                
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}