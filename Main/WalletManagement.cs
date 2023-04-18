using EKassaSystem.Config;
using EKassaSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EKassaSystem.Main
{
    internal class WalletManagement
    {
        public static void CreateAWallet()
        {

            #region Main Menus
            //Main menu : selection 1 - Create a wallet
            Console.WriteLine("\n\tE-Wallet Registration\n");

            string login = MenuUtil.getString("Please enter your Login: ");
            string password = MenuUtil.getString("Please enter your Password: ");
            string fullname = MenuUtil.getString("Please enter your Fullname: ");

            Storage.ewallet.Add(new E_Wallet(fullname, login, password));

            Console.WriteLine("\n\tE-Wallet Registration System Completed\n");
        }

        static string login; //for main menu selection 2
        static string password;
        public static void OpenExistedWallet()
        {
            //Main menu : selection 2 - Open existed wallet

            login = MenuUtil.getString("\nLogin: ");
            password = MenuUtil.getString("Password: ");

            if (checkLoginAndPassword(login, password))
            {
                Console.WriteLine("\nYou have entered...\n");
                accountMenu();
            }
            else
            {
                CheckUtil.wrongLoginOrPassword();
                OpenExistedWallet();
            }

            #region InnerMethods
            static bool checkLoginAndPassword(string login, string password)
            {
                //login or password is wrong

                for (int i = 0; i < Storage.ewallet.Count; i++)
                {
                    if (login == Storage.ewallet[i].Login && password == Storage.ewallet[i].Password)
                    {
                        return true;
                    }
                }
                return false;
            }

            static void accountMenu()
            {
                MenuUtil.accountMenu();
                int menu = MenuUtil.selectMenuAbove();
                if (CheckUtil.Check(menu, 1, 4))
                {
                    switch (menu)
                    {
                        case 1:
                            submenuCreateAccount(login, password);
                            accountMenu();
                            break;
                        case 2:
                            submenuShowAllAccounts(login, password);
                            accountMenu();
                            break;
                        case 3:
                            submenuShowSelectedAccount(login, password);
                            accountMenu();
                            break;
                        case 4:
                            Program.run();
                            break;
                    }
                }
                else
                {
                    CheckUtil.wrongMenuSelection();
                    accountMenu();
                }
            }
            #endregion
        }
        #endregion


        #region SubMenus
        static void submenuCreateAccount(string login, string password)
        {
            //Sub menu : selection 1 - Create Account
            Console.WriteLine("\n\tAccount Registration\n");
            Console.WriteLine("Please fill all sections below\n");

            //creating account
            string accountid = MenuUtil.getString("Please enter your account id: ");
            float balance = MenuUtil.getFloat("Please enter your initialize balance: ");
            string currency = MenuUtil.getString("Please enter your account currency: ");
            string description = MenuUtil.getString("Please enter your account description: ");
            string currentstatus = "Active";

            //determine index of wallet
            for (int i = 0; i < Storage.ewallet.Count; i++)
            {
                if (login == Storage.ewallet[i].Login && password == Storage.ewallet[i].Password)
                {
                    Storage.ewallet[i].AccountNo.Add(new AccountNo(accountid, balance, currency, description, currentstatus));
                    Console.WriteLine("\nRegistration completed...\n");
                    break;
                }
            }
        }

        static void submenuShowAllAccounts(string login, string password)
        {
            //Qeydiyyatdan kecmis butun walletleri gosterir

            for (int i = 0; i < Storage.ewallet.Count; i++)
            {
                if (login == Storage.ewallet[i].Login && password == Storage.ewallet[i].Password)
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
                        }
                        break;
                    }
                }
            }

        }

        static void submenuShowSelectedAccount(string login, string password)
        {
            #region Yoxlamalar
            //wallet indexi teyin et
            int walletindex = 0;
            int accountindex = 0;
            bool flag = true; //true olarsa account tapilmadi demekdir
            for (int i = 0; i < Storage.ewallet.Count; i++)
            {
                if (login == Storage.ewallet[i].Login && password == Storage.ewallet[i].Password)
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

            //account duzgun secildimi yoxla
            if (flag == true)
            {
                Console.WriteLine("\nEntered account id not nound...Try again please...\n");
                submenuShowSelectedAccount(login, password);
            }
            #endregion

            //Main proccess
            MenuUtil.operationMenu();
            int menu = MenuUtil.selectMenuAbove();

            if (CheckUtil.Check(menu, 1, 5))
            {
                switch (menu)
                {
                    case 1:
                        depositMoney(walletindex, accountindex);
                        submenuShowSelectedAccount(login, password);
                        break;
                    case 2:
                        //Codes here
                        submenuShowSelectedAccount(login, password);
                        break;
                    case 3:
                        //Codes here
                        submenuShowSelectedAccount(login, password);
                        break;
                    case 4:
                        //Codes here
                        submenuShowSelectedAccount(login, password);
                        break;
                    case 5:
                        MenuUtil.accountMenu();
                        break;
                }
            }

        }
        #endregion

        #region Account Operations (Menus)

        static void depositMoney(int walletindex, int accountindex)
        {
            Console.WriteLine($"Wallet owner: {Storage.ewallet[walletindex].OwnerName}");
            Console.WriteLine($"\nAccount balance: {Storage.ewallet[walletindex].AccountNo[accountindex].Balance}");

            float amount = MenuUtil.getFloat("Please enter the amount: ");
            Storage.ewallet[walletindex].AccountNo[accountindex].Balance += amount;

            Console.WriteLine(
                $"\n{amount} {Storage.ewallet[walletindex].AccountNo[accountindex].Currency} added to " +
                $"{Storage.ewallet[walletindex].AccountNo[accountindex].AccountId}");

            Console.WriteLine("\nOperation ended...\n");

            //Transactions
            Storage.ewallet[walletindex].AccountNo[accountindex].Transaction +=
                $"Operation: Deposit money" +
                $"\nTime: {DateTime.Now.ToString("dd.mm.yyyy")}" +
                $"\n{amount} added to balance\n";
        }

        #endregion
    }
}