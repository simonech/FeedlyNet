using System.Collections.Generic;

namespace Feedly.NET.Model
{
    public class StreamIdResponse
    {
        public List<string> ids { get; set; }
        public string continuation { get; set; }
    }
}