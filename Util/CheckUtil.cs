using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKassaSystem.Util
{
    internal class CheckUtil
    {
        public static bool Check(int value, int min, int max)
        {
            //if it returns true
            //it means everything is ok

            if (min <= value && value <= max)
            {
                return true;
            }
            return false;
        }

        public static void wrongMenuSelection()
        {
            Console.WriteLine("\nWrong menu selection...\nPlease try again...\n");
        }
        public static void wrongLoginOrPassword()
        {
            Console.WriteLine("\nLogin or Password is wrong...\nPlease try again...\n");
        }
    }
}
