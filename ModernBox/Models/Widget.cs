using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Media.Animation;
using ModernBox.Contracts.Services;
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

        public Uri WidgetIcon
        {
            get; set;
        }


        public String? WidgetName
        {
            get; set;
        }

        public String Description
        {
            get; set;
        }

        public Uri Cover
        {
            get; set;
        }

        public String DllPath
        {
            get; set;
        }

        public WidgetSize WidgetSize
        {
            get; set;
        }

        [JsonIgnore]
        public int Width => getWidth(this.WidgetSize);
        [JsonIgnore]
        public int Height => getHeight(this.WidgetSize);
        private int getWidth(WidgetSize size)
        {
            switch (this.WidgetSize)
            {
                case WidgetSize.Small:
                    return 300;
                case WidgetSize.Middle:
                    return 300;
                case WidgetSize.Big:
                    return 300;
                default: return 100;
            }
        }

        private int getHeight(WidgetSize size)
        {
            switch (this.WidgetSize)
            {
                case WidgetSize.Small:
                    return 192;
                case WidgetSize.Middle:
                    return 400;
                case WidgetSize.Big:
                    return 600;
                default: return 100;
            }
        }

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

        public String WidgetType
        {
            get; set;
        }


        [JsonIgnore]
        public IRelayCommand<Widget> widgetDetailsCommand
        {
            get
            {
                return new RelayCommand<Widget>((w) =>
                {
                    App.GetService<INavigationService>().NavigateTo(typeof(WidgetDetailViewModel).FullName!,w,false);
                });
            }
        }
    }
}