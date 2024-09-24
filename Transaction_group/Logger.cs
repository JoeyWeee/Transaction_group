using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group
{
    public class Logger
    {
        private static List<string> loginEvents = new List<string>();
        private static List<string> transactionEvents = new List<string>();

        public static void LoginHandler(object sender, EventArgs args)
        {
            LoginEventArgs loginArgs = args as LoginEventArgs;

            string logEntry = $"{loginArgs.name} logged in : {(loginArgs.Success ? "successfully" : "unsuccessfully")} on - Time: {Utils.Now}";

            loginEvents.Add(logEntry);
        }

        public static void TransactionHandler(object sender, EventArgs args)
        {
            TransactionEventArgs transactionArgs = args as TransactionEventArgs;
            string operation = transactionArgs.Amount >= 0 ? $"Deposit {transactionArgs.Amount:c2}" : $"Withdraw {-transactionArgs.Amount:c2}";
            string transactionEvent = $"{transactionArgs.name} {operation}  {(transactionArgs.Success ? "successfully" : "unsuccessfully")} on Time: {Utils.Now}";

            transactionEvents.Add(transactionEvent);
        }

        public static void ShowLoginEvents()
        {
            Console.WriteLine(Utils.Now);

            for (int i = 0; i < loginEvents.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {loginEvents[i]}");
            }
        }

        public static void ShowTransactionEvents()
        {
            Console.WriteLine($"Current Time: {Utils.Now}");

            for (int i = 0; i < transactionEvents.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {transactionEvents[i]}");
            }
        }
    }
}
