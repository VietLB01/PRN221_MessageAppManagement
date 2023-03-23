using ContractLibrary.Models;
using SuperSimpleTcp;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ServerChat
{
    
    public partial class MainWindow : Window
    {
        SimpleTcpServer server = null;
        List<AccountManagement> listAccConnected = new List<AccountManagement>();
        Account accConnect;
        string IpAddr;
        string data = "";
        MessageApplicationContext context;
        public MainWindow()
        {
            InitializeComponent();
            string addr = "127.0.0.1:8000";
            context = new MessageApplicationContext();
            server = new SimpleTcpServer(addr);
            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.ClientDisconnected += Events_ClientDisconnected;
            server.Events.DataReceived += Events_DataReceived;
            
        }
        private  void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            data = Encoding.UTF8.GetString(e.Data).ToString();
            if (data.Length >= 13 && data.Substring(0,13) == "accIdConnect#")
            {
                int accId = int.Parse(data.Substring(13));
                Account account = context.Accounts.FirstOrDefault(account=> account.AccountId == accId);

                listAccConnected.Add(new AccountManagement(account, IpAddr));
               
                foreach (var acc in listAccConnected)
                {
                    server.Send( acc.IpAddress, $"newConnect#{accId}");
                }
            }
            else {

                string[] all = data.Split(":");
                if (all[0].Equals("image#"))
                {
                    string filename = all[1];

                } 


            }
        }

        private  void Events_ClientDisconnected(object? sender, ConnectionEventArgs e)
        {
            
        }

        private  void Events_ClientConnected(object? sender, ConnectionEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                if (server.IsListening)
                {
                    IpAddr = e.IpPort.ToString();
                    
                }
            }));
        }

        private void btStartServer_Click(object sender, RoutedEventArgs e)
        {
            server.Start();
            lbNotify.Text = "Server run successfull in port 8000";
        }
    }
}
