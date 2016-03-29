using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treading.Authentication
{
    class Account
    {
        public static bool isLoggedIn;
        public User currentUser;

        private void loginAccount(string username, string password)
        {
            //..
            User harry = new User();
            currentUser = harry;
        }
    
        private void registerAccount(string username, string password, string email)
        {

        }

        private void forgotPassword(string username, string email)
        {

        }

        private void forgotMail(string mail)
        {

        }

        private void logout(User user)
        {

        }
    }
}
