using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ModernBox.ViewModels.Widgets.PhotoBoxWidget;
public class PhotoBoxWidgetViewModel : ObservableRecipient
{
    public ObservableCollection<String> Pictures
    {
        get; set;
    }

    public PhotoBoxWidgetViewModel()
    {
        Pictures = new ObservableCollection<string>();

        Pictures.Add("C:\\Users\\Hui\\OneDrive\\图片\\本机照片\\1k7leg.jpg");
        Pictures.Add("C:\\Users\\Hui\\OneDrive\\图片\\本机照片\\05(1).jpg");
        Pictures.Add("C:\\Users\\Hui\\OneDrive\\图片\\本机照片\\06.jpg");
    }
}
