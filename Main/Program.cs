using EKassaSystem.Config;
using EKassaSystem.Main.Management;
using EKassaSystem.Util;

namespace EKassaSystem.Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\tWelcome to Fintech E-Wallet Management StartUp\n");

            run();

            Console.WriteLine("\nProgram ended");
        }

        public static void run()
        {
            Console.WriteLine("\n");
            MenuUtil.welcomeMenu();
            int menu = MenuUtil.selectMenuAbove();

            if (CheckUtil.Check(menu, 1, 3)) //eger menunun lazimi araligindadirsa
            {
                switch (menu)
                {
                    case 1:
                        Console.WriteLine("\n\tE-Wallet Registration\n");
                        WalletManagement.CreateAWallet();
                        run();
                        break;
                    case 2:
                        WalletManagement.OpenExistedWallet();
                        break;
                    case 3:
                        Console.WriteLine("\nExiting the System...\n");
                        break;
                }
            }
            else
            {
                CheckUtil.wrongMenuSelection();
                run();
            }

        }
    }
}