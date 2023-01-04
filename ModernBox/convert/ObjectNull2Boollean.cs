using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace ModernBox.convert
{
    public class ObjectNull2Boollean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(value == null);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
