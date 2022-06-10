using MockDataGenerator.Core;
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
        private object _currentView;


        public WelcomeViewModel WelcomeVM { get; set; }
        public LoginViewModel LoginVM { get; set; }
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
        #endregion

        public MainViewModel()
        {
            WelcomeVM = new WelcomeViewModel();
            LoginVM = new LoginViewModel();

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
            #endregion

            CurrentView = WelcomeVM;
        }
    }
}
