using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.DataAccess;
using Barbearia.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Barbearia.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddDbContext(services, configuration);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFaturamentoReadOnlyRepository, FaturamentoRepository>();
            services.AddScoped<IFaturamentoUpdateOnlyRepository, FaturamentoRepository>();
            services.AddScoped<IFaturamentoWriteOnlyRepository, FaturamentoRepository>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");

            var version = new Version(8, 0, 42);
            var serverVersion = new MySqlServerVersion(version);

            services.AddDbContext<BarbeariaDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }



    }
}
