using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernBox.Models;

namespace ModernBox.Helpers
{
   public class WidgetSizeHelper
    {

        public static int getWidth(WidgetSize size)
        {
            switch (size)
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

        public static int getHeight(WidgetSize size)
        {
            switch (size)
            {
                case WidgetSize.Small:
                    return 192;
                case WidgetSize.Middle:
                    return 400;
                case WidgetSize.Big:
                    return 600;
                default: return 192;
            }
        }
    }
}
