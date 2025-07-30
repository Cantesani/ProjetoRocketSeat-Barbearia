using Barbearia.Domain.Repositories;
using Barbearia.Domain.Repositories.Criptografia;
using Barbearia.Domain.Security.Tokens;
using Barbearia.Infrastructure.DataAccess;
using Barbearia.Infrastructure.Repositories;
using Barbearia.Infrastructure.Security.Token;
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
            AddToken(services, configuration);

            services.AddScoped<IPasswordCriptografia, Security.BCrypt>();
        }

        private static void AddToken(IServiceCollection services, IConfiguration configuration)
        {
            var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
            var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

            services.AddScoped<IAcessTokenGenerator>(config =>
                new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFaturamentoReadOnlyRepository, FaturamentoRepository>();
            services.AddScoped<IFaturamentoUpdateOnlyRepository, FaturamentoRepository>();
            services.AddScoped<IFaturamentoWriteOnlyRepository, FaturamentoRepository>();
            services.AddScoped<IUsuarioWriteOnlyRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioReadOnlyRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioUpdateOnlyRepository, UsuarioRepository>();
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
