﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockarooApiClient.Model
{
    public class DataParameterDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Default { get; set; }
        public string Value { get; set; }
        public List<string> Values { get; set; }
    }
}
