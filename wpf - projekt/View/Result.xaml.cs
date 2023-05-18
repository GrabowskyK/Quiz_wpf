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
using System.Windows.Shapes;
using wpf___projekt.ViewModel;

namespace wpf___projekt.View
{
    /// <summary>
    /// Logika interakcji dla klasy Result.xaml
    /// </summary>
    public partial class Result : Window
    {
        public Result()
        {
            InitializeComponent();
            DataContext = new ResultViewModel();
        }
    }
}
