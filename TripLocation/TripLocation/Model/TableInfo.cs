using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripLocation.Model
{
    class TableInfo
    {

        private double note;
        private double latitude;
        private double longitude;

        public double Note
        {
            get
            {
                return note;
            }

            set
            {
                note = value;
            }
        }

        public double Latitude
        {
            get
            {
                return latitude;
            }

            set
            {
                latitude = value;
            }
        }

        public double Longitude
        {
            get
            {
                return longitude;
            }

            set
            {
                longitude = value;
            }
        }

        public TableInfo() { }
    }
}
