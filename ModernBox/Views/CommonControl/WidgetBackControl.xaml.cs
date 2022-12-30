// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ModernBox.Views.CommonControl;
public sealed partial class WidgetBackControl : UserControl
{
    public WidgetBackControl()
    {
        this.InitializeComponent();
    }
    private void btnSaveWidgetConfig_Click(object sender, RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send<String, string>("Save","WidgetSettingMessage");
    }

    private void btnCalcelConfig_Click(object sender, RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send<String, string>("Cancel", "WidgetSettingMessage");
    }
}
