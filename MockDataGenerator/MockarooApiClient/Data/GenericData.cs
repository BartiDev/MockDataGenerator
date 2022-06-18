﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MockarooApiClient.Data
{
    public class GenericData
    {
        public async Task<string> TryConnect(string apiKey)
        {
            string url = $"https://api.mockaroo.com/api/generate.json?key={apiKey}";
            string result = "";
            StringContent content = new StringContent(@"[
                                                            {
                                                                ""name"": ""id"",
                                                                ""type"": ""Number"",
                                                                ""min"": 1,
                                                                ""max"": 15,
                                                                ""decimals"": 0
                                                            },{
                                                                ""name"": ""name"",
                                                                ""type"": ""Full Name""
                                                            },{
                                                                ""name"": ""date"",
                                                                ""type"": ""Time""
                                                            }
                                                        ]");

            using(HttpResponseMessage response = await MockarooClient.Client.PostAsync(url,content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }

            return result;
        }
    }
}
