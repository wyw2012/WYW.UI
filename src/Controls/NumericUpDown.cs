using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;
using WYW.UI.Commands;

namespace WYW.UI.Controls
{
    /// <summary>
    /// 数字输入框
    /// </summary>
    public class NumericUpDown : Slider
    {     
        private double lastCorrectValue = 0; // 最后一次正确的值
        public NumericUpDown()
        {
            //Maximum = UInt32.MaxValue; // 系统默认最大值是10，如果绑定的默认值大于10，则自动设置为10，所以在这里修改最大值。
            UpButtonClickCommand = new RelayCommand(()=>
            {
                Slider.IncreaseLarge.Execute(null, null);
                UpButtonClick?.Invoke(this, null);
            });
            DownButtonClickCommand = new RelayCommand(() =>
            {
                Slider.DecreaseLarge.Execute(null, null);
                DownButtonClick?.Invoke(this, null);
            });
            EnterPressedCommand = new RelayCommand(() =>
            {
                MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            });
          
        }

        #region 公共事件
        public event EventHandler UpButtonClick;
        public event EventHandler DownButtonClick;
        #endregion

        #region 依赖属性

        public static readonly DependencyProperty StringFormatProperty =
            DependencyProperty.Register("StringFormat", typeof(string), typeof(NumericUpDown), default);
        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.Register("ButtonWidth", typeof(string), typeof(NumericUpDown), new PropertyMetadata("0.4*"));
        public static readonly DependencyProperty IsLedFontFamilyProperty =
            DependencyProperty.Register("IsLedFontFamily", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(default(bool)));
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NumericUpDown), new PropertyMetadata(null));
        public static readonly DependencyProperty TitleWidthProperty =
            DependencyProperty.Register("TitleWidth", typeof(string), typeof(NumericUpDown), new PropertyMetadata("*"));
        public static readonly DependencyProperty SuffixProperty =
            DependencyProperty.Register("Suffix", typeof(string), typeof(NumericUpDown), new PropertyMetadata(null));
        public static readonly DependencyProperty SuffixWidthProperty =
            DependencyProperty.Register("SuffixWidth", typeof(string), typeof(NumericUpDown), new PropertyMetadata("0.5*"));
        public static readonly DependencyProperty IcoWidthProperty =
            DependencyProperty.Register("IcoWidth", typeof(double), typeof(NumericUpDown), new PropertyMetadata(10.0));
        public static readonly DependencyProperty IcoHeightProperty =
            DependencyProperty.Register("IcoHeight", typeof(double), typeof(NumericUpDown), new PropertyMetadata(10.0));
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NumericUpDown), new PropertyMetadata(new CornerRadius(3.0)));

        public static readonly DependencyProperty IsEditableProperty =
         DependencyProperty.Register("IsEditable", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(true));
        public static readonly DependencyProperty UnitProperty =
          DependencyProperty.Register("Unit", typeof(string), typeof(NumericUpDown), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty LedForegroundProperty =
            DependencyProperty.Register("LedForeground", typeof(Brush), typeof(NumericUpDown), new PropertyMetadata(default(Brush)));



        internal static readonly DependencyProperty FormatedValueProperty =
           DependencyProperty.Register("FormatedValue", typeof(string), typeof(NumericUpDown), new PropertyMetadata("0", OnFormatedValueChanged));
        public string SuffixWidth
        {
            get { return (string)GetValue(SuffixWidthProperty); }
            set { SetValue(SuffixWidthProperty, value); }
        }
        public string Suffix
        {
            get { return (string)GetValue(SuffixProperty); }
            set { SetValue(SuffixProperty, value); }
        }
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public string TitleWidth
        {
            get { return (string)GetValue(TitleWidthProperty); }
            set { SetValue(TitleWidthProperty, value); }
        }

        public string StringFormat
        {
            get { return (string)GetValue(StringFormatProperty); }
            set { SetValue(StringFormatProperty, value); }
        }
    
        public string ButtonWidth
        {
            get { return (string)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }

        public double IcoWidth
        {
            get { return (double)GetValue(IcoWidthProperty); }
            set { SetValue(IcoWidthProperty, value); }
        }

        public double IcoHeight
        {
            get { return (double)GetValue(IcoHeightProperty); }
            set { SetValue(IcoHeightProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// 是否是LED字体
        /// </summary>
        public bool IsLedFontFamily
        {
            get { return (bool)GetValue(IsLedFontFamilyProperty); }
            set { SetValue(IsLedFontFamilyProperty, value); }
        }
        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }



        public string Unit
        {
            get { return (string)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }




        public Brush LedForeground
        {
            get { return (Brush)GetValue(LedForegroundProperty); }
            set { SetValue(LedForegroundProperty, value); }
        }

    



        internal string FormatedValue
        {
            get { return (string)GetValue(FormatedValueProperty); }
            set { SetValue(FormatedValueProperty, value); }
        }
        #endregion

        #region 委托事件
        private static void OnFormatedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown obj = (NumericUpDown)d;
            obj.OnFormatedValueChanged(e.OldValue, e.NewValue);
        }

        #endregion

        #region 内部命令
        public RelayCommand UpButtonClickCommand { get; private set; }
        public RelayCommand DownButtonClickCommand { get; private set; }

        public RelayCommand EnterPressedCommand { get; private set; }
        #endregion

        #region 重载方法
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            Format();
        }
        #endregion

        #region 私有方法
        private void OnFormatedValueChanged(object oldValue, object newValue)
        {
            double value = 0;
            if (newValue != null && double.TryParse(newValue.ToString(), out value))
            {
                if (value > Maximum)
                {
                    value = Maximum;
                }
                if (value < Minimum)
                {
                    value = Minimum;
                }
                Value = value;
                lastCorrectValue = value;
            }
            else
            {
                // 防止输入非法字符串
                Value = lastCorrectValue;
            }
            Format();
        }
        private void Format()
        {
            if (StringFormat != null)
            {
                FormatedValue = Value.ToString(StringFormat);
            }
            else
            {
                FormatedValue = Value.ToString();
            }
        }
        #endregion
    }
}
