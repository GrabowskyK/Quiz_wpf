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
using wpf___projekt.Model;
using wpf___projekt.ViewModel;

namespace wpf___projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            DataContext = new MenuViewModel();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void listBoxQuiz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
