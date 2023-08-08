using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ChurchApp.DTO;
using ChurchApp.Models;
using Newtonsoft.Json;

namespace ChurchApp.Services
{
    internal class AuthService
    {
        private readonly string _authUrl = "https://localhost:7170/api/Auth"; // Replace with your API URL
        private readonly string _baseUrl = "https://localhost:7170/api/"; // Replace with your API URL

        public async Task<User> Register(UserAuth userAuth)
        {
            var httpClient = new HttpClient();
            var jsonContent = JsonConvert.SerializeObject(userAuth);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_authUrl}/register", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(json);
            }

            return null; // Handle errors as needed
        }

        public async Task<string> Login(UserAuth userAuth)
        {
            var httpClient = new HttpClient();
            var jsonContent = JsonConvert.SerializeObject(userAuth);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{_authUrl}/login", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                return tokenResponse["token"];
            }

            return null; // Handle errors as needed
        }

        public async Task<UserDetailsDTO> GetUserDetails(string token, string userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Adjust this URL to match the actual endpoint for fetching user details
            var response = await httpClient.GetAsync($"{_baseUrl}Users");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserDetailsDTO>(json);
            }

            return null; // Handle errors as needed
        }


    }

}
