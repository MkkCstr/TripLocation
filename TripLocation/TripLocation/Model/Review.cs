using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripLocation.Model
{
    class Review
    {
        private string titre;
        private string reviewstr;
        private double note;
        private string langue;

        public string Langue
        {
            get
            {
                return langue;
            }

            set
            {
                langue = value;
            }
        }

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

        public string Titre
        {
            get
            {
                return titre;
            }

            set
            {
                titre = value;
            }
        }

        public string Reviewstr
        {
            get
            {
                return reviewstr;
            }

            set
            {
                reviewstr = value;
            }
        }

        public Review()
        {

        }

    }
}
