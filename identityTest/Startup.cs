using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QuickstartIdentityServer.Data;
using QuickstartIdentityServer.Data.Dtos;
using QuickstartIdentityServer.Data.Services;

namespace QuickstartIdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            Mapper.Initialize(cfg => {
                cfg.CreateMap<User, UserDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.wuuid))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.wechatid));
                
                cfg.CreateMap<TestUser,UserDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubjectId))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username));
            });

        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers())
                .AddProfileService<AdditionalClaimsProfileService>()
                .AddDeveloperSigningCredential();

            // use in memory for testing.
            services
                .AddDbContext<IdentityServerContext>(opt => opt.UseMySql(Configuration.GetConnectionString("MyConnectionString") ))//"Server=localhost;database=uow;uid=root;pwd=root1234;"))
                .AddUnitOfWork<IdentityServerContext>()
                .AddCustomRepository<User, UserRepository>();

            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvc()
                .UseMvcWithDefaultRoute();
        }
    }
}
