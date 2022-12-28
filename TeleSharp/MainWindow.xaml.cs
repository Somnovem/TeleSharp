using Microsoft.Win32;
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
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace TeleSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChatRepository chats;
        public MainWindow()
        {
            chats = new ChatRepository();
            InitializeComponent();
            Chat testChat = new Chat();
            testChat.Name = "test";
            testChat.LastMessage = "qt";
            testChat.timeLast = new TimeOnly(15, 16);
            testChat.Source = new BitmapImage(new Uri("../../../Resources/defpp.png", UriKind.Relative));
            testChat.Context = "Online";
            testChat.Info = "Scratch";
            testChat.Username = "Linuh";
            chats.GetChats().Add(testChat);
            ObjectDataProvider provider = new ObjectDataProvider();
            provider.ObjectInstance = chats;
            provider.MethodName = "GetChats";
            Binding binding = new Binding() { Source = provider };
            chatList.SetBinding(ListBox.ItemsSourceProperty, binding);
            chatTop.SetBinding(StackPanel.DataContextProperty, binding);
            chatSideInfo.SetBinding(StackPanel.DataContextProperty, binding);
        }
        private void txtNicknameSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox obj = sender as TextBox;
            if (obj.Text=="Search...")
            {
                obj.Text = "";
            }
        }

        private void txtNicknameSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox obj = sender as TextBox;
            if (obj.Text == "")
            {
                obj.Text = "Search...";
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button obj = sender as Button;
            string currentPath = ((System.Windows.Controls.Image)obj.Content).Source.ToString();
            string path = currentPath.Insert(currentPath.LastIndexOf('.'), "Alter");
            path = path.Substring(path.LastIndexOf("Resources"));
            ((System.Windows.Controls.Image)obj.Content).Source = new BitmapImage(new Uri(path, UriKind.Relative));
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button obj = sender as Button;
            string currentPath = ((System.Windows.Controls.Image)obj.Content).Source.ToString();
            string path = currentPath.Substring(currentPath.LastIndexOf("Resources"));
            path = path.Remove(path.LastIndexOf("Alter"),5);
            ((System.Windows.Controls.Image)obj.Content).Source = new BitmapImage(new Uri(path, UriKind.Relative));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox obj = sender as TextBox;
            if (obj.Text == "Write a message...")
            {
                obj.Foreground = Brushes.White;
                obj.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox obj = sender as TextBox;
            if (obj.Text == "")
            {
                obj.Foreground = Brushes.LightGray;
                obj.Text = "Write a message...";
            }
        }

        private void BtnPinFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Choose file to pin";
            if (fd.ShowDialog() == true)
            {
                if (new FileInfo(fd.FileName).Length <= 1000000000)
                {
                    MessageBox.Show("File will be sent", "Sending...", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("File is too big!", "Caution", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void chatList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chatInterface.Visibility != Visibility.Visible)
            {
                chatInterface.Visibility = Visibility.Visible;
                chatWaiting.Visibility = Visibility.Collapsed;
            }
        }

        private void SidebarActivation_Click(object sender, RoutedEventArgs e)
        {
            if (chatSideInfo.Visibility == Visibility.Collapsed)
            {
                this.Width += 160;
                chatSideInfo.Visibility = Visibility.Visible;
            }
            else
            {
                this.Width -= 160;
                chatSideInfo.Visibility = Visibility.Collapsed;
            }
        }
    }
}
