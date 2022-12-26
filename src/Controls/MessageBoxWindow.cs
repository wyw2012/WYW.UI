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
using System.Windows.Interop;
using System.Windows.Media;

namespace WYW.UI.Controls
{
    public class MessageBoxWindow : Window
    {
        #region Win32

        private const int GWL_STYLE = -16;
        private const int WS_CHILD = 0x40000000;

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int oldStyle, int newStyle);

        // newStyle采用long类型有时候会报错，建议采用int类型

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int oldStyle);

        #endregion
        private bool isClosed;
        internal MessageBoxWindow()
        {
            Style = Application.Current.Resources["MessageBoxWindowStyle"] as Style;
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
        internal MessageBoxWindow(string messageBoxText = "", string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, bool isAutoClose = false, int windowKeepTime = 5,string imageText=null,string imageColor="#ffffff") : this()
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
            switch(icon)
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
                    if(string.IsNullOrEmpty(imageText))
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
            var wih = new WindowInteropHelper(this);
            var style = GetWindowLong(wih.Handle, GWL_STYLE);
            SetWindowLong(wih.Handle, GWL_STYLE, style | WS_CHILD); // 非激活窗体
        }

        #region 静态方法
        /// <summary>
        /// 
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
            MessageBoxWindow window = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                window = new MessageBoxWindow(text, caption, button, icon, isAutoClose, windowKeepTime,imageText,imageColor);
                if(!string.IsNullOrEmpty(okButtonText))
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
                if(showDialog)
                {
                    window.ShowDialog();
                }
                else
                {
                    window.Show();
                }
               
            }));
            return window != null ? window.MessageResult : MessageBoxResult.None;
        }
        /// <summary>
        /// 消息框显示自定义图标
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        public static MessageBoxResult Custom(string messageBoxText, string caption = "", string imageText = null, string imageColor = "#ffffff", bool isAutoClose = false, int windowKeepTime = 5)
        {
            return Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Custom, isAutoClose, windowKeepTime,imageText:imageText,imageColor:imageColor);
        }
        /// <summary>
        /// 消息框显示 √ 图标
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        public static MessageBoxResult Success(string messageBoxText, string caption = "", bool isAutoClose = false, int windowKeepTime = 5)
        {
            return Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Sucess, isAutoClose, windowKeepTime);
        }
        /// <summary>
        /// 消息框显示 ? 图标
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        public static MessageBoxResult Question(string messageBoxText, string caption = "")
        {
            return Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
        /// <summary>
        /// 消息框显示 ! 图标
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        public static MessageBoxResult Warning(string messageBoxText, string caption = "", bool isAutoClose = false, int windowKeepTime = 5)
        {
            return Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning, isAutoClose, windowKeepTime);
        }
        /// <summary>
        /// 消息框显示 x 图标
        /// </summary>
        /// <param name="messageBoxText"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        public static MessageBoxResult Error(string messageBoxText, string caption = "",bool isPlaySound=false)
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

        #region 属性
        /// <summary>
        /// 显示的消息正文
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 自动关闭模式下，窗口的显示时间，单位秒
        /// </summary>

        /// <summary>
        /// 是否自动关闭，仅对MessageBoxButton=MessageBoxButton.OK有效
        /// </summary>
        public bool IsAutoClose { get; set; }

        public string YesButtonText { get; set; } = CultureInfo.CurrentUICulture.Name== "zh-CN"?"是":"Yes";
        public string NoButtonText { get; set; } = CultureInfo.CurrentUICulture.Name == "zh-CN" ? "否" : "NO";
        public string CancelButtonText { get; set; } = CultureInfo.CurrentUICulture.Name == "zh-CN" ? "取消" : "Cancel";
        public string OKButtonText { get; set; } = CultureInfo.CurrentUICulture.Name == "zh-CN" ? "确定" : "OK";

        MessageBoxResult MessageResult { get; set; } = MessageBoxResult.None;
        public int WindowKeepTime { get; set; } = 5;


        #endregion

        #region 依赖属性
        public static readonly DependencyProperty CountDownProperty =
            DependencyProperty.Register("CountDown", typeof(int), typeof(MessageBoxWindow), new PropertyMetadata(default(int)));
        public static readonly DependencyProperty MessageBoxButtonProperty =
            DependencyProperty.Register("MessageBoxButton", typeof(MessageBoxButton), typeof(MessageBoxWindow), new PropertyMetadata(MessageBoxButton.OK));
        public static readonly DependencyProperty ImageTextProperty =
            DependencyProperty.Register("ImageText", typeof(string), typeof(MessageBoxWindow), new PropertyMetadata(null));
        public static readonly DependencyProperty ImageBackgroundProperty =
            DependencyProperty.Register("ImageBackground", typeof(SolidColorBrush), typeof(MessageBoxWindow), new PropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty HasImageProperty =
          DependencyProperty.Register("HasImage", typeof(bool), typeof(MessageBoxWindow), new PropertyMetadata(true));

        public int CountDown
        {
            get { return (int)GetValue(CountDownProperty); }
            set { SetValue(CountDownProperty, value); }
        }

        public MessageBoxButton MessageBoxButton
        {
            get { return (MessageBoxButton)GetValue(MessageBoxButtonProperty); }
            set { SetValue(MessageBoxButtonProperty, value); }
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
            StoryboardHelper.OpacityChanging(this, 1, 0, WindowKeepTime - 1, WindowKeepTime);
            ThreadPool.QueueUserWorkItem(delegate
            {
                DateTime startTime = DateTime.Now;
                for (int i = WindowKeepTime; i > 0; i--)
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
    }
}
