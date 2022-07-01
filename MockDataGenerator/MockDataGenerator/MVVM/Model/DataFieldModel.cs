using MockDataGenerator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.MVVM.Model
{
    public class DataFieldModel : ObservableObject
    {
        private DataTypeModel _dataType;

        public string Name { get; set; }
        public DataTypeModel DataType
        {
            get { return _dataType; }
            set
            {
                _dataType = value;
                OnPropertyChanged();
            }
        }
        public int Blank { get; set; }
    }
}
