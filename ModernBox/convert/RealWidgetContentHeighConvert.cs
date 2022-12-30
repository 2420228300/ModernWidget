using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Data;

namespace ModernBox.convert;
public class RealWidgetContentHeighConvert : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        int height;
        if (int.TryParse(value.ToString(),out height))
        {
            return height - 48;
        }
        
        return 68;
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}
