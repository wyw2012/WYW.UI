using WYW.UI.Commands;
using WYW.UI.Common;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WYW.UI.Controls
{
    /// <summary>
    /// 消息窗体，用于替代ystem.Windows.MessageBox
    /// </summary>
    public class MessageBoxWindow : Window
    {
        #region 公开
        /// <summary>
        /// 显示消息对话框
        /// </summary>
        /// <param name="text">消息框正文</param>
        /// <param name="caption">消息框标题</param>
        /// <param name="button">按钮格式</param>
        /// <param name="icon">图标格式</param>
        /// <param name="isAutoClose">是否自动关闭</param>
        /// <param name="windowKeepTime">自动关闭模式下消息框显示的时间</param>
        /// <param name="okButtonText">OK按钮显示的文字，默认显示"确定"</param>
        /// <param name="yesButtonText">Yes按钮显示的文字，默认显示"是"</param>
        /// <param name="noButtonText">No按钮显示的文字，默认显示"否"</param>
        /// <param name="cancelButtonText">Cancel按钮显示的文字，默认显示"取消"</param>
        /// <param name="showDialog">是否以模态模式弹出，默认为true。如果不希望阻塞UI线程，可设置为false</param>
        /// <param name="imageText">自定义图标内容，FontAwesome字体的Unicode码，例如"\uf118"。仅在icon = MessageBoxImage.Custom下有效</param>
        /// <param name="imageColor">自定义图标颜色，例如"#ffffffff"。仅在icon = MessageBoxImage.Custom下有效</param>
        /// <returns></returns>
        public static MessageBoxResult Show(string text, string caption = "",
            MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None,
            bool isAutoClose = false, int windowKeepTime = 5,
            string okButtonText = null, string yesButtonText = null, string noButtonText = null, string cancelButtonText = null, bool showDialog = true,
            string imageText = null, string imageColor = "#ffffff")
        {
            MessageBoxResult result = MessageBoxResult.None;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                MessageBoxWindow window = new MessageBoxWindow(text, caption, button, icon, isAutoClose, windowKeepTime, imageText, imageColor);
                if (!string.IsNullOrEmpty(okButtonText))
                {
                    window.OKButtonText = okButtonText;
                }
                if (!string.IsNullOrEmpty(yesButtonText))
                {
                    window.YesButtonText = yesButtonText;
                }
                if (!string.IsNullOrEmpty(noButtonText))
                {
                    window.NoButtonText = noButtonText;
                }
                if (!string.IsNullOrEmpty(cancelButtonText))
                {
                    window.CancelButtonText = cancelButtonText;
                }
                if (showDialog)
                {
                    window.ShowDialog();
                }
                else
                {
                    window.Show();
                }
                if (window != null)
                    result = window.MessageResult;
            }));
            return result;
        }
        /// <summary>
        /// 消息框显示自定义图标
        /// </summary>
        /// <param name="messageBoxText">正文</param>
        /// <param name="caption">标题</param>
        /// <param name="imageText">自定义图标内容，FontAwesome字体的Unicode码，例如"\uf118"</param>
        /// <param name="imageColor">自定义图标颜色，例如"#ffffffff"</param>
        /// <param name="isAutoClose">是否自动关闭，默认false</param>
        /// <param name="windowKeepTime">如果自动关闭为true，则等待的时间，单位为s，默认为5s</param>
        /// <returns></returns>
        public static MessageBoxResult Custom(string messageBoxText, string caption = "", string imageText = null, string imageColor = "#ffffff", bool isAutoClose = false, int windowKeepTime = 5)
        {
            return Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Custom, isAutoClose, windowKeepTime, imageText: imageText, imageColor: imageColor);
        }
        /// <summary>
        /// 消息框显示 √ 图标
        /// </summary>
        /// <param name="messageBoxText">正文</param>
        /// <param name="caption">标题</param>
        /// <param name="isAutoClose">是否自动关闭，默认false</param>
        /// <param name="windowKeepTime">如果自动关闭为true，则等待的时间，单位为s，默认为5s</param>
        /// <returns></returns>
        public static MessageBoxResult Success(string messageBoxText, string caption = "", bool isAutoClose = false, int windowKeepTime = 5)
        {
            return Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Sucess, isAutoClose, windowKeepTime);
        }
        /// <summary>
        /// 消息框显示 ? 图标
        /// </summary>
        /// <param name="messageBoxText">正文</param>
        /// <param name="caption">标题</param>
        /// <returns></returns>
        public static MessageBoxResult Question(string messageBoxText, string caption = "")
        {
            return Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
        /// <summary>
        /// 消息框显示 ! 图标
        /// </summary>
        /// <param name="messageBoxText">正文</param>
        /// <param name="caption">标题</param>
        /// <param name="isAutoClose">是否自动关闭，默认false</param>
        /// <param name="windowKeepTime">如果自动关闭为true，则等待的时间，单位为s，默认为5s</param>
        /// <returns></returns>
        public static MessageBoxResult Warning(string messageBoxText, string caption = "", bool isAutoClose = false, int windowKeepTime = 5)
        {
            return Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning, isAutoClose, windowKeepTime);
        }
        /// <summary>
        /// 消息框显示 x 图标
        /// </summary>
        /// <param name="messageBoxText">正文</param>
        /// <param name="caption">标题</param>
        /// <param name="isPlaySound">是否播放声音提示</param>
        /// <returns></returns>
        public static MessageBoxResult Error(string messageBoxText, string caption = "", bool isPlaySound = false)
        {
            SoundPlayer soundPlayer = null;
            if (isPlaySound)
            {
                soundPlayer = new SoundPlayer(UI.Resources.Warn);
                soundPlayer.PlayLooping();
            }

            var result = Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            if (isPlaySound)
            {
                soundPlayer.Stop();
                soundPlayer.Dispose();
            }
            return result;
        }
        #endregion

        #region 内部
        #region Win32，目的是使该窗体永不成为激活窗体

        private const int GWL_STYLE = -16;
        private const int WS_CHILD = 0x40000000;

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int oldStyle, int newStyle);

        // newStyle采用long类型有时候会报错，建议采用int类型

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int oldStyle);

        #endregion

        private bool isClosed;
        private MessageBoxResult MessageResult { get; set; } = MessageBoxResult.None;

        #region 构造函数
        private MessageBoxWindow()
        {
            Style = Application.Current.Resources["MessageBoxWindowStyle"] as Style;
            // 将当前窗体弹出设置为活动窗体的中央
            Window activedWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (activedWindow != null)
            {
                FontSize = activedWindow.FontSize;
                WindowStartupLocation = WindowStartupLocation.CenterOwner;
                Owner = activedWindow;
            }
            else
            {
                Topmost = true;
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            CommandBindings.Add(new CommandBinding(CustomCommand.OKCommand, OnOK));
            CommandBindings.Add(new CommandBinding(CustomCommand.NoCommand, OnNo));
            CommandBindings.Add(new CommandBinding(CustomCommand.YesCommand, OnYes));
            CommandBindings.Add(new CommandBinding(CustomCommand.CancelCommand, OnCancel));
            CommandBindings.Add(new CommandBinding(CustomCommand.CloseCommand, OnClose));
        }
        private MessageBoxWindow(string messageBoxText = "", string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, bool isAutoClose = false, int windowKeepTime = 5, string imageText = null, string imageColor = "#ffffff") : this()
        {
            Message = messageBoxText;
            Title = caption;
            MessageBoxButton = button;

            IsAutoClose = isAutoClose;
            WindowKeepTime = windowKeepTime;
            if (button == MessageBoxButton.OK)
            {
                if (IsAutoClose)
                {
                    CountDown = windowKeepTime;
                    AutoClose();
                }
            }
            else
            {
                IsAutoClose = false;
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

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            // 2023-02-03 王彦为屏蔽如下代码，原因是MessageBoxWindow可以为激活窗体。
            //var wih = new WindowInteropHelper(this);
            //var style = GetWindowLong(wih.Handle, GWL_STYLE);
            //SetWindowLong(wih.Handle, GWL_STYLE, style | WS_CHILD); // 非激活窗体
        }
        #endregion


        #region 依赖属性
        internal static readonly DependencyProperty CountDownProperty =
            DependencyProperty.Register("CountDown", typeof(int), typeof(MessageBoxWindow), new PropertyMetadata(default(int)));
        internal static readonly DependencyProperty MessageBoxButtonProperty =
            DependencyProperty.Register("MessageBoxButton", typeof(MessageBoxButton), typeof(MessageBoxWindow), new PropertyMetadata(MessageBoxButton.OK));
        internal static readonly DependencyProperty ImageTextProperty =
            DependencyProperty.Register("ImageText", typeof(string), typeof(MessageBoxWindow), new PropertyMetadata(null));
        internal static readonly DependencyProperty ImageBackgroundProperty =
            DependencyProperty.Register("ImageBackground", typeof(SolidColorBrush), typeof(MessageBoxWindow), new PropertyMetadata(Brushes.Black));
        internal static readonly DependencyProperty HasImageProperty =
          DependencyProperty.Register("HasImage", typeof(bool), typeof(MessageBoxWindow), new PropertyMetadata(true));
        internal static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageBoxWindow), new PropertyMetadata(default(string)));
        internal static readonly DependencyProperty IsAutoCloseProperty =
            DependencyProperty.Register("IsAutoClose", typeof(bool), typeof(MessageBoxWindow), new PropertyMetadata(default(bool)));
        internal static readonly DependencyProperty YesButtonTextProperty =
            DependencyProperty.Register("YesButtonText", typeof(string), typeof(MessageBoxWindow), new PropertyMetadata(CultureInfo.CurrentUICulture.Name == "zh-CN" ? "是" : "Yes"));
        internal static readonly DependencyProperty NoButtonTextProperty =
            DependencyProperty.Register("NoButtonText", typeof(string), typeof(MessageBoxWindow), new PropertyMetadata(CultureInfo.CurrentUICulture.Name == "zh-CN" ? "否" : "NO"));
        internal static readonly DependencyProperty CancelButtonTextProperty =
            DependencyProperty.Register("CancelButtonText", typeof(string), typeof(MessageBoxWindow), new PropertyMetadata(CultureInfo.CurrentUICulture.Name == "zh-CN" ? "取消" : "Cancel"));
        internal static readonly DependencyProperty OKButtonTextProperty =
            DependencyProperty.Register("OKButtonText", typeof(string), typeof(MessageBoxWindow), new PropertyMetadata(CultureInfo.CurrentUICulture.Name == "zh-CN" ? "确定" : "OK"));
        internal static readonly DependencyProperty WindowKeepTimeProperty =
            DependencyProperty.Register("WindowKeepTime", typeof(int), typeof(MessageBoxWindow), new PropertyMetadata(5));

        internal int CountDown
        {
            get { return (int)GetValue(CountDownProperty); }
            set { SetValue(CountDownProperty, value); }
        }

        internal MessageBoxButton MessageBoxButton
        {
            get { return (MessageBoxButton)GetValue(MessageBoxButtonProperty); }
            set { SetValue(MessageBoxButtonProperty, value); }
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

        internal string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        internal bool IsAutoClose
        {
            get { return (bool)GetValue(IsAutoCloseProperty); }
            set { SetValue(IsAutoCloseProperty, value); }
        }
        internal string YesButtonText
        {
            get { return (string)GetValue(YesButtonTextProperty); }
            set { SetValue(YesButtonTextProperty, value); }
        }

        internal string NoButtonText
        {
            get { return (string)GetValue(NoButtonTextProperty); }
            set { SetValue(NoButtonTextProperty, value); }
        }

        internal string CancelButtonText
        {
            get { return (string)GetValue(CancelButtonTextProperty); }
            set { SetValue(CancelButtonTextProperty, value); }
        }

        internal string OKButtonText
        {
            get { return (string)GetValue(OKButtonTextProperty); }
            set { SetValue(OKButtonTextProperty, value); }
        }

        internal int WindowKeepTime
        {
            get { return (int)GetValue(WindowKeepTimeProperty); }
            set { SetValue(WindowKeepTimeProperty, value); }
        }
        #endregion

        #region  私有方法
        private void OnOK(object sender, ExecutedRoutedEventArgs e)
        {
            MessageResult = MessageBoxResult.OK;
            Close();
        }
        private void OnCancel(object sender, ExecutedRoutedEventArgs e)
        {
            MessageResult = MessageBoxResult.Cancel;
            Close();
        }
        private void OnYes(object sender, ExecutedRoutedEventArgs e)
        {
            MessageResult = MessageBoxResult.Yes;
            Close();
        }
        private void OnNo(object sender, ExecutedRoutedEventArgs e)
        {
            MessageResult = MessageBoxResult.No;
            Close();
        }
        private void OnClose(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (this.Owner != null)
            {
                Owner.Activate();
            }
            isClosed = true;
        }
        private void AutoClose()
        {
            int keepTime = WindowKeepTime;
            StoryboardHelper.OpacityChanging(this, 1, 0, WindowKeepTime - 1, WindowKeepTime);
            ThreadPool.QueueUserWorkItem(delegate
            {
                DateTime startTime = DateTime.Now;
                for (int i = keepTime; i > 0; i--)
                {
                    if (isClosed)
                        return;
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        CountDown = i;
                    }));

                    Thread.Sleep(1000);
                }

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Close();
                }));
            });
        }
        #endregion
        #endregion
    }
}
