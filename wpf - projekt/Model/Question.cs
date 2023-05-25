using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf___projekt.Model
{
    internal class Question
    {
        public int Id;
        public string Name;
        public string Answer_A;
        public string Answer_B;
        public string Answer_C;
        public string Answer_D;
        public long Correct;
        public long Id_quiz;

        public Question(string name, string answer_A, string answer_B, string answer_C, string answer_D, long correct, long id_quiz)
        {
            Name = name;
            Answer_A = answer_A;
            Answer_B = answer_B;
            Answer_C = answer_C;
            Answer_D = answer_D;
            Correct = correct;
            Id_quiz = id_quiz;
        }

        public Question() { }

        static public List<Question> Questions = new List<Question>();
        static public List<Question> EditedQuestions = new List<Question>();
    }
}
