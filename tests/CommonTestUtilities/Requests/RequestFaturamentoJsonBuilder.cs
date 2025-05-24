using Barbearia.Communication.Enums;
using Barbearia.Communication.Request;
using Bogus;

namespace CommonTestUtilities.Requests
{
    public class RequestFaturamentoJsonBuilder
    {
        public static RequestFaturamentoJson Build()
        {
            return new Faker<RequestFaturamentoJson>()
                   .RuleFor(x => x.Titulo, faker => faker.Commerce.ProductName())
                   .RuleFor(x => x.Descricao, faker => faker.Commerce.ProductDescription())
                   .RuleFor(x => x.Data, faker => faker.Date.Past())
                   .RuleFor(x => x.TipoPagto, faker => faker.PickRandom<TipoPagto>())
                   .RuleFor(x => x.Valor, faker => faker.Random.Decimal(min: 1, max: 1000));
        }
    }
}
