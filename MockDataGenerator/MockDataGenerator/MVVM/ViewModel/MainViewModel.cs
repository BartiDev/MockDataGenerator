using AutoMapper;
using MockarooApiClient;
using MockarooApiClient.Data;
using MockDataGenerator.Core;
using MockDataGenerator.Core.Automapper;
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
    class MainViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;
        private readonly DataTypeStore _dataTypeStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly IMapper _mapper;
        private BaseViewModel _currentView;
        private UserModel _user;
        private bool _isUserLoggedIn;
        private bool _forgetMe;

        public WelcomeViewModel WelcomeVM { get; set; }
        public LoginViewModel LoginVM { get; set; }
        public GeneralViewModel GeneralVM { get; set; }
        public BartiCinemaViewModel BartiCinemaVM { get; set; }
        public SqlServersViewModel SqlServersVM { get; set; }

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
        public BaseViewModel CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public BaseViewModel CurrentModalView => _modalNavigationStore.CurrentModalView;

        public bool IsModalViewOpen
        {
            get { return _modalNavigationStore.IsModalViewOpen; }
            set { _modalNavigationStore.IsModalViewOpen = value; }
        }



        #region Commands
        public RelayCommand DragWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }
        public RelayCommand OpenLoginViewCommand { get; set; }
        public RelayCommand OpenWelcomeViewCommand { get; set; }
        public RelayCommand OpenGeneralViewCommand { get; set; }
        public RelayCommand OpenBartiCinemaViewCommand { get; set; }
        public RelayCommand OpenSqlServersViewCommand { get; set; }
        public RelayCommand LogoutCommand { get; set; }
        public ModalNavigationStore OnModalViewChanged { get; }
        #endregion

        public MainViewModel()
        {
            MockarooClient.Initialize();
            _userStore = new UserStore();
            _dataTypeStore = new DataTypeStore();
            _modalNavigationStore = new ModalNavigationStore();
            _mapper = MyMapper.GetMapper();
            WelcomeVM = new WelcomeViewModel();
            LoginVM = new LoginViewModel(_userStore);
            GeneralVM = new GeneralViewModel(_userStore, _dataTypeStore, _modalNavigationStore);
            BartiCinemaVM = new BartiCinemaViewModel();
            SqlServersVM = new SqlServersViewModel();

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
            OpenGeneralViewCommand = new RelayCommand(o => { if (IsUserLoggedIn) CurrentView = GeneralVM; });
            OpenBartiCinemaViewCommand = new RelayCommand(o => { if(IsUserLoggedIn) CurrentView = BartiCinemaVM; });
            OpenSqlServersViewCommand = new RelayCommand(o => { if (IsUserLoggedIn) CurrentView = SqlServersVM; });
            LogoutCommand = new RelayCommand(o => LogoutUser());
            #endregion

            CurrentView = WelcomeVM;

            _userStore.UserLoggedInEvent += OnUserLogedIn;
            _modalNavigationStore.CurrentModalViewChangedEvent += OnCurrentModalViewChanged;
        }

        private void LogoutUser()
        {
            IsUserLoggedIn = false;
            CurrentView = WelcomeVM;

            _userStore.UserLoggedOut(ForgetMe);
            ForgetMe = false;
        }

        private async void OnUserLogedIn(UserModel user)
        {
            User = user;
            IsUserLoggedIn = true;
            CurrentView = GeneralVM;

            // Get DataTypes and send to the rest of the viewModels
            DataTypeData dataTypeData = new DataTypeData();
            _dataTypeStore.DataTypes = _mapper.Map<List<DataTypeModel>>(await dataTypeData.Get(User.ApiKey));
        }

        private void OnCurrentModalViewChanged()
        {
            OnPropertyChanged(nameof(CurrentModalView));
            OnPropertyChanged(nameof(IsModalViewOpen));
        }

    }
}
