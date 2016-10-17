using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TripLocation.Model;
using System.Windows;


namespace TripLocation
{
    class TripDB
    {
        public TripDB() { }

        public HashSet<Places> getPlaces(string db, string sqlPlace)
        {
            HashSet<Places> result = new HashSet<Places>();
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            try
            {
                conn = new MySqlConnection(db);
                cmd = new MySqlCommand(sqlPlace, conn);

                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Places temp = new Places();
                    temp.Id = reader.GetInt32("id");
                    temp.Nom = reader.GetString("nom");
                    temp.Rating = reader.GetDouble("rating");
                    temp.Latitude = reader.GetDouble("latitude");
                    temp.Longitude = reader.GetDouble("longitude");
                    result.Add(temp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("An error occurred {0}", ex.Message));
            }
            finally{
                if (reader != null) reader.Close();
                if (conn != null) conn.Close();
            }
            return result;
        }

        public HashSet<Review> getReviews(string db, string sqlReview, int idplace)
        {
            HashSet<Review> result = new HashSet<Review>();
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            string sql = sqlReview + idplace.ToString();

            try
            {
                conn = new MySqlConnection(db);
                cmd = new MySqlCommand(sql, conn);

                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Review temp = new Review();
                    temp.Titre = reader.GetString("titre");
                    temp.Reviewstr = reader.GetString("review");
                    temp.Note = reader.GetDouble("note");
                    result.Add(temp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("An error occurred {0}", ex.Message));
            }
            finally
            {
                if (reader != null) reader.Close();
                if (conn != null) conn.Close();
            }
            return result;
        }

        public double getAVG(string db, string sqlAvg, int idplace)
        {
            double result = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            string sql = sqlAvg + idplace.ToString();

            try
            {
                conn = new MySqlConnection(db);
                cmd = new MySqlCommand(sql, conn);

                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result = reader.GetDouble(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("An error occurred {0}", ex.Message));
            }
            finally
            {
                if (reader != null) reader.Close();
                if (conn != null) conn.Close();
            }
            return result;

        }
    }
}
