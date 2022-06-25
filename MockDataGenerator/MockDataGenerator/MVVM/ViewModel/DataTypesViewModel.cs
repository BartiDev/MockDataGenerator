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
    public class DataTypesViewModel : BaseViewModel
    {
        private readonly DataTypeStore _dataTypeStore;

        public ObservableCollection<DataTypeModel> DataTypes => new ObservableCollection<DataTypeModel>(_dataTypeStore.DataTypes);
        public DataTypesViewModel(DataTypeStore dataTypeStore)
        {
            _dataTypeStore = dataTypeStore;
            _dataTypeStore.DataTypesChangedEvent += OnDataTypesChanged;
        }

        private void OnDataTypesChanged() => OnPropertyChanged(nameof(DataTypes));
    }
}
