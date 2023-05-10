using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using wpf___projekt.Model;
using wpf___projekt.View;
using wpf___projekt.ViewModel.BaseClass;

namespace wpf___projekt.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Question question = new Question();


        private int indexList = 0;
        public MainViewModel()
        {
            NextQuestion = new RelayCommand(NextQuestionFunc);
            PreviousQuestion = new RelayCommand(PreviousQuestionFunc);
            StartQuiz = new RelayCommand(StarQuizFunc);
            DisableLockNextQuestionButton = new RelayCommand(DisableLockNextQuestionButtonFunc);
            open2 = new RelayCommand(open);
            openResult = new RelayCommand(openResultFunc);

        }

        public ICommand openResult { get; set; }
        public ICommand open2 { get; set; }
        public void open(object obj)
        {
            var win = new Menu();
            win.Show();

        }

        public void openResultFunc(object obj)
        {
            setPlayerAnswers();
            var win = new Result();
            win.Show();
        }

        private void StarQuizFunc(object obj)
        {
            LoadData(0);
        }
        private void NextQuestionFunc(object obj)
        {
            setPlayerAnswers();
            
            if (indexList < Question.Questions.Count - 1)
            {
                indexList++;
                try
                {
                    if (Player.answersPlayer.ElementAt(indexList) >= 0)
                    {
                        EnabledNextQuestion = true;
                    }
                }
                catch
                {
                    EnabledNextQuestion = false;
                }
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
            EnabledNextQuestion = true;
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
            QuestionNumber = _questionNumber;
            if (indexList == 0)
            {
                EnabledPreviousQuestion = false;
            }
            else
            {
                EnabledPreviousQuestion = true;
            }

            if (indexList == Question.Questions.Count - 1)
            {
                EnabledNextQuestion = false;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_A)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_B)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_C)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer_D)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnabledPreviousQuestion)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnabledNextQuestion)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionNumber)));
            //TODO: Zamienić ↓↓↓, usunąć z set property
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
                if (Player.answersPlayer.ElementAt(indexList) >= 0 && 3 >= Player.answersPlayer.ElementAt(indexList))
                {
                    alreadyExist = true;
                    EnabledNextQuestion = true;
                }
            }
            catch (Exception ex)
            {
                alreadyExist = false;
                EnabledNextQuestion = false;
            }
            //bool alreadyExist = false;

            var chooseOption = 4;
            if (_isCheck_A == true)
            {
                IsCheck_A = true;
                chooseOption = 0;
            }
            else if (_isCheck_B == true)
            {
                IsCheck_B = true;
                chooseOption = 1;
            }
            else if (_isCheck_C == true)
            {
                IsCheck_C = true;
                chooseOption = 2;
            }
            else if (_isCheck_D == true)
            {
                IsCheck_D = true;
                chooseOption = 3;
            }
            if (alreadyExist)
            {
                Player.answersPlayer.RemoveAt(indexList);
                Player.answersPlayer.Insert(indexList, chooseOption);
            }
            else if(chooseOption != 4)
            {
                Player.answersPlayer.Add(chooseOption);
            }
            //var result = player.answersPlayer;
        }

        private void LoadAnswersFromQuestions()
        {
            try
            {
                if (Player.answersPlayer[indexList] == 0)
                {
                    IsCheck_A = true;
                    IsCheck_B = false;
                    IsCheck_C = false;
                    IsCheck_D = false;
                }
                else if (Player.answersPlayer[indexList] == 1)
                {
                    IsCheck_A = false;
                    IsCheck_B = true;
                    IsCheck_C = false;
                    IsCheck_D = false;
                }
                else if (Player.answersPlayer[indexList] == 2)
                {
                    IsCheck_A = false;
                    IsCheck_B = false;
                    IsCheck_C = true;
                    IsCheck_D = false;
                }
                else if (Player.answersPlayer[indexList] == 3)
                {
                    IsCheck_A = false;
                    IsCheck_B = false;
                    IsCheck_C = false;
                    IsCheck_D = true;
                }
            }
            catch (Exception e)
            {

            }
        }
        private void DisableLockNextQuestionButtonFunc(object obj)
        {
            if (indexList != Question.Questions.Count - 1)
            {
                EnabledNextQuestion = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnabledNextQuestion)));
            }

        }



        public ICommand NextQuestion { get; set; }
        public ICommand PreviousQuestion { get; set; }
        public ICommand Add { get; set; }
        public ICommand StartQuiz { get; set; }
        public ICommand DisableLockNextQuestionButton { get; set; }






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


        private bool _enabledPreviousQuestion;
        public bool EnabledPreviousQuestion
        {
            get { return _enabledPreviousQuestion; }
            set
            {
                _enabledPreviousQuestion = value;
            }

        }

        private bool _enabledNextQuestion;
        public bool EnabledNextQuestion
        {
            get { return _enabledNextQuestion; }
            set
            {
                _enabledNextQuestion = value;
            }

        }

        private string _name;
        private string _answer_A;
        private string _answer_B;
        private string _answer_C;
        private string _answer_D;
        private string _questionNumber;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string QuestionNumber
        {
            get { return _questionNumber; }
            set
            {
                _questionNumber = $"Pytanie {indexList + 1} z {Question.Questions.Count}";
            }

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

