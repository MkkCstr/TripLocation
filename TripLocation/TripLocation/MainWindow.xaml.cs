﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TripLocation.Model;
using IronPython.Hosting; // make us use Python


namespace TripLocation
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            /*
            string sqlrequete = "Select avg(rev.note) as AVG, loc.longitude, loc.latitude"
            + " From tripreviewsamst as rev JOIN tripauteuramst as aut On rev.idauteur = aut.memberID JOIN tripamst as loc ON rev.idplace = loc.id"
            + " Where aut.location != \"\""
            + " Group by rev.idplace";

            string cs = @"server=localhost;userid=root;password=;port=3306;database=traitement2";
            TripDB trip = new TripDB();
            HashSet<TableInfo> myTable = trip.getTable(cs, sqlrequete);
                
            RnetInstance rnet = new RnetInstance();
            rnet.runScript(myTable);
            */
        }

        private void hashtableTest()
        {
            string sqlPlace = "Select * FROM tripamst HAVING nbAvis >= 1";
            string sqlReview = "Select * FROM tripreviewsamst HAVING note > 0 AND idplace = ";
            string cs = @"server=localhost;userid=root;password=;database=traitement";

            List<string> APIList = new List<string>();


            Analyzer analyzer = new Analyzer();

            HashSet<TableInfo> myTable = analyzer.getTable(cs, sqlPlace, sqlReview);
        }

        private void avgTest()
        {
            string cs = @"server=localhost;userid=root;password=;database=traitement";
            string sqlAvg = "SELECT AVG(note) FROM tripreviewsamst WHERE idplace =";
            int idplace = 239469;
            TripDB trip = new TripDB();
            double avgTest = trip.getAVG(cs, sqlAvg, idplace);
            //textBlock.Text = avgTest.ToString();
        }

        private void button_connect_Click(object sender, RoutedEventArgs e)
        {
            string server = tb_IP.Text;
            string user = tb_user.Text;
            string mdp = tb_mdp.Text;
            string port = tb_port.Text;
            string db = @"server=" + server + ";" + "userid=" + user + ";" + "password=" + mdp + ";";
            TripDB trip = new TripDB();
            try
            {
                List<string> dblist = trip.showDatabase(db);
                if(dblist.Count > 0)
                {
                    DatabaseSelection ds = new DatabaseSelection(dblist, db);
                    ds.Show();
                    this.Close();
                }
                else
                {

                }
                


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

            
        }
    }
}
