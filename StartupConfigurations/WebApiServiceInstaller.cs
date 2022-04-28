using System.Text;
using AspNetCore_RestAPI.Options;
using AspNetCore_RestAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AspNetCore_RestAPI.StartupConfigurations
{
    public class WebApiServiceInstaller : IStartupConfigInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configurations)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddScoped<IIdentityService, IdentityService>();

            #region JWT
            var jwtSettings = new JwtOptions();
            configurations.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings); //we might need to use this in other class...

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {

                var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Dotnet WebAPI",
                    Version = "v1",
                });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** token, no need to put bearer",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                x.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });
            #endregion
        
            
        }
    }
}