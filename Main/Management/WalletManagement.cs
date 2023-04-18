using EKassaSystem.Config;
using EKassaSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace EKassaSystem.Main.Management
{
    internal class WalletManagement
    {
        static string login;
        static string password;
        static string walletid;
        public static void CreateAWallet()
        {
            //Main menu : selection 1 - Create a wallet

            string login = MenuUtil.getString("Please enter your Login: ");
            string password = MenuUtil.getString("Please enter your Password: ");
            string fullname = MenuUtil.getString("Please enter your Fullname: ");

            if (checkLogins(login))
            {
                Storage.ewallet.Add(new Classes.E_Wallet(fullname, login, password));

                Console.WriteLine("\n\tE-Wallet Registration Completed\n");
            }
            else
            {
                Console.WriteLine("\nLogin is already used...\nTry again...\n");
                CreateAWallet();
            }

            #region Inner Methods
            static bool checkLogins(string login)
            {
                for(int i = 0; i< Storage.ewallet.Count; i++)
                {
                    if (login == Storage.ewallet[i].Login)
                    {
                        return false;
                    }
                }
                return true;
            }
            #endregion
        }

        public static void OpenExistedWallet()
        {
            //Main menu : selection 2 - Open existed wallet

            if (Storage.ewallet.Count > 0)
            {
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
            }
            else
            {
                Console.WriteLine("\nThere is not any registered wallet yet in the system...\n");
                Program.run();
            }

            #region Inner Methods
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
            #endregion
        }

        public static void accountMenu()
        {
            MenuUtil.accountMenu();
            int menu = MenuUtil.selectMenuAbove();
            if (CheckUtil.Check(menu, 1, 4))
            {
                walletid = getWalletId(login, password);
                switch (menu)
                {
                    case 1:
                        Console.WriteLine("\n\tAccount Registration\n");
                        Console.WriteLine("Please fill all sections below\n");
                        AccountManagement.submenuCreateAccount(walletid);
                        accountMenu();
                        break;
                    case 2:
                        AccountManagement.submenuShowAllAccounts(walletid);
                        accountMenu();
                        break;
                    case 3:
                        AccountManagement.submenuShowSelectedAccount(walletid);
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

        static string getWalletId(string login, string password)
        {
            string walletid = "";
            for (int i = 0; i < Storage.ewallet.Count; i++)
            {
                if (login == Storage.ewallet[i].Login && password == Storage.ewallet[i].Password)
                {
                    walletid = Storage.ewallet[i].WalletId;
                    break;
                }
            }
            return walletid;
        }
    }
}