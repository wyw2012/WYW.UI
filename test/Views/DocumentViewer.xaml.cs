using System;
using System.Collections.Generic;
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
using System.Windows.Xps.Packaging;

namespace WYW.UI.Test.Views
{
    /// <summary>
    /// DocumentViewer.xaml 的交互逻辑
    /// </summary>
    public partial class DocumentViewer : UserControl
    {
        public DocumentViewer()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            XpsDocument doc = new XpsDocument("docs\\demo.xps", System.IO.FileAccess.Read);
            documentViewer1.Document = doc.GetFixedDocumentSequence();
        }
    }
}
