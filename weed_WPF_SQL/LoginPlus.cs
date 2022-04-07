using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weed_WPF_SQL
{
    public partial class Login
    {
        public override string ToString()
        {
            string display = this.Username.ToString();

            return display;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Authenticate(string username, string password)
        {
            bool hasMatch = false;

            if(!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                if(this.Username == username)
                {
                    if (this.Password == password)
                    {
                        hasMatch = true;
                    }
                }
            }

            return hasMatch;
        }
    }
}
