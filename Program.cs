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
        const string RequestFilePath = "Requests.txt"; // saving requests in file
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
            LoadAccounts(); // load accounts from file
            LoadRequests(); // load requests from file
            LoadReviews(); // load requests from file
            WelcomeMessage(); // welcome message
            systemStart(); // start the system
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


        // System Start
        static void systemStart()
        {
            bool ExitSystem = true;

            while (ExitSystem != false)
            {

                // Main Menu choose betwen admin and user
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
                        SaveAccounts();// save accounts in file before exit
                        SaveRequests();// save requests in file before exit
                        SaveReviews();// save reviews in file before exit
                        Console.WriteLine("Thank you for using the Mini Bank System. Goodbye!");
                        ExitSystem = false; // exit the system by return false

                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again."); // if the user input invalid option
                        break;
                }


            }
        }

        // Admin Menu
        static void Admin()
        {
            bool AdminMenu = false;
            Console.Clear();
            while (AdminMenu != true) // loop until the user exit
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
                        ViweRequest(); // view requests
                        break;
                    case "2":
                        ViewAccounts(); // view accounts
                        break;
                    case "3":
                        ViewReviews(); // view reviews
                        break;
                    case "4":
                        ProcessRequest(); // process requests
                        break;
                    case "5":
                        AdminMenu = true; // exit the admin menu (finish the loop by return true)
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Invalid option, please try again."); // if the user input invalid option
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
                        CreateAccount(); // create account
                        break;
                    case "2":
                        Deposit(); // deposit money
                        break;
                    case"3":
                        Withdraw(); // withdraw money
                        break;

                    case "4":
                        checkBalance(); // check balance
                        break;
                    case "5":
                        SubmitReview();     // submit review
                        break;
                    case "6":
                        UserMenu = true; // exit the user menu (finish the loop by return true)
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again."); // if the user input invalid option
                        break;
                }
            }
        }


        // =========== Admin Functions ========================
        //====================================================

        // View Requests
        static void ViweRequest()
        {

            
                foreach (string request in createAccount) // loop through the requests
            {
                    string[] parts = request.Split('|'); // split the request into name and national ID by "|"
                Console.WriteLine($"Name: {parts[0]}, National ID: {parts[1]}");
                    Console.WriteLine("====================================");
                 }

            Console.ReadLine();
            Console.Clear();


        }
        // View Accounts
        static void ViewAccounts()
        {
            for (int i = 0; i < accountNumbers.Count; i++) // loop to view the accounts lists
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
            foreach (string review in reviewsStack) // loop to view the reviews
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
            if (createAccount.Count == 0) // check if there is reuquests by checking the count of (creatAccount)
            {
                Console.WriteLine("No pending account requests.");
                Console.ReadLine();
                Console.Clear();
                return; // return to stop the function 
            }

            string request = createAccount.Dequeue(); // get the first request in the queue (FIFO)
            string[] parts = request.Split('|'); // check for '|' to split the name from the ID
            string name = parts[0]; // first part is the name
            string nationalID = parts[1]; // second part it the ID

            int newAccountNumber = lastAccountNumber + 1; // to give the account number (the last account number + 1)

            accountNumbers.Add(newAccountNumber); // add account number to the list (accountNumber)
            accountNames.Add(name); // add account name to the list (accountNames)
            balances.Add(0.0); // add the account balance to the list (balances)

            lastAccountNumber = newAccountNumber; 

            Console.WriteLine($"Account created for {name} with Account Number: {newAccountNumber}"); 
            Console.ReadLine();
            Console.Clear();
        }


        // =========== User Functions ========================
        //====================================================


        // Creat Account 
        static void CreateAccount() 
        {

            Console.WriteLine("Create Account");
            Console.WriteLine("============");
            Console.WriteLine("Enter Your Name: ");
            string Name = Console.ReadLine();
            Console.WriteLine("Enter Your National ID : ");
            string ID = Console.ReadLine();

            string Request = Name + "|" + ID; // save the name and the ID together and split between them using '|'

            createAccount.Enqueue(Request); // add the request in queue (createAccount)
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

            int accountIndex = CheckAccount(); // check the account exist 
            if (accountIndex != -1)
            {
                Console.Write("Enter deposit amount: ");
                double amount = Convert.ToDouble(Console.ReadLine()); // input the amount wanted to deposit

                if (amount <= 0) // check the amount is positve 
                {
                    Console.WriteLine("invaild amount");
                    return;
                }
              
                balances[accountIndex] += amount; //add the amount to the current balance and save it
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
            int accountIndex = CheckAccount(); // check the account exist and return the index of the account
            if (accountIndex != -1) 
            {
                Console.Write("Enter withdraw amount: ");
                double amount = Convert.ToDouble(Console.ReadLine()); // eneter the amount to withdraw
                if (amount <= 0 || amount > balances[accountIndex] || balances[accountIndex]-amount < MinimumBalance)
                {
                    Console.WriteLine("Invalid amount.");
                    return;
                }
                balances[accountIndex] -= amount; // - the amount from current balance
                Console.WriteLine($"Withdrawal successful. New balance: {balances[accountIndex]}");
                Console.ReadLine();
                Console.Clear();
            }
        }



        static void checkBalance()
        {
            int index = CheckAccount(); // check the account exist and return the index
            if (index == -1) return;

            Console.WriteLine($"Account Number: {accountNumbers[index]}"); // display account number
            Console.WriteLine($"Name: {accountNames[index]}");// display account name
            Console.WriteLine($"Current Balance: {balances[index]}");// display account balance
            Console.ReadLine();
            Console.Clear();

        }
        static void SubmitReview()
        {
            Console.WriteLine("please write your Review :");
            string reivew = Console.ReadLine();
            if (reivew == "") //if the input was empty exit return
            {
                return;
            }
            else { 
                reviewsStack.Push(reivew); // add the review to a stack
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



        //========== Save and Load Accounts & requests ==============

        static void SaveAccounts()
        {
            using (StreamWriter writer = new StreamWriter(AccountsFilePath))
            {
                for (int i = 0; i < accountNumbers.Count; i++)
                {
                    writer.WriteLine($"{accountNumbers[i]}|{accountNames[i]}|{balances[i]}");
                }
            }
            Console.WriteLine("Accounts saved successfully.");
        }


        static void LoadAccounts()
        {
            if (File.Exists(AccountsFilePath))
            {
                using (StreamReader reader = new StreamReader(AccountsFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        accountNames.Add(parts[1]);

                        accountNumbers.Add(int.Parse(parts[0]));

                        balances.Add(double.Parse(parts[2]));
                    }
                }
            }
        }


        static void SaveRequests()
        {
            using (StreamWriter writer = new StreamWriter(RequestFilePath))
            {
                foreach (string request in createAccount)
                {
                    writer.WriteLine(request);
                }
            }
            Console.WriteLine("Requests saved successfully.");
        }

        static void LoadRequests()
        {
            if (File.Exists(RequestFilePath))
            {
                using (StreamReader reader = new StreamReader(RequestFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        createAccount.Enqueue(line);
                    }
                }
            }
        }

        //========== Save and Load Reviews ==============
        static void SaveReviews()
        {
            using (StreamWriter writer = new StreamWriter(ReviewsFilePath))
            {
                foreach (string review in reviewsStack)
                {
                    writer.WriteLine(review);
                }
            }
            Console.WriteLine("Reviews saved successfully.");
        }
        static void LoadReviews()
        {
            if (File.Exists(ReviewsFilePath))
            {
                using (StreamReader reader = new StreamReader(ReviewsFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        reviewsStack.Push(line);
                    }
                }
            }
            
        }



    }
}
