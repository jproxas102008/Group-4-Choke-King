using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==============================");
            Console.WriteLine("   CHOKE KING ORDERING KIOSK");
            Console.WriteLine("==============================");

            Console.WriteLine("[1] Customer View");
            Console.WriteLine("[2] Cashier View");
            Console.WriteLine("[3] Exit");

            Console.Write("Enter choice: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                Console.WriteLine("Welcome to Customer View!");
            }
            else if (choice == 2)
            {
                Console.WriteLine("Welcome to Cashier View!");
            }
            else if (choice == 3)
            {
                Console.WriteLine("Thank you!");
            }
            else
            {
                Console.WriteLine("Invalid choice!");
            }

        }
    }
}
