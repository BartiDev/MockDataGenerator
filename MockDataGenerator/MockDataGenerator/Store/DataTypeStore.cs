using MockDataGenerator.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.Store
{
    public class DataTypeStore
    {
        public event Action DataTypesChangedEvent;
        public event Action<DataTypeModel> DataTypeSelectedEvent;

        private List<DataTypeModel> _dataTypes;


        public List<DataTypeModel> DataTypes
        {
            get { return _dataTypes; }
            set 
            { 
                _dataTypes = value;
                OnDataTypesChanged();
            }
        }

        private void OnDataTypesChanged()
        {
            DataTypesChangedEvent?.Invoke();
        }

        public void DataTypeSelected(DataTypeModel dataTypeModel)
        {
            DataTypeSelectedEvent.Invoke(dataTypeModel);
        }
    }
}
