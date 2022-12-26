using WYW.UI.Commands;
using WYW.UI.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WYW.UI.Controls
{
    public class MessageBoxControl : Control
    {
        private bool isRemoved = false;
        public MessageBoxControl()
        {
            CommandBindings.Add(new CommandBinding(CustomCommand.RemoveCommand, ManulRemove));
        }
        public MessageBoxControl(string message, MessageBoxImage icon, bool isAutoRemove, int keepTime, string imageText = null, string imageColor = "#ffffff") : this()
        {
            Message = message;
            if (isAutoRemove)
            {
                AutoRemove(keepTime);
            }
            switch (icon)
            {
                case MessageBoxImage.None:
                    HasImage = false;
                    break;
                case MessageBoxImage.Sucess:
                    ImageText = "\uf058";
                    ImageBackground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF2DB84D");
                    break;
                case MessageBoxImage.Question:
                    ImageText = "\uf059";
                    ImageBackground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF4399DA");
                    break;
                case MessageBoxImage.Warning:
                    ImageText = "\uf071";
                    ImageBackground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFE9AF20");
                    break;
                case MessageBoxImage.Error:
                    ImageText = "\uf057";
                    ImageBackground = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffe81123");
                    break;
                case MessageBoxImage.Custom:
                    if (string.IsNullOrEmpty(imageText))
                    {
                        HasImage = false;
                    }
                    ImageText = imageText;
                    try
                    {
                        ImageBackground = (SolidColorBrush)new BrushConverter().ConvertFrom(imageColor);
                    }
                    catch
                    {
                        ImageBackground = Brushes.Black;
                    }
                    break;
            }
        }
        #region  属性

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageBoxControl), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty ImageTextProperty =
            DependencyProperty.Register("ImageText", typeof(string), typeof(MessageBoxControl), new PropertyMetadata(null));
        public static readonly DependencyProperty ImageBackgroundProperty =
            DependencyProperty.Register("ImageBackground", typeof(SolidColorBrush), typeof(MessageBoxControl), new PropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty HasImageProperty =
            DependencyProperty.Register("HasImage", typeof(bool), typeof(MessageBoxControl), new PropertyMetadata(true));


        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set
            {
                SetValue(MessageProperty, $"{value}");
            }
        }

        public string ImageText
        {
            get { return (string)GetValue(ImageTextProperty); }
            set { SetValue(ImageTextProperty, value); }
        }
        public SolidColorBrush ImageBackground
        {
            get { return (SolidColorBrush)GetValue(ImageBackgroundProperty); }
            set { SetValue(ImageBackgroundProperty, value); }
        }

        public bool HasImage
        {
            get { return (bool)GetValue(HasImageProperty); }
            set { SetValue(HasImageProperty, value); }
        }



        #endregion

        #region  静态属性
        /// <summary>
        /// 最大显示的消息框数量，如果超过该数目，则自动移除较早的消息框
        /// </summary>
        public static int MaxItemsCount { get; set; } = 5;

        public static CornerPlacement Placement { get; set; }= CornerPlacement.BottomRight;

        #endregion

        #region  静态方法

        /// <summary>
        /// 成功信息，5s后自动关闭
        /// </summary>
        /// <param name="message"></param>
        public static void Success(string message)
        {
            Show(message, MessageBoxImage.Sucess, true);
        }
        public static void Custom(string message, string imageText = null, string imageColor = "#ffffff")
        {
            Show(message, MessageBoxImage.Custom, true,5,imageText,imageColor);
        }
        /// <summary>
        /// 警告信息，需要手动关闭
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            Show(message, MessageBoxImage.Warning, false);
        }
        /// <summary>
        /// 错误信息，需要手动关闭
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            Show(message, MessageBoxImage.Error, false);
        }
        public static void Clear()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var container = GetContainer();
                if (container != null)
                {
                    container.Children.Clear();
                }
            }));
        }

        private static Panel CreateContainer()
        {

            Panel container = null;
            FrameworkElement element = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (element == null)
            {
                element = Application.Current.MainWindow;
            }
            var decorator = VisualHelper.FindVisualChild<AdornerDecorator>(element);
            if (decorator != null)
            {

                var layer = decorator.AdornerLayer;
                if (layer != null)
                {
                    var adorners = VisualHelper.FindVisualChildren<Adorner>(layer).ToArray();
                    // 如果没有SampleAdorner，则添加一个新的SampleAdorner
                    if (adorners.Count() == 0)
                    {
                        container = new StackPanel
                        {
                            Background = Brushes.Transparent,
                            CanVerticallyScroll = true,
                        };
                        switch(Placement)
                        {
                            case CornerPlacement.TopLeft:
                                container.VerticalAlignment = VerticalAlignment.Top;
                                container.HorizontalAlignment = HorizontalAlignment.Left;
                                break;
                            case CornerPlacement.TopRight:
                                container.VerticalAlignment = VerticalAlignment.Top;
                                container.HorizontalAlignment = HorizontalAlignment.Right;
                                break;
                            case CornerPlacement.BottomLeft:
                                container.VerticalAlignment = VerticalAlignment.Bottom;
                                container.HorizontalAlignment = HorizontalAlignment.Left;
                                break;
                            case CornerPlacement.BottomRight:
                                container.VerticalAlignment = VerticalAlignment.Bottom;
                                container.HorizontalAlignment = HorizontalAlignment.Right;
                                break;
                        }
                        SampleAdorner adorner = new SampleAdorner(layer)
                        {
                            Child = container,
                        };
                        layer.Add(adorner);
                    }
                    else
                    {
                        container = VisualHelper.FindVisualChild<StackPanel>(adorners[0]);
                        // 根据Placement修改StackPanel的位置
                        switch (Placement)
                        {
                            case CornerPlacement.TopLeft:
                                container.VerticalAlignment = VerticalAlignment.Top;
                                container.HorizontalAlignment = HorizontalAlignment.Left;
                                break;
                            case CornerPlacement.TopRight:
                                container.VerticalAlignment = VerticalAlignment.Top;
                                container.HorizontalAlignment = HorizontalAlignment.Right;
                                break;
                            case CornerPlacement.BottomLeft:
                                container.VerticalAlignment = VerticalAlignment.Bottom;
                                container.HorizontalAlignment = HorizontalAlignment.Left;
                                break;
                            case CornerPlacement.BottomRight:
                                container.VerticalAlignment = VerticalAlignment.Bottom;
                                container.HorizontalAlignment = HorizontalAlignment.Right;
                                break;
                        }
                    }
                }
            }
            return container;
        }
        private static Panel GetContainer()
        {
            Panel panel = null;
            FrameworkElement element = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var decorator = VisualHelper.FindVisualChild<AdornerDecorator>(element);
            if (decorator != null)
            {
                var layer = decorator.AdornerLayer;
                if (layer != null)
                {
                    var adorners = VisualHelper.FindVisualChildren<Adorner>(layer).ToArray();
                    if (adorners.Count() == 1)
                    {
                        panel = VisualHelper.FindVisualChild<Panel>(adorners[0]);
                    }
                }
            }
            return panel;
        }
        private static void Show(string messageBoxText, MessageBoxImage icon, bool isAutoRemove, int keepTime = 5, string imageText = null, string imageColor = "#ffffff")
        {

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                var container = CreateContainer();
                if (container != null)
                {
                    if (container.Children.Count >= MaxItemsCount)
                    {
                        container.Children.RemoveAt(0);
                    }
                    container.Children.Add(new MessageBoxControl(messageBoxText, icon, isAutoRemove, keepTime,imageText,imageColor));
                }
            }));

        }
        #endregion

        private void ManulRemove(object sender, ExecutedRoutedEventArgs e)
        {
            var container = GetContainer();
            if (container != null)
            {
                container.Children.Remove(this);
                isRemoved = true;
            }
        }
        private void AutoRemove(int keepTime)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                DateTime startTime = DateTime.Now;
                while ((DateTime.Now - startTime).TotalSeconds < keepTime)
                {
                    if (isRemoved)
                        return;
                    Thread.Sleep(100);
                }
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    ManulRemove(null, null);
                }));
            });
        }
    }
}
