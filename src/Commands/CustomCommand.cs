using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WYW.UI.Commands
{
    internal class CustomCommand
    {
        public static RoutedCommand OKCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();
        public static RoutedCommand YesCommand = new RoutedCommand();
        public static RoutedCommand NoCommand = new RoutedCommand();
        public static RoutedCommand CloseCommand = new RoutedCommand();
        public static RoutedCommand RemoveCommand = new RoutedCommand();
        public static RoutedCommand RemoveAllCommand = new RoutedCommand();
    }
}
