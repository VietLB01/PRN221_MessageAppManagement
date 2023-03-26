using ContractLibrary.Models;
using MessageApp.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
        public Group curGroup;
        List<Group> listgroups;
        public MessageApplicationContext context;
        string serverAddress = "127.0.0.1:8000";
        public MainWindow(Account a)
        {
            InitializeComponent();
            context = new MessageApplicationContext();
            curAcc= a;
            lbUser.Content= curAcc.Firstname + " " + curAcc.Lastname;
                listgroups = context.Groups.Where(x => x.Accounts.Contains(curAcc)).ToList();
            lvGroups.ItemsSource = listgroups;
                              
        }
        SimpleTcpClient client;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new SimpleTcpClient(serverAddress);
            client.Events.Connected += Events_Connected;
            client.Events.Disconnected += Events_Disconnected;
            client.Events.DataReceived += Events_DataReceived;
            try
            {
                client.Connect();
                if (client.IsConnected)
                {
                    int accidConnect = curAcc.AccountId;
                    string idConnect = "accIdConnect#" + accidConnect;
                    client.Send(idConnect);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                //txtLog.Text += Encoding.UTF8.GetString(e.Data) + Environment.NewLine ;
                string data = Encoding.UTF8.GetString(e.Data).ToString();
                if (data.Length >= 13 && data.Substring(0, 13) == "accIdConnect#")
                {

                }
                else
                {
                    string[] all = data.Split(":");
                    if (all[0].Equals("newmes#"))
                    {
                        int accSend = int.Parse(all[1].ToString());
                        int accAcept = int.Parse(all[2].ToString());
                        string mes = all[3].ToString();
                        FrameTest.Content = new ChatView(accAcept, accSend);
                        scrollview.ScrollToBottom();

                    }

                }
            }));
        }

        private void Events_Disconnected(object? sender, ConnectionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Events_Connected(object? sender, ConnectionEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
               
            }));
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed) {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
                lbnumGroups.Text = string.Empty;

                FrameTest.Content = new ChatView(curAcc.AccountId, accAccept.AccountId);

            }
        }

        private void btClose_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SendMsgButton_Click(object sender, RoutedEventArgs e)
        {
            if (client.IsConnected)
            {
                if (!string.IsNullOrEmpty(MessageBox.Text))
                {
                    string msg = "newmes#:"+ curAcc.AccountId +":" + accAccept.AccountId + ":" + MessageBox.Text;
                    client.Send(msg);
                    Message mes = new Message();
                    mes.AccountIdSend= curAcc.AccountId;
                    mes.AccountIdAccept = accAccept.AccountId;
                    mes.Content = MessageBox.Text;
                    context.Messages.Add(mes);
                    context.SaveChanges();
                    FrameTest.Content = new ChatView(curAcc.AccountId, accAccept.AccountId);
                    scrollview.ScrollToBottom();
                    MessageBox.Text = string.Empty;

                }

            }
        }

        private void lvGroups_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                curGroup = item as Group;
                Uri uri = new Uri(curGroup.Image, UriKind.Absolute);
                ImageSource imgSource = new BitmapImage(uri);
                ImgContectName.ImageSource = imgSource;

                tbContactName.Text = curGroup.GroupName;

                List<Account > a = context.Accounts.Where( x =>x.Groups.Contains(curGroup) ).ToList();
                             
                lbnumGroups.Text =  a.Count()+ " member.";
                chatGroupView groupView = new chatGroupView(curGroup.GroupId, curAcc.AccountId);
                FrameTest.Content = groupView;
                scrollview.ScrollToBottom();
                MessageBox.Text = string.Empty;
            }
        }

       
    }
    }

