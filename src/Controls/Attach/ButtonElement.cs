using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WYW.UI.Controls.Attach
{
    /// <summary>
    /// Button的附加属性类
    /// </summary>
    public class ButtonElement
    {
        /// <summary>
        /// 图标与文字之间的间距
        /// </summary>
        public static readonly DependencyProperty IcoSpaceProperty
            = DependencyProperty.RegisterAttached("IcoSpace", typeof(int), typeof(ButtonElement), new PropertyMetadata(10));
        /// <summary>
        /// 矩形的圆角半径
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty
            = DependencyProperty.RegisterAttached("CornerRadius", typeof(CornerRadius), typeof(ButtonElement), new PropertyMetadata(default(CornerRadius)));
        /// <summary>
        /// 图标形状
        /// </summary>
        public static readonly DependencyProperty IcoGeometryProperty
            = DependencyProperty.RegisterAttached("IcoGeometry", typeof(Geometry), typeof(ButtonElement), new PropertyMetadata(default(Geometry)));
        /// <summary>
        /// 图标宽度
        /// </summary>
        public static readonly DependencyProperty IcoWidthProperty
            = DependencyProperty.RegisterAttached("IcoWidth", typeof(double), typeof(ButtonElement), new PropertyMetadata(16.0));
        /// <summary>
        /// 图标高度
        /// </summary>
        public static readonly DependencyProperty IcoHeightProperty
            = DependencyProperty.RegisterAttached("IcoHeight", typeof(double), typeof(ButtonElement), new PropertyMetadata(16.0));
        /// <summary>
        /// 文字和图标的排版
        /// </summary>
        public static readonly DependencyProperty OrientationProperty
            = DependencyProperty.RegisterAttached("Orientation", typeof(Orientation), typeof(ButtonElement), new PropertyMetadata(default(Orientation)));
        /// <summary>
        /// 是否有边框
        /// </summary>
        public static readonly DependencyProperty IsNoBorderProperty
            = DependencyProperty.RegisterAttached("IsNoBorder", typeof(bool), typeof(ButtonElement), new PropertyMetadata(default(bool)));

        public static Geometry GetIcoGeometry(DependencyObject obj) => (Geometry)obj.GetValue(IcoGeometryProperty);

        public static void SetIcoGeometry(DependencyObject obj, Geometry value) => obj.SetValue(IcoGeometryProperty, value);

        public static double GetIcoWidth(DependencyObject obj) => (double)obj.GetValue(IcoWidthProperty);

        public static void SetIcoWidth(DependencyObject obj, double value) => obj.SetValue(IcoWidthProperty, value);

        public static double GetIcoHeight(DependencyObject obj) => (double)obj.GetValue(IcoHeightProperty);

        public static void SetIcoHeight(DependencyObject obj, double value) => obj.SetValue(IcoHeightProperty, value);

        public static CornerRadius GetCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(CornerRadiusProperty);

        public static void SetCornerRadius(DependencyObject obj, CornerRadius value) => obj.SetValue(CornerRadiusProperty, value);

        public static int GetIcoSpace(DependencyObject obj) => (int)obj.GetValue(IcoSpaceProperty);

        public static void SetIcoSpace(DependencyObject obj, int value) => obj.SetValue(IcoSpaceProperty, value);

        public static Orientation GetOrientation(DependencyObject obj) => (Orientation)obj.GetValue(OrientationProperty);

        public static void SetOrientation(DependencyObject obj, Orientation value) => obj.SetValue(OrientationProperty, value);

        public static bool GetIsNoBorder(DependencyObject obj) => (bool)obj.GetValue(IsNoBorderProperty);

        public static void SetIsNoBorder(DependencyObject obj, bool value) => obj.SetValue(IsNoBorderProperty, value);

    }
}
