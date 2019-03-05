using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibChat
{
    public class Message
    {
        public enum Type
        {
            All, ClientList, ClientInfo, Private
        }

        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public string Msg { get; set; }
        public Type MsgType { get; set; } = Type.All;
        public override string ToString() => $"[{From}] {Msg} [{Date.ToShortTimeString()}]";
    }
}
