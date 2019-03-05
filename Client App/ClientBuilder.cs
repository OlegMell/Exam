using LibChat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp
{
    public class ClientBuilder
    {
        public TcpClient tcpClient = new TcpClient();
        public event Action<Message> UpMessage;
        const string PROTOCOL = @"<from><to><date<message>";
        public event Action<List<Info>> UpClientList;
        private string nick;

        public ClientBuilder(string userName, ImgInfo imgInfo, string ip, string port)
        {
            try
            {
                nick = userName;
                IPEndPoint endPoint;
                if (IPAddress.TryParse(ip, out IPAddress iPAddress))
                    endPoint = new IPEndPoint(iPAddress, int.Parse(port));
                else
                    return;
                
                tcpClient.Connect(endPoint);

                Info info = new Info
                {
                    UserName = userName,
                    Ip = tcpClient.Client.LocalEndPoint.ToString(),
                    ProfileImg = imgInfo
                };
                var jsonStr = JsonConvert.SerializeObject(info);
                Message m = new Message
                {
                    Msg = jsonStr,
                    MsgType = Message.Type.ClientInfo
                };

                string json = JsonConvert.SerializeObject(m);
                var data = Encoding.UTF8.GetBytes(json);
                tcpClient.GetStream().Write(data, 0, data.Length);

                Reciever();
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                tcpClient.Close();
            }
        }

        private async void Reciever()
        {
            StringBuilder builder = new StringBuilder();
            var stream = tcpClient.GetStream();
            while (true)
            {
                List<byte> data = new List<byte>();
                int count = 0;
                byte[] buff;
                do
                {
                    buff = new byte[4096];
                    count += await stream.ReadAsync(buff, 0, buff.Length);
                    data.AddRange(buff);
                } while (stream.DataAvailable);
                byte[] tmp = data.ToArray();
                Array.Resize(ref tmp, count);
                var m = CreateMessage(Encoding.UTF8.GetString(tmp));

                switch (m.MsgType)
                {
                    case Message.Type.All:
                        if(!m.From.Equals(nick))
                            UpMessage?.Invoke(m);
                        break;
                    case Message.Type.Private:
                        UpMessage?.Invoke(m);
                        break;
                    case Message.Type.ClientList:
                        List<Info> list = JsonConvert.DeserializeObject<List<Info>>(m.Msg);
                        UpClientList?.Invoke(list);
                        break;
                    default:
                        break;
                }
            }
        }

        public async void Send(string msg)
        {
            Message m = new Message
            {
                From = nick,
                Msg = msg,
                MsgType = Message.Type.All
            };

            string json = JsonConvert.SerializeObject(m);
            byte[] data = Encoding.UTF8.GetBytes(json); 
            await tcpClient.GetStream().WriteAsync(data, 0, data.Length);
        }

        public async void SendTo(string msg, Info info)
        {
            Message m = new Message
            {
                From = nick,
                To = info.Ip,
                Msg = msg,
                MsgType = Message.Type.Private,
                Date = DateTime.Now
            };

            string json = JsonConvert.SerializeObject(m);
            byte[] data = Encoding.UTF8.GetBytes(json);
            await tcpClient.GetStream().WriteAsync(data, 0, data.Length);
        }
        
        private Message CreateMessage(string msg)
        {
            return JsonConvert.DeserializeObject<Message>(msg);
        }
    }
}
