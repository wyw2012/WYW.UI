using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WYW.UI.Test.ViewModels
{
    class NumericUpDownViewModel : ObservableObject
    {
        public NumericUpDownViewModel()
        {
            ChangeMaximumCommand=new RelayCommand(ChangeMaximum);
        }
        private double _value=20;

        /// <summary>
        /// 
        /// </summary>
        public double Value { get => _value; set => SetProperty(ref _value, value); }

        private double maximum=100.0;

        /// <summary>
        /// 
        /// </summary>
        public double Maximum { get => maximum; set => SetProperty(ref maximum, value); }


        public RelayCommand ChangeMaximumCommand { get; private set; }

        private void ChangeMaximum()
        {
           
        }

    }
}
