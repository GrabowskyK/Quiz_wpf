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
        public long Id;
        public string Nazwa;
        
        public Quiz(long id,string nazwa)
        {
            Id = id;
            Nazwa = nazwa;
        }
        static public ObservableCollection<string> nazwaQuiz = new ObservableCollection<string>();
        static public List<Quiz> AllQuiz = new List<Quiz>();

        public int GetQuizId()
        {
            return 0;
        }
    }
}
