using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WYW.UI.Test.Models;

namespace WYW.UI.Test.ViewModels
{
    internal class ListViewViewModel: ObservableObject
    {
        public ListViewViewModel()
        {
            Items = new Student[20];
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = new Student() { ID = i + 1, Gender = (Gender)(i % 2), Name = $"张{i + 1}" };
            }
        }
        public Student[] Items { get; set; }
    }
}
