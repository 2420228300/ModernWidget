using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace ModernBox.convert;
public class String2UriConvert : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value!=null&&value.ToString()!=String.Empty)
        {
            return new Uri(value.ToString()!);
        }
        return new Uri("ms-appx:///Assets/WidgetIcons/widgetDefaultIcon.png");
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
