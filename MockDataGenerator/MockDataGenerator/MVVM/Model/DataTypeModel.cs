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
        public override string ToString() => Name;


        public DataTypeModel() {}
        public DataTypeModel(DataTypeModel dataType)
        {
            Name = dataType.Name;
            Type = dataType.Type;
            Parameters = new List<DataParameterModel>();

            foreach(var parameter in dataType.Parameters)
            {
                Parameters.Add(new DataParameterModel()
                {
                    Default = parameter.Default,
                    Description = parameter.Description,
                    Name = parameter.Name,
                    Type = parameter.Type,
                    Value = parameter.Default,
                    Values = parameter.Values
                });
            }
        }
    }
}
