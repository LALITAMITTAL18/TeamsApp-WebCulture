using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.TeamsFx;
using Microsoft.TeamsFx.SimpleAuth;
using System;
using TeamsApp_WebCulture.Interop.TeamsSDK;

namespace TeamsApp_WebCulture
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddRazorPages().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            var defaultLocal = Configuration["Default_Culture"];
            string[] supportedCulture = Configuration.GetSection("Supported_Culture").Get<string[]>();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture(defaultLocal);
                // UI string 
                options.AddSupportedUICultures(supportedCulture);
                // Formatting numbers, dates, etc.
                options.AddSupportedCultures(supportedCulture);
                options.FallBackToParentUICultures = true;
            });

            services.AddServerSideBlazor();

            services.AddTeamsFxSimpleAuth(Configuration);
            services.AddScoped<TeamsFx>();
            services.AddScoped<TeamsUserCredential>();
            services.AddScoped<MicrosoftTeams>();

            services.AddControllers();
            services.AddHttpClient("WebClient", client => client.Timeout = TimeSpan.FromSeconds(600));
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();


            var supportedCultures = Configuration.GetSection("Supported_Culture").Get<string[]>(); ;
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllers();
            });
        }
    }
}
