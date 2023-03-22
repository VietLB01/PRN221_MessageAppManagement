using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractLibrary.Models
{
    public class AccountManagement
    {
        public  Account account { get; set; }
        public string IpAddress { get; set; }

        public AccountManagement(Account account, string ipAddress)
        {
            this.account = account;
            IpAddress = ipAddress;
        }
    }
}
