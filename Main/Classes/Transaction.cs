using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKassaSystem.Main.Classes
{
    internal class Transaction
    {
        public string Operation { get; set; }
        public string Date { get; set; }

        public Transaction(string operation, string Date)
        {
            Operation = operation;
            this.Date = Date;
        }
    }
}
