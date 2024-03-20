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
            rayElement1 = new RayElement(X, Y - (height / 2), 0, (width / 2) - radius2, Colors.Brown, 2);
            arcElement2 = new ArcSegmentElement(X + (width / 2) - radius2, Y - (height / 2) + radius2, radius2, 270, 0, Colors.LawnGreen, 2);
            rayElement2 = new RayElement(X + (width / 2), Y - (height / 2) + radius2, 90, (height) - radius2 - radius3, Colors.Khaki, 2);
            arcElement3 = new ArcSegmentElement(X + (width / 2) - radius3, Y + (height / 2) - radius3, radius3, 0, 90, Colors.Tomato, 2);
            rayElement3 = new RayElement(X + (width / 2) - radius3, Y + (height / 2), 180, (width) - radius4 - radius3, Colors.Violet, 2);
            arcElement4 = new ArcSegmentElement(X - (width / 2) + radius4, Y + (height / 2) - radius4, radius4, 90, 180, Colors.RoyalBlue, 2);
            rayElement4 = new RayElement(X - (width / 2), Y + (height / 2) - radius4, 270, (height) - radius4 - radius1, Colors.SeaGreen, 2);
            arcElement1 = new ArcSegmentElement(X - (width / 2) + radius1, Y - (height / 2) + radius1, radius1, 180, 270, Colors.Fuchsia, 2);
            rayElement5 = new RayElement(X - (width / 2) + radius1, Y - (height / 2), 0, (width / 2) - radius1, Colors.Indigo, 2);
            SectionLength = GetSectionLength();
        }

        public void Draw(Canvas canvas)
        {

            rayElement1.Draw(canvas, 0, elementsLengthSummary[0]);
            arcElement2.Draw(canvas, 1, elementsLengthSummary[1]);
            rayElement2.Draw(canvas, 2, elementsLengthSummary[2]);
            arcElement3.Draw(canvas, 3, elementsLengthSummary[3]);
            rayElement3.Draw(canvas, 4, elementsLengthSummary[4]);
            arcElement4.Draw(canvas, 5, elementsLengthSummary[5]);
            rayElement4.Draw(canvas, 6, elementsLengthSummary[6]);
            arcElement1.Draw(canvas, 7, elementsLengthSummary[7]);
            rayElement5.Draw(canvas, 8, elementsLengthSummary[8]);
        }

        public void DrawNormal(Canvas canvas, double normalLength, double pointPositionFromStart)
        {

            if (pointPositionFromStart <= elementsLengthSummary[0] && pointPositionFromStart > 0)
            {
                rayElement1.DrawNormal(canvas, normalLength, pointPositionFromStart - 0);
            }
            else if (pointPositionFromStart > elementsLengthSummary[0] && pointPositionFromStart <= elementsLengthSummary[1])
            {
                arcElement2.DrawNormal(canvas, normalLength, pointPositionFromStart - elementsLengthSummary[0]);
            }
            else if (pointPositionFromStart > elementsLengthSummary[1] && pointPositionFromStart <= elementsLengthSummary[2])
            {
                rayElement2.DrawNormal(canvas, normalLength, pointPositionFromStart - elementsLengthSummary[1]);
            }
            else if (pointPositionFromStart > elementsLengthSummary[2] && pointPositionFromStart <= elementsLengthSummary[3])
            {
                arcElement3.DrawNormal(canvas, normalLength, pointPositionFromStart - elementsLengthSummary[2]);
            }
            else if (pointPositionFromStart > elementsLengthSummary[3] && pointPositionFromStart <= elementsLengthSummary[4])
            {
                rayElement3.DrawNormal(canvas, normalLength, pointPositionFromStart - elementsLengthSummary[3]);
            }
            else if (pointPositionFromStart > elementsLengthSummary[4] && pointPositionFromStart <= elementsLengthSummary[5])
            {
                arcElement4.DrawNormal(canvas, normalLength, pointPositionFromStart - elementsLengthSummary[4]);
            }
            else if (pointPositionFromStart > elementsLengthSummary[5] && pointPositionFromStart <= elementsLengthSummary[6])
            {
                rayElement4.DrawNormal(canvas, normalLength, pointPositionFromStart - elementsLengthSummary[5]);
            }
            else if (pointPositionFromStart > elementsLengthSummary[6] && pointPositionFromStart <= elementsLengthSummary[7])
            {
                arcElement1.DrawNormal(canvas, normalLength, pointPositionFromStart - elementsLengthSummary[6]);
            }
            else if (pointPositionFromStart > elementsLengthSummary[7] && pointPositionFromStart <= elementsLengthSummary[8])
            {
                rayElement5.DrawNormal(canvas, normalLength, pointPositionFromStart - elementsLengthSummary[7]);
            }

            /*          arcElement2.DrawNormal(canvas, normalLength, pointPositionFromStart);
                      arcElement3.DrawNormal(canvas, normalLength, pointPositionFromStart);
                      arcElement4.DrawNormal(canvas, normalLength, pointPositionFromStart);
                      rayElement1.DrawNormal(canvas, normalLength, pointPositionFromStart);
                      rayElement2.DrawNormal(canvas, normalLength, pointPositionFromStart);
                      rayElement3.DrawNormal(canvas, normalLength, pointPositionFromStart);
                      rayElement4.DrawNormal(canvas, normalLength, pointPositionFromStart);
                      rayElement5.DrawNormal(canvas, normalLength, pointPositionFromStart);*/
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
