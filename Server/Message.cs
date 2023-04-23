using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Message
    {
        public Type Type { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
        public string From { get; set; }
    }

    public enum Type
    {
        Text
    }
}
