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
using System.Windows.Shapes;
using TripLocation.Model;

namespace TripLocation
{
    /// <summary>
    /// Logique d'interaction pour DatabaseSelection.xaml
    /// </summary>
    public partial class DatabaseSelection : Window
    {
        private List<string> DbSelect = new List<string>();
        private string connection;
        private string newConnection;
        private string tbl_name_place;
        private string tbl_name_author;
        private string tbl_name_review;

        public DatabaseSelection(List<string> dblist, string con)
        {
            DbSelect = dblist;
            connection = con;
            InitializeComponent();
            
            for(int i = 0; i<dblist.Count; i++)
            {
                cb_dbselect.Items.Add(dblist[i]);
            }
        }

        private void cb_dbselect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            string selectedItem = cmb.SelectedItem.ToString();
            TripDB tripdb = new TripDB();
            newConnection = connection + "database=" + selectedItem;
            List<string> tableList = tripdb.showTables(newConnection);
            setComboBoxdata(cb_place, tableList);
            setComboBoxdata(cb_author, tableList);
            setComboBoxdata(cb_review, tableList);
        }

       private void setComboBoxdata(ComboBox cb, List<string> data)
        {
            cb.Items.Clear();
            cb.IsEnabled = true;
            cb.IsEditable = true;
            for (int i = 0; i < data.Count; i++)
            {
                cb.Items.Add(data[i]);
                
            }
        }

        private void cb_place_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            string selectedItem = cmb.SelectedItem.ToString();
            tbl_name_place = selectedItem;
        }

        private void cb_author_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            string selectedItem = cmb.SelectedItem.ToString();
            tbl_name_author = selectedItem;
        }

        private void cb_review_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            string selectedItem = cmb.SelectedItem.ToString();
            tbl_name_review = selectedItem;
        }

        private void button_doIt_Click(object sender, RoutedEventArgs e)
        {
            string sqlrequete = "Select avg(rev.note) as AVG, loc.longitude, loc.latitude"
            + " From "+ tbl_name_review + " as rev JOIN "+ tbl_name_author +" as aut On rev.idauteur = aut.memberID JOIN "+tbl_name_place+" as loc ON rev.idplace = loc.id"
            + " Where aut.location != \"\""
            + " Group by rev.idplace";

            TripDB trip = new TripDB();
            HashSet<TableInfo> myTable = trip.getTable(newConnection, sqlrequete);

            RnetInstance rnet = new RnetInstance();
            rnet.runScript(myTable);
        }
    }
}
