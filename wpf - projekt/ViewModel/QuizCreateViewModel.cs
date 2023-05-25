using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using wpf___projekt.Model;
using wpf___projekt.ViewModel.BaseClass;

namespace wpf___projekt.ViewModel
{
    class QuizCreateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool clickedButtonPrevious = false;
        private bool clickedButtonNext = false;
        private int index = 0;
        private bool enabledNextQuestion;
        public QuizCreateViewModel()
        {
            NextQuestion = new RelayCommand(NextQuestionFunc);
            CreateQuiz = new RelayCommand(ExportToDatabase);
        }


        //Command
        public ICommand CreateQuiz { get; set; }
        public ICommand NextQuestion { get; set; }
        public ICommand PreviousQuestion { get; set; }


        //Function
        private void ExportToDatabase(object obj)
        {
            DataAccessQuiz.ReadData($"SELECT * FROM Quiz");
            if (Quiz.nazwaQuiz.Contains(QuizNazwa))
            {
                MessageBox.Show("Quiz z taką nazwą już istnieje!");
                Quiz.nazwaQuiz.Clear();
            }
            else
            {
                Quiz.nazwaQuiz.Clear();
                Quiz.AllQuiz.Clear();
                DataAccessQuiz.SaveData($"INSERT INTO Quiz (nazwa) values (\"{QuizNazwa}\")");
                DataAccessQuiz.ReadData($"SELECT * FROM Quiz"); //A co jak istnieje?

                long quiz_id = Quiz.AllQuiz.FirstOrDefault(quiz => quiz.Nazwa == QuizNazwa).Id;
                string text = "";
                foreach (var item in Question.Questions)
                {
                    //text += $"(\"{item.Name}\",\"{item.Answer_A}\",\"{item.Answer_B}\",\"{item.Answer_C}\",\"{item.Answer_D}\",{item.Correct},{quiz_id}),";
                    text += $"(\'{item.Name}\',\'{item.Answer_A}\',\'{item.Answer_B}\',\'{item.Answer_C}\',\'{item.Answer_D}\',{item.Correct},{quiz_id}),";

                }
                text = text.Remove(text.Length - 1, 1);
                DataAccessQuiz.SaveData($"INSERT INTO Question (nazwaPytania,odp1,odp2,odp3,odp4,popr_odp,id_quiz) values {text}");
                Question.Questions.Clear();
                MessageBox.Show("Pomyślnie stworzono Quiz!");
            }
        }
        public void NextQuestionFunc(object obj)
        {
            EnableCreateQuizButton = true;
            if (index == 0 && QuestionName != "" && Answer_A != "" && Answer_B != "" && Answer_C != "" && answer_D != "" && (correctAnswer != 4 || correctAnswer != null))
            {
                Question question = new Question(QuestionName, Answer_A, answer_B, answer_C, answer_D, CorrectAnswer, 0);
                Question.Questions.Add(question);
                QuestionName = "";
                Answer_A = "";
                Answer_B = "";
                Answer_C = "";
                Answer_D = "";
                IsCheck_A = false;
                IsCheck_B = false;
                IsCheck_C = false;
                IsCheck_D = false;
                correctAnswer = 4;
                EnabledNextQuestion = false;
                NumerPytania = "Pytanie " + (Question.Questions.Count() + 1).ToString();
                onPropertyChanged(nameof(EnabledNextQuestion));
            }
            else
            {
                clickedButtonNext = true;
                if (clickedButtonPrevious == true)
                {
                    index -= 2;
                    clickedButtonPrevious = false;
                }
                else if (index > 0)
                {
                    index--;
                }
                var result = Question.Questions.ToArray();
                QuestionName = result[Question.Questions.Count - 1 - index].Name;
                Answer_A = result[Question.Questions.Count - 1 - index].Answer_A;
                Answer_B = result[Question.Questions.Count - 1 - index].Answer_B;
                Answer_C = result[Question.Questions.Count - 1 - index].Answer_C;
                Answer_D = result[Question.Questions.Count - 1 - index].Answer_D;
                switch (result[Question.Questions.Count - 1 - index].Correct)
                {
                    case 0:
                        IsCheck_A = true;
                        break;
                    case 1:
                        IsCheck_B = true;
                        break;
                    case 2:
                        IsCheck_C = true;
                        break;
                    case 3:
                        IsCheck_D = true;
                        break;
                }
            }
        }
        private void IsAll()
        {
            if (QuestionName != null && Answer_A != null && Answer_B != null && Answer_C != null && answer_D != null && correctAnswer != 4)

            {
                EnabledNextQuestion = true;
            }
            else
            {
                EnabledNextQuestion = false;
            }
        }


