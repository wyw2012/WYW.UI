using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WYW.UI.Controls
{
    public class CircleProgressBar : RangeBase
    {

        private bool isIndeterminate = false;
        private double indeterminateAngle = 0;

        public static readonly DependencyProperty RingThicknessProperty =
            DependencyProperty.Register("RingThickness", typeof(double), typeof(CircleProgressBar), new PropertyMetadata(default(double)));
        public static readonly DependencyProperty RingBackgroundProperty =
            DependencyProperty.Register("RingBackground", typeof(SolidColorBrush), typeof(CircleProgressBar), new PropertyMetadata(default(SolidColorBrush)));
        public static readonly DependencyProperty StringFormatProperty =
            DependencyProperty.Register("StringFormat", typeof(string), typeof(CircleProgressBar), new PropertyMetadata("P0"));
        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register("IsIndeterminate", typeof(bool), typeof(CircleProgressBar), new PropertyMetadata(default(bool), OnIsIndeterminateChanged));

        public double RingThickness
        {
            get { return (double)GetValue(RingThicknessProperty); }
            set { SetValue(RingThicknessProperty, value); }
        }

        public SolidColorBrush RingBackground
        {
            get { return (SolidColorBrush)GetValue(RingBackgroundProperty); }
            set { SetValue(RingBackgroundProperty, value); }
        }

        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        public string StringFormat
        {
            get { return (string)GetValue(StringFormatProperty); }
            set { SetValue(StringFormatProperty, value); }
        }
        private static void OnIsIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CircleProgressBar obj = (CircleProgressBar)d;
            obj.OnIsIndeterminateChanged(e.OldValue, e.NewValue);
        }
        private void OnIsIndeterminateChanged(object oldValue, object newValue)
        {
            isIndeterminate = (bool)newValue;
            Thread thread = new Thread(new ThreadStart(delegate
            {
                while (isIndeterminate)
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        indeterminateAngle = (++indeterminateAngle) % 360;
                        InvalidateVisual();
                    }));
                    Thread.Sleep(15);
                }
            }));
            thread.IsBackground = true;
            thread.Start();
        
            Dispatcher.Invoke(new Action(() =>
            {
                InvalidateVisual();
            }));
        }
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            InvalidateVisual();
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            DrawContext(drawingContext);
        }
        private void DrawContext(DrawingContext drawingContext)
        {
            double percent = Value / Maximum;
            string percentString = percent.ToString(StringFormat); // 显示的文字
            double angel = percent * 360D; // 当前刻度的角度
            bool isLargeArc = angel >= 180 ? true : false;
            double minSize = Math.Min(ActualWidth, ActualHeight);
            
            Point centerPoint = new Point(ActualWidth / 2, ActualHeight / 2);
            double radius = minSize / 2;
            double outerRadius = radius;
            double innerRadius = radius - RingThickness;
            //FontSize = minSize / 4==0?10.5: minSize / 4;
            

            FormattedText formatWords = new FormattedText(percentString, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                                        new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch), this.FontSize, Foreground);
            Point contextStartPoint = new Point(centerPoint.X - formatWords.Width / 2, centerPoint.Y - formatWords.Height / 2);


            // 画灰色底线
            drawingContext.DrawEllipse(null, new Pen(RingBackground, RingThickness), centerPoint, radius - RingThickness / 2, radius - RingThickness / 2);
            if (IsIndeterminate)
            {
                isLargeArc = false;
                double weepAngle = 50; // 扇形角度
                int weepCount = 3; // 扇形数量
                PathGeometry pathGeometry = new PathGeometry();
                for (int i = 0; i < weepCount; i++)
                {
                    Point firstpoint = GetPointByAngel(centerPoint, outerRadius, indeterminateAngle + (360 / weepCount) * i - weepAngle);
                    Point secondpoint = GetPointByAngel(centerPoint, outerRadius, indeterminateAngle + (360 / weepCount) * i);
                    Point thirdpoint = GetPointByAngel(centerPoint, innerRadius, indeterminateAngle + (360 / weepCount) * i - weepAngle);
                    Point fourpoint = GetPointByAngel(centerPoint, innerRadius, indeterminateAngle + (360 / weepCount) * i);

                    PathFigure pathFigure = new PathFigure() { IsClosed = true, IsFilled = true };
                    // Add的次序不能错位
                    pathFigure.StartPoint = firstpoint;
                    pathFigure.Segments.Add(new ArcSegment { Point = secondpoint, IsLargeArc = isLargeArc, Size = new Size(outerRadius, outerRadius), SweepDirection = SweepDirection.Clockwise });
                    pathFigure.Segments.Add(new LineSegment { Point = fourpoint });
                    pathFigure.Segments.Add(new ArcSegment { Point = thirdpoint, IsLargeArc = isLargeArc, Size = new Size(innerRadius, innerRadius), SweepDirection = SweepDirection.Counterclockwise });

                    pathGeometry.Figures.Add(pathFigure);
                }

                drawingContext.DrawGeometry(Foreground, new Pen(), pathGeometry);
            }
            else
            {
                // 画文字
                drawingContext.DrawText(formatWords, contextStartPoint);
                if (Value >= Maximum)  // 如果达到 100%
                {
                    drawingContext.DrawEllipse(null, new Pen(Foreground, RingThickness), centerPoint, radius - RingThickness / 2, radius - RingThickness / 2);
                    return;
                }

                // 画当前进度条

                // 计算半径与坐标
                //     secondpoint  *
                //                 *   
                //                *   *  fourpoint
                //               *   * 
                //  firstpoint  *   *  thirdpoint
                //          
                Point firstpoint = GetPointByAngel(centerPoint, outerRadius, 0);
                Point secondpoint = GetPointByAngel(centerPoint, outerRadius, angel);
                Point thirdpoint = GetPointByAngel(centerPoint, innerRadius, 0);
                Point fourpoint = GetPointByAngel(centerPoint, innerRadius, angel);

                PathFigure pathFigure = new PathFigure();
                // Add的次序不能错位
                pathFigure.StartPoint = firstpoint;
                pathFigure.Segments.Add(new ArcSegment { Point = secondpoint, IsLargeArc = isLargeArc, Size = new Size(outerRadius, outerRadius), SweepDirection = SweepDirection.Clockwise });
                pathFigure.Segments.Add(new LineSegment { Point = fourpoint });
                pathFigure.Segments.Add(new ArcSegment { Point = thirdpoint, IsLargeArc = isLargeArc, Size = new Size(innerRadius, innerRadius), SweepDirection = SweepDirection.Counterclockwise });
                PathGeometry pathGeometry = new PathGeometry();
                pathGeometry.Figures.Add(pathFigure);

                drawingContext.DrawGeometry(Foreground, new Pen(), pathGeometry);
            }
        }

        /// <summary>
        /// 根据角度获取坐标
        /// </summary>
        private Point GetPointByAngel(Point centerPoint, double radius, double angle)
        {
            // 从12点方向开始
            double x = centerPoint.X + Math.Sin((Math.PI / 180.0) * angle) * radius;
            double y = centerPoint.Y - Math.Cos((Math.PI / 180.0) * angle) * radius;
            return new Point(x, y);
        }
    }
}
