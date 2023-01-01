using ModernBox.Contracts.Services;
using ModernBox.ViewModels.Widgets;
using ModernBox.Views.Widgets;
using ModernBox.Views.Widgets.PhotoBoxWidget;
using ModernBox.Views.Widgets.TestWidget;

namespace ModernBox.Services
{
    public class WidgetService : IWidgetService
    {
        private Dictionary<String, Type> widgets;

        public WidgetService()
        {
            widgets
                = new Dictionary<String, Type>();
            Configure();
        }

        private void Configure()
        {
            ConfigureSystemWidget(typeof(TestWidgetIndexPage).FullName!, typeof(TestWidgetIndexPage));
            ConfigureSystemWidget(getWidgetSettingKey(typeof(TestWidgetIndexPage)),typeof(Views.Widgets.TestWidget.TestWidgetSettingPage));

            ConfigureSystemWidget(typeof(PhotoBoxWidgetIndexPage).FullName!,typeof(PhotoBoxWidgetIndexPage));
        
        }

        public String getWidgetSettingKey(Type type)
        {
            return $"{type.FullName}.SettingContent";
        }

        public void ConfigureSystemWidget(string key, Type type)
        {
            if (!widgets.ContainsKey(key))
            {
                widgets.Add(key, type);
            }
            else
            {
                throw new InvalidOperationException($"{key} is already exit widgets");   
            }
        }

     
        public object GetSystemWidget(string key)
        {
            return widgets[key];
        }


        public Object GetSystemWidgetSetting(string key)
        {
            return widgets[$"{key}.SettingContent"];
        }

        Type? IWidgetService.GetSystemWidget(string key)
        {
            if (widgets.ContainsKey(key))
            {
                return widgets[key];
            }
            return default;
        }
        Type IWidgetService.GetSystemWidgetSetting(string key)
        {
            if (widgets.ContainsKey($"{key}.SettingContent"))
            {
                return widgets[$"{key}.SettingContent"];
            }
            return default;
        }
    }
}