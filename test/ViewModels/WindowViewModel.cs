using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WYW.UI.Test.Views;
using WYW.UI.Test.Views.Windows;

namespace WYW.UI.Test.ViewModels
{
    class WindowViewModel:ObservableObject
    {
        public RelayCommand NewNormalWindowCommand { get; } = new RelayCommand(() =>
        {
             new NormalWindow().Show();
        });
        public RelayCommand NewDefaultWindowCommand { get; } = new RelayCommand(() =>
        {
            new DefaultWindow().Show();
        });
        public RelayCommand NewNoTitleBarWindowCommand { get; } = new RelayCommand(() =>
        {
            new NoTitleBarWindow().Show();
        });
        public RelayCommand NewWithMenuWindowCommand { get; } = new RelayCommand(() =>
        {
            new WithMenuWindow().Show();
        });
        public RelayCommand NewCornerRadiusWindowCommand { get; } = new RelayCommand(() =>
        {
            new CornerRadiusWindow().Show();
        });
    }
}
