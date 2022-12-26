using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WYW.UI.Controls.Attach
{
    public class TabItemElement
    {
        public static readonly DependencyProperty IcoGeometryProperty
            = DependencyProperty.RegisterAttached("IcoGeometry", typeof(Geometry), typeof(TabItemElement), new PropertyMetadata(default(Geometry)));
        public static readonly DependencyProperty IcoWidthProperty
            = DependencyProperty.RegisterAttached("IcoWidth", typeof(double), typeof(TabItemElement), new PropertyMetadata(16.0));
        public static readonly DependencyProperty IcoHeightProperty
            = DependencyProperty.RegisterAttached("IcoHeight", typeof(double), typeof(TabItemElement), new PropertyMetadata(16.0));
        public static readonly DependencyProperty HeaderHorizontalAlignmentProperty
         = DependencyProperty.RegisterAttached("HeaderHorizontalAlignment", typeof(HorizontalAlignment), typeof(TabItemElement), new PropertyMetadata(HorizontalAlignment.Stretch));
        public static readonly DependencyProperty HeaderForegroundProperty
            = DependencyProperty.RegisterAttached("HeaderForeground", typeof(Brush), typeof(TabItemElement), new PropertyMetadata(default(Brush)));
        public static Geometry GetIcoGeometry(DependencyObject obj) => (Geometry)obj.GetValue(IcoGeometryProperty);

        public static void SetIcoGeometry(DependencyObject obj, Geometry value) => obj.SetValue(IcoGeometryProperty, value);

        public static double GetIcoWidth(DependencyObject obj) => (double)obj.GetValue(IcoWidthProperty);

        public static void SetIcoWidth(DependencyObject obj, double value) => obj.SetValue(IcoWidthProperty, value);

        public static double GetIcoHeight(DependencyObject obj) => (double)obj.GetValue(IcoHeightProperty);

        public static void SetIcoHeight(DependencyObject obj, double value) => obj.SetValue(IcoHeightProperty, value);

        public static HorizontalAlignment GetHeaderHorizontalAlignment(DependencyObject obj) => (HorizontalAlignment)obj.GetValue(HeaderHorizontalAlignmentProperty);

        public static void SetHeaderHorizontalAlignment(DependencyObject obj, HorizontalAlignment value) => obj.SetValue(HeaderHorizontalAlignmentProperty, value);

        public static Brush GetHeaderForeground(DependencyObject obj) => (Brush)obj.GetValue(HeaderForegroundProperty);

        public static void SetHeaderForeground(DependencyObject obj, Brush value) => obj.SetValue(HeaderForegroundProperty, value);




    }
}
