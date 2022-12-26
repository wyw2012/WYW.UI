using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WYW.UI.Controls
{
    /// <summary>
    /// 状态灯，已连接用绿色表示；未连接用红色表示；null用灰色表示
    /// </summary>
    public class StatusLight:Control
    {
        public static readonly DependencyProperty IsConnectedProperty =
         DependencyProperty.Register("IsConnected", typeof(bool?), typeof(StatusLight), new PropertyMetadata(default(bool?)));


        public static readonly DependencyProperty TextProperty =
          DependencyProperty.Register("Text", typeof(string), typeof(StatusLight), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool? IsConnected
        {
            get { return (bool?)GetValue(IsConnectedProperty); }
            set { SetValue(IsConnectedProperty, value); }
        }

    }
}
