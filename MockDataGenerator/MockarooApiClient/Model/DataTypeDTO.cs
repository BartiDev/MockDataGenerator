using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockarooApiClient.Model
{
    public class DataTypeDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public List<DataParameterDTO> Parameters { get; set; }
    }
}
