using AutoMapper;
using MockarooApiClient.Model;
using MockDataGenerator.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockDataGenerator.Core.Automapper
{
    public static class MyMapper
    {
        private static IMapper _mapper;
        static MyMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataParameterDTO, DataParameterModel>();
                cfg.CreateMap<DataTypeDTO, DataTypeModel>();
            });

            _mapper = config.CreateMapper();
        }

        public static IMapper GetMapper() => _mapper;
    }
}
