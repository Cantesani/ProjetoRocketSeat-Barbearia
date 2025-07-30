using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Communication.Response
{
    public class ResponseErrorJson
    {
        public List<string> ErrorsMessages { get; set; }

        public ResponseErrorJson(string errorMessage)
        {
            ErrorsMessages = [errorMessage];
        }

        public ResponseErrorJson(List<string> errorMessages)
        {
            ErrorsMessages = errorMessages;
        }
    }
}
