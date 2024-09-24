using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace group
{
    class VisaAccount : Account
    {
        private double creditLimit;
        static private double INTEREST_RATE = 0.1995;
        //private bool hasOverdraft;
        public VisaAccount(double balance = 0, double creditLimit = 1200):base(Utils.ACCOUNT_TYPES[AccountType.Visa], balance)
        {
            this.creditLimit = creditLimit;
        }
        public void Pay(double amount, Person person)
        {
            Deposit(amount, person); 
            OnTransactionOccur(amount, new TransactionEventArgs(person.Name, amount, true));
        }
        public void Purchase(double amount, Person person)
        {
            if (!IsUser(person.Name))
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                Console.WriteLine(Number+"not User");
                throw new AccountException(ExceptionType.USER_DOES_NOT_EXIST);
            }else if (!person.IsAuthenticated)
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);
            }
            else if (amount > Balance && amount> creditLimit)
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(ExceptionType.CREDIT_LIMIT_HAS_BEEN_EXCEEDED);
            }
            else
            {
                OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
                Deposit(-amount, person);
            }
            
            
        }
        public override void PrepareMonthlyReport()
        {
            double interest = LowestBalance * INTEREST_RATE / 12;
            Balance -= interest;
            transactions.Clear();
        }
    }
}
