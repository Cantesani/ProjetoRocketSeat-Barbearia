using Barbearia.Domain.Enums;

namespace Barbearia.Domain.Entities
{
    public class Faturamento
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public TipoPagto TipoPagto { get; set; }
    }
}
