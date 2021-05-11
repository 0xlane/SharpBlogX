using SharpBlogX.Api.Filters;
using SharpBlogX.Options;
using SharpBlogX.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using SharpBlogX.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.Security.Authentication;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Volo.Abp.Threading;
using SharpBlogX.DataSeed;

namespace SharpBlogX.Api
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(SharpBlogXApplicationModule),
        typeof(SharpBlogXMongoDbModule),
        typeof(SharpBlogXBackgroundWorkersModule)
    )]
    public class SharpBlogXApiModule : AbpModule
    {
        public AppOptions AppOptions { get; set; }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            AppOptions = context.Services.ExecutePreConfiguredActions<AppOptions>();

            ConfigureKestrel();
            ConfigureExceptionFilter();
            ConfigureAutoApiControllers();
            ConfigureDbConnection();
            ConfigureAutoValidate();
            ConfigureRouting(context.Services);
            ConfigureRedis(context.Services);
            ConfigureCors(context.Services);
            CofiggureHealthChecks(context.Services);
            ConfigureAuthentication(context.Services);
            ConfigureSwaggerServices(context.Services);
        }

        private void ConfigureKestrel()
        {
            Configure<KestrelServerOptions>(options => 
            {
                IPAddress address = null;
                try
                {
                    address = IPAddress.Parse(AppOptions.Https.ListenAddress);
                }
                catch (FormatException)
                {
                    address = Dns.GetHostAddresses(AppOptions.Https.ListenAddress).FirstOrDefault();
                }
                IPEndPoint SharpBlogEndpoint = new IPEndPoint(address, AppOptions.Https.ListenPort);

                options.Listen(SharpBlogEndpoint, listenOptions => 
                {
                    listenOptions.UseHttps(httpsOptions =>
                    {
                        if (!File.Exists(AppOptions.Https.PrivateCertFile) || !File.Exists(AppOptions.Https.PublicCertFile))
                        {
                            X509Certificate2 certificate = SharpBlogEndpoint.Address.CreateSelfSignedCertificate("CN=SharpBlog");
                            File.WriteAllBytes(AppOptions.Https.PrivateCertFile, certificate.Export(X509ContentType.Pfx));
                            File.WriteAllBytes(AppOptions.Https.PublicCertFile, certificate.Export(X509ContentType.Cert));
                        }
                        try
                        {
                            httpsOptions.ServerCertificate = new X509Certificate2(AppOptions.Https.PrivateCertFile);
                        }
                        catch (CryptographicException)
                        {
                            Console.Error.WriteLine("Error importing SharpBlog certificate.");
                        }
                        httpsOptions.SslProtocols = SslProtocols.Tls12;
                    });
                });
            });
        }

        private void ConfigureExceptionFilter()
        {
            Configure<MvcOptions>(options =>
            {
                var filterMetadata = options.Filters.FirstOrDefault(x => x is ServiceFilterAttribute attribute && attribute.ServiceType.Equals(typeof(AbpExceptionFilter)));
                options.Filters.Remove(filterMetadata);
                options.Filters.Add(typeof(SharpBlogXExceptionFilter));
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(SharpBlogXApplicationModule).Assembly);
            });
        }

        private void ConfigureDbConnection()
        {
            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = AppOptions.Storage.Mongodb;
            });
        }

        private void ConfigureAutoValidate()
        {
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false;
            });
        }

        private static void ConfigureRouting(IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });
        }

        private void ConfigureRedis(IServiceCollection services)
        {
            if (AppOptions.Storage.RedisIsEnabled)
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = AppOptions.Storage.Redis;
                });
            }
        }

        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AppOptions.Cors.PolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            AppOptions.Cors
                                      .Origins
                                      .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                      .Select(x => x.RemovePostFix("/"))
                                      .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        private void CofiggureHealthChecks(IServiceCollection services)
        {
            services.AddHealthChecks()
                    .AddMongoDb(AppOptions.Storage.Mongodb, name: "MongoDB", timeout: TimeSpan.FromSeconds(3))
                    .AddRedis(AppOptions.Storage.Redis, name: "Redis", timeout: TimeSpan.FromSeconds(3));
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateIssuerSigningKey = true,
                            ValidateLifetime = true,
                            RequireExpirationTime = true,
                            ValidIssuer = AppOptions.Jwt.Issuer,
                            ValidAudience = AppOptions.Jwt.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(AppOptions.Jwt.SigningKey.GetBytes())
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnChallenge = async context =>
                            {
                                context.HandleResponse();
                                context.Response.ContentType = "application/json;charset=utf-8";
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                                var response = new BlogResponse();
                                response.IsFailed("Unauthorized");

                                await context.Response.WriteAsJsonAsync(response);
                            },
                            OnMessageReceived = async context =>
                            {
                                context.Token = context.Request.Query["token"];

                                await Task.CompletedTask;
                            }
                        };
                    });
            services.AddAuthorization();
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(AppOptions.Swagger.Name, new OpenApiInfo
                {
                    Title = AppOptions.Swagger.Title,
                    Version = AppOptions.Swagger.Version,
                    Description = AppOptions.Swagger.Description
                });
                options.DocInclusionPredicate((docName, description) => true);
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SharpBlogX.Core.xml"));
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SharpBlogX.Application.xml"));
                options.CustomSchemaIds(type => type.FullName);
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Scheme = "bearer",
                    Description = "Enter <code>token</code> for authorization.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.DocumentFilter<SwaggerDocumentFilter>();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                ForwardLimit = null
            });
            app.UseHealthChecks("/api/health", new HealthCheckOptions
            {
                ResponseWriter = (context, healthReport) =>
                {
                    context.Response.ContentType = "application/json;charset=utf-8";
                    context.Response.StatusCode = StatusCodes.Status200OK;

                    var result = healthReport.Entries.Select(x => new NameValue
                    {
                        Name = x.Key,
                        Value = x.Value.Status.ToString()
                    });

                    var response = new BlogResponse<IEnumerable<NameValue>>();
                    response.IsSuccess(result);

                    return context.Response.WriteAsJsonAsync(response);
                }
            });
            app.UseHsts();
            app.UseRouting();
            app.UseCors(AppOptions.Cors.PolicyName);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.HeadContent = @"<style>.opblock-summary-description{font-weight: bold;text-align: right;}</style>";
                options.SwaggerEndpoint($"/swagger/{AppOptions.Swagger.Name}/swagger.json", AppOptions.Swagger.Title);
                options.DefaultModelsExpandDepth(-1);
                options.DocExpansion(DocExpansion.None);
                options.RoutePrefix = AppOptions.Swagger.RoutePrefix;
                options.DocumentTitle = AppOptions.Swagger.DocumentTitle;
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();

            // 初始化管理员账户：admin/123456
            using (var scope = context.ServiceProvider.CreateScope())
            {
                AsyncHelper.RunSync(async () =>
                {
                    await scope.ServiceProvider
                        .GetRequiredService<UserDataSeedService>()
                        .InitAdminUserAsync();
                });
            }
        }
    }
}