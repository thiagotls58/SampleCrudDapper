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
              Refer�ncia: http://www.macoratti.net/18/10/aspn_crudap1.htm
              Na implementa��o da Inje��o de depend�ncia do ASP.NET Core, vemos o conceito de lifetimes ou "tempo de vidas".
              Um lifetime ou tempo de vida especifica quando um objeto DI-injetado � criado ou recriado. Existem tr�s possibilidades:
                1- Transient : Criado a cada vez que s�o solicitados.
                2- Scoped: Criado uma vez por solicita��o.
                3- Singleton: Criado na primeira vez que s�o solicitados. Cada solicita��o subseq�ente usa a inst�ncia que foi criada na primeira vez.
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
