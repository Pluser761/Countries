using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

using CountryParse.Models.Adapter;

namespace CountryParseNetCore.Services
{
    internal class ApiService
    {
        private const string sourceUrl = @"https://restcountries.eu/rest/v2";
        private readonly JsonSerializerOptions options = new JsonSerializerOptions();

        public DataNode GetCityByName(string name)
        {
            StreamReader streamReader = new StreamReader(
                ((HttpWebResponse)WebRequest
                .Create($"{sourceUrl}/name/{name}") //создаем запрос
                .GetResponse()) //выполняем его
                .GetResponseStream() //получаем поток ввода благодаря касту в HttpWebResponse
                );
            
            List<DataNode> result = JsonSerializer.Deserialize<List<DataNode>>(streamReader.ReadToEnd(), options);
            return result[0];
        }
    }
}
