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
        private string name;
        private Gender gender;
        private bool isChecked;
        private string password = null;
        private string major;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            get => id;
            set => SetProperty(ref id, value);
        }


        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public Gender Gender
        {
            get => gender;
            set => SetProperty(ref gender, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked
        {
            get => isChecked;
            set => SetProperty(ref isChecked, value);
        }

     
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        /// <summary>
        /// 专业
        /// </summary>
        public string Major
        {
            get => major;
            set => SetProperty(ref major, value);
        }

        private int groupIndex;

        /// <summary>
        /// 
        /// </summary>
        public int GroupIndex { get => groupIndex; set => SetProperty(ref groupIndex, value); }
        private Visibility visibility= Visibility.Visible;

        /// <summary>
        /// 
        /// </summary>
        public Visibility Visibility { get => visibility; set => SetProperty(ref visibility, value); }

        public override string ToString()
        {
            return $@"ID:{ID}，姓名：{Name}，性别：{Gender}，专业：{Major}，分组索引：{GroupIndex}，是否选中：{IsChecked}";
        }
        public double[] Scores { get; set; }
    }
    public enum Gender
    {
        男,
        女
    }
}
