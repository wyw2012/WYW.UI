using System;
using System.Windows;

namespace WYW.UI.Common
{
    /// <summary>
    /// 角度辅助类
    /// </summary>
    internal class AngelHelper
    {
        /// <summary>
      /// 根据圆弧角度获取坐标
      /// </summary>
      /// <param name="centerPoint">圆弧中心坐标</param>
      /// <param name="radius">圆半径</param>
      /// <param name="angle">圆弧角度，12点钟位置为0度，顺时针</param>
      /// <returns></returns>
        public static Point GetPointByAngel(Point centerPoint, double radius, double angle)
        {
            double x = centerPoint.X + Math.Sin((Math.PI / 180.0) * angle) * radius;
            double y = centerPoint.Y - Math.Cos((Math.PI / 180.0) * angle) * radius;
            return new Point(x, y);
        }
        public static Point GetPointByAngel(double centerX, double centerY, double angle, double radius)
        {
            double x = centerX + Math.Sin((Math.PI / 180.0) * angle) * radius;
            double y = centerY - Math.Cos((Math.PI / 180.0) * angle) * radius;
            return new Point(x, y);
        }
    }
}
