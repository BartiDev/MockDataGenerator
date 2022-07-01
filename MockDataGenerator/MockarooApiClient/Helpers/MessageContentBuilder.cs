using MockarooApiClient.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockarooApiClient.Helpers
{
    public class MessageContentBuilder
    {
        public string BuildMessage(List<DataFieldDTO> dataFields)
        {
            StringBuilder result = new StringBuilder();

            result.Append("[");

            foreach (var dataField in dataFields)
            {
                result.Append("{");

                result.AppendFormat(@"""name"": ""{0}"",", dataField.Name);
                result.AppendFormat(@"""type"": ""{0}"",", dataField.DataType.Name);

                foreach(var parameter in dataField.DataType.Parameters)
                {
                    if(dataField.DataType.Name == "Datetime" && parameter.Name == "format")
                        result.AppendFormat(@"""{0}"": ""{1}"",", parameter.Name, EvaluateDatetimeFormat(parameter.Value));
                    else if(parameter.Type == "integer")
                        result.AppendFormat(@"""{0}"": {1},", parameter.Name, parameter.Value);
                    else
                        result.AppendFormat(@"""{0}"": ""{1}"",", parameter.Name, parameter.Value);
                }

                result.Remove(result.Length - 1, 1);
                result.Append("},");
            }


            result.Remove(result.Length - 1, 1);
            result.Append("]");

            return result.ToString();
        }

        private string EvaluateDatetimeFormat(string format)
        {
            switch (format)
            {
                case "m/d/yyyy":
                    return "%-d/%-m/%Y";
                case "mm/dd/yyyy":
                    return "%d/%m/%Y";
                case "yyyy-mm-dd":
                    return "%Y-%m-%d";
                case "yyyy-mm":
                    return "%Y-%m";
                case "d/m/yyyy":
                    return "%-d/%-m/%Y";
                case "dd/mm/yyyy":
                    return "%d/%m/%Y";
                case "d.m.yyyy":
                    return "%-d.%-m.%Y";
                case "dd.mm.yyyy":
                    return "%d.%m.%Y";
                case "dd-mm-yyyy":
                    return "%d-%m-%Y";
                case "dd-Mon-yyyy":
                    return "%d-%b-%Y";
                case "yyyy/mm/dd":
                    return "%Y/%m/%d";
                case "SQL datetime":
                    return "%F %T";
                case "ISO 8601 (UTC)":
                    return "%FT%T%:z";
                case "epoch":
                    return "%s";
                case "mongoDB epoch":
                    return "{\"$date\":{\"$numberLong\":%s}}";
                case "mongoDB ISO":
                    return "{\"$date\":{\"$numberLong\":%FT%T%:z}}";
                case "unix timestamp":
                    return "%Q";
                default:
                    return "%d/%m/%Y";
            }
        }
    }
}
