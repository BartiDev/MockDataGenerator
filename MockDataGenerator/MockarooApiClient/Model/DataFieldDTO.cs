using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockarooApiClient.Model
{
    public class DataFieldDTO
    {
        public string Name { get; set; }
        public DataTypeDTO DataType { get; set; }
        public int Blank { get; set; }
    }
}
