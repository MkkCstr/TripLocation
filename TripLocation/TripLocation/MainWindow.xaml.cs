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
            
           string sqlPlace = "Select * FROM tripamst HAVING nbAvis > 1";
           string sqlReview = "Select * FROM tripreviewsamst HAVING note > 0 AND idplace = ";
           string cs = @"server=localhost;userid=root;password=;database=traitement";
            SentimentAnalysis sent = new SentimentAnalysis();
            string str = "I love this place, it's nice and clean";
            double res = sent.getSentiment(str);



            Analyzer analyzer = new Analyzer(cs,sqlPlace,sqlReview);


           


            
        }
    }
}
