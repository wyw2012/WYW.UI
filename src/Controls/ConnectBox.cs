using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace WYW.UI.Controls
{
    public class ConnectBox : UserControl
    {
        public static readonly DependencyProperty IsConnectedProperty =
            DependencyProperty.Register("IsConnected", typeof(bool), typeof(ConnectBox), new PropertyMetadata(false));

        public static readonly DependencyProperty IconBrushProperty =
            DependencyProperty.Register("IconBrush", typeof(Brush), typeof(ConnectBox),
                new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty ConnectedTextProperty =
            DependencyProperty.Register("ConnectedText", typeof(string), typeof(ConnectBox), new PropertyMetadata("已连接"));

        public static readonly DependencyProperty UnConnectedTextProperty =
            DependencyProperty.Register("UnConnectedText", typeof(string), typeof(ConnectBox),new PropertyMetadata("未连接"));


        public bool IsConnected
        {
            get { return (bool)GetValue(IsConnectedProperty); }
            set { SetValue(IsConnectedProperty, value); }
        }


        public Brush IconBrush
        {
            get { return (Brush)GetValue(IconBrushProperty); }
            set { SetValue(IconBrushProperty, value); }
        }


        public string ConnectedText
        {
            get { return (string)GetValue(ConnectedTextProperty); }
            set { SetValue(ConnectedTextProperty, value); }
        }
        public string UnConnectedText
        {
            get { return (string)GetValue(UnConnectedTextProperty); }
            set { SetValue(UnConnectedTextProperty, value); }
        }
    }
}
