using ContractLibrary.Models;
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
    /// Interaction logic for chatGroupView.xaml
    /// </summary>
    public partial class chatGroupView : Page
    {
        public int groupCurId;
        public int accCurId;
        public MessageApplicationContext db;
        public chatGroupView(int groupCurId, int accCurId)
        {
            InitializeComponent();
            this.groupCurId = groupCurId;
            this.accCurId = accCurId;
            db = new MessageApplicationContext();
        }
        private void creatConversiton(List<MessageGroup> messages)
        {
            foreach (MessageGroup m in messages)
            {

                if (m.AccountIdsend == accCurId)
                {
                    TextBlock text = new TextBlock();
                    text.Text = m.Content;
                    text.Margin = new Thickness(20, 10, 10, 10);
                    TextBlock time = new TextBlock();
                    time.Text = String.Format("{0:f}", m.Time);
                    time.FontSize = 10;
                    time.HorizontalAlignment = HorizontalAlignment.Right;
                    time.Margin = new Thickness(20, 10, 10, 10);
                    Border b = new Border();
                    b.Width = 200;
                    b.Height = 50;
                    b.CornerRadius = new CornerRadius(15);
                    b.BorderBrush = Brushes.Gray;
                    b.Margin = new Thickness(20, 20, 10, 10);
                    b.BorderThickness = new Thickness(2);
                    b.Background = Brushes.White;
                    b.Child = text;
                    b.HorizontalAlignment = HorizontalAlignment.Right;
                    chatlist.Children.Add(b);
                    chatlist.Children.Add(time);



                }
                else
                {
                    Account a = db.Accounts.FirstOrDefault(x => x.AccountId == m.AccountIdsend);
                    TextBlock text = new TextBlock();
                    text.Text = m.Content;
                    text.Margin = new Thickness(20, 10, 10, 10);
                    TextBlock nameSend = new TextBlock();
                    nameSend.Text = a.FullName;
                    nameSend.Margin = new Thickness(20, 5, 5, 3);
                    nameSend.Foreground = Brushes.Red;
                    TextBlock time = new TextBlock();
                    time.Text = String.Format("{0:f}", m.Time);
                    time.FontSize = 10;
                    time.HorizontalAlignment = HorizontalAlignment.Left;
                    time.Margin = new Thickness(20, 10, 10, 10);
                    Border b = new Border();
                    b.Width = 200;
                    b.Height = 50;
                    b.BorderBrush = Brushes.Green;
                    b.Margin = new Thickness(10, 20, 20, 10);
                    b.CornerRadius = new CornerRadius(15);
                    b.BorderThickness = new Thickness(1);
                    b.Background = Brushes.White;
                    b.Child = text;
                    b.HorizontalAlignment = HorizontalAlignment.Left;
                    chatlist.Children.Add(nameSend);
                    chatlist.Children.Add(b);
                    chatlist.Children.Add(time);
                }

            }
        }

        public void loadMessage()
        {
            using (MessageApplicationContext context = new MessageApplicationContext())
            {
                List<MessageGroup> messages = context.MessageGroups.Where(x =>x.GroupIdaccept == groupCurId).ToList();
                creatConversiton(messages);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            loadMessage();
        }
    }
}
