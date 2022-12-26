using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace WYW.UI.Common
{
    internal class StoryboardHelper
    {
        public static void OpacityChanging(DependencyObject value, double opacityFrom, double opacityTo, double startTime ,double durateTime)
        {
            var sb = new Storyboard();
            var da = new DoubleAnimation();
            if (opacityFrom > 0)
            {
                da.From = opacityFrom;
            }
            da.To = opacityTo;
            da.BeginTime = TimeSpan.FromSeconds(startTime);
            da.Duration = TimeSpan.FromSeconds(durateTime); //持续时间
            Storyboard.SetTarget(da, value);
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            sb.Children.Add(da);
            //sb.FillBehavior = FillBehavior.HoldEnd;
            sb.Begin();
        }
    }
}
