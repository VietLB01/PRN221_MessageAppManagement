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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {
        private MessageApplicationContext _context; 
        public Register()
        {
            InitializeComponent();
            _context= new MessageApplicationContext();
        }

        public Account GetAccountObject()
        {
            try
            {
                return new Account
                {
                    Firstname = txtFirst.Text,
                    Lastname = txtLast.Text,
                    Username = txtUser.Text,
                    Password = txtPass.Password,
                };

            }
            catch (Exception e)
            {
                MessageBox.Show("can not get account");
            }
            return null;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            AuthenView authenView = (AuthenView)Window.GetWindow(this);
            authenView.FrameMain.Content = new Login();
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private bool AllowRegister()
        {
            if (txtFirst.Text.Trim() == "")
            {
                MessageBox.Show("You must fill your first name", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtFirst.Focus();
                return false;
            }
            if (txtLast.Text.Trim() == "")
            {
                MessageBox.Show("You must fill your last name", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtLast.Focus();
                return false;
            }
            if (txtUser.Text.Trim() == "")
            {
                MessageBox.Show("You must fill your user name", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUser.Focus();
                return false;
            }
            if (txtPass.Password.Trim() == "")
            {
                MessageBox.Show("You must fill your password ", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPass.Focus();
                return false;
            }
            if (txtConfirm.Password.Trim() == "")
            {
                MessageBox.Show("You must fill your confirm password ", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtConfirm.Focus();
                return false;
            }
            if (!txtPass.Password.Trim().Equals(txtConfirm.Password.Trim()))
            {
                MessageBox.Show("password and confirm password must be same ", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPass.Focus();

                return false;
            }
            return true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var takeName = _context.Accounts.FirstOrDefault(x => x.Username == txtUser.Text.Trim());
            if (!AllowRegister())
            {
                return;
            }
            if (takeName != null)
            {
                MessageBox.Show("user name exist", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUser.Focus();
                return;
            }
            try
            {
                var account = GetAccountObject();
                if (account != null)
                {
                    account.AccountId = 0;
                    account.Avatar = null;
                    account.Dob = DateTime.Now;
                    _context.Accounts.Add(account);
                    _context.SaveChanges();
                    MessageBox.Show("Insert account success", "Add account");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert account failed", "Add account");
            }


        }

    }
}
