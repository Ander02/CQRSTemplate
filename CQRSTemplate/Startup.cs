using CQRSTemplate.GraphQL;
using CQRSTemplate.GraphQL.InputType.Mutations;
using CQRSTemplate.GraphQL.InputType.Querys;
using CQRSTemplate.GraphQL.Root;
using CQRSTemplate.GraphQL.Types;
using CQRSTemplate.Infraestructure.Database;
using CQRSTemplate.Infraestructure.Filters;
using CQRSTemplate.Infraestructure.Middlewares;
using FluentValidation.AspNetCore;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CQRSTemplate
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
            #region Application Config
            services.AddMvc((options) =>
            {
                options.Filters.Add(typeof(ValidationActionFilter));
            })
            .AddFeatureFolders()
            .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            });

            services.AddCors();
            services.AddMediatR(typeof(Startup));
            #endregion

            #region Database Config
            services.AddDbContext<Db>((options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion
            
            #region GraphQL Config
            //Types
            services.AddTransient<UserType>();
            services.AddTransient<MessageType>();

            //Mutation Input Types
            services.AddTransient<UserInputType>();
            services.AddTransient<MessageInputType>();

            //Querys Input Type
            services.AddTransient<SearchUserInputType>();

            //Roots
            services.AddScoped<RootQuery>();
            services.AddScoped<RootMutation>();

            //Schema
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            var servicesProvider = services.BuildServiceProvider();
            services.AddScoped<ISchema>(schema => new GraphQLSchema(type => (GraphType)servicesProvider.GetService(type))
            {
                Query = servicesProvider.GetService<RootQuery>(),
                Mutation = servicesProvider.GetService<RootMutation>()
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Cors Config
            app.UseCors(builder => builder.AllowAnyOrigin().WithMethods(new string[] { "GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS" }).AllowAnyHeader());
            #endregion

            #region Middlewares Config
            app.UseMiddleware<HttpExceptionHandlerMiddleware>();
            #endregion

            app.UseMvc();
        }
    }
}
