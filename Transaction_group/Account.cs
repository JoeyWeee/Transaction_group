using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace group
{
    public abstract class Account
    {
        static private int LAST_NUMBER = 100000;
        protected readonly List<Person> users;
        public readonly List<Transaction> transactions;
        //public virtual EventHandler<double> OnTransaction { get;protected set; }
        public event EventHandler<double> OnTransaction;
        public string Number { get; }
        public double Balance { get; protected set; }
        public double LowestBalance {  get; protected set; }
        public Account(string type,double balance)
        {
            Number= type + "-" +LAST_NUMBER;
            LAST_NUMBER++;
            Balance= balance;
            LowestBalance= balance;
            users = new List<Person>();
            transactions=new List<Transaction>();
        }
        public void Deposit(double amount,Person person)
        {
            Balance += amount;
            LowestBalance = Balance;
            transactions.Add(new Transaction(Number, amount, person));
        }
        public void Withdraw(double amount, Person person)
        {
            amount = -amount;
            Balance += amount;
            LowestBalance = Balance;
            transactions.Add(new Transaction(Number, amount, person));
        }
        public void AddUser(Person person) 
        { 
            users.Add(person);
        }
        public bool IsUser(string name)
        {
            //using linq
            foreach (Person person in users)
            {
                if (person.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public virtual void OnTransactionOccur(object Sender, EventArgs args)
        {
            OnTransaction?.Invoke(Sender, Balance);
        }
        public abstract void PrepareMonthlyReport();
        public override string ToString()
        {
            string info = $"[{Number} ";
            foreach (Person person in users)
            {
                info += $"{person.Name} {(person.IsAuthenticated ? "logged in" : "Not logged in")}, ";
            }
            info += $"{Balance:c2} - transactions ({transactions.Count})]\n";
            foreach (Transaction transaction in transactions)
            {
                info += $"{transaction}\n";
            }
            return info;
        }
    }
}
