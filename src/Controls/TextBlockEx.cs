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
    public class TextBlockEx:Control
    {
        #region 依赖属性

        public static readonly DependencyProperty IsLedFontFamilyProperty =
            DependencyProperty.Register("IsLedFontFamily", typeof(bool), typeof(TextBlockEx), new PropertyMetadata(default(bool)));
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(TextBlockEx), new PropertyMetadata(null));
        public static readonly DependencyProperty TitleWidthProperty =
            DependencyProperty.Register("TitleWidth", typeof(string), typeof(TextBlockEx), new PropertyMetadata("*"));
        public static readonly DependencyProperty SuffixProperty =
            DependencyProperty.Register("Suffix", typeof(string), typeof(TextBlockEx), new PropertyMetadata(null));
        public static readonly DependencyProperty SuffixWidthProperty =
            DependencyProperty.Register("SuffixWidth", typeof(string), typeof(TextBlockEx), new PropertyMetadata("0.5*"));
        public static readonly DependencyProperty LedForegroundProperty =
            DependencyProperty.Register("LedForeground", typeof(Brush), typeof(TextBlockEx), new PropertyMetadata(Brushes.LimeGreen));
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBlockEx), new PropertyMetadata(default(string)));


     
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
        /// <summary>
        /// 是否是LED字体
        /// </summary>
        public bool IsLedFontFamily
        {
            get { return (bool)GetValue(IsLedFontFamilyProperty); }
            set { SetValue(IsLedFontFamilyProperty, value); }
        }
       
        public Brush LedForeground
        {
            get { return (Brush)GetValue(LedForegroundProperty); }
            set { SetValue(LedForegroundProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion



    }
}
