using MockDataGenerator.Core;
using MockDataGenerator.MVVM.Model;
using MockDataGenerator.Store;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.MVVM.ViewModel
{
    public class GenerateRawTextViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;
        private readonly DataTypeStore _dataTypeStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public UserModel User { get; set; }
        public ObservableCollection<DataTypeModel> DataTypes => new ObservableCollection<DataTypeModel>(_dataTypeStore.DataTypes);
        public ObservableCollection<DataFieldModel> DataFields { get; set; }

        public RelayCommand AddNextFieldCommand { get; set; }
        public RelayCommand ChooseDataTypeCommand { get; set; }


        public GenerateRawTextViewModel(UserStore userStore, DataTypeStore dataTypeStore, ModalNavigationStore modalNavigationStore)
        {
            _userStore = userStore;
            _dataTypeStore = dataTypeStore;
            _modalNavigationStore = modalNavigationStore;
            _userStore.UserLoggedInEvent += OnUserLoggedIn;
            _dataTypeStore.DataTypesChangedEvent += OnDataTypesChanged;

            AddNextFieldCommand = new RelayCommand(o => AddDataField());
            ChooseDataTypeCommand = new RelayCommand(o => _modalNavigationStore.CurrentModalView = new DataTypesViewModel(_dataTypeStore));
        }

        private void AddDataField()
        {
            if(DataFields == null)
                DataFields = new ObservableCollection<DataFieldModel>();
            
            DataFields.Add(new DataFieldModel()
            {
                Name = "first_name",
                DataType = DataTypes.FirstOrDefault(dt => dt.Name == "First Name"),
                Blank = 0
            });
        }

        private void OnUserLoggedIn(UserModel userModel)
        {
            User = userModel;
        }

        private void OnDataTypesChanged()
        {
            OnPropertyChanged(nameof(DataTypes));
            AddDataField();
        }
    }
}
