using LibChat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_App
{
    public class ServerBuilder
    {
        const int PORT = 50_000;
        private List<Info> lstClients = new List<Info>();
        private TcpListener tcpListener;

        public ServerBuilder()
        {
            tcpListener = new TcpListener(IPAddress.Parse("10.2.118.14"), PORT);
            tcpListener.Start();
            Listener();
        }
        private async void Listener()
        {
            tcpListener.Start();
            Console.WriteLine("Wait connection...");
            while (true)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();
                Console.WriteLine($"Connect: {client.Client.RemoteEndPoint}");
                Reciver(client);
            }
        }

        private async void Reciver(TcpClient currClient)
        {
            NetworkStream stream = currClient.GetStream();
            while (true)
            {
                List<byte> data = new List<byte>();
                int countByte = 0;
                byte[] buff;
                do
                {
                    buff = new byte[4096];
                    countByte += await stream.ReadAsync(buff, 0, buff.Length);
                    data.AddRange(buff);
                } while (stream.DataAvailable);
                
                byte[] tmp = data.ToArray();
                Array.Resize(ref tmp, countByte);
                var str = Encoding.UTF8.GetString(tmp);
                var json = JsonConvert.DeserializeObject<Message>(str);

                switch (json.MsgType)
                {
                    case Message.Type.All:
                        Mailing(currClient, buff, countByte);
                        break;
                    case Message.Type.Private:
                        var sendClient = lstClients.FirstOrDefault(c => c.Ip.Equals(json.To));
                        if (sendClient != null)
                        {
                            var s = sendClient.GetTcpClient().GetStream();
                            await s.WriteAsync(buff, 0, buff.Length);
                        }
                        break;
                    case Message.Type.ClientInfo:
                        SingUpClient(json, currClient);
                        Mailing(currClient);
                        break;
                    default:
                        break;
                }
            }            
        }

        private void Mailing(TcpClient currentTcpClient)
        {
            if (lstClients.Count > 0)
            {
                var json = JsonConvert.SerializeObject(lstClients);
                Message m = new Message
                {
                    Msg = json,
                    MsgType = Message.Type.ClientList
                };

                var jsonstr = JsonConvert.SerializeObject(m);
                var buff = Encoding.UTF8.GetBytes(jsonstr);
                lstClients.ForEach(async (iClient) =>
                {
                    await iClient.GetTcpClient().GetStream().WriteAsync(buff, 0, buff.Length);
                });
            }
        }

        private void Mailing(TcpClient currClient, byte[] buff, int byteCounter)
        {
            lstClients.ForEach(async (iClient) =>
            {
                if (iClient.GetTcpClient() != currClient)
                    await iClient.GetTcpClient().GetStream().WriteAsync(buff, 0, buff.Length);
            });
        }

        private void SingUpClient(Message json, TcpClient tcpClient)
        {
            var clientIfno = JsonConvert.DeserializeObject<Info>(json.Msg);
            clientIfno.SetTcpClient(tcpClient);
            lstClients.Add(clientIfno);
        }
    }
}
