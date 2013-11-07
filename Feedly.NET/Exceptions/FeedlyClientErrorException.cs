using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedly.NET.Exceptions
{
    public class FeedlyClientErrorException : Exception
    {
        public FeedlyClientErrorException(string message)
            : base(message)
        {
        }
    }
}
