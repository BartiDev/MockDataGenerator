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
        public event Action<List<DataTypeModel>> DataTypesLoadedEvent;

        public void DataTypesLoaded(List<DataTypeModel> dataTypes)
        {
            DataTypesLoadedEvent?.Invoke(dataTypes);
        }
    }
}
