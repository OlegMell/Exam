using ClientApp;
using LibChat;

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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client_App.Windows
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private ImgInfo imgInfo;
        public SignUp()
        {
            InitializeComponent();
        }

        private async void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image files(*.jpg)|*.jpg";
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(openFile.FileName);
                if (fileInfo.Length > 5_000_000)
                {
                    System.Windows.MessageBox.Show("Размер файла не должен превышать 5 мб");
                    return;
                }
                imgInfo = new ImgInfo
                {
                    Name = openFile.SafeFileName
                };
                using (FileStream fs = new FileStream(openFile.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await Task.Factory.StartNew(() =>
                    {
                        imgInfo.Size = File.ReadAllBytes(openFile.FileName);
                    });
                }
                ProfileImg.ImageSource = new BitmapImage(new Uri(openFile.FileName));
            }
        }

        private void FinishSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (Nickname.Text.Equals(string.Empty) || Email.Text.Equals(string.Empty))
            {
                System.Windows.MessageBox.Show("Заполните все поля!", "Внимание!", MessageBoxButton.OK);
                return;
            }
            if (imgInfo is null)
                return;
            MainWindow main = new MainWindow(imgInfo, Nickname.Text, IpAddress.Text, Port.Text);
            main.Show();
            this.Close();
        }
    }
}
