using Microsoft.UI.Xaml.Controls;
using ModernBox.Helpers;
using ModernBox.ViewModels;

namespace ModernBox.Views;
using WinRT;
public sealed partial class MainPage : Page
{


    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
        this.DataContext= ViewModel;
    }

}
