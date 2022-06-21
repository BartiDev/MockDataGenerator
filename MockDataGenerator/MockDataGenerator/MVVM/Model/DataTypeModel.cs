using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.MVVM.Model
{
    public class DataTypeModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<DataParameterModel> Parameters { get; set; }
    }
}
