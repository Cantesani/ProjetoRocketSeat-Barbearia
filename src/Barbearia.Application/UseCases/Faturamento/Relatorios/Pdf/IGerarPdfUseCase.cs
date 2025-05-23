namespace Barbearia.Application.UseCases.Faturamento.Relatorios.Pdf
{
    public interface IGerarPdfUseCase
    {
        public Task<byte[]> Execute(DateOnly mes);
    }
}
