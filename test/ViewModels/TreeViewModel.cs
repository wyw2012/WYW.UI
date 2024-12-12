using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WYW.UI.Test.Models;

namespace WYW.UI.Test.ViewModels
{
    internal class TreeViewModel
    {
        public TreeViewModel()
        {
            Items.Add(new Student() { ID = 1, Name = "张闪", Scores = new double[] { 99, 87 } });
            Items.Add(new Student() { ID = 2, Name = "李四", Scores = new double[] { 45, 98 } });
            Items.Add(new Student() { ID = 3, Name = "王五", Scores = new double[] { 78, 74, 96 } });
        }
        public List<Student> Items { get; } = new List<Student>();
    }
}
