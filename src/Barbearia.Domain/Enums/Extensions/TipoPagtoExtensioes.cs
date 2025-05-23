using Barbearia.Domain.Resources;

namespace Barbearia.Domain.Enums.Extensions
{
    public static class TipoPagtoExtensioes
    {
        public static string TipoPagtoToString(this TipoPagto tipoPagto)
        {
            return tipoPagto switch
            {
                TipoPagto.Dinheiro => ResourcesGerarMessages.DINHEIRO,
                TipoPagto.Pix => ResourcesGerarMessages.PIX,
                TipoPagto.Debito => ResourcesGerarMessages.DEBITO,
                TipoPagto.Credito => ResourcesGerarMessages.CREDITO,
                _ => string.Empty
            };



        }
    }
}
