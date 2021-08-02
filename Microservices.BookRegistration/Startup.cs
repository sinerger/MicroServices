using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microservices.BookRegistration.Entity;
using Microservices.BookRegistration.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.BookRegistration
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var options = Configuration.GetSection("Bus").Get<BusOptions>();

            services.AddControllers();
            DbConnection connection = new SqlConnection(Configuration.GetConnectionString("Default"));

            services.AddSingleton<IDbConnection>(conn => connection);
            services.AddTransient<IBookRepository, BookRepository>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq();
            });

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(options.Host, options.LocalHost, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

                cfg.ReceiveEndpoint(options.Queue, e =>
                {
                    e.Consumer<BookSubmittedEventConsumer>();
                });
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            busControl.Start();
        }

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
