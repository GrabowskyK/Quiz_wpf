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
        //static SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\grabe\Desktop\Question.db;Version=3");
        static SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\grabe\Desktop\quiz.db;Version=3");
        private static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Question";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                long id = (long)reader["id"];
                string name = (string)reader["nazwaPytania"];
                string answer_A = (string)reader["odp1"];
                string answer_B = (string)reader["odp2"];
                string answer_C = (string)reader["odp3"];
                string answer_D = (string)reader["odp4"];
                long correct = (long)reader["popr_odp"];
                long id_quiz = (long)reader["id_quiz"];
                Question question = new Question(name, answer_A, answer_B, answer_C, answer_D, correct, id_quiz);
                Question.Questions.Add(question);
                //Z intem wyskakiwały błędy
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
