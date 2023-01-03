using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBox.Models
{
    

    public class WidgetUpdateControl
    {
        public Guid Id
        {
            get; set;
        }

        public WidgetUpdateEnum WidgetUpdateEnum
        {
            get; set;
        }

        public String Value
        {
            get; set;
        }

        public WidgetSize widgetSize
        {
            get; set;
        }

    }
}
