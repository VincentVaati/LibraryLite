using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLite.UseCases.Interfaces
{
    public abstract class ResponseMessage
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public ResponseMessage(bool success, string message = null)
        {
            Success = success;
            Message = message;
        }
    }
}
