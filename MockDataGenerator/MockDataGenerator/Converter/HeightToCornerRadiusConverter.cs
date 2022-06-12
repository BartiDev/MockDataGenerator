using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MockDataGenerator.Converter
{
    class HeightToCornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Int16 height = System.Convert.ToInt16(value);

            return height / 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Int16 height = System.Convert.ToInt16(value);

            return height * 2;
        }
    }
}
