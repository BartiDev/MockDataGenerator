using MockarooApiClient;
using MockarooApiClient.Data;
using MockDataGenerator.Api.CredantialManager;
using MockDataGenerator.Core;
using MockDataGenerator.MVVM.Model;
using MockDataGenerator.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.MVVM.ViewModel
{
    class LoginViewModel : ObservableObject
    {
        private UserStore _userStore;
        private string _feedBackMessage;


        public string Username { get; set; }
        public string ApiKey { get; set; }
        public bool RememberMe { get; set; }
        public UserModel User { get; set; }
        public string FeedBackMessage
        {
            get { return _feedBackMessage; }
            set 
            { 
                _feedBackMessage = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel(UserStore userStore)
        {
            User = new UserModel();
            _userStore = userStore;
            LoginCommand = new RelayCommand(o => Login());

            CheckForRememberedUser();
            _userStore.UserLoggedOutEvent += OnUserLoggedOut;
        }

        private async void Login()
        {
            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(ApiKey))
            {
                GenericData genericData = new GenericData();
                string connectionResult = await genericData.TryConnect(ApiKey);
                if (String.IsNullOrEmpty(connectionResult))
                {
                    if (RememberMe)
                        WindowsCredantialManager.SaveCredantials(Username, ApiKey);

                    _userStore.UserLoggedIn(new UserModel
                    {
                        ApiKey = ApiKey,
                        Username = Username,
                        RememberMe = User.RememberMe ? true : RememberMe 
                    });

                    RememberMe = false;
                    FeedBackMessage = "Success!";
                }
                else
                {
                    FeedBackMessage = connectionResult;
                }
            }
            else
            {
                FeedBackMessage = "Neither Username nor ApiKey cannot be empty";
            }
        }

        private void CheckForRememberedUser()
        {
            UserModel user = WindowsCredantialManager.ReadCredantials();

            if(user != null)
            {
                ApiKey = user.ApiKey;
                Username = user.Username;
                User.RememberMe = true;
            }
            else
            {
                ApiKey = "";
                Username = "";
                User.RememberMe = false;
            }

            User.ApiKey = ApiKey;
            User.Username = Username;
        }

        private void OnUserLoggedOut(bool forgetMe)
        {
            if(!forgetMe)
                CheckForRememberedUser();
            else
            {
                WindowsCredantialManager.DeleteCredantials();

                ApiKey = "";
                Username = "";
                User.RememberMe = false;
            }
        }

    }
}
