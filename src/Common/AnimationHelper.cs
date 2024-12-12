using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace WYW.UI.Common
{
    /// <summary>
    ///     包含一些常用的动画辅助方法
    /// </summary>
    public class AnimationHelper
    {
        public static void OpacityChanging(DependencyObject value, double opacityFrom, double opacityTo, double startTime, double durateTime)
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
        /// <summary>
        ///     创建一个Thickness动画
        /// </summary>
        /// <param name="thickness"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static ThicknessAnimation CreateAnimation(Thickness thickness = default, double milliseconds = 200)
        {
            return new ThicknessAnimation(thickness, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            };
        }

        /// <summary>
        ///     创建一个Double动画
        /// </summary>
        /// <param name="toValue"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DoubleAnimation CreateAnimation(double toValue, double milliseconds = 200)
        {
            return new DoubleAnimation(toValue, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            };
        }

        internal static Geometry ComposeGeometry(string[] strings, double[] arr)
        {
            var builder = new StringBuilder(strings[0]);
            for (var i = 0; i < arr.Length; i++)
            {
                var s = strings[i + 1];
                var n = arr[i];
                if (!double.IsNaN(n))
                {
                    builder.Append(n).Append(s);
                }
            }

            return Geometry.Parse(builder.ToString());
        }

        internal static Geometry InterpolateGeometry(double[] from, double[] to, double progress, string[] strings)
        {
            var accumulated = new double[to.Length];
            for (var i = 0; i < to.Length; i++)
            {
                var fromValue = from[i];
                accumulated[i] = fromValue + (to[i] - fromValue) * progress;
            }

            return ComposeGeometry(strings, accumulated);
        }

        internal static double[] InterpolateGeometryValue(double[] from, double[] to, double progress)
        {
            var accumulated = new double[to.Length];
            for (var i = 0; i < to.Length; i++)
            {
                var fromValue = from[i];
                accumulated[i] = fromValue + (to[i] - fromValue) * progress;
            }

            return accumulated;
        }
    }
}
