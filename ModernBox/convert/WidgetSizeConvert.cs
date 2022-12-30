using Microsoft.UI.Xaml.Data;
using ModernBox.Models;

namespace ModernBox.convert;

public class WidgetSizeConvert : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        switch (value)
        {
            case WidgetSize.Small:
                if (parameter.ToString() == WidgetSize.Small.ToString())
                    return true;
                else return false;
            case WidgetSize.Middle:
                if (parameter.ToString() == WidgetSize.Middle.ToString())
                    return true;
                else return false;
            case WidgetSize.Big:
                if (parameter.ToString() == WidgetSize.Big.ToString())
                    return true;
                else return false;
            default:
                return false;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
}