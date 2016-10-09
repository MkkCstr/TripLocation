using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TripLocation
{
    class SentimentAnalysis
    {
        public double getSentiment(string str)
        {
            double result = 0;
            var client = new RestClient("http://api.meaningcloud.com/sentiment-2.1");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "*/*");
            request.AddParameter("application/x-www-form-urlencoded", "key=&lang=auto&txt="+str+"&model=general", ParameterType.RequestBody);
            var response = client.Execute(request);

            JToken token = JObject.Parse(response.Content);
            string content = (string)token.SelectToken("score_tag");
            switch (content)
            {
                case "P+":
                    result = 5;
                    break;
                case "P":
                    result = 4;
                    break;
                case "NEU":
                    result = 3;
                    break;
                case "N":
                    result = 2;
                    break;
                case "N+":
                    result = 1;
                    break;
                case "NONE":
                    result = 0;
                    break;
            }
            return result;
        }
    }
}
