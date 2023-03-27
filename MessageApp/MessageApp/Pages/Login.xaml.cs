using ContractLibrary.Models;
using MessageApp.View;
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

namespace MessageApp.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        MessageApplicationContext db;
        public Login()
        {
            db = new MessageApplicationContext();
            InitializeComponent();
        }

       
        private void register_Click(object sender, RoutedEventArgs e)
        {
            AuthenView authenView = (AuthenView)Window.GetWindow(this);
            authenView.FrameMain.Content = new Register();   
        }


        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Password;
            if(username != null && password != null)
            {
                Account a = db.Accounts.FirstOrDefault(x => x.Username == username && x.Password ==password);
                if(a != null)
                {
                    a.IsOnline = true;
                    db.SaveChanges();
                    MainWindow an = new MainWindow(a);
                    an.Show();
                    AuthenView authenView = (AuthenView)Window.GetWindow(this);
                    authenView.Hide();
                }
                else
                {
                    MessageBox.Show("Login failed");
                }
            }
            else{
                MessageBox.Show("Username and password must be not empty");
            }
        }

        private void btClose_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
