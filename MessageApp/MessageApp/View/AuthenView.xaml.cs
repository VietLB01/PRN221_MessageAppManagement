using MessageApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MessageApp.View
{
    /// <summary>
    /// Interaction logic for AuthenView.xaml
    /// </summary>
    public partial class AuthenView : Window
    {
        private static AuthenView instance;
        private static  readonly object instanceLock = new object();
        public static AuthenView Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new AuthenView();
                    }
                    return instance;
                }
            }
        }
        public AuthenView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FrameMain.Content = new Login();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
