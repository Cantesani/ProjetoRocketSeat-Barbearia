using Barbearia.Communication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Communication.Response
{
    public class ResponseFaturamentoJson
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public TipoPagto tipoPagto { get; set; }
    }
}
