using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using wpf___projekt.Model;
using wpf___projekt.ViewModel.BaseClass;


//TODO: Usuneło się Muzyka, i sprawdź jak działa edit!



namespace wpf___projekt.ViewModel
{
    class EditQuizViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int currentIndex = 0;
        private long quiz_id = Quiz.AllQuiz.FirstOrDefault(quiz => quiz.Id == Question.Questions.ElementAt(0).Id_quiz).Id;
        public EditQuizViewModel() 
        {
            NextQuestion = new RelayCommand(NextQuestionFunc);
            PreviousQuestion = new RelayCommand(PreviousQuestionFunc);
            Zapisz = new RelayCommand(ZapiszFunc);
            LoadAnswer(currentIndex);

            //id quizu
            long quiz_id = Quiz.AllQuiz.FirstOrDefault(quiz => quiz.Id == Question.Questions.ElementAt(0).Id_quiz).Id;//nazwa quizu
            QuizNazwa = Quiz.AllQuiz.FirstOrDefault(quiz => quiz.Id == quiz_id).Nazwa;
        }

        //Command
        public ICommand NextQuestion { get; set; }
        public ICommand PreviousQuestion { get; set; }
        public ICommand Zapisz { get; set; }

        //Function
        public void ZapiszFunc(object obj)
        {

            Question.Questions.ElementAt(currentIndex).Name = QuestionName;
            Question.Questions.ElementAt(currentIndex).Answer_A = Answer_A;
            Question.Questions.ElementAt(currentIndex).Answer_B = Answer_B;
            Question.Questions.ElementAt(currentIndex).Answer_C = Answer_C;
            Question.Questions.ElementAt(currentIndex).Answer_D = Answer_D;
            switch (CorrectOption)
            {
                case 0:
                    CorrectOption = 0;
                    break;
                case 1:
                    CorrectOption = 1;
                    break;
                case 2:
                    CorrectOption = 2;
                    break;
                case 3:
                    CorrectOption = 3;
                    break;
            }
            Question.Questions.ElementAt(currentIndex).Correct = CorrectOption;
            DataAccessQuiz.DeleteData($"DELETE FROM Question WHERE id_quiz = {quiz_id};");
                string text = "";
                foreach (var item in Question.Questions)
                {
                    //text += $"(\"{item.Name}\",\"{item.Answer_A}\",\"{item.Answer_B}\",\"{item.Answer_C}\",\"{item.Answer_D}\",{item.Correct},{quiz_id}),";
                    text += $"(\'{item.Name}\',\'{item.Answer_A}\',\'{item.Answer_B}\',\'{item.Answer_C}\',\'{item.Answer_D}\',{item.Correct},{quiz_id}),";

                }
                text = text.Remove(text.Length - 1, 1);
                DataAccessQuiz.SaveData($"INSERT INTO Question (nazwaPytania,odp1,odp2,odp3,odp4,popr_odp,id_quiz) values {text};");
                Question.Questions.Clear();
        }

        public void LoadAnswer(int index) 
        {
            NumerPytania = (currentIndex + 1).ToString();
            if (currentIndex == Question.Questions.Count - 1) 
            {
                EnabledNextQuestion = false;
            }
            else
            {
                EnabledNextQuestion = true;
            }

            if(currentIndex == 0)
            {
                EnabledPreviousQuestion = false;
            }
            else
            {
                EnabledPreviousQuestion = true;
            }
            QuestionName = Question.Questions.ElementAt(index).Name;
            Answer_A = Question.Questions.ElementAt(index).Answer_A;
            Answer_B = Question.Questions.ElementAt(index).Answer_B;
            Answer_C = Question.Questions.ElementAt(index).Answer_C;
            Answer_D = Question.Questions.ElementAt(index).Answer_D;
            Question.Questions.ElementAt(index).Correct = CorrectOption;
            switch (Question.Questions.ElementAt(index).Correct)
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

        public void PreviousQuestionFunc(object obj)
        {
            Question.Questions.ElementAt(currentIndex).Name = QuestionName;
            Question.Questions.ElementAt(currentIndex).Answer_A = Answer_A;
            Question.Questions.ElementAt(currentIndex).Answer_B = Answer_B;
            Question.Questions.ElementAt(currentIndex).Answer_C = Answer_C;
            Question.Questions.ElementAt(currentIndex).Answer_D = Answer_D;

            currentIndex -= 1;
            LoadAnswer(currentIndex);
            
        }
        public void NextQuestionFunc(object obj)
        {

            Question.Questions.ElementAt(currentIndex).Name = QuestionName;
            Question.Questions.ElementAt(currentIndex).Answer_A = Answer_A;
            Question.Questions.ElementAt(currentIndex).Answer_B = Answer_B;
            Question.Questions.ElementAt(currentIndex).Answer_C = Answer_C;
            Question.Questions.ElementAt(currentIndex).Answer_D = Answer_D;
            Question.Questions.ElementAt(currentIndex).Correct = CorrectOption;
            
            

            currentIndex += 1;
            LoadAnswer(currentIndex);
            
        }

        //Property
        private bool enabledPreviousQuestion;
        public bool EnabledPreviousQuestion
        {
            get { return enabledPreviousQuestion; }
            set
            {
                enabledPreviousQuestion = value;
                onPropertyChanged(nameof(EnabledPreviousQuestion));
            }
        }
        private bool enabledNextQuestion;
        public bool EnabledNextQuestion
        {
            get { return enabledNextQuestion; }
            set
            {
                enabledNextQuestion = value;
                onPropertyChanged(nameof(EnabledNextQuestion));
            }
        }

        private string answer_A;
        public string Answer_A
        {
            get { return answer_A; }
            set
            {
                answer_A = value;
                onPropertyChanged(nameof(Answer_A));
            }
        }
        private string answer_B;
        public string Answer_B
        {
            get { return answer_B; }
            set
            {
                answer_B = value;
                onPropertyChanged(nameof(Answer_B));
            }
        }
        private string answer_C;
        public string Answer_C
        {
            get { return answer_C; }
            set
            {
                answer_C = value;
                onPropertyChanged(nameof(Answer_C));
            }
        }
        private string answer_D;
        public string Answer_D
        {
            get { return answer_D; }
            set
            {
                answer_D = value;
                onPropertyChanged(nameof(Answer_D));
            }
        }

        private string questionName;
        public string QuestionName
        {
            get { return questionName; }
            set
            {
                questionName = value;
                onPropertyChanged(nameof(QuestionName));
            }
        }
        private string quizNazwa;
        public string QuizNazwa
        {
            get
            {
                return quizNazwa;
            }
            set
            {
                quizNazwa = value;
                onPropertyChanged(nameof(QuizNazwa));
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
                CorrectOption = 0;
                onPropertyChanged(nameof(IsCheck_A));
                
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
                CorrectOption = 1;
                    onPropertyChanged(nameof(IsCheck_B));
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
                CorrectOption = 2;
                    onPropertyChanged(nameof(IsCheck_C));
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
                CorrectOption = 3;
                    onPropertyChanged(nameof(IsCheck_D));
            }
        }
        private long correctOption;
        public long CorrectOption
        {
            get
            {
                return correctOption;
            }
            set
            {
                correctOption = value;
            }
        }
        private string numerPytania;
        public string NumerPytania
        {
            get
            {
                return numerPytania;
            }
            set
            {
                numerPytania = $"Pytanie {value} z {Question.Questions.Count}";
                onPropertyChanged(nameof(NumerPytania));
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
