﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WestWindSystem.BLL;
using WestWindSystem.DAL;
#endregion

namespace WestWindSystem
{
    public static class WestWindExtensions
    {
        //this class can hold an set of extension methods
        //this sample creates an extension method that will add services
        //  to IServiceCollection
        //this method will be called from an separate project to
        //  gain data from the WestWind database
        //In this demo, the call will be done in the WestWindApp Program.cs file
        //this method will do 2 things:
        //  a) register the context connection string
        //  b) register ALL services that I create within the BLL class(es)
        public static void WestWindExtensionServices(this IServiceCollection services,
                Action<DbContextOptionsBuilder> options)
        {
            //handle the connection string
            //add my context class to the services (IServiceCollection)
            //we wish it to use the string I send in on each creation of the instance of my DbContext class
            services.AddDbContext<WestWindContext>(options);


            //YOU MUST REGISTER EACH AND EVERY BLL SERVICE CLASS YOU WISH TO EXPOSE TO THE WORLD

            //to register a service class you will use the IServiceCollection method
            //  .AddTransient<T> where T is the service class name
            //for any outside user coding that requires access to one or more services
            //  you MUST register the service class, hence all public methods within the class
            //  will be available
            services.AddTransient<BuildVersionServices>((serviceProvider) =>
                {
                    //this statement obtains the context information registered above
                    var context = serviceProvider.GetService<WestWindContext>();

                    //create an instance of the service class and register the said class
                    //  in IServiceCollection
                    //once the class has been registered, it can be used by ANY outside
                    //  source as long as the outside source has referenced the extension 
                    //  class method (see your Program.cs in your wb app)
                    return new BuildVersionServices(context);
                });
            services.AddTransient<RegionServices>((serviceProvider) =>
            {
              
                var context = serviceProvider.GetService<WestWindContext>();

                return new RegionServices(context);
            });
        }
    }
}
