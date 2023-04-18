using EKassaSystem.Config;
using EKassaSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKassaSystem.Main.Management
{
    internal class AccountOperationManagement
    {
        public static void depositMoney(int walletindex, int accountindex)
        {
            #region Shorts
            string ownername = Storage.ewallet[walletindex].OwnerName;
            float balance = Storage.ewallet[walletindex].AccountNo[accountindex].Balance;
            string currency = Storage.ewallet[walletindex].AccountNo[accountindex].Currency;
            string accountid = Storage.ewallet[walletindex].AccountNo[accountindex].AccountId;
            string accstatus = Storage.ewallet[walletindex].AccountNo[accountindex].CurrentStatus;
            #endregion

            if (accstatus == "Active")
            {
                Console.WriteLine($"\nWallet owner: {ownername}");
                Console.WriteLine($"Account balance: {balance}");

                float amount = MenuUtil.getFloat("Please enter the amount: ");
                Storage.ewallet[walletindex].AccountNo[accountindex].Balance += amount;

                Console.WriteLine($"\n{amount} {currency} added to {accountid}");

                Console.WriteLine("\nOperation ended...\n");

                //Transaction
                Common.transaction("Deposit Money", walletindex, accountindex);
            }
            else
            {
                Console.WriteLine("\nAccount is not active...\nPlease activate it firstly...\n");
            }
        }

        public static void withdrawMoney(int walletindex, int accountindex)
        {
            #region Shorts
            string ownername = Storage.ewallet[walletindex].OwnerName;
            float balance = Storage.ewallet[walletindex].AccountNo[accountindex].Balance;
            string accstatus = Storage.ewallet[walletindex].AccountNo[accountindex].CurrentStatus;
            #endregion

            if (accstatus == "Active")
            {

                Console.WriteLine($"\nWallet owner: {ownername}");
                Console.WriteLine($"Account balance: {balance}");

                float amount = MenuUtil.getFloat("Please enter the amount: ");

                if (amount <= balance)
                {
                    Storage.ewallet[walletindex].AccountNo[accountindex].Balance -= amount;
                }
                else
                {
                    Console.WriteLine("\nThere is not enough balance...\nTry again please");
                    withdrawMoney(walletindex, accountindex);
                }

                Console.WriteLine("\nOperation ended...\n");

                //Transaction
                Common.transaction("Withdraw Money", walletindex, accountindex);
            }
            else
            {
                Console.WriteLine("\nAccount is not active...\nPlease activate it firstly...\n");
            }
        }

        public static void showTransactions(int walletindex, int accountindex)
        {
            #region Shorts
            string ownername = Storage.ewallet[walletindex].OwnerName;
            #endregion

            Console.WriteLine($"\nWallet owner: {ownername}");

            for (int i = 0; i < Storage.ewallet[walletindex].AccountNo[accountindex].Transaction.Count; i++)
            {
                string tranname = Storage.ewallet[walletindex].AccountNo[accountindex].Transaction[i].Operation;
                string date = Storage.ewallet[walletindex].AccountNo[accountindex].Transaction[i].Date;

                Console.WriteLine($"\n {i + 1}. transaction");
                Console.WriteLine($" - Operation: {tranname}");
                Console.WriteLine($" - Date: {date}");
            }

            //Transaction
            Common.transaction("Showed transactions", walletindex, accountindex);
        }

        public static void makeActiveDeactiveAccount(int walletindex, int accountindex)
        {
            string status = "";
            string statusflag = "";
            int activeDeactive = MenuUtil.getInteger("Please select the 1 - Active/2 - Deactive: ");

            if (activeDeactive == 1)
            {
                status = Storage.ewallet[walletindex].AccountNo[accountindex].CurrentStatus;

                if (status == "Deactive")
                {
                    Storage.ewallet[walletindex].AccountNo[accountindex].CurrentStatus = "Active";
                    statusflag = "Active";
                    Console.WriteLine("\nAccount activated...\n");
                }
                else
                {
                    Console.WriteLine("\nSelected account is already active...\n");
                }
            }
            else if (activeDeactive == 2)
            {
                status = Storage.ewallet[walletindex].AccountNo[accountindex].CurrentStatus;

                if (status == "Active")
                {
                    Storage.ewallet[walletindex].AccountNo[accountindex].CurrentStatus = "Deactive";
                    statusflag = "Deactive";
                    Console.WriteLine("\nAccount deactivated...\n");
                }
                else
                {
                    Console.WriteLine("\nSelected account is already deactive...\n");
                }
            }
            else
            {
                CheckUtil.wrongMenuSelection();
                makeActiveDeactiveAccount(walletindex, accountindex);
            }

            //Transaction
            Common.transaction($"Account {statusflag}ted", walletindex, accountindex);
        }
    }
}
