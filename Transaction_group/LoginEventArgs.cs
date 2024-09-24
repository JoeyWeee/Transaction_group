using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group
{
    public class LoginEventArgs : EventArgs
    {
        public string name { get; }
        public bool Success { get; }

        public LoginEventArgs(string name, bool success)
        {
            this.name = name;
            Success = success;
        }

    }
}
