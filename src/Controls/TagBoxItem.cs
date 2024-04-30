using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using WYW.UI.Commands;
using WYW.UI.Common;

namespace WYW.UI.Controls
{
    public class TagBoxItem : ContentControl
    {
        public TagBoxItem()
        {
            CommandBindings.Add(new CommandBinding(CustomCommand.RemoveCommand, Remove));
        }

        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }

        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(TagBoxItem), new PropertyMetadata(default(bool)));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(TagBoxItem), new PropertyMetadata(default(string)));

        private void Remove(object sender, ExecutedRoutedEventArgs e)
        {
            var father = VisualTreeHelper.GetParent(this);
            if (father is Panel panel)
            {
                panel.Children.Remove(this);
                return;
            }
            var itemsControl = VisualHelper.FindParent<ItemsControl>(this);
            if (itemsControl != null)
            {
                if (itemsControl.ItemsSource != null)
                {
                    var index = itemsControl.Items.IndexOf(this.DataContext);
                    var items = (IList)itemsControl.ItemsSource;

                    items.RemoveAt(index);
                }
                else
                {
                    itemsControl.Items.Remove(this);
                }
            }
        }
    }
}
