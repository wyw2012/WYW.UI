using System.Windows.Controls;

namespace WYW.UI.Commands
{
    class TabControlCommand
    {
        public static readonly RelayCommand<TabControl> CloseTabItemCommand = new RelayCommand<TabControl>((e) =>
        {
            if (e == null)
                return;

            int removeIndex = -1;
            int index = 0;
            foreach (TabItem item in e.Items)
            {
                if (item.IsMouseOver)
                {
                    removeIndex = index;
                    break;
                }
                index++;
            }
            if (removeIndex > -1)
            {
                e.Items.RemoveAt(removeIndex);
            }
        });
    }
}
