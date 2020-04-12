using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleCrudDapper.IRepository;
using SampleCrudDapper.Repository;

namespace SampleCrudDapper
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
            services.AddControllersWithViews();

            /*
              Referência: http://www.macoratti.net/18/10/aspn_crudap1.htm
              Na implementação da Injeção de dependência do ASP.NET Core, vemos o conceito de lifetimes ou "tempo de vidas".
              Um lifetime ou tempo de vida especifica quando um objeto DI-injetado é criado ou recriado. Existem três possibilidades:
                1- Transient : Criado a cada vez que são solicitados.
                2- Scoped: Criado uma vez por solicitação.
                3- Singleton: Criado na primeira vez que são solicitados. Cada solicitação subseqüente usa a instância que foi criada na primeira vez.
             */

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IProdutosRepository, ProdutosRepository>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
