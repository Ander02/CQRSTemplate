using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation.AspNetCore;
using CQRSTemplate.Infraestructure.Filters;
using CQRSTemplate.Database;
using CQRSTemplate.Infraestructure.Middlewares;
using GraphQL;
using GraphQL.Types;
using CQRSTemplate.Database.Repository.Interface;
using CQRSTemplate.Database.Repository;
using CQRSTemplate.GraphQL.Types;
using CQRSTemplate.GraphQL.Schemas;
using CQRSTemplate.GraphQL.Query;

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
            services.AddMvc(options =>
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

            //Dababase Injection
            services.AddDbContext<Db>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            //Repository Injection
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddCors();
            services.AddMediatR(typeof(Startup));

            //GraphQL Dependency Injection
            services.AddScoped<GraphQLQuery>();
            services.AddTransient<UserType>();
            services.AddTransient<MessageType>();
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            var servicesProvider = services.BuildServiceProvider();
            services.AddScoped<ISchema>(schema => new GraphQLSchema(type => (GraphType)servicesProvider.GetService(type))
            {
                Query = servicesProvider.GetService<GraphQLQuery>()
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().WithMethods(new string[] { "GET", "POST", "OPTIONS" }).AllowAnyHeader());

            app.UseMiddleware<HttpExceptionHandlerMiddleware>();
            app.UseMvc();
        }
    }
}
