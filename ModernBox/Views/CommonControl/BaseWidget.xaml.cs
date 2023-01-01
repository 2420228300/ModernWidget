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



        private void btn_small_Click(object sender, RoutedEventArgs e)
        {
            btn_middle.IsChecked = false;
            btn_big.IsChecked = false;
            btn_small.IsChecked = true;
            //
            this.WidgetSize = Models.WidgetSize.Small;  
            WeakReferenceMessenger.Default.Send<String, String>(this.WidgetSize.ToString(), "RefreshWidgets");
        }

        private void btn_middle_Click(object sender, RoutedEventArgs e)
        {
            btn_small.IsChecked = false;
            btn_big.IsChecked = false;
            btn_middle.IsChecked = true;
            this.WidgetSize = Models.WidgetSize.Middle;
            WeakReferenceMessenger.Default.Send<String, String>(this.WidgetSize.ToString(), "RefreshWidgets");
        }

        private void btn_big_Click(object sender, RoutedEventArgs e)
        {
            btn_middle.IsChecked = false;
            btn_small.IsChecked = false;
            btn_big.IsChecked = true;
            this.WidgetSize = Models.WidgetSize.Big;
            WeakReferenceMessenger.Default.Send<String, String>(this.WidgetSize.ToString(), "RefreshWidgets");
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
     
            ContentFrame.Navigate(this.WidgetConfigContent, null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            if (Id!=Guid.Empty&&State == true)
            {
                App.GetService<ISettingsDataService>().RemoveWidget(Id);
                WeakReferenceMessenger.Default.Send<String, String>(this.WidgetSize.ToString(), "RefreshWidgets");
            }
        }

        private void ContentFrame_Loading(FrameworkElement sender, object args)
        {
            ContentFrame.Navigate(this.WidgetContent, null, new DrillInNavigationTransitionInfo());
        }
    }
}