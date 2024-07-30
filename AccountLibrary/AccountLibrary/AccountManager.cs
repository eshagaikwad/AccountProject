using AccountLibrary.ExceptionHandling;
using System.Security.Principal;

namespace AccountLibrary
{
    public class AccountManager
    {
        public static List<Account> account = new List<Account>();
        static void Print(Account account)
        {
            if (account == null)
            {
                throw new AccountNullException("Account not Found");
            }

            Console.WriteLine("Account Id: " + account._accountId);
            Console.WriteLine("Account Holder name: " + account._accountHolderName);
            Console.WriteLine("Bank Name: " + account._bankAccountName);
            Console.WriteLine("adhaar card: " + account._adhaarNumber);
            Console.WriteLine("Bank Balance: " + account._bankBalance);
            Console.WriteLine("Account Status: " + account.IsActive);
            Console.WriteLine();
        }
        static int GetIntInput(string message)
        {
            Console.WriteLine(message);
            return int.Parse(Console.ReadLine());
        }

        static double GetDoubleInput(string message)
        {
            Console.WriteLine(message);
            return double.Parse(Console.ReadLine());
        }

        static string GetStringInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        static void CreateNewAccount()
        {
            int accountId = GetIntInput("Enter account Id: ");
            string accountHoldername = GetStringInput("Enter Your Name:");
            string bankAccountName = GetStringInput("Enter bank name:");
            string adhaarNumber = GetStringInput("Enter adhaar card number:");
            double bankBalance = GetDoubleInput("Enter amount to be stored in your bank:");

            Account newAccount=new Account(accountId, accountHoldername, bankAccountName, adhaarNumber, bankBalance);
            account.Add(newAccount);
            Console.WriteLine("new Account added successfully");

        }
        static Account GetMaxBalanceAccount()
        {
            if (account.Count == 0)
            {
                Console.WriteLine("No accounts available.");
                return null;
            }
            Account maxBalanceAccount = account[0];
            foreach (Account accounts in account)
            {
                if (accounts._bankBalance > maxBalanceAccount._bankBalance)
                {
                    maxBalanceAccount = accounts;
                }
            }
            return maxBalanceAccount;
        }

        static Account GetMinBalanceAccount()
        {
            if (account.Count == 0)
            {
                Console.WriteLine("No accounts available.");
                return null;
            }
            Account minBalance = account[0];
            foreach (Account accounts in account)
            {
                if (accounts._bankBalance < minBalance._bankBalance)
                {
                    minBalance = accounts;
                }
            }
            return minBalance;
        }

        static Account FindAccountById(int userInput)
        {
            foreach (Account accounts in account)
            {
                if (accounts._accountId == userInput)
                {
                    return accounts;
                }
            }
            throw new AccountNullException("Account not found");
        }

        static void CheckDetails(Account accounts)
        {
            if (account != null && accounts.IsActive == true)
            {
                Print(accounts);
            }
        }

        static void GetDepositedAmount(Account accounts)
        {
            if (accounts != null && accounts.IsActive == true)
            {
                double depositedAmount = GetDoubleInput("enter the amount to deposite");
                accounts.deposite(depositedAmount);
                Console.WriteLine("Deposited Successfully");
            }
        }

        static void GetWithdrawalAmount(Account accounts)
        {
            if (accounts != null && accounts.IsActive == true)
            {
                try
                {
                    Console.WriteLine("enter the amount to Withdraw");
                    double withdrwalAmount = double.Parse(Console.ReadLine());
                    accounts.withdrawl(withdrwalAmount);
                }
                catch (InsufficientBalanceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void RemoveAccount(Account accountToRemoved)
        {
            if (accountToRemoved != null)
            {
                accountToRemoved.IsActive = false;
                Console.WriteLine("account deactivated successfully");
            }
        }

        static void ClearAccount()
        {
            foreach (var acc in account)
            {
                acc.IsActive = false;
            }
        }

        static void UpdateAccount(Account accounts)
        {

            if (account != null)
            {
                string changedName = GetStringInput("Enter your name to be changed");
                accounts._accountHolderName = changedName;
                Console.WriteLine("name changed successfully");
            }
        }

        static void ActivateAccount(Account account)
        {
            try
            {
                if (account != null)
                {
                    if (account.IsActive)
                    {
                        Console.WriteLine("Account is already active.");
                    }
                    else
                    {
                        account.IsActive = true;
                        Console.WriteLine("Account reactivated successfully.");
                    }
                }
            }
            catch (AccountNullException ex) 
            { 
                Console.WriteLine(ex.Message);
            }
          
        }

        static bool AskToContinueWithSameAccount()
        {
            string response = GetStringInput("Do you want to continue with the same account? (yes/no):");
            return response.ToLower() == "yes";
        }

        public static void MenuDrivenForAdmin()
        {
            while (true)
            {
                int userMenuInput = GetIntInput("What do you want to do?\nEnter 1 to check maximum balance\nEnter 2 to check minimum balance\nEnter 3 to add account\nEnter 4 to remove all accounts\nEnter 5 to print all account details\nEnter 6 to exit");
                Console.WriteLine();
                switch (userMenuInput)
                {
                    case 1:
                        Print(GetMaxBalanceAccount());
                        break;

                    case 2:
                        Print(GetMinBalanceAccount());
                        break;

                    case 3:
                        CreateNewAccount();
                        break;

                    case 4:
                        ClearAccount();
                        break;

                    case 5:
                        foreach (Account value in account)
                        {
                            Print(value);
                        }
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }

        public static void MenuDrivenForUser()
        {
            Account currentAccount = null;
            while (true)
            {
                int userMenuInput = GetIntInput("What do you want to do?\nEnter 1 to check details \nEnter 2 to deposit \nEnter 3 to withdraw \nEnter 4 to update an account\nEnter 5 to delete your account\nEnter 6 to reactivate your account\nEnter 7 to exit");
                Console.WriteLine();

                if (userMenuInput == 7)
                {
                    return;
                }

                if (currentAccount == null || !AskToContinueWithSameAccount())
                {
                    int userInput = GetIntInput("Enter account Id:");
                    try
                    {
                        currentAccount = FindAccountById(userInput);
                    }
                    catch (AccountNullException ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }

                    if (!currentAccount.IsActive && userMenuInput != 6)
                    {
                        Console.WriteLine("Account is InActive. Please reactivate your account.");
                        continue;
                    }
                }

                switch (userMenuInput)
                {
                    case 1:
                        CheckDetails(currentAccount);
                        break;

                    case 2:
                        GetDepositedAmount(currentAccount);
                        break;

                    case 3:
                        GetWithdrawalAmount(currentAccount);
                        break;

                    case 4:
                        UpdateAccount(currentAccount);
                        break;

                    case 5:
                        RemoveAccount(currentAccount);
                        currentAccount = null;
                        break;

                    case 6:
                        ActivateAccount(currentAccount);
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }
       

        public static void UserSelection()
        {
            int userInput = GetIntInput("Are you a admin or a user: Enter 0 for admin and 1 for user:");
            if (userInput == 0)
            {
                MenuDrivenForAdmin();
            }
            if (userInput == 1)
            {
                MenuDrivenForUser();
            }
        }

    }
}
