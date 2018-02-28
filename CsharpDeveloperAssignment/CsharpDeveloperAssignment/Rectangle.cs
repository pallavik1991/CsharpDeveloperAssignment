using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CsharpDeveloperAssignment
{
    public class Rectangle
    {
        Point A=  new Point();
        Point B = new Point();
        Point C = new Point();
        Point D = new Point();

        public bool valid;
        public Rectangle(Point A,Point B,Point C,Point D)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;
        }

        //getter methods for coordinates
        public Point getA()
        {
            return this.A;
        }

        public Point getB()
        {
            return this.B;
        }

        public Point getC()
        {
            return this.C;
        }

        public Point getD()
        {
            return this.D;
        }
        //validate non-rotated ractangle
        public int validateRectangle()
        {
            if (A.X == B.X && C.X == D.X)
            {
                if (A.Y == C.Y && B.Y == D.Y)
                    return 1;
                else
                    return 0;
            }
            else if (A.X == C.X && B.X == D.X)
            {
                if (A.Y == B.Y && C.Y == D.Y)
                    return 1;
                else
                    return 0;
            }
            else if (A.X == D.X && B.X == C.X)
            {
                if (A.Y == B.Y && D.Y == C.Y)
                    return 1;
                else
                    return 0;
            }
            else
                return 0;
        }

    }
}
