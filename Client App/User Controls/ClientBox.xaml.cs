using LibChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace Client_App.User_Controls
{
    /// <summary>
    /// Interaction logic for ClientBox.xaml
    /// </summary>
    public partial class ClientBox : UserControl
    {
        public string NickName { get; set; }
        public Info ClientInfo { get; set; }
        public ClientBox()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Background = new SolidColorBrush(Colors.Gray);
        }
    }
}
