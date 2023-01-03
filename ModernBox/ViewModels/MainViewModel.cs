using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ModernBox.Contracts.Services;
using ModernBox.Models;

namespace ModernBox.ViewModels;

public class MainViewModel : ObservableRecipient
{
    private ObservableCollection<Widget> widgets;

    public ObservableCollection<Widget> Widgets
    {
        get => widgets;
        set
        {
            widgets = value;
            OnPropertyChanged("Widgets");
        }
    }

    public IWidgetService widgetService
    {
        get; set;
    }

    private readonly ISettingsDataService settingsDataService;

    public MainViewModel()
    {
        Widgets = new ObservableCollection<Widget>();
        settingsDataService = App.GetService<ISettingsDataService>();
        widgetService = App.GetService<IWidgetService>();
        InitWidget();

        WeakReferenceMessenger.Default.Register<String, String>(this, "RefreshWidgets", (r, e) =>
        {
            switch (e.ToString())
            {
                //TODO 删除组件导致Widget的内容区域改变，未找到原因
                case "RemoveWidget":
                    App.GetService<INavigationService>().NavigateTo(typeof(TempPageViewModel).FullName!, null, true);
                    App.GetService<INavigationService>().NavigateTo(typeof(MainViewModel).FullName!, null, true);
                    break;

                default:
                    Widgets.Clear();
                    InitWidget();
                    settingsDataService.save();
                    break;
            }
        });
    }

    private void InitWidget()
    {
        var settingModel = settingsDataService.getSettings();
        foreach (var item in settingModel.Widgets)
        {
            if (item.State)
            {
                item.WidgetContent = widgetService.GetSystemWidget(item.ClassName!);
                item.WidgetConfigContent = widgetService.GetSystemWidgetSetting(item.ClassName!);
                Widgets.Add(item);
            }
        }
    }

    public IRelayCommand settingCommand => 
        new RelayCommand(() =>
             {
                 App.GetService<INavigationService>().NavigateTo(typeof(SettingsViewModel).FullName!, null, false);
             });

    public IRelayCommand addWidgetCommand =>
        new RelayCommand(() =>
            {
                App.GetService<INavigationService>().NavigateTo(typeof(AddWidgetViewModel).FullName!, null, false);
            });

    public IRelayCommand RefreshWidgetCommand => 
        new RelayCommand(() =>
            {
                App.GetService<INavigationService>().NavigateTo(typeof(TempPageViewModel).FullName!, null, true);
                App.GetService<INavigationService>().NavigateTo(typeof(MainViewModel).FullName!, null, true);
            });
}