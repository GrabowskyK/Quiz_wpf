﻿using System;
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
using wpf___projekt.ViewModel.BaseClass;

namespace wpf___projekt.ViewModel
{
    class MenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MenuViewModel() 
        {
            loadQuiz = new RelayCommand(loadQuizies);
            openQuiz = new RelayCommand(openQuizFunc);
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

        public void loadQuizies(object obj)
        {
            DataAccessQuiz.ReadData("SELECT * FROM Quiz");
            Quizies = Quiz.nazwaQuiz;
            EnabledLoadBaseButton = false;
            EnabledChooseButton = true;
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

        public ICommand openQuiz { get; set; }
        public void openQuizFunc(object obj)
        {
            int result = Quiz.nazwaQuiz.IndexOf(SelectedItem);
            Question.Questions.Clear();
            Player.answersPlayer.Clear();
            DataAccess.ReadData($"SELECT * FROM Question WHERE id_quiz = {result}");
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
