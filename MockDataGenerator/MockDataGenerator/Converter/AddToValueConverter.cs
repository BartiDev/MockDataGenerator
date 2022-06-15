using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MockDataGenerator.Converter
{
    class AddToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = Double.Parse(parameter.ToString(), culture);
            var val = Double.Parse(value.ToString(), culture);
            return val + param;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = Double.Parse(parameter.ToString(), culture);
            var val = Double.Parse(value.ToString(), culture);
            return val - param;
        }
    }
}
