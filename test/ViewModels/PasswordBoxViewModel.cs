namespace WYW.UI.Test.ViewModels
{
    internal class PasswordBoxViewModel: ObservableObject
    {
        private string password=""; // 默认值不能为null

        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

    }
}
