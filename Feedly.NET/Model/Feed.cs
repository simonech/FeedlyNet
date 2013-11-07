using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feedly.NET.Model
{
    public class Feed
    {
        public Feed()
        {
            topics=new List<string>();
        }

        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public double velocity { get; set; }
        public bool featured { get; set; }
        public bool sponsored { get; set; }
        public bool curated { get; set; }
        public int subscribers { get; set; }
        public string state { get; set; }
        public string language { get; set; }

        public List<string> topics { get; set; }
    }
}
