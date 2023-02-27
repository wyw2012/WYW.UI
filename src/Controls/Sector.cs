using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using WYW.UI.Common;

namespace WYW.UI.Controls
{
    /// <summary>
    /// 扇形
    /// </summary>
    public class Sector : Shape
    {
        protected override Geometry DefiningGeometry => (Geometry)GetValue(DataProperty);

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(Sector), new PropertyMetadata(0.0));
        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register("EndAngle", typeof(double), typeof(Sector), new PropertyMetadata(90.0));
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(Geometry), typeof(Sector), new PropertyMetadata(Geometry.Parse("")));

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
            double angel = EndAngle % 360 - StartAngle % 360; 
            bool isLargeArc = angel >= 180 ? true : false;

            Point centerPoint = new Point(ActualWidth / 2, ActualHeight / 2);
            double radius = Math.Min(ActualWidth, ActualHeight) / 2;

            // 计算半径与坐标
            //     secondpoint  *
            //                 *   
            //                *   
            //               *          *  centerPoint
            //  firstpoint  *   
            //          
            Point firstpoint = AngelHelper.GetPointByAngel(centerPoint, radius, StartAngle);
            Point secondpoint = AngelHelper.GetPointByAngel(centerPoint, radius, EndAngle);
            
            PathFigure pathFigure = new PathFigure();
            // Add的次序不能错位
            pathFigure.StartPoint = firstpoint;
            pathFigure.Segments.Add(new ArcSegment { Point = secondpoint, IsLargeArc = isLargeArc, Size = new Size(radius, radius), SweepDirection = SweepDirection.Clockwise });
            pathFigure.Segments.Add(new LineSegment { Point = centerPoint });
        
            pathFigure.IsClosed = true;
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(pathFigure);
            drawingContext.DrawGeometry(Fill, new Pen() { Brush = Stroke }, pathGeometry);
            Data = (Geometry)Geometry.Parse(pathGeometry.ToString());
        }

       
    }
}
