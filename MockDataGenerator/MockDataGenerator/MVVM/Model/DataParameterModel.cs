using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.MVVM.Model
{
    public class DataParameterModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Default { get; set; }
        public string Value { get; set; }
        public ObservableCollection<string> Values { get; set; }
    }
}
