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
        private bool IsCorrect_A;
        private bool IsCorrect_B;
        private bool IsCorrect_C;
        private bool IsCorrect_D;

        public int numer { get; set; }

        public Question(string name, string answer_A, string answer_B, string answer_C, string answer_D, bool isCorrect_A, bool isCorrect_B, bool isCorrect_C, bool isCorrect_D)
        {
            Name = name;
            Answer_A = answer_A;
            Answer_B = answer_B;
            Answer_C = answer_C;
            Answer_D = answer_D;
            IsCorrect_A = isCorrect_A;
            IsCorrect_B = isCorrect_B;
            IsCorrect_C = isCorrect_C;  
            IsCorrect_D = isCorrect_D;
        }

        public Question() { }

        static public List<Question> Answers = new List<Question>();
    }
}
