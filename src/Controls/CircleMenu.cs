using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using WYW.UI.Common;

namespace WYW.UI.Controls
{
    public class CircleMenu : ItemsControl
    {
        #region 依赖属性
        public static readonly DependencyProperty InnerRadiusProperty =
            DependencyProperty.Register("InnerRadius", typeof(double), typeof(CircleMenu), new PropertyMetadata(40.0));
        public static readonly DependencyProperty MenuThicknessProperty =
            DependencyProperty.Register("MenuThickness", typeof(double), typeof(CircleMenu), new PropertyMetadata(40.0));
        public static readonly DependencyProperty MenuMarginProperty =
            DependencyProperty.Register("MenuMargin", typeof(double), typeof(CircleMenu), new PropertyMetadata(5.0));
        public static readonly DependencyProperty SectorAngleProperty =
            DependencyProperty.Register("SectorAngle", typeof(double), typeof(CircleMenu), new PropertyMetadata(360.0));
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(CircleMenu), new PropertyMetadata(0.0));
        public static readonly DependencyProperty IsRotateTextProperty =
            DependencyProperty.Register("IsRotateText", typeof(bool), typeof(CircleMenu), new PropertyMetadata(true));
        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.Register("SelectedBackground", typeof(Brush), typeof(CircleMenu), new PropertyMetadata(Brushes.LawnGreen));

