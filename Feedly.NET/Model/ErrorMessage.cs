using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedly.NET.Model
{
    public class ErrorMessage
    {
        public string errorId { get; set; }
        public string errorMessage { get; set; }
        public int errorCode { get; set; }
    }
}
