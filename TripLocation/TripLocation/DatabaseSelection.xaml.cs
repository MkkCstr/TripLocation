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

        public void setDb(List<string> dblist)
        {
            DbSelect = dblist;
        }

        public void setConnection(string con)
        {
            connection = con;
        }

        public DatabaseSelection(List<string> dblist, string con)
        {
            DbSelect = dblist;
            connection = con;
            InitializeComponent();            
        }
    }
}
