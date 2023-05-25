using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.IO;

namespace wpf___projekt.Model
{
    public class DataAccessQuiz
    {

        static SQLiteConnection conn = new SQLiteConnection(@"Data Source=C:\Users\grabe\Desktop\quiz.db;Version=3");
        private static void ReadData(SQLiteConnection conn, string zapytanie)
        {
            SQLiteDataReader reader;
            SQLiteCommand command;

            command = conn.CreateCommand();
            command.CommandText = zapytanie;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                long id = (long)reader["id"];
                string nazwa = (string)reader["nazwa"];
                Quiz quiz = new Quiz(id, nazwa);
                Quiz.nazwaQuiz.Add(nazwa);
                Quiz.AllQuiz.Add(quiz);
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
        
        public static void SaveData(string zapytanie)
        {
            try
            {
                conn.Open();
                SQLiteCommand command = conn.CreateCommand();
                command.CommandText = zapytanie;
                command.ExecuteNonQuery();
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

        public static void DeleteData(string zapytanie)
        {
            try
            {
                conn.Open();
                SQLiteCommand command = conn.CreateCommand();
                command.CommandText = zapytanie;
                command.ExecuteNonQuery();
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
