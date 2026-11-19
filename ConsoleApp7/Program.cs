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
    // CUSTOMER VIEW FATALLA
    static void CustomerView()
    {
        Array.Clear(cartQuantities, 0, cartQuantities.Length);

        while (true)
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("          CUSTOMER VIEW");
            Console.WriteLine("==================================");

            DisplayFoodMenu();

            Console.WriteLine("\n[1] Add Item");
            Console.WriteLine("[2] Review Order");
            Console.WriteLine("[3] Cancel Order");
            Console.Write("\nEnter choice: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "1")
            {
                AddItemToCart();
            }
            else if (choice == "2")
            {
                if (CalculateTotal() == 0)
                {
                    Console.WriteLine("\nYour cart is empty.");
                    Pause();
                }
                else
                {
                    ReviewOrder();
                    return;
                }
            }
            else if (choice == "3")
            {
                Array.Clear(cartQuantities, 0, cartQuantities.Length);

                Console.WriteLine("\nOrder cancelled successfully.");
                Pause();
                return;
            }
            else
            {
                Console.WriteLine("\nInvalid choice.");
                Pause();
            }
        }
    }

    // DISPLAY FOOD MENU FATALLA
    static void DisplayFoodMenu()
    {
        Console.WriteLine("\n-------------- FOOD MENU --------------");
        Console.WriteLine("{0,-10} {1,-20} {2,10}",
            "Item No.", "Food Name", "Price");

        for (int i = 0; i < menuNames.Length; i++)
        {
            Console.WriteLine("{0,-10} {1,-20} PHP {2,7:F2}",
                itemNumbers[i],
                menuNames[i],
                menuPrices[i]);
        }

        Console.WriteLine("---------------------------------------");
    }

    // ADD ITEM TO CART FATALLA
    static void AddItemToCart()
    {
        Console.Write("\nEnter Item Number: ");

        string input = Console.ReadLine()?.Trim() ?? "";

        if (!int.TryParse(input, out int itemNumber))
        {
            Console.WriteLine("Invalid item number.");
            Pause();
            return;
        }

        int index = Array.IndexOf(itemNumbers, itemNumber);

        if (index == -1)
        {
            Console.WriteLine("Item number not found.");
            Pause();
            return;
        }

        Console.Write("Enter Quantity (1-99): ");

        string quantityInput = Console.ReadLine()?.Trim() ?? "";

        if (!int.TryParse(quantityInput, NumberStyles.None,
            CultureInfo.InvariantCulture, out int quantity))
        {
            Console.WriteLine("Invalid quantity. Enter a whole number.");
            Pause();
            return;
        }

        if (quantity < 1 || quantity > 99)
        {
            Console.WriteLine("Quantity must be between 1 and 99.");
            Pause();
            return;
        }

        if (cartQuantities[index] + quantity > 99)
        {
            Console.WriteLine("Combined quantity cannot exceed 99.");
            Pause();
            return;
        }

        cartQuantities[index] += quantity;

        Console.WriteLine("\nItem added successfully!");
        Console.WriteLine("Food: " + menuNames[index]);
        Console.WriteLine("Quantity: " + cartQuantities[index]);

        Pause();
    }

    // CALCULATE ORDER TOTAL FATALLA
    static decimal CalculateTotal()
    {
        decimal total = 0;

        for (int i = 0; i < menuNames.Length; i++)
        {
            decimal subtotal = menuPrices[i] * cartQuantities[i];
            total += subtotal;
        }

        return total;
    }

    // REVIEW ORDER GALLANO
    static void ReviewOrder()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("==========================================");
            Console.WriteLine("               ORDER REVIEW");
            Console.WriteLine("==========================================");

            Console.WriteLine("{0,-18} {1,8} {2,8} {3,12}",
                "Food", "Price", "Qty", "Subtotal");

            Console.WriteLine("------------------------------------------------");

            for (int i = 0; i < menuNames.Length; i++)
            {
                if (cartQuantities[i] > 0)
                {
                    decimal subtotal =
                        menuPrices[i] * cartQuantities[i];

                    Console.WriteLine("{0,-18} {1,8:F2} {2,8} {3,12:F2}",
                        menuNames[i],
                        menuPrices[i],
                        cartQuantities[i],
                        subtotal);
                }
            }

            decimal total = CalculateTotal();

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("ORDER TOTAL: PHP " + total.ToString("F2"));

            Console.WriteLine("\n[1] Add More Items");
            Console.WriteLine("[2] Confirm and Pay");
            Console.WriteLine("[3] Cancel Order");

            Console.Write("\nEnter choice: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    CustomerAddMoreItems();
                    break;

                case "2":
                    if (total > 0)
                    {
                        ProcessPayment(total);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Empty order cannot proceed.");
                        Pause();
                    }
                    break;

                case "3":
                    Array.Clear(cartQuantities, 0, cartQuantities.Length);

                    Console.WriteLine("\nOrder cancelled.");
                    Pause();
                    return;

                default:
                    Console.WriteLine("\nInvalid choice.");
                    Pause();
                    break;
            }
        }
    }