        /// <summary>
        /// 主菜单半径
        /// </summary>
        public double InnerRadius
        {
            get { return (double)GetValue(InnerRadiusProperty); }
            set { SetValue(InnerRadiusProperty, value); }
        }
        /// <summary>
        /// 子菜单厚度（高度）
        /// </summary>
        public double MenuThickness
        {
            get { return (double)GetValue(MenuThicknessProperty); }
            set { SetValue(MenuThicknessProperty, value); }
        }
        /// <summary>
        /// 子菜单之间的间距
        /// </summary>
        public double MenuMargin
        {
            get { return (double)GetValue(MenuMarginProperty); }
            set { SetValue(MenuMarginProperty, value); }
        }
        /// <summary>
        /// 扇形起始角度
        /// </summary>
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }
        /// <summary>
        /// 扇形角度
        /// </summary>
        public double SectorAngle
        {
            get { return (double)GetValue(SectorAngleProperty); }
            set { SetValue(SectorAngleProperty, value); }
        }

        /// <summary>
        /// 选中扇形的背景颜色
        /// </summary>
        public Brush SelectedBackground
        {
            get { return (Brush)GetValue(SelectedBackgroundProperty); }
            set { SetValue(SelectedBackgroundProperty, value); }
        }

        /// <summary>
        /// 文本对齐方式,是否旋转文本,默认True
        /// </summary>
        public bool IsRotateText
        {
            get { return (bool)GetValue(IsRotateTextProperty); }
            set { SetValue(IsRotateTextProperty, value); }
        }

        public Size ClientSize
        {
            get
            {
                return new Size(menuSize, menuSize);
            }
        }
        #endregion

        /// <summary>
        /// 扇形队列，含义是：层索引->扇区索引->扇区起始角度和终止角度
        /// </summary>
        private List<List<Tuple<double, double>>> sectorList = new List<List<Tuple<double, double>>>();
        private Selection selection = new Selection();

        private int currentLevel = -1;     // 菜单层索引，从0开始
        private int currentSelection = -1; // 当前扇区索引，从0开始
        private bool isSelected = false;
        private double menuSize;  // 菜单控件大小（含子菜单）

        #region 重绘尺寸
        protected override Size MeasureOverride(Size availablesize)
        {
            int layer = GetSubMenuLayerCount(Items);

            double newSize = (layer * (MenuThickness + MenuMargin) + InnerRadius) * 2;
            foreach (UIElement i in Items)
            {
                i.Measure(availablesize);
            }
            menuSize = newSize;
            return new Size(newSize, newSize);
        }

        protected override Size ArrangeOverride(Size finalsize)
        {
            return finalsize;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            sectorList.Clear();
            // Draw the menu (level 0)
            DrawMenu(this, 0, StartAngle % 360, SectorAngle, drawingContext);
        }


        private void DrawMenu(ItemsControl itemsControl, int level, double startAngle, double sectorAngle, DrawingContext drawingContext)
        {

            // Make sure sectorAngle is no more than 360 degrees
            double full_sector = Math.Min(sectorAngle, 360.0);

            // find number of menu items at current level
            // return if none
            int count = itemsControl.Items.Count;
            if (count == 0)
                return;

            // Add list to remember sectors at this level
            sectorList.Add(new List<Tuple<double, double>>());

            // Coordinates of center of (full) menu
            double x = menuSize / 2.0;
            double y = menuSize / 2.0;


            // calculate inner and outer radius for this level
            double ringRadius = MenuThickness + MenuMargin;
            double innerRadius = InnerRadius + ringRadius * level;
            if (innerRadius < MenuMargin)
                innerRadius = MenuMargin;
            double outerRadius = innerRadius + MenuThickness;

            // Sector gap is given as a length. Find the startAngle giving this gap on inner and outer radius
            double inner_gap_angle;
            if (innerRadius == 0)
                inner_gap_angle = 0;
            else
                inner_gap_angle = 2.0 * Math.Asin(MenuMargin / (2.0 * innerRadius)) * 360.0 / (2.0 * Math.PI);
            double outer_gap_angle = 2.0 * Math.Asin(MenuMargin / (2.0 * outerRadius)) * 360.0 / (2.0 * Math.PI);

            // numbers of sectorAngle gaps (one more if we are using full circle)
            int c = (full_sector < 360.0 ? count - 1 : count);

            // Calculate the inner and outer arc of each menu item
            double inner_angle = (full_sector - inner_gap_angle * c) / count;
            double outer_angle = (full_sector - outer_gap_angle * c) / count;

            // 当前子菜单外侧扇形周长
            var menuItemPerimeter = 2 * Math.PI * (InnerRadius + ringRadius * (level + 1)) * (outer_angle / 360.0);
            // draw each menu item 
            for (int i = 0; i < count; i++)
            {
                // calculate the boundaries of menu item as startAngle of the inner and outer arcs
                double start_inner_angle = startAngle + i * (full_sector / count) + inner_gap_angle / 2.0;
                double end_inner_angle = start_inner_angle + (full_sector / count) - inner_gap_angle;
                double start_outer_angle = startAngle + i * (full_sector / count) + outer_gap_angle / 2.0;
                double end_outer_angle = start_outer_angle + (full_sector / count) - outer_gap_angle;

                // remeber the boundaries (as sectorAngle angles) of the menu item
                sectorList[level].Add(new Tuple<double, double>(start_outer_angle, end_outer_angle));

                // Calculate the corners of the sectorAngle
                Point p1 = AngelHelper.GetPointByAngel(x, y, start_inner_angle, innerRadius);
                Point p2 = AngelHelper.GetPointByAngel(x, y, end_inner_angle, innerRadius);
                Point p3 = AngelHelper.GetPointByAngel(x, y, end_outer_angle, outerRadius);
                Point p4 = AngelHelper.GetPointByAngel(x, y, start_outer_angle, outerRadius);

                // find center of the corners 
                Point center = AngelHelper.GetPointByAngel(x, y, (start_inner_angle + end_inner_angle) / 2.0, (innerRadius + outerRadius) / 2.0);

                // specify the figure representing the menu item
                PathFigure pathFigure = new PathFigure();
                pathFigure.Segments = new PathSegmentCollection();
                pathFigure.StartPoint = p1;
                pathFigure.Segments.Add(new ArcSegment(p2, new Size(innerRadius, innerRadius), end_inner_angle - start_inner_angle, end_inner_angle - start_inner_angle > 180.0, SweepDirection.Clockwise, true));
                pathFigure.Segments.Add(new LineSegment(p3, true));
                pathFigure.Segments.Add(new ArcSegment(p4, new Size(outerRadius, outerRadius), end_outer_angle - start_outer_angle, end_outer_angle - start_outer_angle > 180.0, SweepDirection.Counterclockwise, true));
                pathFigure.IsClosed = true;
                pathFigure.IsFilled = true;

                // Create geometry object and add the figure
                PathGeometry geometry = new PathGeometry();
                geometry.Figures.Add(pathFigure);

                // Get the menu item to extract properties
                CircleMenuItem menuItem = itemsControl.Items[i] as CircleMenuItem;

                // find color for backgound and border 
                Brush backgroundBrush = menuItem.Background;
                if (backgroundBrush == null)
                {
                    backgroundBrush = this.Background;
                }
                if (backgroundBrush == null)
                {
                    backgroundBrush = Brushes.White;
                }

                //if (selection.GetSelection(level) == i)
                //{
                //    backgroundBrush = this.SelectedBackground;
                //}

                Brush border_brush = menuItem.BorderBrush;
                if (border_brush == null) 
                    border_brush = this.BorderBrush;

                Thickness border_thickness = menuItem.BorderThickness;
                if (border_thickness.Equals(new Thickness())) 
                    border_thickness = this.BorderThickness;


                // Draw the geometry representing the menu item
                drawingContext.DrawGeometry(backgroundBrush, new Pen(border_brush, border_thickness.Left), geometry);

                // Get header of menu item as string and make a formatted text based on properties of menu item
                String header = (String)menuItem.Header;

                FormattedText text = new FormattedText(header,
                                CultureInfo.CurrentCulture,
                                FlowDirection.LeftToRight,
                                new Typeface(menuItem.FontFamily, menuItem.FontStyle, menuItem.FontWeight, menuItem.FontStretch),
                                menuItem.FontSize,
                                menuItem.Foreground);


                // Calculate placement of text
                Point text_point = new Point(center.X - text.Width / 2.0, center.Y - text.Height / 2.0);


                // Draw the text as name of menu item
                if (this.IsRotateText)
                    drawingContext.PushTransform(new RotateTransform((start_inner_angle + end_inner_angle) / 2.0, center.X, center.Y));


                // 如果显示图标
                if (!string.IsNullOrEmpty(menuItem.ImageSource))
                {

                    ImageSource imageSource = new BitmapImage(new Uri(menuItem.ImageSource, UriKind.Relative));
                    Rect rectangle;
                    if (!string.IsNullOrEmpty(header)) // 如果显示图标和文字
                    {
                        text_point = new Point(center.X - text.Width / 2.0, center.Y - text.Height / 2.0 - ringRadius / 6);
                        rectangle = new Rect(center.X - ringRadius * 2 / 5.0 / 2.0, center.Y - ringRadius * 2 / 5.0 / 2.0 + ringRadius / 6, ringRadius * 2 / 5.0, ringRadius * 2 / 5.0);
                    }
                    else  //如果只显示图标
                    {
                        rectangle = new Rect(center.X - ringRadius * 3 / 5.0 / 2.0, center.Y - ringRadius * 3 / 5.0 / 2.0, ringRadius * 3 / 5.0, ringRadius * 3 / 5.0);
                    }
                    drawingContext.DrawImage(imageSource, rectangle);
                }
                drawingContext.DrawText(text, text_point);
                if (this.IsRotateText)
                    drawingContext.Pop();
                // if this menu item is selected, draw the next level
                if (selection.GetSelection(level) == i)
                {
                    if (menuItem.IsAutoFitSectorAngle)
                    {
                        var outer_Radius = InnerRadius + ringRadius * (level + 2);
                        menuItem.SectorAngle = menuItem.Items.Count * menuItemPerimeter / (2 * Math.PI * outer_Radius) * 360 + (menuItem.Items.Count - 1) * Math.Atan(MenuMargin / outer_Radius);
                        menuItem.SectorAngle = Math.Min(menuItem.SectorAngle, 360);
                    }
                    // start startAngle of sub menu is center startAngle of menu item minus half the sectorAngle of the sub menu
                    double new_angle = (start_inner_angle + end_inner_angle) / 2.0 - menuItem.SectorAngle / 2.0;
                    DrawMenu(menuItem, level + 1, new_angle, menuItem.SectorAngle, drawingContext);
                }
            }
        }
        #endregion

        #region 鼠标事件
        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Point click_point = e.MouseDevice.GetPosition(this);
            Down(click_point);
            e.MouseDevice.Capture(this);
            InvalidateVisual();
        }

        protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            Point point = e.MouseDevice.GetPosition(this);
            Up(point);
            // release mouse device
            e.MouseDevice.Capture(null);
            InvalidateVisual();
        }

        protected override void OnTouchDown(System.Windows.Input.TouchEventArgs e)
        {
            base.OnTouchDown(e);
            Point touch_point = e.TouchDevice.GetTouchPoint(this).Position;
            Down(touch_point);
            e.TouchDevice.Capture(this);
            e.Handled = true;
            InvalidateVisual();
        }

        protected override void OnTouchUp(System.Windows.Input.TouchEventArgs e)
        {
            base.OnTouchUp(e);
            Point point = e.TouchDevice.GetTouchPoint(this).Position;
            Up(point);
            // release touch device
            e.TouchDevice.Capture(null);
            e.Handled = true;
            InvalidateVisual();
        }

        private void Down(Point point)
        {
            // find the menu item on which the point is
            Tuple<int, int> t = FindMenuItem(point);
            int level = t.Item1;
            int selection = t.Item2;

            if (selection != -1)
            {
                // remeber the selection
                currentLevel = level;
                currentSelection = selection;

                if (selection != this.selection.GetSelection(level))
                {
                    // select if it is not already selected
                    this.selection.SetSelection(level, selection);
                    isSelected = false;
                }
                else
                {
                    // remember that is was already selected

                    isSelected = true;
                }
            }
        }

        private void Up(Point point)
        {
            if (currentLevel == -1 || currentSelection == -1) 
                return;

            // find the menu item on which the (release) point is
            Tuple<int, int> t = FindMenuItem(point);
            int level = t.Item1;
            int selection = t.Item2;

            if (level == currentLevel && selection == currentSelection)
            {
                // restore running sectorAngle backcolor

                // release happens in same menu item as the current selection

                // Find menu item and trigger actions
                ItemsControl items_control = this;
                for (int i = 0; i <= level; i++)
                {
                    int sel = this.selection.GetSelection(i);

                    items_control = (ItemsControl)items_control.Items[sel];
                }
                (items_control as CircleMenuItem).OnClick();

                if (items_control.Items.Count == 0)
                {
                    // If no children (means it is a leaf and we have come to our end), then deselect whole menu
                    this.selection.SetSelection(0, -1);
                    //_last_backcolor = itemsControl.Background; // record current backcolor
                    //itemsControl.Background = RunningBackgroud;
                }
                else if (isSelected)
                {
                    // deselect if it was already selected
                    this.selection.SetSelection(level, -1);
                }
            }
            else
            {
                // release happens somewhere else than pressdown

                // Deselect and else do nothing
                this.selection.SetSelection(currentLevel, -1);
            }

            // reset current selection
            currentLevel = -1;
            currentSelection = -1;
            isSelected = false;
        }

        #endregion

        #region  私有方法
        /// <summary>
        /// 获取子菜单层数
        /// </summary>
        /// <returns></returns>
        private int GetSubMenuLayerCount(ItemCollection items, int currentLayer = 0)
        {
            int layer = currentLayer;
            foreach (ItemsControl item in items)
            {
                if (item.Items.Count > 0)
                {
                    layer++;
                    layer = Math.Max(layer, GetSubMenuLayerCount(item.Items, currentLayer + 1));
                }
            }
            return layer;
        }

        /// <summary>
        /// 根据鼠标坐标位置获取子菜单所在的层数索引及扇区索引
        /// </summary>
        /// <param name="point"></param>
        /// <returns>层数索引，扇区索引</returns>
        private Tuple<int, int> FindMenuItem(Point point)
        {
            int level = 0;
            // coordinates of point relative to center
            double x = point.X - menuSize / 2.0;
            double y = point.Y - menuSize / 2.0;

            // distance from center
            double distance = Math.Sqrt(x * x + y * y);

            level = (int)((distance - InnerRadius) / (MenuThickness + MenuMargin));

            if (level >= sectorList.Count)
            {
                // point is outside higest level of visible menu
                return new Tuple<int, int>(-1, -1);
            }

            // startAngle in radians
            double theta = Math.Acos(Math.Abs(y / distance));

            // convert startAngle to degrees
            double angle = 0;
            if (x >= 0 && y < 0) // 0°-90°
            {
                angle = theta * (360.0 / (2.0 * Math.PI));
            }
            else if (x >= 0 && y >= 0) // 90°-180°
            {
                angle = 180.0 - theta * (360.0 / (2.0 * Math.PI));
            }
            else if (x < 0 && y >= 0) // 180°-270°
            {
                angle = 180 + theta * (360.0 / (2.0 * Math.PI));
            }
            else if (x < 0 && y < 0) // 270°-360°
            {
                angle = 360.0 - theta * (360.0 / (2.0 * Math.PI));
            }

            // run through the visible sectors at present level to see in which sectorAngle the startAngle belongs
            // must consider the possibility that angles defining sectors might be less that 0 and greater than 360
            // if rotation is less than -180 or greater than 360 there may be some problems, but this is handled in initial call to DrawMenu

            int selection = -1;

            for (int i = 0; i < sectorList[level].Count; i++)
            {
                double startAngle = sectorList[level][i].Item1;
                double endAngle = sectorList[level][i].Item2;

                // we have as invariant startAngle < endAngle

                if (startAngle < 0.0 && endAngle >= 0.0)
                {
                    if (startAngle + 360.0 <= angle || angle <= endAngle)
                    {
                        selection = i;
                        break;
                    }
                }
                else if (startAngle < 0.0 && endAngle < 0.0)
                {
                    if (startAngle + 360.0 <= angle && angle <= endAngle + 360.0)
                    {
                        selection = i;
                        break;
                    }
                }
                else if (startAngle > 360.0)
                {
                    if (startAngle - 360.0 <= angle && angle <= endAngle - 360.0)
                    {
                        selection = i;
                        break;
                    }
                }
                else if (endAngle > 360.0)  // Must have: 0.0 <= startAngle <= 360.0
                {
                    if (startAngle <= angle || angle <= endAngle - 360.0)
                    {
                        selection = i;
                        break;
                    }
                }
                else // 0.0 <= startAngle <= 360.0 && 0.0 <= endAngle <= 360.0
                {
                    if (startAngle <= angle && angle <= endAngle)
                    {
                        selection = i;
                        break;
                    }
                }
            }

            return new Tuple<int, int>(level, selection);
        }
        #endregion
    }


    class Selection
    {
        private List<int> _selection = new List<int>();

        public void SetSelection(int level, int selection)
        {
            // make sure internal list is long enough
            while (_selection.Count < level + 1)
            {
                _selection.Add(-1);
            }

            // set selection or deselect if same item is already selected
            if (_selection[level] == selection)
            {
                _selection[level] = -1;
            }
               
            else
            {
                _selection[level] = selection;
            }
               

            // deselect items at higher level
            for (int i = level + 1; i < _selection.Count; i++)
            {
                _selection[i] = -1;
            }
        }

        public int GetSelection(int level)
        {
            if (level >= _selection.Count) return -1;

            return _selection[level];
        }

        public int GetLevel()
        {
            for (int i = _selection.Count - 1; i >= 0; i--)
            {
                if (_selection[i] != -1) return i;
            }
            return -1;
        }
    }
}
