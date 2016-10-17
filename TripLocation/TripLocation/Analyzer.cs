using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using TripLocation.Model;

namespace TripLocation
{
    class Analyzer
    {
        public Analyzer()
        {
           
        }

        /// <summary>
        /// Prendre une table qui contient les coordonnées de la place et sa note
        /// </summary>
        /// <param name="db">
        /// Paramètre de connexion
        /// </param>
        /// <param name="sqlPlace">
        /// requête pour avoir la place
        /// </param>
        /// <param name="sqlAvg">
        /// requête pour avoir la moyenne
        /// </param>
        /// <returns></returns>
        public HashSet<TableInfo> getTable(string db, string sqlPlace, string sqlAvg)
        {
            TripDB tripdb = new TripDB();

            HashSet<TableInfo> result = new HashSet<TableInfo>();
            HashSet<Places> placeSet = tripdb.getPlaces(db, sqlPlace);
            Parallel.ForEach(placeSet, place =>
            {

                double avg = tripdb.getAVG(db, sqlAvg, place.Id);
                TableInfo temp = new TableInfo();
                temp.Note = avg;
                temp.Latitude = place.Latitude;
                temp.Longitude = place.Longitude;
                result.Add(temp);
            });
            return result;
        }

        /// <summary>
        /// Prendre une table qui contient les coordonnées de la place et le résultat de l'analyse de sentiment
        /// </summary>
        /// <param name="db">
        /// Paramètre de connexion
        /// </param>
        /// <param name="sqlPlace">
        /// Requête sql pour avoir la liste de place
        /// </param>
        /// <param name="sqlReview">
        /// Requête sql pour avoir la liste d'avis
        /// </param>
        /// <param name="APIList">
        /// la liste de clé API
        /// </param>
        /// <returns></returns>
        public HashSet<TableInfo> getTable(string db, string sqlPlace, string sqlReview, List<string> APIList)
        {
            TripDB tripdb = new TripDB();
            SentimentAnalysis sent = new SentimentAnalysis();

            HashSet<TableInfo> result = new HashSet<TableInfo>();
            HashSet<Places> placeSet = tripdb.getPlaces(db, sqlPlace);
            Parallel.ForEach(placeSet, place =>
            {
                string id = place.Id.ToString();
                HashSet<Review> reviewSet = tripdb.getReviews(db, sqlReview, place.Id);
                double note = 0;
                int counter = 0;
                double notetmp = 0;
                foreach (Review review in reviewSet)
                {
                    if(APIList.Count>0)
                    {
                        notetmp = sent.getSentiment(review.Reviewstr, APIList);
                        if (notetmp != 0)
                        {
                            note += notetmp;
                            counter++;
                        }
                    }
                }
                double moyenne = note / counter;
                TableInfo temp = new TableInfo();
                temp.Note = moyenne;
                temp.Latitude = place.Latitude;
                temp.Longitude = place.Longitude;
                result.Add(temp);
            });
            return result;
        }
    }
}
