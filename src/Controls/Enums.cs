using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYW.UI.Controls
{
    /// <summary>
    /// 消息框图标，替代ystem.Windows.MessageBoxImage
    /// </summary>
    public enum MessageBoxImage
    {
        None = 0,
        Error = 0x10,
        Question = 0x20,
        Warning = 0x30,
        Sucess = 0x40,
        /// <summary>
        /// 提示信息
        /// </summary>
        Tip = 0x50,
        /// <summary>
        /// 自定义图标
        /// </summary>
        Custom = 0x80,
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

        TopCenter,
        /// <summary>
        /// 右下角
        /// </summary>
        BottomCenter,
        /// <summary>
        /// 中心
        /// </summary>
        Center,
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
