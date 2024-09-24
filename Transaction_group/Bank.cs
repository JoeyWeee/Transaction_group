using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace group
{
	static class Bank {

		public static Dictionary<string, Account> ACCOUNTS { get; } = new Dictionary<string, Account>();
        public static Dictionary<string, Person> USERS { get; } = new Dictionary<string, Person>();
        static Bank()
        {
            //initialize the USERS collection
            AddPerson("Narendra", "1234-5678"); //0
            AddPerson("Ilia", "2345-6789"); //1
            AddPerson("Mehrdad", "3456-7890"); //2
            AddPerson("Vijay", "4567-8901"); //3
            AddPerson("Arben", "5678-9012"); //4
            AddPerson("Patrick", "6789-0123"); //5
            AddPerson("Yin", "7890-1234"); //6
            AddPerson("Hao", "8901-2345"); //7
            AddPerson("Jake", "9012-3456"); //8
            AddPerson("Mayy", "1224-5678"); //9
            AddPerson("Nicoletta", "2344-6789"); //10
            
            //initialize the ACCOUNTS collection

            AddAccount(new VisaAccount()); //VS-100000
            AddAccount(new VisaAccount(150, -500)); //VS-100001
            AddAccount(new SavingAccount(5000)); //SV-100002
            AddAccount(new SavingAccount()); //SV-100003
            AddAccount(new CheckingAccount(2000)); //CK-100004
            AddAccount(new CheckingAccount(1500, true));//CK-100005
            AddAccount(new VisaAccount(50, -550)); //VS-100006
            AddAccount(new SavingAccount(1000)); //SV-100007
                                                 //associate users with accounts
            
            string number = "VS-100000";
            AddUserToAccount(number, "Narendra");
            AddUserToAccount(number, "Ilia");
            AddUserToAccount(number, "Mehrdad");
            number = "VS-100001";
            AddUserToAccount(number, "Vijay");
            AddUserToAccount(number, "Arben");
            AddUserToAccount(number, "Patrick");
            number = "SV-100002";
            AddUserToAccount(number, "Yin");
            AddUserToAccount(number, "Hao");
            AddUserToAccount(number, "Jake");
            number = "SV-100003";
            AddUserToAccount(number, "Mayy");
            AddUserToAccount(number, "Nicoletta");
            number = "CK-100004";
            AddUserToAccount(number, "Mehrdad");
            AddUserToAccount(number, "Arben");
            AddUserToAccount(number, "Yin");
            number = "CK-100005";
            AddUserToAccount(number, "Jake");
            AddUserToAccount(number, "Nicoletta");
            number = "VS-100006";
            AddUserToAccount(number, "Ilia");
            AddUserToAccount(number, "Vijay");
            number = "SV-100007";
            AddUserToAccount(number, "Patrick");
            AddUserToAccount(number, "Hao");
            
        }

        public static void PrintAccounts()
        {
            foreach(Account acc in ACCOUNTS.Values)
            {
                Console.WriteLine($"{acc.Number},{acc}");
            }
        }

        public static void PrintPersons()
        {
            foreach (Person user in USERS.Values)
            {
                string log = user.IsAuthenticated ? "logged in":"Not logged in";
                Console.WriteLine($"[{user},{user.Name} {log}]");
            }
        }

        public static Person GetPerson(string name)
        {
            if (USERS.ContainsKey(name))
            {
                return USERS[name];
            }
            else
            {
                throw new AccountException(ExceptionType.USER_DOES_NOT_EXIST);
            }
        }

        public static Account GetAccount(string number)
        {
            if (ACCOUNTS.ContainsKey(number))
            {
                return ACCOUNTS[number];
            }
            else
            {
                throw  new AccountException(ExceptionType.ACCOUNT_DOES_NOT_EXIST);
            }
        }

        public static void AddPerson(string name, string sin)
        {
            Person p = new Person(name, sin);
            p.OnLogin += Logger.LoginHandler; 
            USERS.Add(name, p);
        }

        public static void AddAccount(Account account)
        {
            account.OnTransaction += (sender, balance) =>
            {
                Logger.TransactionHandler(account, new TransactionEventArgs(account.Number, balance, true));
            };
            ACCOUNTS.Add(account.Number, account);
        }
        public static void AddUserToAccount(string number, string name)
        {
            Account acc = ACCOUNTS[number];
            Person pers = USERS[name];
            acc.AddUser(pers);

        }
        public static List<Transaction> GetAllTransactions()
        {
            List < Transaction > transactions = new List < Transaction >();
            foreach (Account account in ACCOUNTS.Values)
            {
                transactions.AddRange(account.transactions);
            }
            return transactions;
        } 

    }
}

