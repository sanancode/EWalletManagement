using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKassaSystem.Util
{
    internal class MenuUtil
    {

        #region Menus
        public static void welcomeMenu()
        {
            Console.WriteLine(
                "1. Create a wallet" +
                "\n2. Open existed wallet" +
                "\n3. Exit the system");
        }

        public static void accountMenu()
        {
            Console.WriteLine(
                "\n1. Create an account" +
                "\n2. Show all accounts" +
                "\n3. Show selected account" +
                "\n4. Back to main menu");
        }

        public static void operationMenu()
        {
            //Operation menu from selection 4 of account menu
            Console.WriteLine(
                "\nOperations..." +
                "\n1. Deposit money to account" +
                "\n2. Withdraw money from account" +
                "\n3. Show Transactions" +
                "\n4. Make account Active/Deactive" +
                "\n5. Back to account menu");
        }
        #endregion

        #region Getting Numeric Values
        public static int getInteger(string title)
        {
            Console.Write(title);
            return int.Parse(Console.ReadLine());
        }
        public static float getFloat(string title)
        {
            Console.Write(title);
            return float.Parse(Console.ReadLine());
        }
        public static string getString(string title)
        {
            Console.Write(title);
            return Console.ReadLine();
        }
        #endregion

        public static int selectMenuAbove()
        {
            Console.Write("Please select the menu above: ");
            return int.Parse(Console.ReadLine());
        }

    }
}
