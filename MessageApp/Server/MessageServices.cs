using ContractLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
namespace Server
{

    public  class MessageServices : IMessageService
    {
        private IMessageCallBack _messageCallBack = null;
        private List<Account> accounts;
        private Dictionary<int, IMessageCallBack> _clients;

       
        public MessageServices() { 
            accounts = new List<Account> {  };
            _clients= new Dictionary<int, IMessageCallBack>();
        }
        void IMessageService.Connect(Account account)
        {
            _messageCallBack = OperationContext.Current.GetCallbackChannel<IMessageCallBack>();
            if(_messageCallBack != null)
            {
               _clients.Add(account.AccountId, _messageCallBack);
                accounts.Add(account);
                _clients?.ToList().ForEach(c => c.Value.AccountConnected(accounts));
            }
        }

        List<Account> IMessageService.GetConnectedAccount()
        {
            return accounts;
        }

        void IMessageService.SendMessage(Message message)
        {
            var sendTo = _clients?.First(c => c.Key == message.AccountIdAccept).Value;
            if(sendTo != null)
            {
                sendTo.ForwardToClient(message);
            }
        }
    }
}
