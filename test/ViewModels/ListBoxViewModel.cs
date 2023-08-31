using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WYW.UI.Test.Models;

namespace WYW.UI.Test.ViewModels
{
    internal class ListBoxViewModel: ObservableObject
    {
        public ListBoxViewModel()
        {
            Students = new Student[5];
            for (int i = 0; i < 5; i++)
            {
                Students[i] = new Student() { ID=(i+1),Name=$"终结者{i+1}号",IsChecked=true};
            }
            Students[2].Visibility=Visibility.Collapsed; 
        }
        public Student[] Students { get; set; }

        public string[] Items { get; set; } = { "1", "2", "3", "4", "5" };
    }
   
}
