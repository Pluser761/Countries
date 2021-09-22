using CountryParseNetCore.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CountryParseNetCore.Services
{
    public static class DatabaseRegistrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<DatabaseContext>(opt =>
            {
                var type = configuration["Type"];
                switch (type)
                {
                    case null:
                        throw new InvalidOperationException("Database type is not defined");
                    default:
                        throw new InvalidOperationException($"Database type {type} not found");

                    case "MSSQL":
                        opt.UseSqlServer(configuration.GetConnectionString(type));
                        break;
                }
            });
    }
}
