using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripLocation.Model
{
    class Points
    {
        private double axisX;
        private double axisY;

        public double AxisX
        {
            get
            {
                return axisX;
            }

            set
            {
                axisX = value;
            }
        }

        public double AxisY
        {
            get
            {
                return axisY;
            }

            set
            {
                axisY = value;
            }
        }
    }
}
