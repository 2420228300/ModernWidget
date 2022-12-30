// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using ModernBox.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ModernBox.Views.CommonControl
{
    public sealed partial class BaseWidget : UserControl
    {
        public BaseWidget()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public Object? CacheWidget
        {
            get;set;
        }

        public Object? WidgetContent
        {
            get => (Object)GetValue(WidgetContentProperty);
            set => SetValue(WidgetContentProperty, value);
        }

        public Object? WidgetConfigContent
        {
            get;
            set;
        }

        public String? WidgetName
        {
            get; set;
        }

        public String? WidgetIcon
        {
            get; set;
        }

        public String? ClassName
        {
            get; set;
        }

        public WidgetSize? WidgetSize
        {
            get; set;
        }

        

        public static readonly DependencyProperty ClassNameProperty =
          DependencyProperty.Register("ClassName", typeof(String), typeof(BaseWidget), new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty WidgetIconProperty =
            DependencyProperty.Register("WidgetIcon", typeof(String), typeof(BaseWidget), new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty WidgetNameProperty =
            DependencyProperty.Register("WidgetName", typeof(String), typeof(BaseWidget), new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty WidgetContentProperty =
          DependencyProperty.Register("WidgetContent", typeof(object), typeof(BaseWidget), new PropertyMetadata(null));

        public static readonly DependencyProperty WidgetConfigContentProperty =
        DependencyProperty.Register("WidgetConfigContent", typeof(object), typeof(BaseWidget), new PropertyMetadata(null));

        public static readonly DependencyProperty WidgetSizeProperty =
            DependencyProperty.Register("WidgetSize", typeof(Enum), typeof(BaseWidget), new PropertyMetadata(0));



        private void btn_small_Click(object sender, RoutedEventArgs e)
        {
            btn_middle.IsChecked = false;
            btn_big.IsChecked = false;
            btn_small.IsChecked = true;
            //
        }

        private void btn_middle_Click(object sender, RoutedEventArgs e)
        {
            btn_small.IsChecked = false;
            btn_big.IsChecked = false;
            btn_middle.IsChecked = true;
        }

        private void btn_big_Click(object sender, RoutedEventArgs e)
        {
            btn_middle.IsChecked = false;
            btn_small.IsChecked = false;
            btn_big.IsChecked = true;
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {

            //this.CacheWidget = WidgetContent;
            //this.WidgetContent = WidgetConfigContent;
            
            ContentFrame.Navigate(
                this.WidgetConfigContent.GetType(),
                null, 
                new SuppressNavigationTransitionInfo());
        }

        private void btn_remove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}