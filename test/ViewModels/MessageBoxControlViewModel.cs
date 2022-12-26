using WYW.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYW.UI.Test.ViewModels
{
    internal class MessageBoxControlViewModel: ObservableObject
    {
 
        public RelayCommand WarningCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxControl.Warning("设备未启动");
        });
        public RelayCommand ErrorCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxControl.Error("执行失败");
        });
        public RelayCommand SucessCommand { get; } = new RelayCommand(() =>
		{
			MessageBoxControl.Success("任务完成");
		});
     
        public RelayCommand CustomCommand { get; } = new RelayCommand(() =>
        {
           MessageBoxControl.Custom("自定义图标", "\uf004","#ff123456");
        });
        public RelayCommand ClearCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxControl.Clear();
        });
        public RelayCommand SetTopRightPlacementCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxControl.Placement = CornerPlacement.TopRight;
            MessageBoxControl.Clear();
            MessageBoxControl.Success("右上角弹出");
        });
        public RelayCommand SetBottomRightPlacementCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxControl.Placement = CornerPlacement.BottomRight;
            MessageBoxControl.Clear();
            MessageBoxControl.Success("右下角弹出");
        });
        public RelayCommand SetTopLeftPlacementCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxControl.Placement = CornerPlacement.TopLeft;
            MessageBoxControl.Clear();
            MessageBoxControl.Success("左上角弹出");
        });
        public RelayCommand SetBottomLeftPlacementCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxControl.Placement = CornerPlacement.BottomLeft;
            MessageBoxControl.Clear();
            MessageBoxControl.Success("左下角弹出");
        });
    }
}
