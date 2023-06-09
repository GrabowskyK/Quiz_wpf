﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using wpf___projekt.Model;
using wpf___projekt.ViewModel.BaseClass;

namespace wpf___projekt.ViewModel
{
    class ResultViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int points = 0; //punkty
        public ResultViewModel()
        {
            showAnswer();
        }


        //Function
        private void showAnswer()
        {
            //Question.Questions.Clear();
            LoadAnswer = false;
            DataAccess.ReadData("SELECT nazwaPytania, correct FROM Question");
            for (int i = 0; i < Question.Questions.Count; i++)
            {
                string playerAnswer = "";
                string correctAnswer = "";
                switch (Player.answersPlayer.ElementAt(i))
                {
                    case 0:
                        playerAnswer = Question.Questions.ElementAt(i).Answer_A;
                        break;
                    case 1:
                        playerAnswer = Question.Questions.ElementAt(i).Answer_B;
                        break;
                    case 2:
                        playerAnswer = Question.Questions.ElementAt(i).Answer_C;
                        break;
                    case 3:
                        playerAnswer = Question.Questions.ElementAt(i).Answer_D;
                        break;
                }

                switch ((int)Question.Questions.ElementAt(i).Correct)
                {
                    case 0:
                        correctAnswer = Question.Questions.ElementAt(i).Answer_A;
                        break;
                    case 1:
                        correctAnswer = Question.Questions.ElementAt(i).Answer_B;
                        break;
                    case 2:
                        correctAnswer = Question.Questions.ElementAt(i).Answer_C;
                        break;
                    case 3:
                        correctAnswer = Question.Questions.ElementAt(i).Answer_D;
                        break;
                }


                if (playerAnswer == correctAnswer)
                {
                    points += 1;
                }
                PointsString = $"{points}";
                ShowResult showResult = new ShowResult(i + 1, Question.Questions.ElementAt(i).Name, playerAnswer, correctAnswer);
                results.Add(showResult);
                Results = results;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Results)));
            }
        }



        //Property
        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                onPropertyChanged(nameof(SelectedItem));
            }
        }   

        private string pointsString;
        public string PointsString
        {
            get
            {
                return pointsString;
            }
            set
            {
                pointsString = $"Zdobyłeś/aś {value} z {Question.Questions.Count} punktów";

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PointsString)));
            }
        }
        private string colorTextBox;
        public string ColorTextBox
        {
            get
            {
                return colorTextBox;
            }
            set
            {
                colorTextBox = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ColorTextBox)));
            }
        }

        private ObservableCollection<ShowResult> results = new ObservableCollection<ShowResult>();
        public ObservableCollection<ShowResult> Results
        {
            get { return results; }
            set
            {
                results = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Results)));
            }
        }
        private bool loadAnswer = true;
        public bool LoadAnswer {
            get { return loadAnswer; }
            set
            {
                loadAnswer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoadAnswer)));
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
