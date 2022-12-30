using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBox.Contracts.Services
{
    public interface IWidgetService
    {
        Object GetSystemWidget(string key);

        void ConfigureSystemWidget(String key, Type type);

        Object GetSystemWidgetSetting(string key);
    }
}
