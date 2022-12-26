using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYW.UI.Test.Models
{
    internal class Student : ObservableObject
    {
        private int id;

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private string name;

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private int gender;

        /// <summary>
        /// 
        /// </summary>
        public int Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }

        private bool isChecked;

        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked
        {
            get => isChecked;
            set => SetProperty(ref isChecked, value);
        }

        private string password = null;

        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }


    }
    public enum Gender
    {
        Male,
        Female
    }
}
