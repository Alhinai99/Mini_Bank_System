using Microsoft.Win32;
using System;
using System.Dynamic;

namespace Mini_Bank_System
{
    internal class Program
    {
        // ======Constants====
        const double MinimumBalance = 100.0; // minimum balance (account cant get lower )
        const string AccountsFilePath = "accounts.txt"; //saving accounts in file
        const string ReviewsFilePath = "reviews.txt"; //saving reviews in file

        //========== Global lists (parallel)==========
        static List<int> accountNumbers = new List<int>(); // account numbers list
        static List<string> accountNames = new List<string>(); // account names list
        static List<double> balances = new List<double>(); // account balances list

        static Queue<string> createAccount = new Queue<string>(); // "Name|NationalID"
        static Stack<string> reviewsStack = new Stack<string>(); // reviews stack

        // ======Account number generator=====
        static int lastAccountNumber; //used to give accounts a number 


        static void Main(string[] args)
        {
            WelcomeMessage();
            systemStart();
        }
        // =========== Main Menu ========================
        //===========================================

        // Welcome Message
        static void WelcomeMessage()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("Welcome to the Mini Bank System");
            Console.WriteLine("=========================================");
            Console.WriteLine("        ▄████▄        ");
            Console.WriteLine("      ▄▀       ▀▄      ");
            Console.WriteLine("     █   BANK   █     ");
            Console.WriteLine("     █▄▄▄▄▄▄▄▄ ▄█     ");
            Console.WriteLine("     █   █  █   █    ");
            Console.WriteLine("     █   █  █   █    ");
            Console.WriteLine("     █   █  █   █    ");
            Console.WriteLine("     █▄▄▄█▄▄█▄▄▄█    ");
            Console.WriteLine("    ▄████████████▄   ");
            Console.WriteLine("   ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀   ");

        }
        //system start function
        static void systemStart()
        {
            bool ExitSystem = true;

            while (ExitSystem != false)
            {


                Console.WriteLine("Please select role: ");
                Console.WriteLine("1. Admin ");
                Console.WriteLine("2. User ");
                Console.WriteLine("3. Exit ");
                string role = Console.ReadLine();

                switch (role)
                {
                    case "1":
                        Admin();
                        break;
                    case "2":
                        User();
                        break;
                    case "3":
                        ExitSystem = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }


            }
        }

