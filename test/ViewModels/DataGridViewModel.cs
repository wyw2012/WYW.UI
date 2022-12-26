using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WYW.UI.Test.Models;

namespace WYW.UI.Test.ViewModels
{
    internal class DataGridViewModel: ObservableObject
    {
        public DataGridViewModel()
        {
            Items.Add(new Student() { ID = 1, Name = "张闪", Gender = 0 });
            Items.Add(new Student() { ID = 2, Name = "李四", Gender = 1 });
            Items.Add(new Student() { ID = 3, Name = "王五", Gender = 0, IsChecked = true });
        }
        public List<Student> Items { get; } = new List<Student>();
    }
}
