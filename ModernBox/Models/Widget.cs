using CommunityToolkit.Mvvm.ComponentModel;
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

        public String Icon
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

        public String Cover
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

        public int Width => getWidth(this.WidgetSize);

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
                    return 100;
                case WidgetSize.Middle:
                    return 400;
                case WidgetSize.Big:
                    return 600;
                default: return 100;
            }
        }

        [JsonIgnore]
        public Object WidgetContent
        {
            get; set;
        }

        [JsonIgnore]
        public Object? WidgetConfigContent
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
    }
}