﻿using System;
using Question1;



    class Program
    {
        #region Description
        static SaleTransaction? LastTransaction;
        static bool HasLastTransaction = false;
        #endregion

        #region Main Method
        static void Main()
        {
            int choice;

            // Main menu loop
            do
            {
                
                Console.WriteLine("================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input.");
                    Console.ReadLine();
                    continue;
                }

                switch (choice) // Handle user choice
                {
                    case 1:
                        CreateTransaction();
                        break;
                    case 2:
                        ViewLastTransaction();
                        break;
                    case 3:
                        CalculateProfitOrLoss();
                        break;
                    case 4:
                        Console.WriteLine("Thank you. Application closed normally.");
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

            } while (choice != 4); 
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new sale transaction by taking user input.
        /// </summary>
        static void CreateTransaction()
        {
            Console.Write("Enter Invoice No: ");
            string invoiceNo = Console.ReadLine() ?? "";

            Console.Write("Enter Customer Name: ");
            string customerName = Console.ReadLine() ?? "";

            Console.Write("Enter Item Name: ");
            string itemName = Console.ReadLine() ?? "";

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter Purchase Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal purchaseAmount) || purchaseAmount <= 0)
            {
                Console.WriteLine("Invalid purchase amount.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter Selling Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal sellingAmount))
            {
                Console.WriteLine("Invalid selling amount.");
                Console.ReadLine();
                return;
            }

            // Create and store the transaction
            LastTransaction = new SaleTransaction
            {
                InvoiceNo = invoiceNo,
                CustomerName = customerName,
                ItemName = itemName,
                Quantity = quantity,
                PurchaseAmount = purchaseAmount,
                SellingAmount = sellingAmount
            };

            LastTransaction.CalculateProfitOrLoss();
            HasLastTransaction = true;

            Console.WriteLine("Transaction saved successfully.");
          
        }

        /// <summary>
        /// Displays the last transaction details.
        /// </summary>
        static void ViewLastTransaction()
        {
            if (!HasLastTransaction)
                Console.WriteLine("No transaction available.");
            else
                LastTransaction!.PrintTransaction();

           
        }

        /// <summary>
        /// Calculates and displays the profit or loss of the last transaction.
        /// </summary>
        static void CalculateProfitOrLoss()
        {
            if (!HasLastTransaction)
                Console.WriteLine("No transaction available.");
            else
            {
                LastTransaction!.CalculateProfitOrLoss();
                LastTransaction.PrintTransaction();
            }

            
        }
        #endregion
    }
