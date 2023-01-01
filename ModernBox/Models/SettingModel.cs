using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ModernBox.Models;
public class SettingModel
{
    public int MaxWidth
    {
        get; set;
    }

    public int MaxHeight
    {
        get; set;
    }

    public int MarginLeft
    {
        get; set;
    }

    public int MarginTop
    {
        get; set;
    }


    public int Width
    {
        get; set;
    }

    public int Height
    {
        get; set;
    }

    public List<Widget> Widgets
    {
        get; set;
    }

    public List<Widget> AllSystemWidgets
    {
    get; set; }
}
