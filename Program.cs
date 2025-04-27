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
            try
            {
                LoadAccounts(); // load accounts from file
                LoadRequests(); // load requests from file
                LoadReviews(); // load requests from file
                WelcomeMessage(); // welcome message
                systemStart(); // start the system
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A critical error occurred: {ex.Message}");
                Console.WriteLine("The application will now exit.");
                Environment.Exit(1);
            }
        }
        // =========== Main Menu ========================
        //===========================================

        // Welcome Message
        static void WelcomeMessage()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying welcome message: {ex.Message}");
            }
        }


        // System Start
        static void systemStart()
        {
            bool ExitSystem = true;

            while (ExitSystem != false)
            {
                try
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
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Please try again.");
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
                try
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
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred in admin menu: {ex.Message}");
                    Console.WriteLine("Please try again.");
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
                try
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
                        case "3":
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
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred in user menu: {ex.Message}");
                    Console.WriteLine("Please try again.");
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
            try
            {
                if (accountNumbers.Count == 0)
                {
                    Console.WriteLine("No accounts available.");
                }
                else
                {
                    for (int i = 0; i < accountNumbers.Count; i++) // loop to view the accounts lists
                    {
                        Console.WriteLine("account number :" + accountNumbers[i]);
                        Console.WriteLine(" Name : " + accountNames[i]);
                        Console.WriteLine("Balance :" + balances[i]);
                        Console.WriteLine("====================================");
                    }
                }
                Console.ReadLine();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error viewing accounts: {ex.Message}");
            }
        }

        
        // View Reviews
        static void ViewReviews()
        {
            try
            {
                if (accountNumbers.Count == 0) // check if there is no accounts
                {
                    Console.WriteLine("No accounts available.");
                }
                else
                {
                    for (int i = 0; i < accountNumbers.Count; i++) // loop to view the accounts lists
                    {
                        Console.WriteLine("account number :" + accountNumbers[i]);
                        Console.WriteLine(" Name : " + accountNames[i]);
                        Console.WriteLine("Balance :" + balances[i]);
                        Console.WriteLine("====================================");
                    }
                }
                Console.ReadLine();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error viewing accounts: {ex.Message}");
            }
        }
        // Process Requests
        static void ProcessRequest()
        {
            try
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
                if (parts.Length < 2)
                {
                    Console.WriteLine("Invalid request format.");
                    return;
                }

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
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing request: {ex.Message}");
            }
        }


        // =========== User Functions ========================
        //====================================================


        // Creat Account 
        static void CreateAccount() 
        {

            try
            {
                Console.WriteLine("Create Account");
                Console.WriteLine("============");
                Console.WriteLine("Enter Your Name: ");
                string Name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(Name)) // check if the name is empty
                {
                    Console.WriteLine("Name cannot be empty.");
                    return;
                }

                Console.WriteLine("Enter Your National ID : ");
                string ID = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ID)) // check if the ID is empty
                {
                    Console.WriteLine("National ID cannot be empty.");
                    return;
                }

                string Request = Name + "|" + ID; // save the name and the ID together and split between them using '|'

                createAccount.Enqueue(Request); // add the request in queue (createAccount)
                Console.WriteLine("Account request submitted successfully.");
                Console.WriteLine("Please wait for admin approval.");
                Console.ReadLine();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating account request: {ex.Message}");
            }


        }
        static void Deposit()
        {
            try
            {
                Console.WriteLine("==============");
                Console.WriteLine("Deposit");
                Console.WriteLine("==============");

                int accountIndex = CheckAccount(); // check the account exist 
                if (accountIndex != -1)
                {
                    Console.Write("Enter deposit amount: ");
                    if (!double.TryParse(Console.ReadLine(), out double amount)) // check if the input number
                    {
                        Console.WriteLine("Invalid amount format.");
                        return;
                    }

                    if (amount <= 0) // check the amount is positive 
                    {
                        Console.WriteLine("Invalid amount. Amount must be positive.");
                        return;
                    }

                    balances[accountIndex] += amount; //add the amount to the current balance and save it
                    Console.WriteLine($"Deposit successful in account: {accountNames[accountIndex]} with amount: {amount} new balance: {balances[accountIndex]}");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deposit: {ex.Message}");
            }
        }
        static void Withdraw()
        {
            try
            {
                Console.WriteLine("==============");
                Console.WriteLine("Withdraw");
                Console.WriteLine("==============");
                int accountIndex = CheckAccount(); // check the account exist and return the index of the account
                if (accountIndex != -1)
                {
                    Console.Write("Enter withdraw amount: ");
                    if (!double.TryParse(Console.ReadLine(), out double amount)) // check if the input is anumber
                    {
                        Console.WriteLine("Invalid amount format.");
                        return;
                    }

                    if (amount <= 0) // check the amount is positive
                    {
                        Console.WriteLine("Amount must be positive.");
                        return;
                    }

                    if (amount > balances[accountIndex]) // check if the amount is greater than the current balance
                    {
                        Console.WriteLine("amount to wtihdraw are more then avalible balance.");
                        return;
                    }
 

                    if (balances[accountIndex] - amount < MinimumBalance) // check if the amount will leave the balance below the minimum required
                    {
                        Console.WriteLine($"Withdrawal would leave balance below minimum required ({MinimumBalance}).");
                        return;
                    }

                    balances[accountIndex] -= amount; // - the amount from current balance
                    Console.WriteLine($"Withdrawal successful. New balance: {balances[accountIndex]}");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during withdrawal: {ex.Message}");
            }
        }



        static void checkBalance()
        {
            try
            {
                int index = CheckAccount(); // check the account exist and return the index
                if (index == -1) return;

                Console.WriteLine($"Account Number: {accountNumbers[index]}"); // display account number
                Console.WriteLine($"Name: {accountNames[index]}");// display account name
                Console.WriteLine($"Current Balance: {balances[index]}");// display account balance
                Console.ReadLine();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking balance: {ex.Message}");
            }
        }
        static void SubmitReview()
        {
            try
            {
                Console.WriteLine("Please write your Review:");
                string review = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(review)) //if the input was empty exit return
                {
                    Console.WriteLine("Review cannot be empty.");
                    return;
                }
                else
                {
                    reviewsStack.Push(review); // add the review to a stack
                    Console.WriteLine("Review submitted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error submitting review: {ex.Message}");
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
            try
            {
                using (StreamWriter writer = new StreamWriter(AccountsFilePath))
                {
                    for (int i = 0; i < accountNumbers.Count; i++)
                    {
                        writer.WriteLine($"{accountNumbers[i]}|{accountNames[i]}|{balances[i]}");// Save each account's details in the format: AccountNumber|AccountName|Balance
                    }
                }
                Console.WriteLine("Accounts saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving accounts: {ex.Message}");
            }
        }


        static void LoadAccounts()
        {
            try {
                if (File.Exists(AccountsFilePath)) // check if the file exists
                {
                    using (StreamReader reader = new StreamReader(AccountsFilePath)) // read the file
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null) // check if the line is not null
                        {
                            string[] parts = line.Split('|'); // split the line into parts by "|"
                            accountNumbers.Add(int.Parse(parts[0])); // add the account number to the list
                            accountNames.Add(parts[1]); // add the account name to the list
                            balances.Add(double.Parse(parts[2])); // add the balance to the list
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading accounts: {ex.Message}");
            }
            
        }


        static void SaveRequests()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(RequestFilePath)) // save the requests in the file
                {
                    foreach (string request in createAccount)
                    {
                        writer.WriteLine(request); // add the request to the file
                    }
                }
                Console.WriteLine("Requests saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving requests: {ex.Message}");
            }

        }

        static void LoadRequests() // load the requests from the file
            {
            try
            {
                using (StreamWriter writer = new StreamWriter(RequestFilePath)) // save the requests in the file
                {
                    foreach (string request in createAccount)
                    {
                        writer.WriteLine(request); // add the request to the file
                    }
                }
                Console.WriteLine("Requests saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving requests: {ex.Message}");
            }
        }

        //========== Save and Load Reviews ==============
        static void SaveReviews()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ReviewsFilePath)) // save the reviews in the file
                {
                    foreach (string review in reviewsStack) // loop through the reviews
                    {
                        writer.WriteLine(review);
                    }
                }
                Console.WriteLine("Reviews saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving reviews: {ex.Message}");
            }
        }

        static void LoadReviews()
        {
            try
            {
                if (File.Exists(ReviewsFilePath)) // check if the file exists
                {
                    using (StreamReader reader = new StreamReader(ReviewsFilePath)) // read the file
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                reviewsStack.Push(line); // add the review to the stack
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading reviews: {ex.Message}");
            }
        }
    }
}
