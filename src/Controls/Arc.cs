using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using WYW.UI.Common;

namespace WYW.UI.Controls
{
    /// <summary>
    /// 弧形
    /// </summary>
    public class Arc : Shape
    {
        protected override Geometry DefiningGeometry => (Geometry)GetValue(DataProperty);

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(Arc), new PropertyMetadata(0.0));
        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register("EndAngle", typeof(double), typeof(Arc), new PropertyMetadata(90.0));
        public static readonly DependencyProperty RingThicknessProperty =
            DependencyProperty.Register("RingThickness", typeof(double), typeof(Arc), new PropertyMetadata(1.0));
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(Geometry), typeof(Arc), new PropertyMetadata(Geometry.Parse("")));

        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }
        public double EndAngle
        {
            get { return (double)GetValue(EndAngleProperty); }
            set { SetValue(EndAngleProperty, value); }
        }

        public double RingThickness
        {
            get { return (double)GetValue(RingThicknessProperty); }
            set { SetValue(RingThicknessProperty, value); }
        }

        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            DrawContext(drawingContext);
        }
        private void DrawContext(DrawingContext drawingContext)
        {
            double angel = EndAngle % 360 - StartAngle % 360; // 当前刻度的角度
            bool isLargeArc = angel >= 180 ? true : false;

            Point centerPoint = new Point(ActualWidth / 2, ActualHeight / 2);
            double radius = Math.Min(ActualWidth, ActualHeight) / 2;
            double outerRadius = radius;
            double innerRadius = radius - RingThickness;
            if (angel == 360)  // 如果达到 100%
            {
                drawingContext.DrawEllipse(null, new Pen(Stroke, RingThickness), centerPoint, radius - RingThickness / 2, radius - RingThickness / 2);
                return;
            }

            // 计算半径与坐标
            //     secondpoint  *
            //                 *   * thirdpoint 
            //                *   *  
            //               *   * 
            //  firstpoint  *   *  fourpoint
            //          
            Point firstpoint = AngelHelper.GetPointByAngel(centerPoint, outerRadius, StartAngle);
            Point secondpoint = AngelHelper.GetPointByAngel(centerPoint, outerRadius, EndAngle);
            Point thirdpoint = AngelHelper.GetPointByAngel(centerPoint, innerRadius, EndAngle);
            Point fourpoint  = AngelHelper.GetPointByAngel(centerPoint, innerRadius, StartAngle);


            PathFigure pathFigure = new PathFigure();
            pathFigure.StartPoint = firstpoint;
            pathFigure.Segments.Add(new ArcSegment { Point = secondpoint, IsLargeArc = isLargeArc, Size = new Size(outerRadius, outerRadius), SweepDirection = SweepDirection.Clockwise });
            pathFigure.Segments.Add(new LineSegment { Point = thirdpoint });
            pathFigure.Segments.Add(new ArcSegment { Point = fourpoint, IsLargeArc = isLargeArc, Size = new Size(innerRadius, innerRadius), SweepDirection = SweepDirection.Counterclockwise });
            pathFigure.IsClosed = true;
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);
            drawingContext.DrawGeometry(Fill, new Pen() { Brush = Stroke }, pathGeometry);
            Data = (Geometry)Geometry.Parse(pathGeometry.ToString());
        }
    }
}
