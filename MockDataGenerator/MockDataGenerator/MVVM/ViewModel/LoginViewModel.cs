using MockDataGenerator.Api.CredantialManager;
using MockDataGenerator.Core;
using MockDataGenerator.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.MVVM.ViewModel
{
    class LoginViewModel
    {
        public string Username { get; set; }
        public string ApiKey { get; set; }
        public bool RememberMe { get; set; }

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(o => Login());

            CheckForRememberedUser();
        }

        private void Login()
        {
            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(ApiKey))
            {
                if (RememberMe)
                    WindowsCredantialManager.SaveCredantials(Username, ApiKey);
            }
        }

        private void CheckForRememberedUser()
        {
            UserModel user = WindowsCredantialManager.ReadCredantials();

            if(user != null)
            {
                ApiKey = user.ApiKey;
                Username = user.Username;
            }
        }
    }
}
