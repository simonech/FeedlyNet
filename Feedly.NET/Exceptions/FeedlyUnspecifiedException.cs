using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedly.NET.Exceptions
{
    public class FeedlyUnspecifiedException : Exception
    {
        public FeedlyUnspecifiedException(string message)
            : base(message)
        {
        }
    }
}
