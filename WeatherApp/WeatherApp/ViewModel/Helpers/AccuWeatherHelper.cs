﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com/";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{1}?apikey={0}";
        public const string API_KEY = "rGpUJ3X8FvW4lnF84y8Hu1ELGrSPxKQ4";

        public static async Task<List<City>> GetCities(string query) =>
            await GetAsync<List<City>>(query);

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey) =>
            (await GetAsync<List<CurrentConditions>>(cityKey)).FirstOrDefault();


        public static async Task<T> GetAsync<T>(string parameter)
        {
            string url = $"{BASE_URL}{string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, parameter)}";

            using (var cliente = new HttpClient())
            {
                var response = await cliente.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                var responseObject = JsonConvert.DeserializeObject<T>(json);

                return responseObject;
            }
        }
    }
}