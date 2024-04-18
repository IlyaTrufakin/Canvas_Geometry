using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace RectangleApp
{
    public class RectangleElement
    {
        private double[] elementsLength = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private double[] elementsLengthSummary = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private double[] elementsXcoord = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private double[] elementsYcoord = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public ArcSegmentElement arcElement1;
        public ArcSegmentElement arcElement2;
        public ArcSegmentElement arcElement3;
        public ArcSegmentElement arcElement4;
        public RayElement rayElement1;
        public RayElement rayElement2;
        public RayElement rayElement3;
        public RayElement rayElement4;
        public RayElement rayElement5;
        public double X { get; set; } // Координата X
        public double Y { get; set; } // Координата Y
        public double Rectangle_Width { get; set; }
        public double RectangleHeight { get; set; }
        private double Radius1 { get; set; }
        private double Radius2 { get; set; }
        private double Radius3 { get; set; }
        private double Radius4 { get; set; }
        private double SectionLength { get; set; }
        private LaserVector laserVector;

        public RectangleElement(double x, double y, double width, double height, double radius1, double radius2, double radius3, double radius4)
        {
            X = x;
            Y = y;
            Radius1 = radius1;
            Radius2 = radius2;
            Radius3 = radius3;
            Radius4 = radius4;
            Rectangle_Width = width;
            RectangleHeight = height;

            /*           elementsXcoord[0] = 0;
                       elementsYcoord[0] = height / 2;
                       elementsXcoord[1] = (width / 2) - radius2;
                       elementsYcoord[1] = (height / 2) - radius2;
                       elementsXcoord[2] = (width / 2);
                       elementsYcoord[2] = (height / 2) - radius2;
                       elementsXcoord[3] = (width / 2) - radius3;
                       elementsYcoord[3] = (height / 2) - radius3;
                       elementsXcoord[4] = (width / 2) - radius3;
                       elementsYcoord[4] = (height / 2);
                       elementsXcoord[5] = (width / 2) - radius4;
                       elementsYcoord[5] = (height / 2) - radius4;
                       elementsXcoord[6] = (width / 2);
                       elementsYcoord[6] = (height / 2) - radius4;
                       elementsXcoord[7] = (width / 2) - radius1;
                       elementsYcoord[7] = (height / 2) - radius1;
                       elementsXcoord[8] = (width / 2) - radius1;
                       elementsYcoord[8] = (height / 2);*/

            elementsYcoord[0] = 0;
            elementsXcoord[0] = height / 2;
            elementsYcoord[1] = (width / 2) - radius2;
            elementsXcoord[1] = (height / 2) - radius2;
            elementsYcoord[2] = (width / 2);
            elementsXcoord[2] = (height / 2) - radius2;
            elementsYcoord[3] = (width / 2) - radius3;
            elementsXcoord[3] = (height / 2) - radius3;
            elementsYcoord[4] = (width / 2) - radius3;
            elementsXcoord[4] = (height / 2);
            elementsYcoord[5] = (width / 2) - radius4;
            elementsXcoord[5] = (height / 2) - radius4;
            elementsYcoord[6] = (width / 2);
            elementsXcoord[6] = (height / 2) - radius4;
            elementsYcoord[7] = (width / 2) - radius1;
            elementsXcoord[7] = (height / 2) - radius1;
            elementsYcoord[8] = (width / 2) - radius1;
            elementsXcoord[8] = (height / 2);


            /*            rayElement1 = new RayElement(elementsXcoord[0], -elementsYcoord[0], 0, (width / 2) - radius2, Colors.Brown, 2);
                        arcElement2 = new ArcSegmentElement(elementsXcoord[1], -elementsYcoord[1], radius2, 270, 0, Colors.DarkCyan, 2);
                        rayElement2 = new RayElement(elementsXcoord[2], -elementsYcoord[2], 90, (height) - radius2 - radius3, Colors.Khaki, 2);
                        arcElement3 = new ArcSegmentElement(elementsXcoord[3], elementsYcoord[3], radius3, 0, 90, Colors.Tomato, 2);
                        rayElement3 = new RayElement(elementsXcoord[4], elementsYcoord[4], 180, (width) - radius4 - radius3, Colors.Violet, 2);
                        arcElement4 = new ArcSegmentElement(-elementsXcoord[5], elementsYcoord[5], radius4, 90, 180, Colors.RoyalBlue, 2);
                        rayElement4 = new RayElement(-elementsXcoord[6], elementsYcoord[6], 270, (height) - radius4 - radius1, Colors.SeaGreen, 2);
                        arcElement1 = new ArcSegmentElement(-elementsXcoord[7], -elementsYcoord[7], radius1, 180, 270, Colors.Fuchsia, 2);
                        rayElement5 = new RayElement(-elementsXcoord[8], -elementsYcoord[8], 0, (width / 2) - radius1, Colors.Indigo, 2);*/

            rayElement1 = new RayElement(elementsXcoord[0], elementsYcoord[0], 90, (width / 2) - radius2, Colors.Brown, 2);
            arcElement2 = new ArcSegmentElement(elementsXcoord[1], elementsYcoord[1], radius2, 0, 90, Colors.DarkCyan, 2);
            rayElement2 = new RayElement(elementsXcoord[2], elementsYcoord[2], 180, (height) - radius2 - radius3, Colors.Khaki, 2);
            arcElement3 = new ArcSegmentElement(-elementsXcoord[3], elementsYcoord[3], radius3, 90, 180, Colors.Tomato, 2);
            rayElement3 = new RayElement(-elementsXcoord[4], elementsYcoord[4], 270, (width) - radius4 - radius3, Colors.Violet, 2);
            arcElement4 = new ArcSegmentElement(-elementsXcoord[5], -elementsYcoord[5], radius4, 180, 270, Colors.RoyalBlue, 2);
            rayElement4 = new RayElement(-elementsXcoord[6], -elementsYcoord[6], 0, (height) - radius4 - radius1, Colors.SeaGreen, 2);
            arcElement1 = new ArcSegmentElement(elementsXcoord[7], -elementsYcoord[7], radius1, 270, 0, Colors.Fuchsia, 2);
            rayElement5 = new RayElement(elementsXcoord[8], -elementsYcoord[8], 90, (width / 2) - radius1, Colors.Indigo, 2);


            SectionLength = GetSectionLength();
        }

        public void Draw(Canvas canvas)
        {

            rayElement1.Draw(canvas, X, Y, 0, elementsLengthSummary[0]);
            arcElement2.Draw(canvas, X, Y, 1, elementsLengthSummary[1]);
            rayElement2.Draw(canvas, X, Y, 2, elementsLengthSummary[2]);
            arcElement3.Draw(canvas, X, Y, 3, elementsLengthSummary[3]);
            rayElement3.Draw(canvas, X, Y, 4, elementsLengthSummary[4]);
            arcElement4.Draw(canvas, X, Y, 5, elementsLengthSummary[5]);
            rayElement4.Draw(canvas, X, Y, 6, elementsLengthSummary[6]);
            arcElement1.Draw(canvas, X, Y, 7, elementsLengthSummary[7]);
            rayElement5.Draw(canvas, X, Y, 8, elementsLengthSummary[8]);
        }

        public LaserVector DrawNormal(Canvas canvas, double normalLength, double pointPositionFromStart)
        {
            LaserVector laserVectorTemp;
            if (pointPositionFromStart <= elementsLengthSummary[0] && pointPositionFromStart >= 0)
            {
                rayElement1.DrawNormal(canvas, X, Y, normalLength, pointPositionFromStart - 0);
                laserVectorTemp = rayElement1.CalculatePointPosition(pointPositionFromStart, normalLength);
                return new LaserVector(laserVectorTemp.X, laserVectorTemp.Y, laserVectorTemp.AngleRadians, laserVectorTemp.Length);
            }
            else if (pointPositionFromStart > elementsLengthSummary[0] && pointPositionFromStart <= elementsLengthSummary[1])
            {
                arcElement2.DrawNormal(canvas, X, Y, normalLength, pointPositionFromStart - elementsLengthSummary[0]);
                laserVectorTemp = arcElement2.CalculatePointPosition(pointPositionFromStart - elementsLengthSummary[0], normalLength);
                return new LaserVector(laserVectorTemp.X, laserVectorTemp.Y, laserVectorTemp.AngleRadians, laserVectorTemp.Length);

            }
            else if (pointPositionFromStart > elementsLengthSummary[1] && pointPositionFromStart <= elementsLengthSummary[2])
            {
                rayElement2.DrawNormal(canvas, X, Y, normalLength, pointPositionFromStart - elementsLengthSummary[1]);
                laserVectorTemp = rayElement2.CalculatePointPosition(pointPositionFromStart - elementsLengthSummary[1], normalLength);
                return new LaserVector(laserVectorTemp.X, laserVectorTemp.Y, laserVectorTemp.AngleRadians, laserVectorTemp.Length);
            }
            else if (pointPositionFromStart > elementsLengthSummary[2] && pointPositionFromStart <= elementsLengthSummary[3])
            {
                arcElement3.DrawNormal(canvas, X, Y, normalLength, pointPositionFromStart - elementsLengthSummary[2]);
                laserVectorTemp = arcElement3.CalculatePointPosition(pointPositionFromStart - elementsLengthSummary[2], normalLength);
                return new LaserVector(laserVectorTemp.X, laserVectorTemp.Y, laserVectorTemp.AngleRadians, laserVectorTemp.Length);
            }
            else if (pointPositionFromStart > elementsLengthSummary[3] && pointPositionFromStart <= elementsLengthSummary[4])
            {
                rayElement3.DrawNormal(canvas, X, Y, normalLength, pointPositionFromStart - elementsLengthSummary[3]);
                laserVectorTemp = rayElement3.CalculatePointPosition(pointPositionFromStart - elementsLengthSummary[3], normalLength);
                return new LaserVector(laserVectorTemp.X, laserVectorTemp.Y, laserVectorTemp.AngleRadians, laserVectorTemp.Length);
            }
            else if (pointPositionFromStart > elementsLengthSummary[4] && pointPositionFromStart <= elementsLengthSummary[5])
            {
                arcElement4.DrawNormal(canvas, X, Y, normalLength, pointPositionFromStart - elementsLengthSummary[4]);
                laserVectorTemp = arcElement4.CalculatePointPosition(pointPositionFromStart - elementsLengthSummary[4], normalLength);
                return new LaserVector(laserVectorTemp.X, laserVectorTemp.Y, laserVectorTemp.AngleRadians, laserVectorTemp.Length);
            }
            else if (pointPositionFromStart > elementsLengthSummary[5] && pointPositionFromStart <= elementsLengthSummary[6])
            {
                rayElement4.DrawNormal(canvas, X, Y, normalLength, pointPositionFromStart - elementsLengthSummary[5]);
                laserVectorTemp = rayElement4.CalculatePointPosition(pointPositionFromStart - elementsLengthSummary[5], normalLength);
                return new LaserVector(laserVectorTemp.X, laserVectorTemp.Y, laserVectorTemp.AngleRadians, laserVectorTemp.Length);
            }
            else if (pointPositionFromStart > elementsLengthSummary[6] && pointPositionFromStart <= elementsLengthSummary[7])
            {
                arcElement1.DrawNormal(canvas, X, Y, normalLength, pointPositionFromStart - elementsLengthSummary[6]);
                laserVectorTemp = arcElement1.CalculatePointPosition(pointPositionFromStart - elementsLengthSummary[6], normalLength);
                return new LaserVector(laserVectorTemp.X, laserVectorTemp.Y, laserVectorTemp.AngleRadians, laserVectorTemp.Length);
            }
            else if (pointPositionFromStart > elementsLengthSummary[7] && pointPositionFromStart <= elementsLengthSummary[8])
            {
                rayElement5.DrawNormal(canvas, X, Y, normalLength, pointPositionFromStart - elementsLengthSummary[7]);
                laserVectorTemp = rayElement5.CalculatePointPosition(pointPositionFromStart - elementsLengthSummary[7], normalLength);
                return new LaserVector(laserVectorTemp.X, laserVectorTemp.Y, laserVectorTemp.AngleRadians, laserVectorTemp.Length);
            }
            else
            {
                return new LaserVector(0, 0, 0,0);
            }
        }

        public double GetSectionLength()
        {
            elementsLength[0] = rayElement1.Length;
            elementsLength[1] = arcElement2.ArcLength;
            elementsLength[2] = rayElement2.Length;
            elementsLength[3] = arcElement3.ArcLength;
            elementsLength[4] = rayElement3.Length;
            elementsLength[5] = arcElement4.ArcLength;
            elementsLength[6] = rayElement4.Length;
            elementsLength[7] = arcElement1.ArcLength;
            elementsLength[8] = rayElement5.Length;

            elementsLengthSummary[0] = elementsLength[0];
            elementsLengthSummary[1] = elementsLength[0] + elementsLength[1];
            elementsLengthSummary[2] = elementsLength[0] + elementsLength[1] + elementsLength[2];
            elementsLengthSummary[3] = elementsLength[0] + elementsLength[1] + elementsLength[2] + elementsLength[3];
            elementsLengthSummary[4] = elementsLength[0] + elementsLength[1] + elementsLength[2] + elementsLength[3] + elementsLength[4];
            elementsLengthSummary[5] = elementsLength[0] + elementsLength[1] + elementsLength[2] + elementsLength[3] + elementsLength[4] + elementsLength[5];
            elementsLengthSummary[6] = elementsLength[0] + elementsLength[1] + elementsLength[2] + elementsLength[3] + elementsLength[4] + elementsLength[5] + elementsLength[6];
            elementsLengthSummary[7] = elementsLength[0] + elementsLength[1] + elementsLength[2] + elementsLength[3] + elementsLength[4] + elementsLength[5] + elementsLength[6] + elementsLength[7];
            elementsLengthSummary[8] = elementsLength[0] + elementsLength[1] + elementsLength[2] + elementsLength[3] + elementsLength[4] + elementsLength[5] + elementsLength[6] + elementsLength[7] + elementsLength[8];

            return elementsLength.Sum();
        }
        public int GetCurrentElement(double positionOnSection)
        {
            int element;
            double positionAdded = elementsLength[0];
            if (positionOnSection >= 0 && positionOnSection <= SectionLength)
            {
                for (int i = 0; i < 9;)
                {
                    if (positionOnSection <= positionAdded)
                    {
                        return i;
                    }
                    i++;
                    positionAdded += elementsLength[i];
                }
                element = 1;
            }
            else
            {
                element = -1;
            }
            return element;
        }
    }
}
