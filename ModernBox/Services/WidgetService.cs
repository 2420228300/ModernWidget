using ModernBox.Contracts.Services;
using ModernBox.ViewModels.Widgets;
using ModernBox.Views.Widgets;
using ModernBox.Views.Widgets.TestWidget;

namespace ModernBox.Services
{
    public class WidgetService : IWidgetService
    {
        private Dictionary<String, Object> widgets;

        public WidgetService()
        {
            widgets
                = new Dictionary<String, Object>();
            Configure();
        }

        private void Configure()
        {
            ConfigureSystemWidget(typeof(TestWidgetIndexPage).FullName!, typeof(TestWidgetIndexPage));
            ConfigureSystemWidget(getWidgetSettingKey(typeof(TestWidgetIndexPage)),typeof(Views.Widgets.TestWidget.TestWidgetSettingPage));
        }

        public String getWidgetSettingKey(Type type)
        {
            return $"{type.FullName}.SettingContent";
        }

        public void ConfigureSystemWidget(string key, Type type)
        {
            if (!widgets.ContainsKey(key))
            {
                var instance = Activator.CreateInstance(type);
                if (instance!=null)
                {
                    widgets.Add(key, instance);
                }
                else
                {
                    throw new NullReferenceException();
                }
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



    }
}