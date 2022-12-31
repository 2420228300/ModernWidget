using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernBox.Contracts.Services;
using ModernBox.Models;

namespace ModernBox.ViewModels
{
    public class AddWidgetViewModel : ObservableRecipient
    {
        private readonly ISettingsDataService settingsDataService;

        private ObservableCollection<WidgetsKind> widgetCategory;
        public ObservableCollection<WidgetsKind> WidgetCategory
        {
            get => widgetCategory;
            set
            {
                widgetCategory = value;
                OnPropertyChanged("WidgetCategory");
               
            }
        }

        public AddWidgetViewModel()
        {
            WidgetCategory = new ObservableCollection<WidgetsKind>();
            settingsDataService = App.GetService<ISettingsDataService>();
            InitData();
        }


        private void InitData()
        {


            WidgetCategory.Clear();
            List<Widget> widgets = settingsDataService.getSettings().Widgets;
            var enumerable = widgets.GroupBy(w => w.WidgetType);
            foreach(var group in enumerable)
            {
                WidgetCategory.Add(new WidgetsKind()
                {
                    CategoryName = group.Key,
                    CategoryItems = group.ToList()
                });
            }
        }

        
    }
}
