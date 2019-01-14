using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Superdigital.Backend.ContaCorrente.AppService;
using Superdigital.Backend.ContaCorrente.Data.Context;
using Superdigital.Backend.ContaCorrente.Data.Repositories;
using Superdigital.Backend.ContaCorrente.Domain.Interfaces;
using Superdigital.Backend.ContaCorrente.Domain.Services;

namespace Superdigital.Backend.ContaCorrente
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
            var conn = Configuration.GetConnectionString("ContaCorrenteDb");
            services.AddDbContext<ContaCorrenteContext>(options => options.UseSqlServer(conn));

            services.AddTransient<IContaClienteRepository, ContaClienteRepository>();
            services.AddTransient<ILancamentoRepository, LancamentoRepository>();
            services.AddTransient<IOperacaoServico, OperacaoServico>();
            services.AddTransient<IContaClienteServico, ContaClienteServico>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
