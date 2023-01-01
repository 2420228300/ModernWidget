using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ModernBox.attr;

namespace ModernBox.Helpers;
public class ModelCopyAttribute
{
    public static TR Copy<T, TR>(object o) where T : class
    {
        Type type = o.GetType();
        PropertyInfo[] propertyInfos = type.GetProperties();
        TR targetInstance = Activator.CreateInstance<TR>()!;
        Type targetType = targetInstance.GetType();
        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            PropertyInfo? property = targetType.GetProperty(propertyInfo.Name);
            var customAttributes = property.CustomAttributes;
            if (customAttributes != null)
            {


                var flag = customAttributes.Any(w => w.AttributeType == typeof(WidthoutCopyAttribute));
                if (flag)
                {
                    break;
                }
            }
            if (property != null)
            {
                object? value = propertyInfo.GetValue(o);
                property.SetValue(targetInstance, value);
            }
        }
        return targetInstance;
    } 
}
