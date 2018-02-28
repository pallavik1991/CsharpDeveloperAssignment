using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CsharpDeveloperAssignment
{
    public partial class Form2 : Form
    {
        List<Rectangle> list = new List<Rectangle>();
        List<Rectangle> intersect_points = new List<Rectangle>();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        public Form2(List<Rectangle> list,List<Rectangle> intersect_points)
        {
            this.list = list;
            this.intersect_points = intersect_points;

            //statement to call Form2_Paint
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form2_Paint);
        }
       
        public void Form2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            int i;
            Graphics g = this.CreateGraphics();
            char a = 'A';
            Pen p = new Pen(Color.Red,2);
            Pen q = new Pen(Color.RoyalBlue, 2);
            System.Drawing.SolidBrush brush=new System.Drawing.SolidBrush(Color.Black);

            //statements to draw rectangles
            for ( i = 0; i < list.Count; i++)
            {
                g.DrawLine(p, list[i].getA().X, list[i].getA().Y, list[i].getB().X, list[i].getB().Y);
                g.DrawLine(p, list[i].getA().X, list[i].getA().Y, list[i].getC().X, list[i].getC().Y);
                g.DrawString(a.ToString(), new Font("Ariel", 10), brush, new Point(list[i].getA().X, list[i].getA().Y));
                g.DrawLine(p, list[i].getC().X, list[i].getC().Y, list[i].getD().X, list[i].getD().Y);
                g.DrawLine(p, list[i].getB().X, list[i].getB().Y, list[i].getD().X, list[i].getD().Y);

                a++;
            }

            //statements to highlight intersection area
            int ay, dy;            
            for ( i = 0; i < intersect_points.Count; i++)
            {
                ay=intersect_points[i].getA().Y;
                dy=intersect_points[i].getD().Y;
                for (int j = 0; ay!=dy; j++)
                {
                    g.DrawLine(q, intersect_points[i].getA().X,ay, intersect_points[i].getB().X, ay);
                    ay += 2;
                }
            }
        
        }

    }
}
