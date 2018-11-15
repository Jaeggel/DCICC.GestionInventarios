using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCICC.AccesoDatos;
using DCICC.Seguridad.Encryption;
using DCICC.Seguridad.TokensJWT;
using DCICC.WebServiceInventarios.Configuration;
using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DCICC.WebServiceInventarios
{
    public class Startup
    {
        //Instancia para la utilización de LOGS en la clase Startup
        private static readonly ILog Logs = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Constructor para inicializar el Web Service, en donde de paso se inicializa la llave de encriptación para sus métodos.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigSeguridad confServSeguridad = new ConfigSeguridad();
            ConfigEncryption.SetEncryptKey(confServSeguridad.ObtenerEncryptionKey());
            ConfigBaseDatos.SetCadenaConexion(confServSeguridad.ObtenerCadenaConexion());
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Método en donde se define los parámetros que tendrá 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "DCICC.Inventarios.Seguridad.Bearer",
                            ValidAudience = "DCICC.Inventarios.Seguridad.Bearer",
                            IssuerSigningKey = JwtSecurityKey.Create("DCICC.Inventarios.Secret.Key")
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Logs.Error("OnAuthenticationFailed: " + context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                Logs.Error("OnTokenValidated: " + context.SecurityToken);
                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Member", policy => policy.RequireClaim("DCICC.Inventarios.MemberId"));
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
