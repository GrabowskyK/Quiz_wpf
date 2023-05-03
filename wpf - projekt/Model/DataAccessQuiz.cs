using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace wpf___projekt.Model
{
    public class DataAccessQuiz
    {
        //static SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\grabe\Desktop\Question.db;Version=3");
        static SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\grabe\Desktop\quiz.db;Version=3");
        private static void ReadData(SQLiteConnection conn, string zapytanie)
        {
            SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Quiz";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                long id = (long)reader["id"];
                string name = (string)reader["nazwa"];
                Quiz quiz = new Quiz(name);
                Quiz.nazwaQuiz.Add(name);
                var result = 0;
                //Z intem wyskakiwały błędy
            }

        }
        public static void ReadData(string zapytanie)
        {
            try
            {
                conn.Open();
                ReadData(conn, zapytanie);
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
