using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace ModernBox.convert
{
    public class Visibility2BooleanConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Visibility? visibility = (Visibility)value;
            if (visibility != null) {
                 return visibility == Visibility.Visible;   
            }
            return true;
        
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
