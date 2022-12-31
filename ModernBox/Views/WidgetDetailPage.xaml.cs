// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ModernBox.ViewModels;
using ModernBox.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ModernBox.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WidgetDetailPage : Page
    {
        public WidgetDetailViewModel viewModel
        {
            get; set;
        }
        public WidgetDetailPage()
        {
            this.InitializeComponent();
            viewModel = App.GetService<WidgetDetailViewModel>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Widget widget = e.Parameter as Widget;
            if (widget != null) { 
                TB_WidgetDescription.Text = widget.Description;
                TB_WidgetName.Text = widget.WidgetName;
                TB_WidgetType.Text = widget.WidgetType;
                BI_WidgetCover.UriSource = widget.WidgetIcon;
            }
        }
    }
}
