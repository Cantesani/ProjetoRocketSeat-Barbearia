using Barbearia.Communication.Enums;

namespace Barbearia.Communication.Request
{
    public class RequestFaturamentoJson
    {
        public string Titulo { get; set; } =  string.Empty;
        public string Descricao { get; set; } =  string.Empty;
        public DateTime Data { get; set; }
        public decimal Valor { get; set; } 
        public TipoPagto TipoPagto { get; set; }
    }
}
