using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace group
{

	public struct Transaction
	{
		public string AccountNumber { get; }
        public double Amount { get; }
        public Person Originator { get; }
        public DayTime Time { get; }

        public Transaction(string accountNumber, double amount, Person person)
        {
			Time = Utils.Time;
			Amount = amount;
			Originator = person;
			AccountNumber = accountNumber;
		}
		
        public override string ToString()
		{
			string tString="";
			if (Amount>0)
			{tString = $"{AccountNumber} {Amount:C2} deposited by {Originator} on {Time}";}
			else if (Amount < 0)
			{
                double a=-Amount;
                tString = $"{AccountNumber} {a:C2} withdrawn by {Originator} on {Time}";
			}
			return tString;
        }

    }
}

