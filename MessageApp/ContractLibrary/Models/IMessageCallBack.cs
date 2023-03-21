using System.Collections.Generic;
using System.ServiceModel;

namespace ContractLibrary.Models
{
    public interface IMessageCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ForwardToClient(Message message);

        [OperationContract(IsOneWay = true)]

        void  AccountConnected(List<Account> accounts);
    }

}
