using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKassaSystem.Main.Classes
{
    internal class AccountNo
    {
        public string AccountId { get; set; }
        public float Balance { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string CurrentStatus { get; set; }
        public List<Transaction> Transaction { get; set; }

        public AccountNo(string accountId, float balance, string currency,
            string description, string currentStatus)
        {
            AccountId = accountId;
            Balance = balance;
            Currency = currency;
            Description = description;
            CurrentStatus = currentStatus;
            Transaction = new List<Transaction>();
        }
    }
}
