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
using System.Windows.Media;
using wpf___projekt.Model;
using wpf___projekt.ViewModel.BaseClass;

namespace wpf___projekt.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Question question = new Question();
        private Player player = new Player();

        




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
            setPlayerAnswers();
            //LoadAnswersFromQuestions();
            if (indexList < Question.Questions.Count-1)
            {
                indexList++;
                LoadData(indexList);
            } 
            else
            {
                indexList = Question.Questions.Count - 1;
                LoadData(indexList);
            }
            LoadAnswersFromQuestions();
        }
        private void PreviousQuestionFunc(object obj)
        {
            setPlayerAnswers();
            if (indexList > 0)
            {
                indexList--;
                LoadData(indexList);

            }
            else
            {
                indexList = 0;
                LoadData(indexList);
            }
            LoadAnswersFromQuestions();
        }

        private void LoadData(int indexList)
        {
            var question = Question.Questions[indexList];
            Name = question.Name;
           // Answer_A = question.Answer_A;
            Answer_A = question.Answer_A;
            Answer_B = question.Answer_B;
            Answer_C = question.Answer_C;
            Answer_D = question.Answer_D;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_A)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_B)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_C)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_D)));
            IsCheck_A = false;
            IsCheck_B = false;
            IsCheck_C = false;
            IsCheck_D = false;

        }

        private void setPlayerAnswers()
        {
            bool alreadyExist = false;
            try
            {
                if (player.AnswearsPlayers.ElementAt(indexList).Contains(0) || player.AnswearsPlayers.ElementAt(indexList).Contains(1))
                {
                    alreadyExist = true;
                }
            }
            catch (Exception e)
            {
                alreadyExist = false;
            }
            int[] tab = { 0, 0, 0, 0 };
                if (_isCheck_A == true)
                {
                    tab[0] = 1;
                }
                if (_isCheck_B == true)
                {
                    tab[1] = 1;
                }
                if (_isCheck_C == true)
                {
                    tab[2] = 1;
                }
                if (_isCheck_D == true)
                {
                    tab[3] = 1; ;
                }
            if (alreadyExist)
            {
                player.AnswearsPlayers.RemoveAt(indexList);
                player.AnswearsPlayers.Insert(indexList, tab.ToList());
            }
            else
            {
                player.AnswearsPlayers.Add(tab.ToList());
            }
            var result = player.AnswearsPlayers;
            var xd = 1;
        }

        private void LoadAnswersFromQuestions()
        {
            try { 
                if (player.AnswearsPlayers[indexList][0] == 1)
                {
                    IsCheck_A = true;
                }
                else
                {
                    IsCheck_A = false;
                }

                if (player.AnswearsPlayers[indexList][1] == 1)
                {
                    IsCheck_B = true;
                }
                else
                {
                    IsCheck_B = false;
                }

                if (player.AnswearsPlayers[indexList][2] == 1)
                {
                    IsCheck_C = true;
                }
                else
                {
                    IsCheck_C = false;
                }

                if (player.AnswearsPlayers[indexList][3] == 1)
                {
                    IsCheck_D = true;
                }
                else
                {
                    IsCheck_D = false;
                }
            }
            catch(Exception e)
            {

            }
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
        private bool _isCheck_B;
        public bool IsCheck_B
        {
            get
            {
                return _isCheck_B;
            }
            set
            {
                _isCheck_B = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCheck_B)));
            }
        }
        private bool _isCheck_C;
        public bool IsCheck_C
        {
            get
            {
                return _isCheck_C;
            }
            set
            {
                _isCheck_C = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCheck_C)));
            }
        }
        private bool _isCheck_D;
        public bool IsCheck_D
        {
            get
            {
                return _isCheck_D;
            }
            set
            {
                _isCheck_D = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCheck_D)));
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

