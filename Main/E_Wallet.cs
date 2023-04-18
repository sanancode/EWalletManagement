using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKassaSystem.Main
{
    internal class E_Wallet
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string OwnerName { get; set; }
        public List<AccountNo> AccountNo { get; set; }

        public E_Wallet(string ownername, string login, string password)
        {
            Login = login;
            Password = password;
            OwnerName = ownername;
            AccountNo = new List<AccountNo>(); //initialize
        }
    }
}
