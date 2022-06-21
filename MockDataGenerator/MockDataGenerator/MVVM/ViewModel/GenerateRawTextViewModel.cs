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
    public class GenerateRawTextViewModel
    {
        private readonly UserStore _userStore;
        private readonly DataTypeStore _dataTypeStore;

        public UserModel User { get; set; }
        public ObservableCollection<DataTypeModel> DataTypes { get; set; }


        public GenerateRawTextViewModel(UserStore userStore, DataTypeStore dataTypeStore)
        {
            _userStore = userStore;
            _dataTypeStore = dataTypeStore;
            _userStore.UserLoggedInEvent += OnUserLoggedIn;
            _dataTypeStore.DataTypesLoadedEvent += OnDataTypesLoaded;
        }

        private void OnUserLoggedIn(UserModel userModel)
        {
            User = userModel;
        }

        private void OnDataTypesLoaded(List<DataTypeModel> dataTypes)
        {
            DataTypes = new ObservableCollection<DataTypeModel>(dataTypes); 
        }
    }
}
