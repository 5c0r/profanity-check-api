using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProfanityCheckLib.Model;
using ProfanityCheckLib.Service;

namespace ProfanityCheck.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Allow Content-Negotiation
            services.AddControllers(opts =>
            {
                opts.RespectBrowserAcceptHeader = true;
            });

            services.AddSingleton<ICheckProfanity, ProfanityCheckService>((svc) =>
               {
                   var newSvc = new ProfanityCheckService();
                   
                   newSvc.AddNewBannedWordsFromGitHub("https://raw.githubusercontent.com/chucknorris-io/swear-words/master/fi");
                   newSvc.AddNewBannedWordsFromGitHub("https://raw.githubusercontent.com/chucknorris-io/swear-words/master/en");
                   newSvc.AddBannedWordsFromTextFile("Banned_Words.txt");

                   return newSvc;
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
