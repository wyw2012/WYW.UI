using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYW.UI.Controls
{
    public enum MessageBoxImage
    {
        None = 0,
        Question = 0x20,
        Error = 0x10,
        Warning = 48,
        Sucess = 0x40,
        /// <summary>
        /// 自定义图标
        /// </summary>
        Custom=0x80,
    }
    public enum CornerPlacement
    {
        /// <summary>
        /// 左上角
        /// </summary>
        TopLeft,
        /// <summary>
        /// 右上角
        /// </summary>
        TopRight,
        /// <summary>
        /// 左下角
        /// </summary>
        BottomLeft,
        /// <summary>
        /// 右下角
        /// </summary>
        BottomRight,
    }
    /// <summary>
    /// StepBarItem的状态
    /// </summary>
    public enum EventStatus
    {
        /// <summary>
        /// 已完成
        /// </summary>
        Completed,

        /// <summary>
        /// 进行中
        /// </summary>
        Running,

        /// <summary>
        /// 等待中
        /// </summary>
        Waiting
    }
}
