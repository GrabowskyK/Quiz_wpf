using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf___projekt.ViewModel.BaseClass;

namespace wpf___projekt.ViewModel
{
    class TestViewModel : INotifyPropertyChanged
    {
            public event PropertyChangedEventHandler PropertyChanged;
            public TestViewModel()
            {

            }
        public void onPropertyChanged(params string[] properties)
        {
            if (PropertyChanged != null)
                foreach (var property in properties)
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
