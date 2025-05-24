using Barbearia.Application.UseCases;
using Barbearia.Communication.Enums;
using Barbearia.Communication.Request;
using Barbearia.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Tests.Faturamento.Create
{
    public class FaturamentoValidationTests
    {
        [Fact]
        public void Sucess()
        {
            //Arrange
            var validator = new FaturamentoValidation();
            var request = RequestFaturamentoJsonBuilder.Build();

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Error_Title_Empty(string titulo)
        {
            //Arrange
            var validator = new FaturamentoValidation();
            var request = RequestFaturamentoJsonBuilder.Build();
            request.Titulo = titulo;

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.TITULO_OBRIGATORIO));

        }

        [Fact]
        public void Error_Data_Futuro()
        {
            //Arrange
            var validator = new FaturamentoValidation();
            var request = RequestFaturamentoJsonBuilder.Build();
            request.Data = DateTime.UtcNow.AddDays(1);

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.DATA_PRECISA_SER_ANTERIOR_A_ATUAL));

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-7)]
        public void Error_Valor_Invalido(decimal valor)
        {
            //Arrange
            var validator = new FaturamentoValidation();
            var request = RequestFaturamentoJsonBuilder.Build();
            request.Valor = valor;

            //Act
            var result = validator.Validate(request);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(x => x.ErrorMessage.Equals(ResourceErrorMessages.VALOR_PRECISA_SER_MAIOR_QUE_ZERO));

        }


    }
}
