using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LibChat
{
    [Serializable]
    public class Info
    {
        public string Ip { get; set; }
        public string UserName { get; set; }
        public ImgInfo ProfileImg { get; set; }
        [NonSerialized]
        private TcpClient tcpClient;
        public TcpClient GetTcpClient() => tcpClient;
        public void SetTcpClient(TcpClient client) => tcpClient = client;

        public static byte[] Serializaer(Info clientInfo)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, clientInfo);

            return stream.ToArray();
        }

        public static Info Deserializer(byte[] data)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Write(data, 0, data.Length);

            return formatter.Deserialize(stream) as Info;
        }
    }
}
