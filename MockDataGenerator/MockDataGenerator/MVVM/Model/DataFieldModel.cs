using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.MVVM.Model
{
    public class DataFieldModel
    {
        public string Name { get; set; }
        public DataTypeModel DataType { get; set; }
        public int Blank { get; set; }
    }
}
