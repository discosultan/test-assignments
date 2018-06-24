using Akka.Actor;
using Gradilium.ShoppingBasket.Baskets;
using Gradilium.ShoppingBasket.Products;
using Gradilium.ShoppingBasket.WebAPI.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace Gradilium.ShoppingBasket.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(c =>
                {
                    c.SerializerSettings.ContractResolver = new PrivateSetterContractResolver();
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Shopping Basket API", Version = "v1" });
                c.IncludeXmlComments(
                    Path.Combine(AppContext.BaseDirectory, GetType().Assembly.GetName().Name + ".xml"),
                    includeControllerXmlComments: true);
            });

            var actorSystem = ActorSystem.Create("ShoppingBasket");
            services.AddSingleton(actorSystem);

            var productsManager = new NamedActorRef<ProductsManagerActor>(actorSystem);
            services.AddSingleton(productsManager);

            var basketsManager = new NamedActorRef<BasketsManagerActor>(
                actorSystem,
                Props.Create<BasketsManagerActor>(productsManager.Actor));
            services.AddSingleton(basketsManager);
        }

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

            // TODO: HTTPS redirection disabled for simplicity.
            //app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping Basket API v1");
                c.RoutePrefix = "";
            });
        }
    }
}
