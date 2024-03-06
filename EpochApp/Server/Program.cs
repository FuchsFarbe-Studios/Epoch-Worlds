using EpochApp.Server.Data;
using EpochApp.Server.Maps;
using EpochApp.Server.Services;
using EpochApp.Server.Services.MailService;
using EpochApp.Shared;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using ArticleService=EpochApp.Server.Services.ArticleService;
using LookupService=EpochApp.Server.Services.LookupService;
using WorldService=EpochApp.Server.Services.WorldService;

#pragma warning disable CS1591// Missing XML comment for publicly visible type or member

namespace EpochApp.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            var services = builder.Services;

            // Add services to the container.
            services.AddSingleton(builder.Configuration.GetSection("MailSettings").Get<MailSettings>());
            services.AddScoped<IMailService, MailService>();
            services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
                    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        options.InvalidModelStateResponseFactory = context => new BadRequestObjectResult(context.ModelState);
                    });
            services.AddDbContext<EpochDataDbContext>(
            options =>
            {
                options.UseSqlServer(config.GetConnectionString("LocalUserConnection"));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                                                            {
                                                                ValidateAudience = true,
                                                                ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                                                                ValidateIssuerSigningKey = true,
                                                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
                                                                ValidateIssuer = true,
                                                                ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                                                                ClockSkew = TimeSpan.Zero
                                                            };
                    });
            ConfigBuilder.ConfigureCommonServices(services);
            services.AddHttpContextAccessor();

            // Automapper mappings
            services.AddAutoMapper(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
            });
            services.AddAutoMapper(typeof(ArticleProfile));
            services.AddAutoMapper(typeof(ManuscriptProfile));
            services.AddAutoMapper(typeof(FileProfile));
            services.AddAutoMapper(typeof(WorldProfile));
            services.AddAutoMapper(typeof(UserMapProfile));

            // Custom services
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IProfileSerivce, ProfileService>();
            services.AddScoped<IUserModeration, ModerationService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<IManuscriptService, ManuscriptService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IWorldService, WorldService>();
            services.AddScoped<IFileService, UserFileService>();
            services.AddRazorPages();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                                   {
                                       Title = "The Epoch Worlds API",
                                       Description = "This is the api for the various endpoints for the Epoch Worlds application.",
                                       Version = "v1",
                                       Contact = new OpenApiContact
                                                 {
                                                     Name = "Oliver Conover",
                                                     Email = "contact@epochgen.com",
                                                 },
                                   });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            app.UseStatusCodePages();
            var env = app.Environment;
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Epoch World API V1");
                    c.DocumentTitle = "Epoch World API";
                });
                app.UseWebAssemblyDebugging();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            var userContentDirectory = Path.Combine(env.ContentRootPath, StaticUtils.Constants.UserFilesDirectory);
            var worldContentDirectory = Path.Combine(env.ContentRootPath, StaticUtils.Constants.WorldFilesDirectory);
            if (!Directory.Exists(userContentDirectory))
                Directory.CreateDirectory(userContentDirectory);
            if (!Directory.Exists(worldContentDirectory))
                Directory.CreateDirectory(worldContentDirectory);
            app.UseStaticFiles(
            new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(userContentDirectory),
                RequestPath = $"/{StaticUtils.Constants.UserFilesDirectory}"
            }
            );
            app.UseStaticFiles(
            new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(worldContentDirectory),
                RequestPath = $"/{StaticUtils.Constants.WorldFilesDirectory}"
            }
            );

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }

}