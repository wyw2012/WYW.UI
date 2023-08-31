using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WYW.UI.Controls
{
    /// <summary>
    /// 状态灯
    /// </summary>
    public class StatusLight : Control
    {
        public static readonly DependencyProperty TextProperty =
          DependencyProperty.Register("Text", typeof(string), typeof(StatusLight), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty PopupTextProperty =
          DependencyProperty.Register("PopupText", typeof(string), typeof(StatusLight), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty LightColorProperty =
            DependencyProperty.Register("LightColor", typeof(Brush), typeof(StatusLight), new PropertyMetadata(Brushes.Gray));
        public static readonly DependencyProperty IsShowPopupProperty =
            DependencyProperty.Register("IsShowPopup", typeof(bool), typeof(StatusLight), new PropertyMetadata(false));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Brush LightColor
        {
            get { return (Brush)GetValue(LightColorProperty); }
            set { SetValue(LightColorProperty, value); }
        }


        public string PopupText
        {
            get { return (string)GetValue(PopupTextProperty); }
            set { SetValue(PopupTextProperty, value); }
        }


        public bool IsShowPopup
        {
            get { return (bool)GetValue(IsShowPopupProperty); }
            set { SetValue(IsShowPopupProperty, value); }
        }

    }


}
