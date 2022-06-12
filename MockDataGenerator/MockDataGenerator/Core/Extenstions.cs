using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MockDataGenerator.Core
{
    class Extensions
    {
        #region Icon
        public static readonly DependencyProperty Icon =
            DependencyProperty.RegisterAttached("Icon", typeof(string), typeof(Extensions), new PropertyMetadata(default(string)));

        public static void SetIcon(UIElement element, string value)
        {
            element.SetValue(Icon, value);
        }

        public static string GetIcon(UIElement element)
        {
            return (string)element.GetValue(Icon);
        }
        #endregion

        #region WatermarkText
        public static readonly DependencyProperty WatermarkText =
            DependencyProperty.RegisterAttached("WatermarkText", typeof(string), typeof(Extensions), new PropertyMetadata(default(string)));

        public static void SetWatermarkText(UIElement element, string value)
        {
            element.SetValue(WatermarkText, value);
        }

        public static string GetWatermarkText(UIElement element)
        {
            return (string)element.GetValue(WatermarkText);
        }
        #endregion

    }
}
