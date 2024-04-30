using WYW.UI.Commands;
using WYW.UI.Common;
using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WYW.UI.Controls
{
    /// <summary>
    /// 消息控件，消息以控件的方式追加到当前激活的窗体中
    /// </summary>
    public class MessageBoxControl : Control
    {

        #region 公开
        /// <summary>
        /// 最大显示的消息框数量，如果超过该数目，则自动移除较早的消息框
        /// </summary>
        public static int MaxItemsCount { get; set; } = 5;

        /// <summary>
        /// 显示时间
        /// </summary>
        public static bool IsShowTime { get; set; } = false;
        /// <summary>
        /// 消息框出现的位置
        /// </summary>
        public static CornerPlacement Placement { get; set; } = CornerPlacement.BottomRight;
        /// <summary>
        /// 成功信息，5s后自动关闭
        /// </summary>
        /// <param name="message">消息正文</param>
        public static void Success(string message)
        {
            Show(message, MessageBoxImage.Sucess, true);
        }
        /// <summary>
        /// 自定义信息
        /// </summary>
        /// <param name="message">消息正文</param>
        /// <param name="imageText">自定义图标内容，FontAwesome字体的Unicode码，例如"\uf118"</param>
        /// <param name="imageColor">自定义图标颜色，例如"#ffffffff"</param>
        public static void Custom(string message, string imageText = null, string imageColor = "#ffffff")
        {
            Show(message, MessageBoxImage.Custom, true, 5, imageText, imageColor);
        }
        /// <summary>
        /// 通知信息，自动关闭
        /// </summary>
        /// <param name="message">消息正文</param>
        public static void Tip(string message)
        {
            Show(message, MessageBoxImage.Tip, true);
        }
        /// <summary>
        /// 警告信息，需要手动关闭
        /// </summary>
        /// <param name="message">消息正文</param>
        public static void Warning(string message)
        {
            Show(message, MessageBoxImage.Warning, false);
        }
        /// <summary>
        /// 错误信息，需要手动关闭
        /// </summary>
        /// <param name="message">消息正文</param>
        public static void Error(string message)
        {
            Show(message, MessageBoxImage.Error, false);
        }
        /// <summary>
        /// 清除所有消息框
        /// </summary>
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
        #endregion

        #region 内部
        private bool isRemoved = false;
        private Panel owner = null;
        #region  依赖属性

        internal static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageBoxControl), new PropertyMetadata(default(string)));
        internal static readonly DependencyProperty ImageTextProperty =
            DependencyProperty.Register("ImageText", typeof(string), typeof(MessageBoxControl), new PropertyMetadata(null));
        internal static readonly DependencyProperty ImageBackgroundProperty =
            DependencyProperty.Register("ImageBackground", typeof(SolidColorBrush), typeof(MessageBoxControl), new PropertyMetadata(Brushes.Black));
        internal static readonly DependencyProperty HasImageProperty =
            DependencyProperty.Register("HasImage", typeof(bool), typeof(MessageBoxControl), new PropertyMetadata(true));


        internal string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set
            {
                SetValue(MessageProperty, $"{value}");
            }
        }

        internal string ImageText
        {
            get { return (string)GetValue(ImageTextProperty); }
            set { SetValue(ImageTextProperty, value); }
        }
        internal SolidColorBrush ImageBackground
        {
            get { return (SolidColorBrush)GetValue(ImageBackgroundProperty); }
            set { SetValue(ImageBackgroundProperty, value); }
        }

        internal bool HasImage
        {
            get { return (bool)GetValue(HasImageProperty); }
            set { SetValue(HasImageProperty, value); }
        }



        #endregion

        #region 构造函数
        private MessageBoxControl()
        {
            CommandBindings.Add(new CommandBinding(CustomCommand.RemoveCommand, ManulRemove));
        }
        private MessageBoxControl(Panel owner, string message, MessageBoxImage icon, bool isAutoRemove, int keepTime, string imageText = null, string imageColor = "#ffffff") : this()
        {
            this.owner = owner;
           
            Message = IsShowTime?$"[{DateTime.Now:HH:mm:ss}] {message}":message;
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
                case MessageBoxImage.Tip:
                    ImageText = "\uf0A2";
                    ImageBackground = (SolidColorBrush)new BrushConverter().ConvertFrom("#ffFFCC00");
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

            if (isAutoRemove)
            {
                AutoRemove(keepTime);
            }
        }
        #endregion

        #region 私有方法

        private void ManulRemove(object sender, ExecutedRoutedEventArgs e)
        {
            if (owner != null)
            {
                owner.Children.Remove(this);
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
                        switch (Placement)
                        {
                            case CornerPlacement.TopLeft:
                                container.VerticalAlignment = VerticalAlignment.Top;
                                container.HorizontalAlignment = HorizontalAlignment.Left;
                                break;
                            case CornerPlacement.TopCenter:
                                container.VerticalAlignment = VerticalAlignment.Top;
                                container.HorizontalAlignment = HorizontalAlignment.Center;
                                break;
                            case CornerPlacement.TopRight:
                                container.VerticalAlignment = VerticalAlignment.Top;
                                container.HorizontalAlignment = HorizontalAlignment.Right;
                                break;
                            case CornerPlacement.BottomLeft:
                                container.VerticalAlignment = VerticalAlignment.Bottom;
                                container.HorizontalAlignment = HorizontalAlignment.Left;
                                break;
                            case CornerPlacement.BottomCenter:
                                container.VerticalAlignment = VerticalAlignment.Bottom;
                                container.HorizontalAlignment = HorizontalAlignment.Center;
                                break;
                            case CornerPlacement.BottomRight:
                                container.VerticalAlignment = VerticalAlignment.Bottom;
                                container.HorizontalAlignment = HorizontalAlignment.Right;
                                break;
                            case CornerPlacement.Center:
                                container.VerticalAlignment = VerticalAlignment.Center;
                                container.HorizontalAlignment = HorizontalAlignment.Center;
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
                            case CornerPlacement.TopCenter:
                                container.VerticalAlignment = VerticalAlignment.Top;
                                container.HorizontalAlignment = HorizontalAlignment.Center;
                                break;
                            case CornerPlacement.TopRight:
                                container.VerticalAlignment = VerticalAlignment.Top;
                                container.HorizontalAlignment = HorizontalAlignment.Right;
                                break;
                            case CornerPlacement.BottomLeft:
                                container.VerticalAlignment = VerticalAlignment.Bottom;
                                container.HorizontalAlignment = HorizontalAlignment.Left;
                                break;
                            case CornerPlacement.BottomCenter:
                                container.VerticalAlignment = VerticalAlignment.Bottom;
                                container.HorizontalAlignment = HorizontalAlignment.Center;
                                break;
                            case CornerPlacement.BottomRight:
                                container.VerticalAlignment = VerticalAlignment.Bottom;
                                container.HorizontalAlignment = HorizontalAlignment.Right;
                                break;
                            case CornerPlacement.Center:
                                container.VerticalAlignment = VerticalAlignment.Center;
                                container.HorizontalAlignment = HorizontalAlignment.Center;
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
        private static void Show(string messageBoxText, MessageBoxImage icon, bool isAutoRemove, int keepTime = 3, string imageText = null, string imageColor = "#ffffff")
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
                    container.Children.Add(new MessageBoxControl(container, messageBoxText, icon, isAutoRemove, keepTime, imageText, imageColor));
                }
            }));
        }
        #endregion
        #endregion

    }
}
