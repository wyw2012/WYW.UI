using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WYW.UI.Commands
{
    /// <summary>
    /// Window窗体命令
    /// </summary>
    class WindowCommand
    {
        public static readonly RelayCommand<Window> CloseWindowCommand = new RelayCommand<Window>((e)=>
        {
            SystemCommands.CloseWindow(e);
        });
        public static readonly RelayCommand<Window> MaximizeWindowCommand = new RelayCommand<Window>((e) =>
        {
            SystemCommands.MaximizeWindow(e);
        });
        public static readonly RelayCommand<Window> MinimizeWindowCommand = new RelayCommand<Window>((e) =>
        {
            SystemCommands.MinimizeWindow(e);
        });
        public static readonly RelayCommand<Window> RestoreWindowCommand = new RelayCommand<Window>((e) =>
        {
            SystemCommands.RestoreWindow(e);
        });
    }
}
