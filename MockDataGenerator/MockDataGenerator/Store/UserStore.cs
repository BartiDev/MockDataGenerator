using MockDataGenerator.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.Store
{
    public class UserStore
    {
        public event Action<UserModel> UserLoggedInEvent;
        public event Action<bool> UserLoggedOutEvent;

        public void UserLoggedIn(UserModel user)
        {
            UserLoggedInEvent?.Invoke(user);
        }

        public void UserLoggedOut(bool forgetMe)
        {
            UserLoggedOutEvent?.Invoke(forgetMe);
        }
    }
}
