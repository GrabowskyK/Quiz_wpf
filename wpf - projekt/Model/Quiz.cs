using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf___projekt.Model
{
    internal class Quiz
    {
        private int Id;
        private string Nazwa;
        static public ObservableCollection<string> nazwaQuiz = new ObservableCollection<string>();
        public Quiz(string nazwa)
        {
            Nazwa = nazwa;
        }
    }
}
