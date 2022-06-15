using MockDataGenerator.Core;
using MockDataGenerator.MVVM.Model;
using MockDataGenerator.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MockDataGenerator.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        private readonly UserStore _userStore;
        private object _currentView;
        private UserModel _user;
        private bool _isUserLoggedIn;
        private bool _forgetMe;

        public WelcomeViewModel WelcomeVM { get; set; }
        public LoginViewModel LoginVM { get; set; }

        public bool ForgetMe
        {
            get { return _forgetMe; }
            set 
            { 
                _forgetMe = value;
                OnPropertyChanged();
            }
        }
        public UserModel User
        {
            get { return _user; }
            set 
            { 
                _user = value;
                OnPropertyChanged();
            }
        }
        public bool IsUserLoggedIn
        {
            get { return _isUserLoggedIn; }
            set 
            { 
                _isUserLoggedIn = value;
                OnPropertyChanged();
            }
        }
        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }


        #region Commands
        public RelayCommand DragWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand OpenLoginViewCommand { get; set; }
        public RelayCommand OpenWelcomeViewCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        #endregion

        public MainViewModel()
        {
            _userStore = new UserStore();

            WelcomeVM = new WelcomeViewModel();
            LoginVM = new LoginViewModel(_userStore);

            #region InsantiateCommands
            DragWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.DragMove(); });
            MinimizeWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });
            MaximizeWindowCommand = new RelayCommand(o =>
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                else
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
            });
            CloseWindowCommand = new RelayCommand(o => { Application.Current.Shutdown(); });
            OpenLoginViewCommand = new RelayCommand(o => { CurrentView = LoginVM; });
            OpenWelcomeViewCommand = new RelayCommand(o => { CurrentView = WelcomeVM; });
            LogoutCommand = new RelayCommand(o => LogoutUser());
            #endregion

            CurrentView = WelcomeVM;

            _userStore.UserLoggedInEvent += OnUserLogedIn;
        }

        private void LogoutUser()
        {
            IsUserLoggedIn = false;
            CurrentView = WelcomeVM;

            _userStore.UserLoggedOut(ForgetMe);
            ForgetMe = false;
        }

        private void OnUserLogedIn(UserModel user)
        {
            string credantials = user.Username + " " + user.ApiKey;
            User = user;
            IsUserLoggedIn = true;
        }
    }
}
