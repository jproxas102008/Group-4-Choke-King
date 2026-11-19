using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

class Program
{
    // RESTAURANT INFORMATION PATRICK
    static string restaurantName = "CHOKE KINGINA MO";

    // MENU ARRAYS PATRICK
    static int[] itemNumbers = { 1, 2, 3, 4, 5, 6 };

    static string[] menuNames =
    {
        "Pork Chao Fan",
        "Beef Chao Fan",
        "Chinese-Style Chicken",
        "Pancit Canton",
        "Pork Siomai (4 pcs)",
        "Halo-Halo"
    };

    static decimal[] menuPrices =
    {
        79.00m,
        89.00m,
        99.00m,
        89.00m,
        55.00m,
        79.00m
    };

    // CART QUANTITIES PATRICK
    static int[] cartQuantities = new int[6];

    // FILE STORAGE PATRICK
    static string folderPath = "records";
    static string filePath = Path.Combine(folderPath, "orders.txt");

    static void Main()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("       ChOKE KINGINA MO");
            Console.WriteLine("==================================");
            Console.WriteLine("[1] Customer View");
            Console.WriteLine("[2] Cashier View");
            Console.WriteLine("[3] Exit");
            Console.WriteLine("==================================");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    CustomerView();
                    break;

                case "2":
                    CashierView();
                    break;

                case "3":
                    Console.WriteLine("\nThank you for using ChOKE KINGINA MO!");
                    return;

                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    Pause();
                    break;
            }
        }
    }