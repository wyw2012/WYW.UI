using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Items.Add(new Student() { ID = 1, Name = "张闪", Gender = Gender.男});
            Items.Add(new Student() { ID = 2, Name = "李四", Gender = Gender.女 });
            Items.Add(new Student() { ID = 3, Name = "王五", Gender = Gender.男, IsChecked = true });
            TestCommand = new RelayCommand(Test);
        }
        public List<Student> Items { get; } = new List<Student>();


        public RelayCommand TestCommand { get; private set; }

        private void Test()
        {
            foreach (var item in Items)
            {
                Debug.WriteLine(item);
            }
        }

    }
}
