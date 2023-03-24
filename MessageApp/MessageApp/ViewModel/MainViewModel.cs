using ContractLibrary.Models;
using MessageApp.Commands;
using MessageApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MessageApp.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Message> Messages { get; set; }
        public List<Account> Accounts { get; set; }
        public MessageApplicationContext context;
       
        public MainViewModel() 
        {
            context= new MessageApplicationContext();
            Accounts = context.Accounts.ToList();           

        }
        
            
    }
}
