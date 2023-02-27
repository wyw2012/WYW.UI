using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace WYW.UI.Controls
{
    public class CircleMenuItem : HeaderedItemsControl
    {
        public event RoutedEventHandler Click;

        #region 依赖属性
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CircleMenuItem), new PropertyMetadata(default(ICommand)));
        public static readonly DependencyProperty SectorAngleProperty =
            DependencyProperty.Register("SectorAngle", typeof(double), typeof(CircleMenuItem), new PropertyMetadata(90.0));
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(CircleMenuItem), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty IsAutoFitSectorAngleProperty =
            DependencyProperty.Register("IsAutoFitSectorAngle", typeof(bool), typeof(CircleMenuItem), new PropertyMetadata(true));
        public static readonly DependencyProperty IsPressedProperty =
            DependencyProperty.Register("IsPressed", typeof(bool), typeof(CircleMenuItem), new PropertyMetadata(false));


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// 子菜单显示的图形
        /// </summary>
        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        /// <summary>
        /// 扇形角度，仅在IsAutoFitSectorAngle=false有效
        /// </summary>
        public double SectorAngle
        {
            get { return (double)GetValue(SectorAngleProperty); }
            set { SetValue(SectorAngleProperty, value); }
        }


        /// <summary>
        /// 自动适配扇形角度，如果为true，则属性SectorAngle值无效
        /// </summary>
        public bool IsAutoFitSectorAngle
        {
            get { return (bool)GetValue(IsAutoFitSectorAngleProperty); }
            set { SetValue(IsAutoFitSectorAngleProperty, value); }
        }



        public bool IsPressed
        {
            get { return (bool)GetValue(IsPressedProperty); }
            protected set { SetValue(IsPressedProperty, value); }
        }





        #endregion

        public void OnClick()
        {
            IsPressed = true;
            if (Command != null && Command.CanExecute(null))
            {
                Command.Execute(Header);
            }

            if (Click != null)
            {
                Click(this, new RoutedEventArgs());
            }
        }
    }
}
