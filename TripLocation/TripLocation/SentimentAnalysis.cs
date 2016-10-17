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

        /// <summary>
        /// Prendre le résultat 
        /// </summary>
        /// <param name="str">
        /// La phrase à analyser
        /// </param>
        /// <param name="APIList">
        /// la liste de clé API
        /// </param>
        /// <returns>
        /// Une note allant de 1 à 5 sachant que 0 est envoyé lorsqu'il y a une erreur
        /// </returns>
        public double getSentiment(string str, List<string> APIList)
        {
  
            double result = 0;
            var client = new RestClient("http://api.meaningcloud.com/sentiment-2.1");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "*/*");
            request.AddParameter("application/x-www-form-urlencoded", "key="+APIList[0]+"&lang=auto&txt="+str+"&model=general", ParameterType.RequestBody);
            var response = client.Execute(request);

            JToken token = JObject.Parse(response.Content);
            string code = (string)token.SelectToken("status.code");
            long credits = Convert.ToInt64((string)token.SelectToken("status.credits"));

            if (code == "100")
            {
                if(APIList.Count > 0)
                {
                    APIList.RemoveAt(0);
                }
                else
                {
                    return result;
                }
                
            }
            else
            {
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

                if(credits <= 1)
                {
                   APIList.RemoveAt(0);
                }
            }
            
            return result;
        }
    }
}
