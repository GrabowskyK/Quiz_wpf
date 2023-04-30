using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using wpf___projekt.Model;
using wpf___projekt.ViewModel.BaseClass;

namespace wpf___projekt.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Question question = new Question();
        private static bool isRun = true;
        private int indexList = 0;
        public MainViewModel()
        {
            NextQuestion = new RelayCommand(NextQuestionFunc);
            PreviousQuestion = new RelayCommand(PreviousQuestionFunc);
            StartQuiz = new RelayCommand(StarQuizFunc);
        }

        public ICommand StartQuiz { get; set; }
        private void StarQuizFunc(object obj)
        {
            LoadData(0);
        }
        private void NextQuestionFunc(object obj)
        {
            var a = _isCheck_A;
            if(indexList < Question.Answers.Count-1)
            {
                indexList++;
                LoadData(indexList);
            } 
            else
            {
                indexList = Question.Answers.Count - 1;
                LoadData(indexList);
            }
        }
        private void PreviousQuestionFunc(object obj)
        {
            if(indexList > 0)
            {
                indexList--;
                LoadData(indexList);

            }
            else
            {
                indexList = 0;
                LoadData(indexList);
            }
        }

        private void LoadData(int indexList)
        {
            var question = Question.Answers[indexList];
            Name = question.Name;
            Answer_A = question.Answer_A;
            Answer_B = question.Answer_B;
            Answer_C = question.Answer_C;
            Answer_D = question.Answer_D;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_A)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_B)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_C)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_D)));

        }

        
        public ICommand NextQuestion { get; set; }
        public ICommand PreviousQuestion { get; set; }
        public ICommand Add { get; set; }



        private bool _isCheck_A;
        public bool IsCheck_A
        {
            get
            {
                return _isCheck_A;
            }
            set
            {
                _isCheck_A = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCheck_A)));
            }
        }
        




        private string _name;
        private string _answer_A;
        private string _answer_B;
        private string _answer_C;
        private string _answer_D;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Answer_A
        {
            get
            {
                return _answer_A;
            }
            set
            {
                _answer_A = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_A)));
            }
        }

        public string Answer_B
        {
            get
            {
                return _answer_B;
            }
            set
            {
                _answer_B = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_B)));
            }
        }

        public string Answer_C
        {
            get
            {
                return _answer_C;
            }
            set
            {
                _answer_C = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_C)));
            }
        }

        public string Answer_D
        {
            get
            {
                return _answer_D;
            }
            set
            {
                _answer_D = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_D)));
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

