using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WYW.UI.Controls
{
    public class NumericUpDown : Slider
    {
        public NumericUpDown()
        {

            Maximum = UInt32.MaxValue; // 系统默认最大值是10，如果绑定的默认值大于10，则自动设置为10，所以在这里修改最大值。
            Format();
        }
        private double lastCorrectValue = 0; // 最后一次正确的值

        public static readonly DependencyProperty FormatedValueProperty =
            DependencyProperty.Register("FormatedValue", typeof(string), typeof(NumericUpDown), new PropertyMetadata("", OnFormatedValueChanged));
        public static readonly DependencyProperty StringFormatProperty =
            DependencyProperty.Register("StringFormat", typeof(string), typeof(NumericUpDown), new PropertyMetadata("N0"));
        public static readonly DependencyProperty ButtonWidthProperty =
            DependencyProperty.Register("ButtonWidth", typeof(double), typeof(NumericUpDown), new PropertyMetadata(32.0));
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
        public string FormatedValue
        {
            get { return (string)GetValue(FormatedValueProperty); }
            set { SetValue(FormatedValueProperty, value); }
        }
        public double ButtonWidth
        {
            get { return (double)GetValue(ButtonWidthProperty); }
            set { SetValue(ButtonWidthProperty, value); }
        }

        /// <summary>
        /// 是否是LED字体
        /// </summary>
        public bool IsLedFontFamily
        {
            get { return (bool)GetValue(IsLedFontFamilyProperty); }
            set { SetValue(IsLedFontFamilyProperty, value); }
        }

        private static void OnFormatedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NumericUpDown obj = (NumericUpDown)d;
            obj.OnFormatedValueChanged(e.OldValue, e.NewValue);
        }
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            Format();
        }

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
                Format();
            }
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
    }
}
