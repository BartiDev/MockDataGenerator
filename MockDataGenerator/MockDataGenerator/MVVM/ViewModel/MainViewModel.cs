using MockDataGenerator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MockDataGenerator.MVVM.ViewModel
{
    class MainViewModel
    {
        // Window Commands
        public RelayCommand DragWindowCommand { get; set; }
        public RelayCommand MinimizeWindowCommand { get; set; }
        public RelayCommand MaximizeWindowCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }

        public MainViewModel()
        {
            // Window Commands
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
        }
    }
}
