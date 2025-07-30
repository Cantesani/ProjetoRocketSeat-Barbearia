using AutoMapper;
using Barbearia.Communication.Request;
using Barbearia.Communication.Request.Usuario;
using Barbearia.Communication.Response;
using Barbearia.Communication.Response.Usuario;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestFaturamentoJson, Faturamento>();
            CreateMap<RequestRegisterUsuarioJson, Usuario>()
                .ForMember(x => x.Senha, config => config.Ignore());
        }

        private void EntityToResponse()
        {
            CreateMap<Faturamento, ResponseFaturamentoJson>();
            CreateMap<Faturamento, ResponseShortFaturamentoJson>();
            CreateMap<Usuario, ResponseUsuarioJson>();
        }
    }
}