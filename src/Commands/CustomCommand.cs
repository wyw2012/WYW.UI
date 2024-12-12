using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WYW.UI.Commands
{
    /// <summary>
    /// 通用命令类，通常在控件的构造函数中使用CommandBindings.Add()方法添加命令绑定
    /// </summary>
    internal class CustomCommand
    {
        public static RoutedCommand OKCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();
        public static RoutedCommand YesCommand = new RoutedCommand();
        public static RoutedCommand NoCommand = new RoutedCommand();
        public static RoutedCommand CloseCommand = new RoutedCommand();
        public static RoutedCommand RemoveCommand = new RoutedCommand();
        public static RoutedCommand RemoveAllCommand = new RoutedCommand();

        public static RoutedCommand OpenCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand RestoreCommand = new RoutedCommand();
        public static RoutedCommand ReduceCommand = new RoutedCommand();
        public static RoutedCommand EnlargeCommand = new RoutedCommand();
        public static RoutedCommand RotateLeftCommand = new RoutedCommand();
        public static RoutedCommand RotateRightCommand = new RoutedCommand();
        public static RoutedCommand MouseMoveCommand = new RoutedCommand();
    }
}
