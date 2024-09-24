using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group
{
    public class Person
    {
        private string password;
        public event EventHandler<EventArgs> OnLogin;

        public string SIN { get; }
        public string Name { get; }
        public bool IsAuthenticated { get; private set; }

        public Person(string name, string sin)
        {
            Name = name;
            SIN = sin;
            password = sin.Substring(0, 3);
            IsAuthenticated = false;
        }

        public void Login(string password)
        {
            if (this.password != password)
            {
                IsAuthenticated = false;
                OnLogin?.Invoke(this, new LoginEventArgs(Name, false));
                throw new AccountException(ExceptionType.PASSWORD_INCORRECT);
            }
            else
            {
                IsAuthenticated = true;
                OnLogin?.Invoke(this, new LoginEventArgs(Name, true));
            }
        }

        public void Logout()
        {
            IsAuthenticated = false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