        //Property
        private bool enableCreateQuizButton = false;
        public bool EnableCreateQuizButton
        {
            get
            {
                return enableCreateQuizButton;
            }
            set
            {
                enableCreateQuizButton = value;
                onPropertyChanged(nameof(EnableCreateQuizButton));
            }
        }
        public bool EnabledNextQuestion
        {
            get { return enabledNextQuestion; }
            set
            {
                enabledNextQuestion = value;
                onPropertyChanged(nameof(EnabledNextQuestion));
            }
        }

        private string quizNazwa = "Nazwa Quizu...";
        public string QuizNazwa
        {
            get { return quizNazwa; }
            set
            {
                quizNazwa = value;
                onPropertyChanged(nameof(QuizNazwa));
            }
        }

        private string answer_A = "Odpowiedź A...";
        public string Answer_A
        {
            get { return answer_A; }
            set
            {
                answer_A = value;
                IsAll();
                onPropertyChanged(nameof(Answer_A));
            }
        }

        private string answer_B = "Odpowiedź B...";
        public string Answer_B
        {
            get { return answer_B; }
            set
            {
                answer_B = value;
                IsAll();
                onPropertyChanged(nameof(Answer_B));
            }
        }

        private string answer_C = "Odpowiedź C...";
        public string Answer_C
        {
            get { return answer_C; }
            set
            {
                answer_C = value;
                IsAll();
                onPropertyChanged(nameof(Answer_C));
            }
        }

        private string answer_D = "Odpowiedź D...";
        public string Answer_D
        {
            get { return answer_D; }
            set
            {
                answer_D = value;
                IsAll();
                onPropertyChanged(nameof(Answer_D));
            }
        }

        private string questionName = "Treść pytania...";
        public string QuestionName
        {
            get { return questionName; }
            set
            {
                questionName = value;
                IsAll();
                onPropertyChanged(nameof(QuestionName));
            }
        }

        private long correctAnswer = 4;
        public long CorrectAnswer
        {
            get { return correctAnswer; }
            set
            {
                correctAnswer = value;
                IsAll();
            }
        }

        private string numerPytania = "Pytanie 1";
        public string NumerPytania
        {
            get { return numerPytania; }
            set
            {
                numerPytania = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumerPytania)));
            }
        }

        private bool isCheck_A;
        public bool IsCheck_A
        {
            get
            {
                return isCheck_A;
            }
            set
            {
                isCheck_A = value;
                CorrectAnswer = 0;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCheck_A)));
            }
        }
        private bool isCheck_B;
        public bool IsCheck_B
        {
            get
            {
                return isCheck_B;
            }
            set
            {
                isCheck_B = value;
                CorrectAnswer = 1;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCheck_B)));
            }
        }
        private bool isCheck_C;
        public bool IsCheck_C
        {
            get
            {
                return isCheck_C;
            }
            set
            {
                isCheck_C = value;
                CorrectAnswer = 2;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCheck_C)));
            }
        }
        private bool isCheck_D;
        public bool IsCheck_D
        {
            get
            {
                return isCheck_D;
            }
            set
            {
                isCheck_D = value;
                CorrectAnswer = 3;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCheck_D)));
            }
        }

        public void onPropertyChanged(params string[] properties)
        {
            if (PropertyChanged != null)
                foreach (var property in properties)
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
