using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYW.UI.Test.ViewModels
{
    internal class ListBoxViewModel: ObservableObject
    {
        public string[] Items { get; set; } = { "1", "2", "3", "11", "22", "33", "111" };
    }
}
