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
    public class CornerRadiusMask : Border
    {
        public static new readonly DependencyProperty BorderThicknessProperty =
         DependencyProperty.Register("BorderThickness", typeof(int), typeof(CornerRadiusMask), new PropertyMetadata(default(int)));

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(SolidColorBrush), typeof(CornerRadiusMask), new PropertyMetadata(Brushes.Transparent));

        public SolidColorBrush Fill
        {
            get { return (SolidColorBrush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        public new int BorderThickness
        {
            get { return (int)GetValue( BorderThicknessProperty); }
            set { SetValue( BorderThicknessProperty, value); }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            DrawContext(drawingContext);
        }
        private void DrawContext(DrawingContext drawingContext)
        {

            // 左侧
            Point firstpoint = new Point(ActualHeight / 2, 0);
            Point secondpoint = new Point(ActualHeight / 2, ActualHeight);
            Point thirdpoint = new Point(0, ActualHeight);
            Point fourpoint = new Point(0, 0);

            PathFigure figure1 = new PathFigure();

            figure1.StartPoint = firstpoint;
            figure1.Segments.Add(new ArcSegment { Point = secondpoint, Size = new Size(ActualHeight / 2, ActualHeight / 2) });
            figure1.Segments.Add(new LineSegment { Point = thirdpoint });
            figure1.Segments.Add(new LineSegment { Point = fourpoint });
            figure1.Segments.Add(new LineSegment { Point = firstpoint });

            // 右侧
            firstpoint = new Point(ActualWidth - ActualHeight / 2, 0);
            secondpoint = new Point(ActualWidth - ActualHeight / 2, ActualHeight); ;
            thirdpoint = new Point(ActualWidth, ActualHeight);
            fourpoint = new Point(ActualWidth, 0);

            PathFigure figure2 = new PathFigure();
            figure2.StartPoint = firstpoint;
            figure2.Segments.Add(new ArcSegment { Point = secondpoint, Size = new Size(ActualHeight / 2, ActualHeight / 2), SweepDirection = SweepDirection.Clockwise });
            figure2.Segments.Add(new LineSegment { Point = thirdpoint });
            figure2.Segments.Add(new LineSegment { Point = fourpoint });
            figure2.Segments.Add(new LineSegment { Point = firstpoint });

            // 画左侧和右侧的区域
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(figure1);
            pathGeometry.Figures.Add(figure2);
            drawingContext.DrawGeometry(Background, new Pen(), pathGeometry);

            // 画中间圆环
            firstpoint = new Point(ActualHeight / 2, 0);
            secondpoint = new Point(ActualHeight / 2, ActualHeight);
            thirdpoint = new Point(ActualWidth - ActualHeight / 2, ActualHeight);
            fourpoint = new Point(ActualWidth - ActualHeight / 2, 0);
            PathFigure figure3 = new PathFigure();
            figure3.StartPoint = firstpoint;
            figure3.Segments.Add(new ArcSegment { Point = secondpoint, Size = new Size(ActualHeight / 2, ActualHeight / 2), });
            figure3.Segments.Add(new LineSegment { Point = thirdpoint });
            figure3.Segments.Add(new ArcSegment { Point = fourpoint, Size = new Size(ActualHeight / 2, ActualHeight / 2) });
            figure3.Segments.Add(new LineSegment { Point = firstpoint });
            PathGeometry pathGeometry3 = new PathGeometry();
            pathGeometry3.Figures.Add(figure3);

            drawingContext.DrawGeometry(Fill, new Pen(BorderBrush, BorderThickness), pathGeometry3);

        }
    }
}
