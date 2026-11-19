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

    // ADD MORE ITEMS GALLANO
    static void CustomerAddMoreItems()
    {
        while (true)
        {
            Console.Clear();

            DisplayFoodMenu();

            Console.WriteLine("\n[1] Add Item");
            Console.WriteLine("[2] Back to Order Review");
            Console.Write("\nEnter choice: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "1")
            {
                AddItemToCart();
            }
            else if (choice == "2")
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                Pause();
            }
        }
    }
    // PROCESS PAYMENT DELENA


    static void ProcessPayment(decimal total)
    {
        decimal cash = 0;
        bool paymentAccepted = false;
        string paymentMethod = "";

       
        Console.Clear();

        Console.WriteLine("==================================");
        Console.WriteLine("         PAYMENT METHOD");
        Console.WriteLine("==================================");
        Console.WriteLine("[1] Cash");
        Console.WriteLine("[2] Online Payment");
        Console.Write("\nChoose payment method: ");

        string paymentChoice = Console.ReadLine()?.Trim() ?? "";

        if (paymentChoice == "1")
        {
            paymentMethod = "Cash";
        }
        else if (paymentChoice == "2")
        {
            Console.WriteLine("\n==================================");
            Console.WriteLine("       ONLINE PAYMENT");
            Console.WriteLine("==================================");
            Console.WriteLine("[1] GCash");
            Console.WriteLine("[2] Maya");
            Console.Write("\nChoose online payment: ");

            string onlineChoice = Console.ReadLine()?.Trim() ?? "";

            if (onlineChoice == "1")
            {
                paymentMethod = "GCash (Simulation)";
            }
            else if (onlineChoice == "2")
            {
                paymentMethod = "Maya (Simulation)";
            }
            else
            {
                Console.WriteLine("\nInvalid online payment method.");
                Pause();
                return;
            }

            Console.WriteLine("\nProcessing " + paymentMethod + "...");
            Pause();

            cash = total;
            paymentAccepted = true;

            Console.WriteLine("\nOnline payment successful! (SIMULATION)");
        }
        else
        {
            Console.WriteLine("\nInvalid payment method.");
            Pause();
            return;
        }
       
        while (!paymentAccepted)
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("            PAYMENT");
            Console.WriteLine("==================================");

            Console.WriteLine("Order Total: PHP " + total.ToString("F2"));

            Console.Write("\nEnter Cash Amount: PHP ");

            string cashInput = Console.ReadLine()?.Trim() ?? "";

            if (!IsValidMoney(cashInput, out cash))
            {
                Console.WriteLine("\nInvalid amount.");
                Console.WriteLine("Enter a nonnegative amount with");
                Console.WriteLine("a maximum of two decimal places.");

                Pause();
                continue;
            }

            if (cash < total)
            {
                Console.WriteLine("\nInsufficient payment!");

                Console.WriteLine("Still Needed: PHP " +
                    (total - cash).ToString("F2"));

                Console.WriteLine("\n[1] Re-enter Full Cash Amount");
                Console.WriteLine("[2] Cancel Order");

                Console.Write("Enter choice: ");

                string choice = Console.ReadLine()?.Trim() ?? "";

                if (choice == "1")
                {
                    continue;
                }
                else if (choice == "2")
                {
                    Array.Clear(cartQuantities, 0, cartQuantities.Length);

                    Console.WriteLine("\nOrder cancelled.");
                    Pause();
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    Pause();
                }

                continue;
            }

            paymentAccepted = true;
        }

        decimal change = cash - total;

        string orderId = GenerateOrderId();

        while (true)
        {
            bool saved = SaveOrder(
                orderId,
                total,
                cash,
                change
            );

            if (saved)
            {
                Console.WriteLine("\nPayment successful!");

                DisplayReceipt(
                    orderId,
                    total,
                    cash,
                    change
                );

             
                Console.WriteLine("Payment Method: " + paymentMethod);

                Array.Clear(cartQuantities, 0, cartQuantities.Length);

                Pause();
                return;
            }

            Console.WriteLine("\nUnable to save the transaction.");

            Console.WriteLine("[1] Retry Saving");
            Console.WriteLine("[2] Return to Main Menu");

            Console.Write("Enter choice: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "1")
            {
                if (OrderExists(orderId))
                {
                    Console.WriteLine("\nOrder already exists in records.");
                    Console.WriteLine("Please check the saved transaction.");
                    Pause();
                    return;
                }

                continue;
            }
            else if (choice == "2")
            {
                Console.WriteLine("\nTransaction was not confirmed.");
                Pause();
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
                Pause();
            }
        }
    }


    // VALIDATE MONEY INPUT DELENA
    static bool IsValidMoney(string input, out decimal amount)
    {
        amount = 0;

        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        input = input.Trim();

        if (input.StartsWith("-"))
        {
            return false;
        }

        int decimalIndex = input.IndexOf('.');

        if (decimalIndex >= 0)
        {
            if (input.IndexOf('.', decimalIndex + 1) >= 0)
            {
                return false;
            }

            int decimalPlaces = input.Length - decimalIndex - 1;

            if (decimalPlaces < 1 || decimalPlaces > 2)
            {
                return false;
            }
        }

        return decimal.TryParse(
            input,
            NumberStyles.AllowDecimalPoint,
            CultureInfo.InvariantCulture,
            out amount
        ) && amount >= 0;
    }
    // GENERATE UNIQUE ORDER ID ESTRADA
    static string GenerateOrderId()
    {
        int highestNumber = 0;

        if (File.Exists(filePath))
        {
            string[] records = File.ReadAllLines(filePath);

            foreach (string record in records)
            {
                string[] fields = record.Split('|');

                if (fields.Length > 0 &&
                    fields[0].StartsWith("ORD",
                    StringComparison.OrdinalIgnoreCase))
                {
                    string numberPart = fields[0].Substring(3);

                    if (int.TryParse(numberPart, out int number))
                    {
                        if (number > highestNumber)
                        {
                            highestNumber = number;
                        }
                    }
                }
            }
        }

        return "ORD" + (highestNumber + 1).ToString("D4");
    }

    // SAVE ORDER TO TEXT FILE ESTRADA
    static bool SaveOrder(
        string orderId,
        decimal total,
        decimal cash,
        decimal change)
    {
        try
        {
            Directory.CreateDirectory(folderPath);

            if (OrderExists(orderId))
            {
                return false;
            }

            StringBuilder record = new StringBuilder();

            record.Append(orderId);
            record.Append("|PAID");
            record.Append("|" + total.ToString("F2", CultureInfo.InvariantCulture));
            record.Append("|" + cash.ToString("F2", CultureInfo.InvariantCulture));
            record.Append("|" + change.ToString("F2", CultureInfo.InvariantCulture));

            int itemCount = 0;

            for (int i = 0; i < menuNames.Length; i++)
            {
                if (cartQuantities[i] > 0)
                {
                    itemCount++;
                }
            }

            record.Append("|" + itemCount);

            for (int i = 0; i < menuNames.Length; i++)
            {
                if (cartQuantities[i] > 0)
                {
                    decimal subtotal =
                        menuPrices[i] * cartQuantities[i];

                    record.Append("|" + menuNames[i]);
                    record.Append("|" + menuPrices[i].ToString("F2", CultureInfo.InvariantCulture));
                    record.Append("|" + cartQuantities[i]);
                    record.Append("|" + subtotal.ToString("F2", CultureInfo.InvariantCulture));
                }
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(record.ToString());
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Storage Error: " + ex.Message);
            return false;
        }
    }

    // CHECK IF ORDER ID EXISTS ESTRADA
    static bool OrderExists(string orderId)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] fields = line.Split('|');

                if (fields.Length > 0 &&
                    fields[0].Equals(
                        orderId,
                        StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }
        catch
        {
            return false;
        }

        return false;
    }
    // DISPLAY RECEIPT VINAS
    static void DisplayReceipt(
        string orderId,
        decimal total,
        decimal cash,
        decimal change)
    {
        Console.Clear();

        Console.WriteLine("==========================================");
        Console.WriteLine("              " + restaurantName);
        Console.WriteLine("==========================================");

        Console.WriteLine("Order ID: " + orderId);
        Console.WriteLine("Status: PAID");

        Console.WriteLine("------------------------------------------");

        Console.WriteLine("{0,-18} {1,6} {2,5} {3,10}",
            "Item", "Price", "Qty", "Subtotal");

        for (int i = 0; i < menuNames.Length; i++)
        {
            if (cartQuantities[i] > 0)
            {
                decimal subtotal =
                    menuPrices[i] * cartQuantities[i];

                Console.WriteLine("{0,-18} {1,6:F2} {2,5} {3,10:F2}",
                    menuNames[i],
                    menuPrices[i],
                    cartQuantities[i],
                    subtotal);
            }
        }

        Console.WriteLine("------------------------------------------");

        Console.WriteLine("Total:  PHP " + total.ToString("F2"));
        Console.WriteLine("Cash:   PHP " + cash.ToString("F2"));
        Console.WriteLine("Change: PHP " + change.ToString("F2"));

        Console.WriteLine("==========================================");
        Console.WriteLine("       THANK YOU FOR YOUR ORDER!");
        Console.WriteLine("==========================================");
    }

    // CASHIER VIEW RAMIREZ
    static void CashierView()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("           CASHIER VIEW");
            Console.WriteLine("==================================");

            Console.WriteLine("[1] Find Order");
            Console.WriteLine("[2] Sales Report");
            Console.WriteLine("[3] Reset Sales");
            Console.WriteLine("[4] Return to Main Menu");

            Console.Write("\nEnter choice: ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            switch (choice)
            {
                case "1":
                    FindOrder();
                    break;

                case "2":
                    ShowSalesReport();
                    break;

                case "3":
                    ResetSales();
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("\nInvalid choice.");
                    Pause();
                    break;
            }
        }
    }

    // FIND SAVED ORDER ACAYLAR
    static void FindOrder()
    {
        Console.Clear();

        Console.WriteLine("==================================");
        Console.WriteLine("            FIND ORDER");
        Console.WriteLine("==================================");

        Console.Write("Enter Order ID: ");

        string orderId = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(orderId))
        {
            Console.WriteLine("\nOrder ID cannot be blank.");
            Pause();
            return;
        }

        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("\nOrder not found.");
                Pause();
                return;
            }

            string[] records = File.ReadAllLines(filePath);

            foreach (string record in records)
            {
                string[] fields = record.Split('|');

                if (fields.Length > 0 &&
                    fields[0].Equals(
                        orderId,
                        StringComparison.OrdinalIgnoreCase))
                {
                    DisplaySavedReceipt(fields);
                    Pause();
                    return;
                }
            }

            Console.WriteLine("\nOrder not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nUnable to read records: " + ex.Message);
        }

        Pause();
    }

    // DISPLAY SAVED RECEIPT ACAYLAR
    static void DisplaySavedReceipt(string[] fields)
    {
        try
        {
            if (fields.Length < 6)
            {
                Console.WriteLine("Invalid saved record.");
                return;
            }

            string orderId = fields[0];
            string status = fields[1];

            decimal total = decimal.Parse(fields[2], CultureInfo.InvariantCulture);
            decimal cash = decimal.Parse(fields[3], CultureInfo.InvariantCulture);
            decimal change = decimal.Parse(fields[4], CultureInfo.InvariantCulture);

            int itemCount = int.Parse(fields[5]);

            if (itemCount < 1 || fields.Length != 6 + itemCount * 4)
            {
                Console.WriteLine("Invalid saved record.");
                return;
            }

            Console.WriteLine("\n==========================================");
            Console.WriteLine("              " + restaurantName);
            Console.WriteLine("==========================================");

            Console.WriteLine("Order ID: " + orderId);
            Console.WriteLine("Status: " + status);

            Console.WriteLine("------------------------------------------");

            Console.WriteLine("{0,-18} {1,6} {2,5} {3,10}",
                "Item", "Price", "Qty", "Subtotal");

            int position = 6;

            for (int i = 0; i < itemCount; i++)
            {
                string name = fields[position++];
                decimal price = decimal.Parse(fields[position++], CultureInfo.InvariantCulture);
                int quantity = int.Parse(fields[position++]);
                decimal subtotal = decimal.Parse(fields[position++], CultureInfo.InvariantCulture);

                Console.WriteLine("{0,-18} {1,6:F2} {2,5} {3,10:F2}",
                    name, price, quantity, subtotal);
            }

            Console.WriteLine("------------------------------------------");

            Console.WriteLine("Total:  PHP " + total.ToString("F2"));
            Console.WriteLine("Cash:   PHP " + cash.ToString("F2"));
            Console.WriteLine("Change: PHP " + change.ToString("F2"));

            Console.WriteLine("==========================================");
        }
        catch
        {
            Console.WriteLine("Unable to display saved receipt.");
        }
    }
    // SALES REPORT ORZALES & BUENSUCESO
    static void ShowSalesReport()
    {
        Console.Clear();

        Console.WriteLine("==================================");
        Console.WriteLine("           SALES REPORT");
        Console.WriteLine("==================================");

        int orderCount = 0;
        decimal totalSales = 0;

        try
        {
            if (File.Exists(filePath))
            {
                string[] records = File.ReadAllLines(filePath);

                foreach (string record in records)
                {
                    string[] fields = record.Split('|');

                    if (fields.Length >= 6 &&
                        fields[1] == "PAID" &&
                        decimal.TryParse(
                            fields[2],
                            NumberStyles.Number,
                            CultureInfo.InvariantCulture,
                            out decimal total))
                    {
                        orderCount++;
                        totalSales += total;
                    }
                }
            }

            Console.WriteLine("\nCompleted Orders: " + orderCount);

            Console.WriteLine("Total Sales: PHP " +
                totalSales.ToString("F2"));

            if (orderCount == 0)
            {
                Console.WriteLine("\nNo sales yet");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nUnable to read sales records: " + ex.Message);
        }

        Console.WriteLine("\n==================================");

        Pause();
    }

    // RESET SALES RAMIREZ
    static void ResetSales()
    {
        Console.Clear();

        Console.WriteLine("==================================");
        Console.WriteLine("           RESET SALES");
        Console.WriteLine("==================================");

        Console.Write("Are you sure? (Y/N): ");
        string choice = Console.ReadLine()?.Trim().ToUpper() ?? "";

        if (choice == "Y")
        {
            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }

            Console.WriteLine("\nSales successfully reset!");
        }
        else
        {
            Console.WriteLine("\nReset cancelled.");
        }

        Pause();
    }

    // PAUSE PROGRAM BAES
    static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}