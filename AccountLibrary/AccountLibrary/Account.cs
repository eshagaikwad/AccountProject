using AccountLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountLibrary
{
    public class Account
    {
        public int _accountId { get; set; }
        public string _accountHolderName { get; set; }
        public string _bankAccountName { get; set; }
        public string _adhaarNumber { get; set; }
        public double _bankBalance { get; set; }

        public const double MIN_Balance = 500;
        public bool IsActive { get; set; } = true;
        Account() { }
        public Account(int accountId, string accountHolderName, string bankAccountName)
        {
            _accountId = accountId;
            _accountHolderName = accountHolderName;
            _bankAccountName = bankAccountName;
            _bankBalance = MIN_Balance;
        }
        public Account(int accountId, string accountHolderName, string bankAccountName, double bankBalance) : this(accountId, accountHolderName, bankAccountName)
        {
            if (bankBalance > MIN_Balance)
            {
                _bankBalance = bankBalance;
            }
            else
            {
                _bankBalance = MIN_Balance;

            }

        }
        public Account(int accountId, string accountHolderName, string bankAccountName, string aadhar) : this(accountId, accountHolderName, bankAccountName)
        {

            _adhaarNumber = aadhar;
        }

        public Account(int accountId, string accountHolderName, string bankAccountName, string aadhar, double bankBalance) : this(accountId, accountHolderName, bankAccountName, aadhar)
        {
            if (bankBalance > MIN_Balance)
            {
                _bankBalance = bankBalance;
            }
            else
            {
                _bankBalance = MIN_Balance;

            }

        }
        public double deposite(double depositedAmount)
        {

            _bankBalance += depositedAmount;
            return _bankBalance;

        }
        public double withdrawl(double withdrawlAmount)
        {
            double afterWithdrwal = _bankBalance - withdrawlAmount;
            if (afterWithdrwal < MIN_Balance)
            {
                throw new InsufficientBalanceException("your account balance is not sufficient to withdraw the amount.");
            }

            else
            {
                Console.WriteLine("Withdrwal Sucessfully");
                _bankBalance = afterWithdrwal;
                return _bankBalance;
            }
        }
    }
}
