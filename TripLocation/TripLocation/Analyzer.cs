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
        public Analyzer(string cs, string sqlPlace, string sqlReview, List<string> APIList)
        {
            HashSet<TableInfo> myTable = getTable(cs, sqlPlace, sqlReview,APIList);
            foreach(TableInfo info in myTable)
            {

            }
        }

        public HashSet<TableInfo> getTable(string db, string sqlPlace, string sqlReview)
        {
            TripDB tripdb = new TripDB();

            HashSet<TableInfo> result = new HashSet<TableInfo>();
            HashSet<Places> placeSet = tripdb.getPlaces(db, sqlPlace);
            Parallel.ForEach(placeSet, place =>
            {
                string id = place.Id.ToString();
                HashSet<Review> reviewSet = tripdb.getReviews(db, sqlReview, place.Id);
                double note = 0;
                foreach (Review review in reviewSet)
                {
                    note += (double)review.Note;
                }
                double moyenne = note / (double)reviewSet.Count;
                TableInfo temp = new TableInfo();
                temp.Note = moyenne;
                temp.Latitude = place.Latitude;
                temp.Longitude = place.Longitude;
                result.Add(temp);
            });
            return result;
        }

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
                foreach (Review review in reviewSet)
                {
                    if (APIList.Count > 0)
                    {
                        note += sent.getSentiment(review.Reviewstr, APIList);
                        counter++;
                    }
                    else
                    {
                        break;
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
