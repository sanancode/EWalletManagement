using EKassaSystem.Config;
using EKassaSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKassaSystem.Main.Management
{
    internal class AccountManagement
    {
        static int walletindex = 0;
        static int accountindex = 0;

        public static void submenuCreateAccount(string walletid)
        {
            //Sub menu : selection 1 - Create Account

            //creating account
            string accountid = MenuUtil.getString("Please enter your account id: ");
            float balance = MenuUtil.getFloat("Please enter your initialize balance: ");
            string currency = MenuUtil.getString("Please enter your account currency: ");
            string description = MenuUtil.getString("Please enter your account description: ");
            string currentstatus = "Active";

            //check same accountid
            if (checkAccountId(accountid, walletid))
            {
                //determine index of wallet
                for (int i = 0; i < Storage.ewallet.Count; i++)
                {
                    if (walletid == Storage.ewallet[i].WalletId)
                    {
                        Storage.ewallet[i].AccountNo.Add(new Classes.AccountNo(accountid, balance, currency, description, currentstatus));
                        Console.WriteLine("\nRegistration completed...\n");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("\nSelected account id is already used...\nPlease try again...\n");
                submenuCreateAccount(walletid);
            }

            #region Inner Methods
            static bool checkAccountId(string accountid, string walletid)
            {
                for (int i = 0; i < Storage.ewallet.Count; i++)
                {
                    if (walletid == Storage.ewallet[i].WalletId) //daxil olunan walleti tapir
                    {

                        //walletin accountlari icinde axtaris edir
                        for (int j = 0; j < Storage.ewallet[i].AccountNo.Count; j++)
                        {
                            if (accountid == Storage.ewallet[i].AccountNo[j].AccountId)
                            {
                                return false;
                            }
                        }

                    }
                }
                return true;
            }
            #endregion
        }

        public static void submenuShowAllAccounts(string waletid)
        {
            //Qeydiyyatdan kecmis butun accountlari gosterir
            bool flag = false;

            for (int i = 0; i < Storage.ewallet.Count; i++)
            {
                if (waletid == Storage.ewallet[i].WalletId)
                {
                    {
                        Console.WriteLine($"\n{Storage.ewallet[i].OwnerName}'s wallet"); //e walletin sahibinin adini gosterir daxil gosterir

                        for (int j = 0; j < Storage.ewallet[i].AccountNo.Count; j++)
                        {
                            Console.WriteLine($"\n  - Account id: {Storage.ewallet[i].AccountNo[j].AccountId}");
                            Console.WriteLine($"  - Balance: {Storage.ewallet[i].AccountNo[j].Balance}");
                            Console.WriteLine($"  - Currency: {Storage.ewallet[i].AccountNo[j].Currency}");
                            Console.WriteLine($"  - Description: {Storage.ewallet[i].AccountNo[j].Description}");
                            Console.WriteLine($"  - CurrentStatus: {Storage.ewallet[i].AccountNo[j].CurrentStatus}");
                            flag = true;
                        }
                        break;
                    }
                }
            }

            if (!flag)
            {
                Console.WriteLine("There is not any registered account...\n");
            }
        }

        public static void submenuShowSelectedAccount(string walletid)
        {
            #region Yoxlamalar
            //wallet indexi teyin et
            bool flag = true; //true olarsa account tapilmadi demekdir

            if (checkAccounts(walletid))
            {
                for (int i = 0; i < Storage.ewallet.Count; i++)
                {
                    if (walletid == Storage.ewallet[i].WalletId)
                    {
                        //hansi accountda emeliyyat aparacaq onu teyin edir
                        string account = MenuUtil.getString("\nPlease enter your account id: ");

                        for (int j = 0; j < Storage.ewallet[walletindex].AccountNo.Count; j++)
                        {
                            if (account == Storage.ewallet[walletindex].AccountNo[j].AccountId)
                            {
                                accountindex = j;
                                flag = false;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("\nThere is not any registered account in the system...\n");
                return;
            }
            //account duzgun secildimi yoxla
            if (flag == true)
            {
                Console.WriteLine("\nEntered account is not found...Try again please...\n");
                submenuShowSelectedAccount(walletid);
            }
            #endregion

            #region Inner Methods
            static bool checkAccounts(string walletid)
            {
                for (int i = 0; i < Storage.ewallet.Count; i++)
                {
                    if (walletid == Storage.ewallet[i].WalletId)
                    {
                        if (Storage.ewallet[i].AccountNo.Count > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            #endregion

            MainProccess();
        }

        public static void MainProccess()
        {
            MenuUtil.operationMenu();
            int menu = MenuUtil.selectMenuAbove();

            Console.WriteLine("\n\tAccount Operations\n");
            if (CheckUtil.Check(menu, 1, 5))
            {
                switch (menu)
                {
                    case 1:
                        AccountOperationManagement.depositMoney(walletindex, accountindex);
                        MainProccess();
                        break;
                    case 2:
                        AccountOperationManagement.withdrawMoney(walletindex, accountindex);
                        MainProccess();
                        break;
                    case 3:
                        AccountOperationManagement.showTransactions(walletindex, accountindex);
                        MainProccess();
                        break;
                    case 4:
                        AccountOperationManagement.makeActiveDeactiveAccount(walletindex, accountindex);
                        MainProccess();
                        break;
                    case 5:
                        break;
                }
            }
        }

    }
}
