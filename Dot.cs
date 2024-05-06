using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RectangleApp
{
    internal class Dot
    {
        public Coordinate Coordinate { get; set; }
        public LaserVector vector { get; set; }

        public LaserVector GetVector(Coordinate coordinate)
        {
            Coordinate = coordinate;

            return this.vector;
        }
    
    }
}
