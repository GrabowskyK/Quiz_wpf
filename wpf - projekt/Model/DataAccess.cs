using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace wpf___projekt.Model
{
    public class DataAccess
    {
        static SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\grabe\Desktop\Question.db;Version=3");
        private static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM questions";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                long id = (long)reader["id"];
                string name = (string)reader["name"];
                string answer_A = (string)reader["answer_A"];
                string answer_B = (string)reader["answer_B"];
                string answer_C = (string)reader["answer_C"];
                string answer_D = (string)reader["answer_D"];
                bool isCorrect_A = (bool)reader["is_correct_A"];
                bool isCorrect_B = (bool)reader["is_correct_B"];
                bool isCorrect_C = (bool)reader["is_correct_C"];
                bool isCorrect_D = (bool)reader["is_correct_D"];
                //kolejne atyrbuty
                Question question = new Question(name, answer_A, answer_B, answer_C, answer_D, isCorrect_A, isCorrect_B, isCorrect_C, isCorrect_D);
                Question.Answers.Add(question);
                //var item = $"{id} {name} {answer_A} {answer_B} {answer_C} {answer_D} {isCorrect_A} {isCorrect_B} {isCorrect_C} {isCorrect_D}";
            }

    }
        public static void ReadData()
        {
            try
            {
                conn.Open();
                ReadData(conn); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally //Jak pójdzie exception podczas conn to się potem zamknie
            {
                conn.Close();
               
            }
        }
    }
}
