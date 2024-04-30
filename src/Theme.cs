using System;
using System.Windows;
using System.Windows.Input;

namespace WYW.UI
{
    public class Theme
    {
        /// <summary>
        /// 根据屏幕分辨率自动调整控件的默认尺寸
        /// </summary>
        /// <param name="mode">触屏模式</param>
        public static void AutoFitResolution(TouchMode mode = TouchMode.Auto)
        {
            var zoomRatio = SystemParameters.PrimaryScreenHeight / 1080.0;
            if ((mode == TouchMode.Auto && HasTouchInput()) || mode == TouchMode.Touch)
            {
                Application.Current.Resources["TextElement.Height"] = Math.Round(zoomRatio * 40);
                Application.Current.Resources["Item.Height"] = Math.Round(zoomRatio * 40);
                Application.Current.Resources["Header.Height"] = Math.Round(zoomRatio * 45);
                Application.Current.Resources["TabItem.Height"] = Math.Round(zoomRatio * 70);
            }
            else
            {
                Application.Current.Resources["TextElement.Height"] = Math.Round(zoomRatio * 36);
                Application.Current.Resources["Item.Height"] = Math.Round(zoomRatio * 36);
                Application.Current.Resources["Header.Height"] = Math.Round(zoomRatio * 40);
                Application.Current.Resources["TabItem.Height"] = Math.Round(zoomRatio * 60);

            }
            Application.Current.Resources["DefaultFontSize"] = Math.Round(zoomRatio * 18);
            Application.Current.Resources["DefaultLEDFontSize"] = Math.Round(zoomRatio * 32);
            Application.Current.Resources["Panel.Space"] = Math.Round(zoomRatio * 20);
            Application.Current.Resources["Button.Width"] = Math.Round(zoomRatio * 140);
            Application.Current.Resources["Button.Height"] = Math.Round(zoomRatio * 70);
            Application.Current.Resources["TabItem.Width"] = Math.Round(zoomRatio * 200);
            Application.Current.Resources["CaptionHeight"] = Math.Round(zoomRatio * 40);
            Application.Current.Resources["RingThickness"] = Math.Round(zoomRatio * 10);
            Application.Current.Resources["DataGridItem.Width"] = Math.Round(zoomRatio * 120);
            Application.Current.Resources["Calendar.Height"] = Math.Round(zoomRatio * 300);
            Application.Current.Resources["Calendar.Width"] = Math.Round(zoomRatio * 300);

            Application.Current.Resources["Panel.Padding"] = new Thickness()
            {
                Left = Math.Round(20.0 * zoomRatio),
                Top = Math.Round(10.0 * zoomRatio),
                Right = Math.Round(20.0 * zoomRatio),
                Bottom = Math.Round(10.0 * zoomRatio),
            };
            Application.Current.Resources["Panel.Margin"] = new Thickness()
            {
                Left = Math.Round(20.0 * zoomRatio),
                Top = Math.Round(20.0 * zoomRatio),
                Right = Math.Round(20.0 * zoomRatio),
                Bottom = Math.Round(20.0 * zoomRatio),
            };
            Application.Current.Resources["Control.Margin"] = new Thickness()
            {
                Left = Math.Round(5.0 * zoomRatio),
                Top = Math.Round(5.0 * zoomRatio),
                Right = Math.Round(5.0 * zoomRatio),
                Bottom = Math.Round(5.0 * zoomRatio),
            };
            Application.Current.Resources["Control.HorizontalMargin"] = new Thickness()
            {
                Left = Math.Round(5.0 * zoomRatio),
                Top = 0,
                Right = Math.Round(5.0 * zoomRatio),
                Bottom = 0,
            };

            Application.Current.Resources["Control.VerticalMargin"] = new Thickness()
            {
                Left = 0,
                Top = Math.Round(5.0 * zoomRatio),
                Right = 0,
                Bottom = Math.Round(5.0 * zoomRatio),
            };
            Application.Current.Resources["Control.LeftMargin"] = new Thickness()
            {
                Left = Math.Round(5.0 * zoomRatio),
            };
            Application.Current.Resources["Control.RightMargin"] = new Thickness()
            {
                Right = Math.Round(5.0 * zoomRatio),
            };
            Application.Current.Resources["Text.Margin"] = new Thickness()
            {
                Left = Math.Round(5.0 * zoomRatio),
                Top = Math.Round(5.0 * zoomRatio),
                Right = Math.Round(5.0 * zoomRatio),
                Bottom = Math.Round(5.0 * zoomRatio),
            };
            Application.Current.Resources["Text.HorizontalMargin"] = new Thickness()
            {
                Left = Math.Round(5.0 * zoomRatio),
                Top = 0,
                Right = Math.Round(5.0 * zoomRatio),
                Bottom = 0,
            };

            Application.Current.Resources["Text.VerticalMargin"] = new Thickness()
            {
                Left = 0,
                Top = Math.Round(3.0 * zoomRatio),
                Right = 0,
                Bottom = Math.Round(3.0 * zoomRatio),
            };
            Application.Current.Resources["Control.CornerRadius"] = new CornerRadius()
            {
                TopLeft = Math.Round(5.0 * zoomRatio),
                TopRight = Math.Round(5.0 * zoomRatio),
                BottomRight = Math.Round(5.0 * zoomRatio),
                BottomLeft = Math.Round(5.0 * zoomRatio),
         
            };

            Application.Current.Resources["Control.CornerRadiusTop"] = new CornerRadius()
            {
                TopLeft = Math.Round(5.0 * zoomRatio),
                TopRight = Math.Round(5.0 * zoomRatio),
            };

            Application.Current.Resources["Control.CornerRadiusBottom"] = new CornerRadius()
            {
    
                BottomLeft = Math.Round(5.0 * zoomRatio),
                BottomRight = Math.Round(5.0 * zoomRatio),
            };

            Application.Current.Resources["Control.CornerRadiusLeft"] = new CornerRadius()
            {
                TopLeft = Math.Round(5.0 * zoomRatio),
                BottomLeft = Math.Round(5.0 * zoomRatio),
            };

            Application.Current.Resources["Control.CornerRadiusRight"] = new CornerRadius()
            {
    
                TopRight = Math.Round(5.0 * zoomRatio),
                BottomRight = Math.Round(5.0 * zoomRatio),
            };
        }

        /// <summary>
        /// 设置主题颜色
        /// </summary>
        /// <param name="theme">主题颜色</param>
        public static void SetTheme(ThemeColor theme)
        {
            switch (theme)
            {
                case ThemeColor.Dark:
                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/WYW.UI;component/Themes/Basic/Colors/DarkColor.xaml", UriKind.Absolute) });
                    break;
            }
        }
        private static bool HasTouchInput()
        {
            foreach (TabletDevice tabletDevice in Tablet.TabletDevices)
            {
                //Only detect if it is a touch Screen not how many touches (i.e. Single touch or Multi-touch)
                if (tabletDevice.Type == TabletDeviceType.Touch)
                    return true;
            }

            return false;
        }
    }

    /// <summary>
    /// 触屏模式
    /// </summary>
    public enum TouchMode
    {
        /// <summary>
        /// 自动识别
        /// </summary>
        Auto,
        /// <summary>
        /// 非触屏模式
        /// </summary>
        NoTouch,
        /// <summary>
        /// 触屏模式
        /// </summary>
        Touch,
    }
    /// <summary>
    /// 主题颜色
    /// </summary>
    public enum ThemeColor
    {
        /// <summary>
        /// 浅色，默认值
        /// </summary>
        Light,
        /// <summary>
        /// 深色
        /// </summary>
        Dark,
    }
}
