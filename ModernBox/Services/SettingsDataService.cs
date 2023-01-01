using ModernBox.Contracts.Services;
using ModernBox.Core.Contracts.Services;
using ModernBox.Helpers;
using ModernBox.Models;
using ModernBox.Views.Widgets.PhotoBoxWidget;
using ModernBox.Views.Widgets.TestWidget;

namespace ModernBox.Services;

public class SettingsDataService : ISettingsDataService
{
    private String folderPath = null;

    private String fileName = null;

    public SettingModel settingModel
    {
        get; set;
    }

    public SettingsDataService()
    {
        folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".modernbox");
        fileName = "config.json";
    }

    public SettingModel getSettings()
    {
        return settingModel;
    }

    public void initSettings()
    {
        IFileService fileService = App.GetService<IFileService>();
        if (!File.Exists(Path.Combine(folderPath, fileName)))
        {
            settingModel = new SettingModel();
            settingModel.MaxWidth = NativeMethods.GetSystemMetrics(0);
            settingModel.MaxHeight = NativeMethods.GetSystemMetrics(1);
            settingModel.MarginLeft = 10;
            settingModel.MarginTop = 10;
            settingModel.Width = settingModel.MaxWidth / 3;
            settingModel.Height = settingModel.MaxHeight - 20;
            settingModel.Widgets = new List<Widget>();
            settingModel.AllSystemWidgets = new List<Widget>();
            settingModel.AllSystemWidgets.Add(new Widget()
            {
                Id = Guid.NewGuid(),
                WidgetName = "ModernWidget使用简介",
                WidgetIcon = new Uri("ms-appx:///Assets/WidgetIcons/widgetDefaultIcon.png"),
                State = true,
                WidgetType= "系统组件",
                ClassName = typeof(TestWidgetIndexPage).FullName,
                WidgetSize = WidgetSize.Big,
            }) ;
            settingModel.Widgets.Add(new Widget()
            {
                Id = Guid.NewGuid(),
                WidgetName = "ModernWidget使用简介",
                WidgetIcon = new Uri("ms-appx:///Assets/WidgetIcons/widgetDefaultIcon.png"),
                State = true,
                WidgetType = "系统组件",
                ClassName = typeof(TestWidgetIndexPage).FullName,
                WidgetSize = WidgetSize.Big,
            });
            settingModel.AllSystemWidgets.Add(new Widget()
            {
                Id = Guid.NewGuid(),
                WidgetName = "照片盒子",
                WidgetIcon = new Uri("ms-appx:///Assets/WidgetIcons/PhotoBoxWidget/icon.png"),
                WidgetType = "图片展示",
                State = true,
                ClassName = typeof(PhotoBoxWidgetIndexPage).FullName,
                WidgetSize = WidgetSize.Middle,
            });

            fileService.Save(folderPath, fileName, settingModel);
        }
        else
        {
            this.settingModel = fileService.Read<SettingModel>(folderPath, fileName);
        }
    }

    public void save()
    {
        IFileService fileService = App.GetService<IFileService>();
        if (settingModel != null)
        {
            fileService.Save<SettingModel>(folderPath, fileName, settingModel);
        }
    }

    public void setSettings(SettingModel settingModel)
    {
        this.settingModel = settingModel;
    }


    public void AddWidget(Widget widget)
    {
        widget.Id = Guid.NewGuid();
        widget.State = true;
        settingModel.Widgets.Add(widget);
        save();
    }
    
}