using AutoMapper;
using Barbearia.Communication.Request;
using Barbearia.Communication.Response;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.AutoMapper
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            RequestToEntity();
            EntityToResponse();
        }

        private void RequestToEntity()
        {
            CreateMap<RequestFaturamentoJson, Faturamento>();
        }

        private void EntityToResponse()
        {
            CreateMap<Faturamento, ResponseFaturamentoJson>();
            CreateMap<Faturamento, ResponseShortFaturamentoJson>();

        }
    }
}
