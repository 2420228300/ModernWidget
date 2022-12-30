using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernBox.Contracts.Services;
using ModernBox.Models;
using ModernBox.Services;
using ModernBox.Views.Widgets;

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

    public IRelayCommand settingCommand
    {
        get
        {
            return new RelayCommand(() =>
            {
                App.GetService<INavigationService>().NavigateTo(typeof(SettingsViewModel).FullName!);
            });
        }
    }


    public IRelayCommand addWidgetCommand
    {
        get
        {
            return new RelayCommand(() =>
            {       
                App.GetService<INavigationService>().NavigateTo(typeof(AddWidgetViewModel).FullName!);
            });
        }
    }
}
