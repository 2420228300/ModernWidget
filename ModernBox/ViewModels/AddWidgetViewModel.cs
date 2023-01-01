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
            List<Widget> widgets = settingsDataService.getSettings().AllSystemWidgets;
            var enumerable = widgets.GroupBy(w => w.WidgetType);
            foreach(var group in enumerable)
            {
                var temp_widgets = group.Select(w =>
                {
                    if (w.Cover==null||!File.Exists(w.Cover.AbsolutePath))
                    {
                        var baseDirectory = AppContext.BaseDirectory;
                        var widgetDefaultAssets = Path.Combine(baseDirectory, "Assets", "WidgetDefaultCover");
                        DirectoryInfo directoryInfo= new DirectoryInfo(widgetDefaultAssets);
                        var fileInfos = directoryInfo.GetFiles();
                        if (fileInfos.Length>0)
                        {
                            Random random = new Random();
                            w.Cover = new Uri(fileInfos[random.Next(0, fileInfos.Length)].FullName);
                        }
                    }
                    return w;
                }).ToList();

                WidgetCategory.Add(new WidgetsKind()
                {
                    CategoryName = group.Key,
                    CategoryItems = temp_widgets
                });
            }
        }

        
    }
}
