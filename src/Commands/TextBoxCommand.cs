using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WYW.UI.Commands
{
    /// <summary>
    /// TextBox命令
    /// </summary>
    internal class TextBoxCommand
    {
        public static readonly RelayCommand<TextBox> ClearCommand = new RelayCommand<TextBox>((e) =>
        {
            if (e == null)
                return;
            e.Text = "";
        });
    }
}
