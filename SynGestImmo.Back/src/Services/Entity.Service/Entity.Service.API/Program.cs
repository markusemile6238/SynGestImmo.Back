using Entity.Service.Application;
using Entity.Service.Application.Common;
using Entity.Service.Application.Handler;
using Entity.Service.Infrastructure.Handlers;
using Entity.Service.Structure;
using System.Globalization;
using Tools.Security;
using Tools.Security.Extensions;



namespace Entity.Service.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // globalization
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            builder.Services.AddScoped<ISqlExceptionTranslator, SqlExceptionsHandler>();

            // Add services to the container.
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<TokenPropagationHandler>();


            //Authentification
            builder.Services.AddJwtSecurity(builder.Configuration);

            /* builder.Services.AddAuthentication("Bearer")
             .AddJwtBearer(options =>
 {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,

                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
                     ValidAudience = builder.Configuration["Jwt:Audience"],
                     IssuerSigningKey =
                         new SymmetricSecurityKey(
                             Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                 };
             });*/

            builder.Services.AddControllers()
                  .ConfigureApiBehaviorOptions(options =>
                  {
                      options.SuppressModelStateInvalidFilter = true;
                  })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.Services.AddEndpointsApiExplorer();

            AuthorizationPolicies.AddPolicies(builder.Services);

            /*builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("Frontend", policy =>
                {
                    policy

                        .WithOrigins("https://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });*/



            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
