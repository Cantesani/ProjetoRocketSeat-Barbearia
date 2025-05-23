using Barbearia.Application.UseCases;
using Barbearia.Communication.Request;

namespace Validators.Tests.Faturamento.Create
{
    public class FaturamentoValidationTests
    {
        [Fact]
        public void Sucess()
        {
            //Arrange
            var validator = new FaturamentoValidation();
            var request = new RequestFaturamentoJson
            {
                Valor = 100,
                Data = DateTime.Now,
                Descricao = "Descrição teste",
                Titulo = "Celular",
                TipoPagto = Barbearia.Communication.Enums.TipoPagto.Debito
            };



            //Act

            //Assert

        }
    }
}
