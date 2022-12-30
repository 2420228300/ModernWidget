using ModernBox.Contracts.Services;
using ModernBox.Core.Contracts.Services;
using ModernBox.Helpers;
using ModernBox.Models;
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
            settingModel.Widgets.Add(new Widget()
            {
                Id = Guid.NewGuid(),
                WidgetName = "测试组件",
                WidgetIcon = new Uri("ms-appx:///Assets/WidgetIcons/widgetDefaultIcon.png"),
                State = true,
                ClassName = typeof(TestWidgetIndexPage).FullName,
                WidgetSize = WidgetSize.Big,
            }) ;
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
}