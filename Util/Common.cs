using EKassaSystem.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKassaSystem.Util
{
    internal class Common
    {
        public static void transaction(string title,int walletindex, int accountindex)
        {
            string operation = title;
            string date = DateTime.Now.ToString("dd.mm.yyy - hh.mm");
            Storage.ewallet[walletindex].AccountNo[accountindex].Transaction.Add(new Main.Classes.Transaction(operation, date));
        }
    }
}
