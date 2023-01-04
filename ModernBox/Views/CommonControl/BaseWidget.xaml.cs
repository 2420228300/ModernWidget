// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using ModernBox.Contracts.Services;
using ModernBox.Models;

namespace ModernBox.Views.CommonControl
{
    public sealed partial class BaseWidget : UserControl
    {
        public BaseWidget()
        {
            this.InitializeComponent();
            this.DataContext = this;
            WeakReferenceMessenger.Default.Register<String, String>(this, "WidgetSettingMessage", (r,e) =>
            {
                try
                {
                    switch (e)
                    {
                        case "Save":
                            if (ContentFrame.BackStackDepth > 0)
                            {
                                ContentFrame.GoBack();
                            }
                            break;
                        case "Cancel":

                            if (ContentFrame.BackStackDepth > 0)
                            {
                                ContentFrame.GoBack();
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            });
        }


        public Guid Id
        {
            get => (Guid)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public Boolean State
        {
            get => (Boolean)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        public Object? CacheWidget
        {
            get; set;
        }

        public Type WidgetContent
        {
            get => (Type)GetValue(WidgetContentProperty);
            set => SetValue(WidgetContentProperty, value);
        }

        public Type WidgetConfigContent
        {
            get => (Type)GetValue(WidgetConfigContentProperty);
            set => SetValue(WidgetConfigContentProperty, value);
        }

        public String? WidgetName
        {
            get => (String)GetValue(WidgetNameProperty);
            set => SetValue(WidgetNameProperty, value);
        }

        public Uri? WidgetIcon
        {
            get => (Uri)GetValue(WidgetIconProperty);
            set => SetValue(WidgetIconProperty, value);
        }

        public String? ClassName
        {
            get => (String)GetValue(ClassNameProperty);
            set => SetValue(ClassNameProperty, value);
        }

        public WidgetSize WidgetSize
        {
            get => (WidgetSize)GetValue(WidgetSizeProperty);
            set => SetValue(WidgetSizeProperty, value);
        }

        public Visibility HasTitleBar
        {
            get => (Visibility)GetValue(HasTitleBarProperty);
            set => SetValue(HasTitleBarProperty, value);
        }

        public Thickness WidgetPadding
        {
            get => (Thickness)GetValue(WidgetPaddingProperty);
            set => SetValue(WidgetPaddingProperty, value);
        }

        public static readonly DependencyProperty IdProperty =
          DependencyProperty.Register("Id", typeof(Guid), typeof(BaseWidget), new PropertyMetadata(Guid.Empty));

        public static readonly DependencyProperty StateProperty =
         DependencyProperty.Register("State", typeof(Boolean), typeof(BaseWidget), new PropertyMetadata(false));

        public static readonly DependencyProperty ClassNameProperty =
          DependencyProperty.Register("ClassName", typeof(String), typeof(BaseWidget), new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty WidgetIconProperty =
            DependencyProperty.Register("WidgetIcon", typeof(Uri), typeof(BaseWidget), new PropertyMetadata(null));

        public static readonly DependencyProperty WidgetNameProperty =
            DependencyProperty.Register("WidgetName", typeof(String), typeof(BaseWidget), new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty WidgetContentProperty =
          DependencyProperty.Register("WidgetContent", typeof(Type), typeof(BaseWidget), new PropertyMetadata(null));

        public static readonly DependencyProperty WidgetConfigContentProperty =
        DependencyProperty.Register("WidgetConfigContent", typeof(Type), typeof(BaseWidget), new PropertyMetadata(null));

        public static readonly DependencyProperty WidgetSizeProperty =
            DependencyProperty.Register("WidgetSize", typeof(Enum), typeof(BaseWidget), new PropertyMetadata(default(BaseWidget)));

        public static readonly DependencyProperty HasTitleBarProperty =
           DependencyProperty.Register("HasTitleBar", typeof(Visibility), typeof(BaseWidget), new PropertyMetadata(Visibility.Visible));
        
        public static readonly DependencyProperty WidgetPaddingProperty =
           DependencyProperty.Register("WidgetPadding", typeof(Thickness), typeof(BaseWidget), new PropertyMetadata(new Thickness(8)));
        private void btn_small_Click(object sender, RoutedEventArgs e)
        {

        
            //
            this.WidgetSize = Models.WidgetSize.Small;  
            WeakReferenceMessenger.Default.Send<String, String>(this.WidgetSize.ToString(), "RefreshWidgets");
            refreshSvContentHeight();
        }

        private void btn_middle_Click(object sender, RoutedEventArgs e)
        {
           
            this.WidgetSize = Models.WidgetSize.Middle;
            WeakReferenceMessenger.Default.Send<String, String>(this.WidgetSize.ToString(), "RefreshWidgets");
            refreshSvContentHeight();
        }

        private void btn_big_Click(object sender, RoutedEventArgs e)
        {
            
            this.WidgetSize = Models.WidgetSize.Big;
            WeakReferenceMessenger.Default.Send<String, String>(this.WidgetSize.ToString(), "RefreshWidgets");
            refreshSvContentHeight();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (WidgetConfigContent!=null)
            {
                ContentFrame.Navigate(this.WidgetConfigContent, Id, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            if (Id!=Guid.Empty&&State == true)
            {
                this.State = false;
                App.GetService<ISettingsDataService>().RemoveWidget(Id);
                WeakReferenceMessenger.Default.Send<String, String>("RemoveWidget", "RefreshWidgets");
            }
        }

        private void ContentFrame_Loading(FrameworkElement sender, object args)
        {
            ContentFrame.Navigate(this.WidgetContent, WidgetSize, new DrillInNavigationTransitionInfo());
            
        }

        private void btn_hideOpenBar_Click(object sender, RoutedEventArgs e)
        {
            this.HasTitleBar = this.HasTitleBar == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            WeakReferenceMessenger.Default.Send<String, String>("HideTitleBar", "RefreshWidgets");
        }

        private void SV_Content_Loading(FrameworkElement sender, object args)
        {
            refreshSvContentHeight();
        }

        private void SV_Content_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            refreshSvContentHeight();
        }
        private void refreshSvContentHeight()
        {
            var sv_contentHeight = calculateRealHeight();
            SV_Content.Height = sv_contentHeight;
        }

        public Double calculateRealHeight()
        {
            var stackPanelHeight = this.stackPanel.Height;
            if (this.HasTitleBar == Visibility.Visible)
            {
                return stackPanelHeight - 48;
            }
            return stackPanelHeight;
        }
    }
}