using MockDataGenerator.Core;
using MockDataGenerator.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.MVVM.ViewModel
{
    class GeneralViewModel : ObservableObject
    {
        private object _currentView;
        private readonly UserStore _userStore;
        private readonly DataTypeStore _dataTypeStore;

        #region Properties
        public GenerateRawTextViewModel GenerateRawTextVM { get; set; }
        public InsertIntoDatabaseViewModel InsertIntoDatabaseVM { get; set; }
        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commads
        public RelayCommand OpenGenerateRawTextViewCommand { get; set; }
        public RelayCommand OpenInsertIntoDatabaseViewCommand { get; set; }
        #endregion


        public GeneralViewModel(UserStore userStore, DataTypeStore dataTypeStore)
        {
            _userStore = userStore;
            _dataTypeStore = dataTypeStore;
            GenerateRawTextVM = new GenerateRawTextViewModel(_userStore, _dataTypeStore);
            InsertIntoDatabaseVM = new InsertIntoDatabaseViewModel();

            OpenGenerateRawTextViewCommand = new RelayCommand(o => { CurrentView = GenerateRawTextVM; });
            OpenInsertIntoDatabaseViewCommand = new RelayCommand(o => { CurrentView = InsertIntoDatabaseVM; });
        }
    }
}
