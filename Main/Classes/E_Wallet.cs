using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKassaSystem.Main.Classes
{
    internal class E_Wallet
    {
        public string WalletId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string OwnerName { get; set; }
        public List<AccountNo> AccountNo { get; set; }

        public E_Wallet(string ownername, string login,
            string password)
        {
            Login = login;
            Password = password;
            OwnerName = ownername;
            WalletId = makeWalletID();
            AccountNo = new List<AccountNo>(); //initialize
        }

        string makeWalletID()
        {
            return $"{Login}:{DateTime.Now.ToString("ddmmyyhhmmss")}";
        }
    }
}
