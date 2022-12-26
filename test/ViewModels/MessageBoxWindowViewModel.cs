using WYW.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYW.UI.Test.ViewModels
{
    internal class MessageBoxWindowViewModel:ObservableObject
    {
        public RelayCommand QuestionCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxWindow.Question("是否准备好？", "询问");
        });
        public RelayCommand WarningCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxWindow.Warning("设备未启动", "警告");
        });
        public RelayCommand ErrorCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxWindow.Error("执行失败", "错误");
        });
        public RelayCommand SucessCommand { get; } = new RelayCommand(() =>
		{
			MessageBoxWindow.Success("任务完成","提示");
		});
        public RelayCommand AutoCloseCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxWindow.Success("消息框在5秒后自动关闭", "提示",isAutoClose:true);
        });
        public RelayCommand CustomCommand { get; } = new RelayCommand(() =>
        {
           MessageBoxWindow.Custom("自定义图标", "提示","\uf004","#ff123456");
        });
        public RelayCommand ShowDialogCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxWindow.Show("任务完成", "提示");
        });
        public RelayCommand ShowCommand { get; } = new RelayCommand(() =>
        {
            MessageBoxWindow.Show("任务完成", "提示",showDialog:false);
        });
    }
}
