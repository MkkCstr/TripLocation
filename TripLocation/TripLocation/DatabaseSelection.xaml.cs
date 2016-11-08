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

namespace TripLocation
{
    /// <summary>
    /// Logique d'interaction pour DatabaseSelection.xaml
    /// </summary>
    public partial class DatabaseSelection : Window
    {
        private List<string> DbSelect = new List<string>();
        private string connection;

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
            string newConnection = connection + "database=" + selectedItem;
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
    }
}
