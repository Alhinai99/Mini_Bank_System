using Microsoft.Win32;
using System;
using System.Dynamic;

namespace Mini_Bank_System
{
    internal class Program
    {
        // ======Constants====
        const double MinimumBalance = 100.0;
        const string AccountsFilePath = "accounts.txt";
        const string ReviewsFilePath = "reviews.txt";

        //========== Global lists (parallel)==========
        static List<int> accountNumbers = new List<int>();
        static List<string> accountNames = new List<string>();
        static List<double> balances = new List<double>();

        static Queue<string> createAccount = new Queue<string>(); // format: "Name|NationalID"
        static Stack<string> reviewsStack = new Stack<string>();

        // ======Account number generator=====
        static int lastAccountNumber;


        static void Main(string[] args)
        {
            WelcomeMessage();
            systemStart();
        }
        // =========== Main Menu ========================
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

        // =========== Admin Menu ========================
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

        // =========== User Menu ========================
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
                Console.WriteLine("|   [3]     | Check Balance  |");
                Console.WriteLine("|   [4]     | Submit Review  |");
                Console.WriteLine("|   [5]     | Exit           |");
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
                    case "3":
                        checkBalance();
                        break;
                    case "4":
                        SubmitReview();
                        break;
                    case "5":
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
        static int CheckAccount()
        {
            Console.Write("Enter account number: ");
            try
            {
                int accNum = Convert.ToInt32(Console.ReadLine());
                int index = accountNumbers.IndexOf(accNum);

                if (index == -1)
                {
                    Console.WriteLine("Account not found.");
                    return -1;
                }

                return index;
            }
            catch
            {
                Console.WriteLine("Invalid input.");
                return -1;
            }
        }
    }
}
