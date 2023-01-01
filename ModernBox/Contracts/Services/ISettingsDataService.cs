using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernBox.Models;

namespace ModernBox.Contracts.Services;
public interface ISettingsDataService
{
    void initSettings();

    SettingModel getSettings();

    void setSettings(SettingModel settingModel);
    void AddWidget(Widget widget);
    void save();

    Boolean RemoveWidget(Guid guid);
}
