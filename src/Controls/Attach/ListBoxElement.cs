using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WYW.UI.Controls.Attach
{
    public class ListBoxElement
    {
        #region  ListBoxUniformGridStyle使用
        public static int GetRows(DependencyObject obj) => (int)obj.GetValue(RowsProperty);

        public static void SetRows(DependencyObject obj, int value) => obj.SetValue(RowsProperty, value);

        public static readonly DependencyProperty RowsProperty
            = DependencyProperty.RegisterAttached("Rows", typeof(int), typeof(ListBoxElement), new PropertyMetadata(1));


        public static int GetColumns(DependencyObject obj) => (int)obj.GetValue(ColumnsProperty);

        public static void SetColumns(DependencyObject obj, int value) => obj.SetValue(ColumnsProperty, value);

        public static readonly DependencyProperty ColumnsProperty
            = DependencyProperty.RegisterAttached("Columns", typeof(int), typeof(ListBoxElement), new PropertyMetadata(1));

        #endregion
    }
}
