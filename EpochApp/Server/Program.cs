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
using System.Text;
using System.Text.Json.Serialization;

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
            services.AddAutoMapper(typeof(ArticleProfile));
            services.AddAutoMapper(typeof(ManuscriptProfile));
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IWorldService, WorldService>();
            services.AddRazorPages();
            services.AddSwaggerGen();

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