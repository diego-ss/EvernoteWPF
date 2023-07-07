using EvernoteWPF.Model;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EvernoteWPF.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");
        private static string dbPath = "https://evernote-clone-wpf-b9974-default-rtdb.firebaseio.com/";

        public static async Task<bool> Insert<T>(T item)
        {
            //bool result = false;
            //using(SQLiteConnection connection = new SQLiteConnection(dbFile))
            //{
            //    connection.CreateTable<T>();
            //    int rows = connection.Insert(item);
            //    result = rows > 0;
            //}

            //return result;

            var jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using(var client = new HttpClient())
            {
                var result = await client.PostAsync($"{dbPath}{typeof(T).Name.ToLower()}.json", content);
                return result.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> Update<T>(T item) where T : HasId
        {
            //bool result = false;
            //using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            //{
            //    connection.CreateTable<T>();
            //    int rows = connection.Update(item);
            //    result = rows > 0;
            //}

            //return result;

            var jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var result = await client.PatchAsync($"{dbPath}{typeof(T).Name.ToLower()}/{item.Id}.json", content);
                return result.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> Delete<T>(T item) where T : HasId
        {
            //bool result = false;
            //using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            //{
            //    connection.CreateTable<T>();
            //    int rows = connection.Delete(item);
            //    result = rows > 0;
            //}

            //return result;

            var jsonBody = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync($"{dbPath}{typeof(T).Name.ToLower()}/{item.Id}.json");
                return result.IsSuccessStatusCode;
            }
        }

        public async static Task<List<T>> ListItems<T>() where T : HasId
        {
            //List<T> items;

            //using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            //{
            //    connection.CreateTable<T>();
            //    items = connection.Table<T>().ToList();
            //}

            //return items;

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"{dbPath}{typeof(T).Name.ToLower()}.json");

                if (result.IsSuccessStatusCode)
                {
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, T>>(await result.Content.ReadAsStringAsync());
                    List<T> list = new List<T>();

                    if (dict == null)
                        return null;

                    foreach(var item in dict)
                    {
                        item.Value.Id = item.Key;
                        list.Add(item.Value);
                    }

                    return list;
                }
                else
                    return null;
            }
        }
    }
}
