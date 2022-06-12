using MockDataGenerator.Core;
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

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(o => Login());
        }

        private void Login()
        {

        }
    }
}
