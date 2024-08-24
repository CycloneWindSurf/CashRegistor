using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterApp
    {
        class Program
        {
            static decimal balance = 0.00m;
            static List<Transaction> transactions = new List<Transaction>();

            static void Main(string[] args)
            {
                InitializeDrawer();
                int transactionNumber = 1;
                bool continueSales = true;

                while (continueSales)
                {
                    ProcessSale(transactionNumber);
                    transactionNumber++;

                    Console.Write("Do you want to process another sale? (y/n): ");
                    continueSales = Console.ReadLine().ToLower() == "y";
                }

                PrintReport();
            }

            static void InitializeDrawer()
            {
                Console.Write("Enter the starting balance from bank withdrawal: ");
                balance = decimal.Parse(Console.ReadLine());
            }

            static void ProcessSale(int transactionNumber)
            {
                Console.Write($"Enter the sale amount for Transaction {transactionNumber}: ");
                decimal saleAmount = decimal.Parse(Console.ReadLine());

                Console.Write("Enter the cash received from the customer: ");
                decimal cashReceived = decimal.Parse(Console.ReadLine());

                if (cashReceived > 100)
                {
                    Console.WriteLine("Error: Cannot accept bills greater than $100.");
                    return;
                }

                if (cashReceived < saleAmount)
                {
                    Console.WriteLine("Error: Cash received is less than the sale amount.");
                    return;
                }

                decimal changeDue = cashReceived - saleAmount;

                if (changeDue > balance)
                {
                    Console.WriteLine("Error: Not enough change in the drawer.");
                    return;
                }

                balance += saleAmount - changeDue;

                transactions.Add(new Transaction
                {
                    TransactionNumber = transactionNumber,
                    SaleAmount = saleAmount,
                    CashReceived = cashReceived,
                    ChangeGiven = changeDue
                });
            }

            static void PrintReport()
            {
                Console.WriteLine("\nSales Report:");
                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"Transaction {transaction.TransactionNumber}: Sale Amount: {transaction.SaleAmount:C}, Cash Received: {transaction.CashReceived:C}, Change Given: {transaction.ChangeGiven:C}");
                }
                Console.WriteLine($"\nFinal balance in the drawer: {balance:C}");
            }
        }

        class Transaction
        {
            public int TransactionNumber { get; set; }
            public decimal SaleAmount { get; set; }
            public decimal CashReceived { get; set; }
            public decimal ChangeGiven { get; set; }
        }
    }


