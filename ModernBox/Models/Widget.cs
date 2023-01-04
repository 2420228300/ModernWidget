using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Animation;
using ModernBox.attr;
using ModernBox.Contracts.Services;
using ModernBox.Helpers;
using ModernBox.ViewModels;
using ModernBox.Views;
using Newtonsoft.Json;

namespace ModernBox.Models
{
  

    [JsonObject]
    public class Widget 
    {
        public Guid Id
        {
            get; set;
        }

        public String? ClassName
        {
            get; set;
        }

        public Uri? WidgetIcon
        {
            get; set;
        }


        public String? WidgetName
        {
            get; set;
        }

        public String? Description
        {
            get; set;
        }

        public Uri? Cover
        {
            get; set;
        }

        public String? DllPath
        {
            get; set;
        }

        public WidgetSize WidgetSize
        {
            get; set;
        }

        [JsonIgnore]
        [WidthoutCopy]
        public int Width => WidgetSizeHelper.getWidth(this.WidgetSize);
        [JsonIgnore]
        [WidthoutCopy]
        public int Height => WidgetSizeHelper.getHeight(this.WidgetSize);
        

        [JsonIgnore]
        public Type? WidgetContent
        {
            get; set;
        }

        [JsonIgnore]
        public Type? WidgetConfigContent
        {
            get;
            set;
        }

        public Boolean State
        {
            get; set;
        }

        public Boolean IsOther
        {
            get; set;
        }

        public String? WidgetType
        {
            get; set;
        }


        public Visibility HasTitleBar
        {
            get; set;
        } = Visibility.Visible;

        public int LPadding
        {
            get; set;
        } = 8;

        public int TPadding
        {
            get; set;
        } = 8;

        public int RPadding
        {
            get; set;
        } = 8;

        public int BPadding
        {
            get; set;
        } = 8;

        [JsonIgnore]
        public Thickness WidgetPadding => new(LPadding, TPadding, RPadding, BPadding);

        [JsonIgnore]
        public IRelayCommand<Widget> widgetDetailsCommand => new RelayCommand<Widget>((w) =>
                                                                          {
                                                                              App.GetService<INavigationService>().NavigateTo(typeof(WidgetDetailViewModel).FullName!, w, false);
                                                                          });
    }
}