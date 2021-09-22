using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;

using CountryParse.Models.Adapter;
using Microsoft.Extensions.Configuration;

namespace CountryParseNetCore.Services
{
    internal class ApiService
    {
        private readonly IConfiguration configuration;
        private const string sourceUrl = @"http://api.countrylayer.com/v2";
        private readonly JsonSerializerOptions options = new JsonSerializerOptions();

        public DataNode GetCityByName(string name)
        {
            var token = configuration.GetSection("ApiService")["Token"];
            StreamReader streamReader = new StreamReader(
                ((HttpWebResponse)WebRequest
                .Create($"{sourceUrl}/name/{name}?access_key={token}") //создаем запрос
                .GetResponse()) //выполняем его
                .GetResponseStream() //получаем поток ввода благодаря касту в HttpWebResponse
                );
            
            List<DataNode> result = JsonSerializer.Deserialize<List<DataNode>>(streamReader.ReadToEnd(), options);
            return result[0];
        }

        public ApiService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}
