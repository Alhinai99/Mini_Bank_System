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
            Console.WriteLine("      ▄▀      ▀▄      ");
            Console.WriteLine("     █  BANK   █     ");
            Console.WriteLine("     █▄▄▄▄▄▄▄▄▄█     ");
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
            Console.WriteLine("View Requests");

        }
        static void ViewAccounts()
        {
            Console.WriteLine("View Accounts");

        }
        static void ViewReviews()
        {
            Console.WriteLine("View Reviews");
        }
        static void ProcessRequest()
        {
            Console.WriteLine("Process Requests");
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
                Console.WriteLine("Deposit successful in account : " + accountNames[accountIndex]+ "with amount :" + amount + "new balance :" + balances[accountIndex]);
            }
        }


        
        static void checkBalance()
        {
            int index = CheckAccount();
            if (index == -1) return;

            Console.WriteLine($"Account Number: {accountNumbers[index]}");
            Console.WriteLine($"Holder Name: {accountNames[index]}");
            Console.WriteLine($"Current Balance: {balances[index]}");

        }
        static void SubmitReview()
        {
            Console.WriteLine("Submit Review");
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
