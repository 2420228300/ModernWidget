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

        //StaggeredRepeater.CanDrag = true;
        //StaggeredRepeater.DragStarting += StaggeredRepeater_DragStarting;
       


    }

    //private void StaggeredRepeater_DragStarting(Microsoft.UI.Xaml.UIElement sender, Microsoft.UI.Xaml.DragStartingEventArgs args)
    //{
    //    StaggeredRepeater.InvalidateMeasure();  
    //    StaggeredRepeater.InvalidateArrange();   
    //}
}
