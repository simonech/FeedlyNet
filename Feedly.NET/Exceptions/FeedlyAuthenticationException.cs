using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedly.NET.Exceptions
{
    public class FeedlyAuthenticationException : Exception
    {
        public FeedlyAuthenticationException(string message) : base(message)
        {
        }
    }
}
