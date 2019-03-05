using Client_App.User_Controls;
using ClientApp;
using LibChat;
using Server_App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Client_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClientBuilder clientBuilder;
        private string currentUser;
        public MainWindow(ImgInfo imgInfo, string nickname, string ip, string port)
        {
            InitializeComponent();
            DataContext = this;
            currentUser = nickname;
            clientBuilder = new ClientBuilder(nickname, imgInfo, ip, port);
            clientBuilder.UpClientList += ClientBuilder_UpClientList;
            clientBuilder.UpMessage += ClientBuilder_UpMessage;

            var clientBox = new ClientBox();
            clientBox.ClientName.Text = "GROUP CHAT";
            clientBox.ProfileImage.ImageSource = new BitmapImage(new Uri(Path.GetFullPath(@"..\Debug\groupIcon.png")));
            ClientPanel.Children.Add(clientBox);
        }
        
        private void ClientBuilder_UpMessage(Message message) => MessagesPanel.Children.Add(new TextBlock
        {
            Text = message.From + "\n" + message.Msg + "\n" + message.Date
        });

        private void ClientBuilder_UpClientList(List<Info> clients)
        {
            clients.ForEach((client) =>
            {
                if (!client.UserName.Equals(currentUser))
                {
                    var clientBox = new ClientBox();
                    clientBox.NickName = currentUser;
                    clientBox.ClientInfo = client;
                    clientBox.MouseDown += ClientBox_MouseDown;
                    clientBox.ClientName.Text = currentUser;
                    var path = Utilities.ConvertToImage(client.ProfileImg.Name, client.ProfileImg.Size);
                    clientBox.ProfileImage.ImageSource = new BitmapImage(new Uri(Path.GetFullPath(path)));
                    ClientPanel.Children.Add(clientBox);
                }
            });
        }

        private void ClientBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var c = sender as ClientBox;
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (TextMessage.Text.Equals(string.Empty)) return;
            clientBuilder.Send(TextMessage.Text);
            MessagesPanel.Children.Add(new TextBlock { Text = currentUser + "\n" + TextMessage.Text + "\n" + DateTime.Now.ToShortTimeString() });
        }
    }
}
