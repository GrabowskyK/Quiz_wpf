using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using wpf___projekt.Model;
using wpf___projekt.View;
using wpf___projekt.ViewModel.BaseClass;

namespace wpf___projekt.ViewModel
{
    class MenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MenuViewModel() 
        {
            loadQuizies();
            openQuiz = new RelayCommand(openQuizFunc);
            Delete = new RelayCommand(DeleteFunc);
            CreateQuiz = new RelayCommand(createQuizFunc);
            EditQuiz = new RelayCommand(EditQuizFunc);
        }

        private ObservableCollection<string> quizies = new ObservableCollection<string>();
        public ObservableCollection<string> Quizies
            {
                get { return quizies; }
                set 
                {
                quizies = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quizies)));
                }
            }
        public ICommand loadQuiz { get; set; }
        public ICommand CreateQuiz { get; set; }
        public void createQuizFunc(object obj)
        {
            var qc = new QuizCreate();
            qc.Show();
        }
        public void loadQuizies()
        {
            Quiz.nazwaQuiz.Clear();
            DataAccessQuiz.ReadData("SELECT * FROM Quiz");
            Quizies = Quiz.nazwaQuiz;
            EnabledLoadBaseButton = false;
            EnabledChooseButton = true;
        }

        public ICommand Delete { get; set; }
        public void DeleteFunc(object obj)
        {
            DataAccessQuiz.DeleteData($"DELETE FROM Quiz WHERE nazwa = \"{SelectedItem}\";");
            long quiz_id = Quiz.AllQuiz.FirstOrDefault(quiz => quiz.Nazwa == SelectedItem).Id;
            DataAccess.ReadData($"DELETE FROM Question WHERE id_quiz = {quiz_id}");
            loadQuizies();
        }

        private bool enabledLoadBaseButton = true;
        public bool EnabledLoadBaseButton
        {
            get { return enabledLoadBaseButton; }
            set
            {
                enabledLoadBaseButton = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnabledLoadBaseButton)));
            }

        }
        private bool enabledChooseButton = false;
        public bool EnabledChooseButton
        {
            get { return enabledChooseButton; }
            set
            {
                enabledChooseButton = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EnabledChooseButton)));
            }
        }

        private string _selectedItem;

        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                onPropertyChanged(nameof(SelectedItem));
                // Tutaj można wykonać dodatkową logikę w zależności od wartości SelectedItem
            }
        }

        public ICommand EditQuiz { get; set; }
        public void EditQuizFunc(object obj)
        {
            long quiz_id = Quiz.AllQuiz.FirstOrDefault(quiz => quiz.Nazwa == SelectedItem).Id;
            Question.Questions.Clear();
            DataAccess.ReadData($"SELECT * FROM Question WHERE id_quiz = {quiz_id};");
            var edit = new EditQuiz();
            edit.Show();
        }

        public ICommand openQuiz { get; set; }
        public void openQuizFunc(object obj)
        {
            long quiz_id = Quiz.AllQuiz.FirstOrDefault(quiz => quiz.Nazwa == SelectedItem).Id;
            Question.Questions.Clear();
            Player.answersPlayer.Clear();
            DataAccess.ReadData($"SELECT * FROM Question WHERE id_quiz = {quiz_id};");
            var win = new MainWindow();
            win.Show();
        }
        public void onPropertyChanged(params string[] properties)
        {
            if (PropertyChanged != null)
                foreach (var property in properties)
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}
