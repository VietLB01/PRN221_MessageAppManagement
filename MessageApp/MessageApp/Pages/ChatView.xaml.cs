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
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : Page
    {
        public int accCur;
        public int accIdAccept;
        public ChatView(int accIdCur, int accIdAccept)
        {
            InitializeComponent();
            accCur = accIdCur;
            this.accIdAccept = accIdAccept;
        }
        private void creatConversiton(List<Message> messages)
        {
            foreach (Message m in messages)
            {

                if (m.AccountIdSend == accCur)
                {
                    TextBlock text = new TextBlock();
                    text.Text = m.Content;
                    text.Margin = new Thickness(20, 10, 10, 10);
                    TextBlock time = new TextBlock();
                    time.Text = String.Format("{0:f}", m.Time);
                    time.FontSize = 10;
                    time.HorizontalAlignment = HorizontalAlignment.Left;
                    time.Margin = new Thickness(20, 10, 10, 10);
                    Border b = new Border();
                    b.Width = 200;
                    b.Height = 50;
                    b.BorderBrush = Brushes.Green;
                    b.Margin =  new Thickness(10, 20, 20, 10);
                    b.CornerRadius = new CornerRadius(15);
                    b.BorderThickness = new Thickness(1);
                    b.Background = Brushes.White;
                    b.Child = text;
                    b.HorizontalAlignment = HorizontalAlignment.Left;
                    chatlist.Children.Add(b);
                    chatlist.Children.Add(time);
                }
                else
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

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (MessageApplicationContext context = new MessageApplicationContext())
            {
                List<Message> messages = context.Messages.Where(x => (x.AccountIdSend == accCur && x.AccountIdAccept == accIdAccept) || (x.AccountIdSend == accIdAccept && x.AccountIdAccept == accCur)).ToList();
                creatConversiton(messages);
            }
        }
    }
}
