﻿using EvernoteWPF.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvernoteWPF.ViewModel.Helpers
{
    public class FirebaseAuthHelper
    {
        private static string api_key = "AIzaSyANYjL4HJ5OjyaIoCtMPRiLRfwDlwFu8xM";
        
        public static async Task<bool> Register(User user)
        {
            using(HttpClient client = new HttpClient())
            {
                var body = new
                {
                    email = user.Username,
                    password = user.Password,
                    returnSecureToken = true
                };

                var bodyJson = JsonConvert.SerializeObject(body);
                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={api_key}", data);

                if(response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    var registerResult = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);
                    App.UserId = registerResult.localId;
                    return true;
                }
                else
                {
                    string errorJson = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<Error>(errorJson);
                    MessageBox.Show(error.error.message);

                    return false;
                }
            }
        }

        public static async Task<bool> Login(User user)
        {
            using (HttpClient client = new HttpClient())
            {
                var body = new
                {
                    email = user.Username,
                    password = user.Password,
                    returnSecureToken = true
                };

                var bodyJson = JsonConvert.SerializeObject(body);
                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={api_key}", data);

                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    var registerResult = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);
                    App.UserId = registerResult.localId;
                    return true;
                }
                else
                {
                    string errorJson = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<Error>(errorJson);
                    MessageBox.Show(error.error.message);

                    return false;
                }
            }
        }


        public class FirebaseResult
        {
            public string kind { get; set; }
            public string idToken { get; set; }
            public string email { get; set; }
            public string refreshToken { get; set; }
            public string expiresIn { get; set; }
            public string localId { get; set; }
        }

        public class ErroDetails
        {
            public int code { get; set; }
            public string message { get; set; }
        }

        public class Error
        {
            public ErroDetails error { get; set; }
        }
    }
}
