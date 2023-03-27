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
        private readonly MessageApplicationContext _context;
        public bool IsLoggedIn = false;
        public Login(MessageApplicationContext context)
        {
            _context = context;
            InitializeComponent();
        }

       
        private void register_Click(object sender, RoutedEventArgs e)
        {
            AuthenView authenView = (AuthenView)Window.GetWindow(this);
            authenView.FrameMain.Content = new Register();   
        }


        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!AllowLogin())
            {
                return;
            }
            var accCount = _context.Accounts.Where(x => x.Username == txtUserName.Text.Trim() && x.Password == txtPassword.Text.Trim()).Count();
            var takeName = _context.Accounts.FirstOrDefault(x => x.Username == txtUserName.Text.Trim() && x.Password == txtPassword.Text.Trim());

            if (accCount > 0)
            {
                IsLoggedIn = true;
                if(takeName != null)
                {
                    var takeUserName = takeName.Lastname;
                    if(takeUserName != null)
                    {
                        MainWindow mainWindow = new MainWindow(takeUserName);
                        mainWindow.Show();
                    }
                    
                }
                
            }
            else
            {
                IsLoggedIn = false;
                MessageBox.Show("Username or Password invalid!");
            }

        }

    

        private bool AllowLogin()
        {
            if(txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("You must fill your username", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUserName.Focus();
                return false;
            }
            if(txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("You must fill your password", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPassword.Focus();
                return false;
            }
            return true;
        }
    }
}
