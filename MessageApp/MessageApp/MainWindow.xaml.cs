using ContractLibrary.Models;
using MessageApp.Pages;
using System;
using System.Collections.Generic;
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

namespace MessageApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Account curAcc;
        public Account accAccept;
        public MessageApplicationContext context;
        public MainWindow(Account a)
        {
            InitializeComponent();
            context = new MessageApplicationContext();
            curAcc= a;
            lbUser.Content= curAcc.Firstname + " " + curAcc.Lastname;
           
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.Shutdown();
        }

      
        private void lvAccounts_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                accAccept = item as Account;
                Uri uri = new Uri(accAccept.Avatar, UriKind.Absolute);
                ImageSource imgSource = new BitmapImage(uri);
                ImgContectName.ImageSource = imgSource;

                tbContactName.Text = accAccept.FullName;

                FrameTest.Content = new ChatView(curAcc.AccountId, accAccept.AccountId);

            }
        }

        private void btClose_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
    }

