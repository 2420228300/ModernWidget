using System.Runtime.InteropServices;
using System.Windows.Input;
using GlobalHotKey;
using Microsoft.UI.Xaml;
using ModernBox.Contracts.Services;
using ModernBox.Helpers;
using ModernBox.Models;
using WinRT; // required to support Window.As<ICompositionSupportsSystemBackdrop>()

namespace ModernBox;

public delegate void ChangeWindowHandler(ChangeWindowType changeWindowType, int value);

public sealed partial class MainWindow : WindowEx
{
    private WindowsSystemDispatcherQueueHelper m_wsdqHelper;

    private Microsoft.UI.Composition.SystemBackdrops.MicaController m_micaController;
    private Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController m_acrylicController;
    private Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration m_configurationSource;

    private static event ChangeWindowHandler m_changeWindowEvent;

    private ISettingsDataService SettingsDataService;
    private bool state = true;

    public MainWindow()
    {
        InitializeComponent();

        #region set hot  key

        var hotKeyManager = new HotKeyManager();
        var hotKey = hotKeyManager.Register(Key.Space, ModifierKeys.Control);
        hotKeyManager.KeyPressed += HotKeyManager_KeyPressed;

        #endregion set hot  key
       
        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
        Title = "AppDisplayName".GetLocalized();
        this.SettingsDataService = App.GetService<ISettingsDataService>();
        this.SettingsDataService.initSettings();
        //Remove the title bar
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

        Microsoft.UI.WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
        Microsoft.UI.Windowing.AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
        var presenter = appWindow.Presenter as Microsoft.UI.Windowing.OverlappedPresenter;
        presenter?.SetBorderAndTitleBar(true, false);
        // Remove white border but rounded corner stops working
        var styleCurrentWindowStandard = NativeMethods.GetWindowLongPtr(this.GetWindowHandle(), (int)NativeMethods.GWL.GWL_STYLE);
        var styleNewWindowStandard = styleCurrentWindowStandard.ToInt64() & ~((long)NativeMethods.WindowStyles.WS_THICKFRAME);
        if (NativeMethods.SetWindowLongPtr(new HandleRef(null, this.GetWindowHandle()), (int)NativeMethods.GWL.GWL_STYLE, (IntPtr)styleNewWindowStandard) == IntPtr.Zero)
        {
            //fail
        }

        if (IsWindows11_OrGreater)
        {
            //Bring back win11 rounded corner
            var attribute = NativeMethods.DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = NativeMethods.DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            NativeMethods.DwmSetWindowAttribute(this.GetWindowHandle(), attribute, ref preference, sizeof(uint));

            if (NativeMethods.GetCursorPos(out NativeMethods.POINT P))
            {
                //Force redraw and pos
                NativeMethods.SetWindowPos(this.GetWindowHandle(), -1, P.X + 25, P.Y + 25, SettingsDataService.getSettings().Width, SettingsDataService.getSettings().Height, (int)NativeMethods.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
        }
        // set window margin
        SetPosAndForeground(SettingsDataService.getSettings().MarginLeft, SettingsDataService.getSettings().MarginTop);
        // set default theme
        m_wsdqHelper = new WindowsSystemDispatcherQueueHelper();
        m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController();
        //SetBackdrop(BackdropType.DesktopAcrylic);
        TrySetMicaBackdrop(true);
        //TrySetAcrylicBackdrop();
        this.Content = null;

        //
        m_changeWindowEvent += MainWindow_m_changeWindowEvent;
    }

    private void HotKeyManager_KeyPressed(object? sender, KeyPressedEventArgs e)
    {
        if (e.HotKey.Key == Key.Space)
        {
            if (state)
            {
                new Task(() =>
                {
                    var settingModel = SettingsDataService.getSettings();
                    for (int i = settingModel.MarginLeft * -1; i >= settingModel.Width * -1 - settingModel.MarginLeft; i--)
                    {
                        NativeMethods.SetWindowPos(this.GetWindowHandle(), -1, i, settingModel.MarginTop, settingModel.Width, settingModel.Height, (int)NativeMethods.SetWindowPosFlags.SWP_SHOWWINDOW);
                    }
                }).Start();
                state = false;

            }
            else
            {
                new Task(() =>
                {
                    var settingModel = SettingsDataService.getSettings();
                    for (int i = settingModel.Width * -1 - settingModel.MarginLeft; i <= settingModel.MarginLeft; i++)
                    {
                        NativeMethods.SetWindowPos(this.GetWindowHandle(), -1, i, settingModel.MarginTop, settingModel.Width, settingModel.Height, (int)NativeMethods.SetWindowPosFlags.SWP_SHOWWINDOW);
                    }
                }).Start();
                state = true;
                m_configurationSource.IsInputActive = true;
            }
          
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="changeWindowType"></param>
    /// <param name="value"></param>
    private void MainWindow_m_changeWindowEvent(ChangeWindowType changeWindowType, int value)
    {
        var settingModel = SettingsDataService.getSettings();
        switch (changeWindowType)
        {
            case ChangeWindowType.Width:
                NativeMethods.SetWindowPos(this.GetWindowHandle(), -1, 10, 10, value, SettingsDataService.getSettings().Height, (int)NativeMethods.SetWindowPosFlags.SWP_SHOWWINDOW);
                settingModel.Width = value;
                break;

            case ChangeWindowType.Height:
                NativeMethods.SetWindowPos(this.GetWindowHandle(), -1, 10, 10, SettingsDataService.getSettings().Width, value, (int)NativeMethods.SetWindowPosFlags.SWP_SHOWWINDOW);
                settingModel.Height = value;
                break;

            case ChangeWindowType.MarginLeft:
                NativeMethods.SetWindowPos(this.GetWindowHandle(), -1, value, SettingsDataService.getSettings().MarginTop, SettingsDataService.getSettings().Width, SettingsDataService.getSettings().Height, (int)NativeMethods.SetWindowPosFlags.SWP_SHOWWINDOW);
                settingModel.MarginLeft = value;
                break;

            case ChangeWindowType.MarginTop:
                NativeMethods.SetWindowPos(this.GetWindowHandle(), -1, SettingsDataService.getSettings().MarginLeft, value, SettingsDataService.getSettings().Width, SettingsDataService.getSettings().Height, (int)NativeMethods.SetWindowPosFlags.SWP_SHOWWINDOW);
                settingModel.MarginTop = value;
                break;

            case ChangeWindowType.Material:
                ChangeMaterial(value);
                break;
        }
        SettingsDataService.setSettings(settingModel);
        SettingsDataService.save();
    }

    public static void ChangeWindow(ChangeWindowType changeWindowType, int value)
    {
        m_changeWindowEvent.Invoke(changeWindowType, value);
    }

    public void SetPosAndForeground(int x, int y)
    {
        NativeMethods.SetWindowPos(this.GetWindowHandle(), -1, x, y, 0, 0, (int)NativeMethods.SetWindowPosFlags.SWP_NOSIZE);
    }

    public void ChangeMaterial(int index)
    {
        switch (index)
        {
            case 0:

                TrySetMicaBackdrop(true);
                break;

            case 1:
                TrySetAcrylicBackdrop();
                break;

            case 2:
                SetBackdrop(BackdropType.DefaultColor);
                break;
        }
    }

    public static bool IsWindows11_OrGreater => Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= 22000;

    public void SetBackdrop(BackdropType type)
    {
        // Reset to default color. If the requested type is supported, we'll update to that.
        // Note: This sample completely removes any previous controller to reset to the default
        //       state. This is done so this sample can show what is expected to be the most
        //       common pattern of an app simply choosing one controller type which it sets at
        //       startup. If an app wants to toggle between Mica and Acrylic it could simply
        //       call RemoveSystemBackdropTarget() on the old controller and then setup the new
        //       controller, reusing any existing m_configurationSource and Activated/Closed
        //       event handlers.
        if (m_micaController != null)
        {
            m_micaController.Dispose();
            m_micaController = null;
        }
        if (m_acrylicController != null)
        {
            m_acrylicController.Dispose();
            m_acrylicController = null;
        }
        this.Activated -= Window_Activated;

        this.Closed -= Window_Closed;
        var content = this.Content;
        ((FrameworkElement)this.Content).ActualThemeChanged -= Window_ThemeChanged;
        m_configurationSource = null;
    }

    private bool TrySetMicaBackdrop(bool useMicaAlt)
    {
        if (Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported())
        {
            // Hooking up the policy object
            m_configurationSource = new Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration();
            this.Activated += Window_Activated;
            this.Closed += Window_Closed;
            ((FrameworkElement)this.Content).ActualThemeChanged += Window_ThemeChanged;

            // Initial configuration state.
            m_configurationSource.IsInputActive = true;
            SetConfigurationSourceTheme();

            m_micaController = new Microsoft.UI.Composition.SystemBackdrops.MicaController();

            if (useMicaAlt)
            {
                m_micaController.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.BaseAlt;
            }
            else
            {
                m_micaController.Kind = Microsoft.UI.Composition.SystemBackdrops.MicaKind.Base;
            }

            // Enable the system backdrop.
            // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
            m_micaController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
            m_micaController.SetSystemBackdropConfiguration(m_configurationSource);
            return true; // succeeded
        }

        return false; // Mica is not supported on this system
    }

    private bool TrySetAcrylicBackdrop()
    {
        if (Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController.IsSupported())
        {
            // Hooking up the policy object
            m_configurationSource = new Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration();
            this.Activated += Window_Activated;
            this.Closed += Window_Closed;
            ((FrameworkElement)this.Content).ActualThemeChanged += Window_ThemeChanged;

            // Initial configuration state.
            m_configurationSource.IsInputActive = true;
            SetConfigurationSourceTheme();

            m_acrylicController = new Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController();

            // Enable the system backdrop.
            // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
            m_acrylicController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
            m_acrylicController.SetSystemBackdropConfiguration(m_configurationSource);
            return true; // succeeded
        }

        return false; // Acrylic is not supported on this system
    }

    private void Window_Activated(object sender, WindowActivatedEventArgs args)
    {
        m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
    }

    private void Window_Closed(object sender, WindowEventArgs args)
    {
        // Make sure any Mica/Acrylic controller is disposed so it doesn't try to
        // use this closed window.
        if (m_micaController != null)
        {
            m_micaController.Dispose();
            m_micaController = null;
        }
        if (m_acrylicController != null)
        {
            m_acrylicController.Dispose();
            m_acrylicController = null;
        }
        this.Activated -= Window_Activated;
        m_configurationSource = null;
    }

    private void Window_ThemeChanged(FrameworkElement sender, object args)
    {
        if (m_configurationSource != null)
        {
            SetConfigurationSourceTheme();
        }
    }

    private void SetConfigurationSourceTheme()
    {
        switch (((FrameworkElement)this.Content).ActualTheme)
        {
            case ElementTheme.Dark: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
            case ElementTheme.Light: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light; break;
            case ElementTheme.Default: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Default; break;
        }
    }
}