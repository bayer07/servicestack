using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Runtime.Serialization;

namespace ToDoServiceStackCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost()); //Register your ServiceStack AppHost as a .NET Core module
        }
    }

    public class AppHost : AppHostBase
    {
        // Initializes your AppHost Instance, with the Service Name and assembly containing the Services
        public AppHost() : base("Backbone.js TODO", typeof(UserService).GetAssembly()) { }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(
            "", SqlServerDialect.Provider));
            using (var db = container.Resolve<IDbConnectionFactory>().Open())
            {
                if (db.CreateTableIfNotExists<User>())
                {
                    //Add seed data
                }
            }
        }
    }

    public class UserService : Service
    {
        public object Get(UsersRequest request) =>
                    new UsersResponse { Users = Db.Select<User>() };

        public object Post(CreateUserRequest request)
        {
            var user = new User { FirstName = request.FirstName, LastName = request.LastName };
            Db.Save(user);
            return user;
        }
    }
}
