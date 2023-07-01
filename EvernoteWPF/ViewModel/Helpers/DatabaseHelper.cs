using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EvernoteWPF.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        public static bool Insert<T>(T item)
        {
            bool result = false;
            using(SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int rows = connection.Insert(item);
                result = rows > 0;
            }

            return result;
        }

        public static bool Update<T>(T item)
        {
            bool result = false;
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int rows = connection.Update(item);
                result = rows > 0;
            }

            return result;
        }

        public static bool Delete<T>(T item)
        {
            bool result = false;
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                int rows = connection.Delete(item);
                result = rows > 0;
            }

            return result;
        }

        public static List<T> ListItems<T>() where T : new()
        {
            List<T> items;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                items = connection.Table<T>().ToList();
            }

            return items;
        }
    }
}
