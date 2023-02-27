using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WYW.UI.Test.Models;

namespace WYW.UI.Test.Views
{
    /// <summary>
    /// TempTest.xaml 的交互逻辑
    /// </summary>
    public partial class TempTest : UserControl
    {
        Student stu = new Student() { Name = "王彦为" };
        public TempTest()
        {
            var s =(Geometry)Geometry.Parse("");

            InitializeComponent();
            this.DataContext = stu;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(stu.Name);
        }
    }
}
