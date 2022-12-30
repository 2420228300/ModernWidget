using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ModernBox.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ModernBox.Views;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingPage : Page
{
    public SettingsViewModel viewModel
    {
        get;
    }
    private Boolean flag = true;
    public SettingPage()
    {
        viewModel = App.GetService<SettingsViewModel>();
        this.InitializeComponent();
        this.DataContext = viewModel;
    }

    private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        if (e.OldValue != 0)
        {
            MainWindow.ChangeWindow(Models.ChangeWindowType.Width, (int)widthSlider.Value);
        }
        
    }

    private void heightSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {

        if (e.OldValue!=0)
        {
            MainWindow.ChangeWindow(Models.ChangeWindowType.Height, (int)heightSlider.Value);
        }

        
    }

    private void marginLeftSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        if (e.OldValue != 0)
        {
            MainWindow.ChangeWindow(Models.ChangeWindowType.MarginLeft, (int)marginLeftSlider.Value);
        }
    }

    private void marginTopSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        if (e.OldValue != 0)
        {
            MainWindow.ChangeWindow(Models.ChangeWindowType.MarginTop, (int)marginTopSlider.Value);
        }
    }

    private void BackgroundRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //var selectedIndex = BackgroundRadioButtons.SelectedIndex;
        //MainWindow.ChangeWindow(Models.ChangeWindowType.Material, selectedIndex);
    }
}
