using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WYW.UI.Controls
{
    /// <summary>
    /// 简单装饰器
    /// </summary>
    public class SampleAdorner : Adorner
    {

        private UIElement child;

        public SampleAdorner(UIElement adornedElement) : base(adornedElement)
        {
        }

        public UIElement Child
        {
            get => child;
            set
            {
                if (value == null)
                {
                    RemoveVisualChild(child);
                }
                else
                {
                    AddVisualChild(value);
                }
                child = value;
            }
        }

        protected override int VisualChildrenCount => child != null ? 1 : 0;

        protected override Size ArrangeOverride(Size finalSize)
        {
            child?.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index == 0 && child != null)
                return child;
            return base.GetVisualChild(index);
        }

    }
}