        // Admin Menu
        static void Admin()
        {
            bool AdminMenu = false;
            Console.Clear();
            while (AdminMenu != true)
            {
                Console.WriteLine("===========================");
                Console.WriteLine("Welcome Admin");


                Console.WriteLine("===============================");
                Console.WriteLine("| Option      | Description    |");
                Console.WriteLine("=============================");
                Console.WriteLine("|   [1]     | View Requests    |");
                Console.WriteLine("|   [2]     | View Accounts    |");
                Console.WriteLine("|   [3]     | View Reviews     |");
                Console.WriteLine("|   [4]     | process Requests |");
                Console.WriteLine("|   [5]     | Exit             |");
                Console.WriteLine("============================");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        ViweRequest();
                        break;
                    case "2":
                        ViewAccounts();
                        break;
                    case "3":
                        ViewReviews();
                        break;
                    case "4":
                        ProcessRequest();
                        break;
                    case "5":
                        AdminMenu = true;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        // User Menu 
        static void User()
        {
            bool UserMenu = false;
            Console.Clear();
            while (UserMenu != true)
            {
                Console.WriteLine("\n=============================");
                Console.WriteLine("| Option    | Description |");
                Console.WriteLine("===========================");
                Console.WriteLine("|   [1]     | Create Account |");
                Console.WriteLine("|   [2]     | Deposit        |");
                Console.WriteLine("|   [3]     | withdraw       |");
                Console.WriteLine("|   [4]     | Check Balance  |");
                Console.WriteLine("|   [5]     | Submit Review  |");
                Console.WriteLine("|   [6]     | Exit           |");
                Console.WriteLine("============================");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        CreateAccount();
                        break;
                    case "2":
                        Deposit();
                        break;
                    case"3":
                        Withdraw();
                        break;

                    case "4":
                        checkBalance();
                        break;
                    case "5":
                        SubmitReview();
                        break;
                    case "6":
                        UserMenu = true;
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }


        // =========== Admin Functions ========================
        //====================================================

        // View Requests
        static void ViweRequest()
        {

            
                foreach (string request in createAccount)
                {
                    string[] parts = request.Split('|');
                    Console.WriteLine($"Name: {parts[0]}, National ID: {parts[1]}");
                    Console.WriteLine("====================================");
                 }

            Console.ReadLine();
            Console.Clear();


        }
        // View Accounts
        static void ViewAccounts()
        {
            for (int i = 0; i < accountNumbers.Count; i++)
            {
                Console.WriteLine("account number :" +accountNumbers[i]);
                Console.WriteLine(" Name : "+ accountNames[i]);
                Console.WriteLine("Balance :"+balances[i]);
                Console.WriteLine("====================================");
                Console.ReadLine();
                Console.Clear();
            }

        }
        // View Reviews
        static void ViewReviews()
        {
            foreach (string review in reviewsStack)
            {
                Console.WriteLine(review);
                Console.WriteLine("====================================");
                Console.ReadLine();
                Console.Clear();
            }
        }
        // Process Requests
        static void ProcessRequest()
        {
            if (createAccount.Count == 0)
            {
                Console.WriteLine("No pending account requests.");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            string request = createAccount.Dequeue();
            string[] parts = request.Split('|');
            string name = parts[0];
            string nationalID = parts[1];

            int newAccountNumber = lastAccountNumber + 1;

            accountNumbers.Add(newAccountNumber);
            accountNames.Add(name);
            balances.Add(0.0);

            lastAccountNumber = newAccountNumber;

            Console.WriteLine($"Account created for {name} with Account Number: {newAccountNumber}");
            Console.ReadLine();
            Console.Clear();
        }


        // =========== User Functions ========================
        //====================================================


        static void CreateAccount()
        {

            Console.WriteLine("Create Account");
            Console.WriteLine("============");
            Console.WriteLine("Enter Your Name: ");
            string Name = Console.ReadLine();
            Console.WriteLine("Enter Your National ID : ");
            string ID = Console.ReadLine();

            string Request = Name + "|" + ID;

            createAccount.Enqueue(Request);
            Console.WriteLine("Account request submitted successfully.");
            Console.WriteLine("Please wait for admin approval.");
            Console.ReadLine();
            Console.Clear();


        }
        static void Deposit()
        {
            Console.WriteLine("==============");
            Console.WriteLine("Deposit");
            Console.WriteLine("==============");

            int accountIndex = CheckAccount();
            if (accountIndex != -1)
            {
                Console.Write("Enter deposit amount: ");
                double amount = Convert.ToDouble(Console.ReadLine());

                if (amount <= 0)
                {
                    Console.WriteLine("invaild amount");
                    return;
                }
              
                balances[accountIndex] += amount;
                Console.WriteLine("Deposit successful in account : " + accountNames[accountIndex]+ " with amount :" + amount + " new balance :" + balances[accountIndex]);
                Console.ReadLine();
                Console.Clear();
            }
        }
        static void Withdraw()
        {
            Console.WriteLine("==============");
            Console.WriteLine("Withdraw");
            Console.WriteLine("==============");
            int accountIndex = CheckAccount();
            if (accountIndex != -1)
            {
                Console.Write("Enter withdraw amount: ");
                double amount = Convert.ToDouble(Console.ReadLine());
                if (amount <= 0 || amount > balances[accountIndex] || balances[accountIndex]-amount < MinimumBalance)
                {
                    Console.WriteLine("Invalid amount.");
                    return;
                }
                balances[accountIndex] -= amount;
                Console.WriteLine($"Withdrawal successful. New balance: {balances[accountIndex]}");
                Console.ReadLine();
                Console.Clear();
            }
        }



        static void checkBalance()
        {
            int index = CheckAccount();
            if (index == -1) return;

            Console.WriteLine($"Account Number: {accountNumbers[index]}");
            Console.WriteLine($"Name: {accountNames[index]}");
            Console.WriteLine($"Current Balance: {balances[index]}");
            Console.ReadLine();
            Console.Clear();

        }
        static void SubmitReview()
        {
            Console.WriteLine("please write your Review :");
            string reivew = Console.ReadLine();
            if (reivew == "")
            {
                return;
            }
            else { 
                reviewsStack.Push(reivew);
                Console.WriteLine("Review submited");
            }
        }





        //========== checkAccount avalibality ======
        //=========================================
        static int CheckAccount()
        {
            Console.Write("Enter account number: ");
            try
            {
                int accNum = Convert.ToInt32(Console.ReadLine()); //input account number
                int index = accountNumbers.IndexOf(accNum); // get index of account number

                if (index == -1) // account not found
                {
                    Console.WriteLine("Account not found.");
                    return -1;
                }

                return index; // account found (return the index number of the account)
            }
            catch
            {
                Console.WriteLine("Invalid input.");
                return -1;
            }
        }
    }
}
