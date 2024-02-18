using EpochApp.Server.Data;
using EpochApp.Shared;
using EpochApp.Shared.Services;
using EpochApp.Shared.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                                                                ValidateIssuerSigningKey = true,
                                                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
                                                                ValidateIssuer = false,
                                                                ValidateAudience = false,
                                                                ClockSkew = TimeSpan.Zero
                                                            };
                    });
            ConfigBuilder.ConfigureCommonServices(services);
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

            var userContentDirectory = Path.Combine(env.ContentRootPath, "UserContent");
            if (!Directory.Exists(userContentDirectory))
            {
                Directory.CreateDirectory(userContentDirectory);
            }
            app.UseStaticFiles(
            new StaticFileOptions
            {
                FileProvider = null,
                RequestPath = "/UserContent"
            }
            );

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}