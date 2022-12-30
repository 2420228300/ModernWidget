using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernBox.Contracts.Services;
using ModernBox.Helpers;
using ModernBox.Models;

namespace ModernBox.ViewModels;
public class SettingsViewModel : ObservableRecipient
{

    public SettingModel  settingModel
    {
        get; set;
    }
    public IRelayCommand<int>? changeWidthCommand = null;
    public IRelayCommand? changeHeightCommand = null;
    public IRelayCommand? changMarginLeftCommand = null;
    public IRelayCommand? changeMarginRighthCommand = null  ;



    public IRelayCommand GoBackCommand
    {
        get
        {
            return new RelayCommand(() =>
            {
                App.GetService<INavigationService>().GoBack();
                var settingsDataService = App.GetService<ISettingsDataService>();
                settingsDataService.save();
            });
        }
    }


    public SettingsViewModel()
    {
        this.settingModel = App.GetService<ISettingsDataService>().getSettings();
    }


}
