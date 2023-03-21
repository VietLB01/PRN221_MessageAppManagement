using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Collections.ObjectModel;

namespace ContractLibrary.Models
{
    [ServiceContract(CallbackContract = typeof(IMessageCallBack),SessionMode =SessionMode.Required)]
    public interface IMessageService
    {
        [OperationContract(IsOneWay = true)]
        void Connect(Account account);

        [OperationContract(IsOneWay = true)]
        void SendMessage(Message message);

        [OperationContract(IsOneWay = false)]
        List<Account> GetConnectedAccount();

    }

}
