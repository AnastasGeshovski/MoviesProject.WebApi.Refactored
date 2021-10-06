using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.Movies.DataAccess.Context;
using SEDC.Movies.DataAccess.Repositories.Classes;
using SEDC.Movies.DataAccess.Repositories.Interfaces;
using SEDC.Movies.Services.Classes;
using SEDC.Movies.Services.Interfaces;
using SEDC.Movies.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Movies.IoC
{
    //Microsoft.EntityFrameworkCore.SqlServer 3.1.19
    //Microsoft.Extensions.DependencyInjection.Abstractions 5.0.0
    public static class IoCContainer
    {
        public static IServiceCollection ConfigureIoCContainer(IServiceCollection services,
                                                              string conectionString)
        {
            services.AddDbContext<MoviesAppDbContext>(x =>
            {
                x.UseSqlServer(conectionString);
            });

            //register repositories
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Movie>, MovieRepository>();

            //regioster services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMovieService, MovieService>();

            return services;
        }
    }
}
