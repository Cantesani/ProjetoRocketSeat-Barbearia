using Barbearia.Application.AutoMapper;
using Barbearia.Application.UseCases.Faturamento.Create;
using Barbearia.Application.UseCases.Faturamento.Delete;
using Barbearia.Application.UseCases.Faturamento.GetAll;
using Barbearia.Application.UseCases.Faturamento.GetById;
using Barbearia.Application.UseCases.Faturamento.Relatorios.Excel;
using Barbearia.Application.UseCases.Faturamento.Relatorios.Pdf;
using Barbearia.Application.UseCases.Faturamento.Update;
using Barbearia.Application.UseCases.Login;
using Barbearia.Application.UseCases.Usuario.Delete;
using Barbearia.Application.UseCases.Usuario.GetById;
using Barbearia.Application.UseCases.Usuario.Update;
using Barbearia.Application.UseCases.Uuario.Register;
using Microsoft.Extensions.DependencyInjection;

namespace Barbearia.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddAplication(this IServiceCollection services)
        {
            AddAutoMapper(services);
            AddAUseCase(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }

        private static void AddAUseCase(IServiceCollection services)
        {
            services.AddScoped<ICreateFaturamentoUseCase, CreateFaturamentoUseCase>();
            services.AddScoped<IGetAllFaturamentoUseCase, GetAllFaturamentoUseCase>();
            services.AddScoped<IGetByIdFaturamentoUseCase, GetByIdFaturamentoUseCase>();
            services.AddScoped<IUpdateFaturamentoUseCase, UpdateFaturamentoUseCase>();
            services.AddScoped<IDeleteFaturamentoUseCase, DeleteFaturamentoUseCase>();
            services.AddScoped<IGerarExcelUseCase, GerarExcelUseCase>();
            services.AddScoped<IGerarPdfUseCase, GerarPdfUseCase>();
            services.AddScoped<IRegisterUsuarioUseCase, RegisterUsuarioUseCase>();
            services.AddScoped<IDeleteUsuarioUseCase, DeleteUsuarioUseCase>();
            services.AddScoped<IGetUsuarioByIdUseCase, GetUsuarioByIdUseCase>();
            services.AddScoped<IUpdateUsuarioUseCase, UpdateUsuarioUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
        }
    }
}
