using MockarooApiClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MockarooApiClient.Data
{
    public class DataTypeData
    {
        public async Task<List<DataTypeDTO>> Get(string apiKey)
        {
            string url = $"https://api.mockaroo.com/api/types?key={apiKey}";
            List<DataTypeDTO> dataTypes = new List<DataTypeDTO>();

            using(HttpResponseMessage response = await MockarooClient.Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var result = await response.Content.ReadAsAsync<DataTypesDTO>();
                    dataTypes = result.Types;

                    dataTypes.FirstOrDefault(dt => dt.Name == "Datetime")
                        .Parameters.FirstOrDefault(p => p.Name == "format")
                        .Values = new List<string>()
                        {
                            "m/d/yyyy",
                            "mm/dd/yyyy",
                            "yyyy-mm-dd",
                            "yyyy-mm",
                            "d/m/yyyy",
                            "dd/mm/yyyy",
                            "d.m.yyyy",
                            "dd.mm.yyyy",
                            "dd-mm-yyyy",
                            "dd-Mon-yyyy",
                            "yyyy/mm/dd",
                            "SQL datetime",
                            "ISO 8601 (UTC)",
                            "epoch",
                            "mongoDB epoch",
                            "mongoDB ISO",
                            "unix timestamp",
                        };


                }
            }

            return dataTypes;
        }
    }

    public class DataTypesDTO
    {
        public List<DataTypeDTO> Types { get; set; }
    }
}
