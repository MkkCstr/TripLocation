using System;
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
            string sqlPlace = "Select * FROM tripamst HAVING nbAvis >= 1";
            string sqlReview = "Select * FROM tripreviewsamst HAVING note > 0 AND idplace = ";
            string cs = @"server=localhost;userid=root;password=;database=traitement";
            Analyzer analyzer = new Analyzer();
            HashSet<TableInfo> myTable = analyzer.getTable(cs, sqlPlace, sqlReview);

            RnetInstance rnet = new RnetInstance(myTable);
            rnet.runScript();
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
            textBlock.Text = avgTest.ToString();
        }
    }
}
