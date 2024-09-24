using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group
{
    class SavingAccount : Account, ITransaction
    {
        static private double COST_PER_TRANSACTION = 0.5;
        static private double INTEREST_RATE = 0.015;
        private bool hasOverdraft;
        public SavingAccount(double balance = 0, bool hasOverdraft = false) : base(Utils.ACCOUNT_TYPES[AccountType.Saving], balance)
        {
            this.hasOverdraft= hasOverdraft;
        }
        public new void Deposit(double amount, Person person)
        {
            base.Deposit(amount, person);
            base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
        }
        public new void Withdraw(double amount, Person person)
        {
            if (!IsUser(person.Name))
            {
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(ExceptionType.USER_DOES_NOT_EXIST);
            }

            if (!person.IsAuthenticated)
            {
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(ExceptionType.USER_NOT_LOGGED_IN);
            }

            if (amount > Balance && hasOverdraft)
            {
                base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, false));
                throw new AccountException(ExceptionType.NO_OVERDRAFT);
            }

            base.OnTransactionOccur(this, new TransactionEventArgs(person.Name, amount, true));
            base.Deposit(-amount, person);
        }
        public override void PrepareMonthlyReport()
        {
            double serviceCharge = transactions.Count * COST_PER_TRANSACTION;
            double interest = (LowestBalance * INTEREST_RATE) / 12;

            Balance = Balance + interest - serviceCharge;

            transactions.Clear();
        }

    }
    
}
