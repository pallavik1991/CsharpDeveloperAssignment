using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CsharpDeveloperAssignment
{
    public partial class Form1 : Form
    {
        string varshape;
        int n;
        
        
        TextBox[] txtA = new TextBox[20];
        TextBox[] txtB = new TextBox[20];
        TextBox[] txtC = new TextBox[20];
        TextBox[] txtD = new TextBox[20];

        //4 Lists to hold 4 cordinates of rectangle 
        List<Point> ptA = new List<Point>();
        List<Point> ptB = new List<Point>();
        List<Point> ptC = new List<Point>();
        List<Point> ptD = new List<Point>();

        //List to hold number of rectangles
        List<Rectangle> list = new List<Rectangle>();

        //List to hold intersect points
        List<Rectangle> intersect_points = new List<Rectangle>();
        public Form1()
        { 
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //fetch the input from controls
            varshape = comboBox1.SelectedItem.ToString();
            n = int.Parse(textBox1.Text);
                
            
            int pointX=80;
            int pointY=70;
            if (n <= 0)
            {
                MessageBox.Show("Provide valid number of shapes");
                System.Windows.Forms.Application.Exit();
            }
            if (varshape.Equals("Triangle"))
            {
                MessageBox.Show("Right now this application is for Rectangles only. Can be extended to other shapes also in future");
                System.Windows.Forms.Application.Exit();
            }
            if (varshape.Equals("Rectangle"))
            {

                //generate label, textboxes dynamically for n rectangle coordinates
                for (int i = 0; i < n; i++)
                {
                
                    Label lblcord = new Label();
                    lblcord.Text =i+1+ " Rectangle Coordinates";
                    lblcord.Location = new Point(pointX, pointY);
                    lblcord.Size = new Size(130, 13);
                    panel1.Controls.Add(lblcord);

                    txtA[i] = new TextBox();
                    txtA[i].Location = new Point(pointX+150, pointY);
                    txtA[i].Size = new Size(60, 13);
                    panel1.Controls.Add(txtA[i]);

                    txtB[i] = new TextBox();
                    txtB[i].Location = new Point(pointX + 230, pointY);
                    txtB[i].Size = new Size(60, 13);
                    panel1.Controls.Add(txtB[i]);

                    txtC[i] = new TextBox();
                    txtC[i].Location = new Point(pointX + 300, pointY);
                    txtC[i].Size = new Size(60, 13);
                    panel1.Controls.Add(txtC[i]);

                    txtD[i] = new TextBox();
                    txtD[i].Location = new Point(pointX + 370, pointY);
                    txtD[i].Size = new Size(60, 13);
                    panel1.Controls.Add(txtD[i]);

                    pointY += 30;
                }
            }
            panel1.Visible = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //fetch coordinate values from dynamically generated textboxes
            for (int i = 0; i < n; i++)
            {
                char[] splitchar = { ',' };

                if (txtA[i].Text.Contains("-") || txtB[i].Text.Contains("-") || txtC[i].Text.Contains("-") || txtD[i].Text.Contains("-"))
                {
                    MessageBox.Show("Provide Only Positive Coordinates");
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    string[] t = (txtA[i].Text).Split(splitchar);
                    ptA.Add(new Point(int.Parse(t[0]), int.Parse(t[1])));

                    string[] t1 = (txtB[i].Text).Split(splitchar);
                    ptB.Add(new Point(int.Parse(t1[0]), int.Parse(t1[1])));

                    string[] t2 = (txtC[i].Text).Split(splitchar);
                    ptC.Add(new Point(int.Parse(t2[0]), int.Parse(t2[1])));

                    string[] t3 = (txtD[i].Text).Split(splitchar);
                    ptD.Add(new Point(int.Parse(t3[0]), int.Parse(t3[1])));

                    //rectangle is added to list
                    list.Add(new Rectangle(ptA[i], ptB[i], ptC[i], ptD[i]));
                }
            }
            
            //check whether provided coordinates form regular/valid rectangles or not
            validateAllRectangles();
            string validmsg="";
            for (int j = 0; j < list.Count; j++)
            {
                if (list[j].valid == true)
                {
                    validmsg += " Coordinates Provided for Rectangle " + (j + 1) + " do form  a regular Rectangle\n";
                }
                else
                {
                    validmsg += " Coordinates Provided  for Rectangle " + (j + 1) + " do not form a regular Rectangle\n";
                }
            }

            MessageBox.Show(validmsg);

            //check how many rectangles intersect
            intersection();

            //Load another form to view rectangles
            Form2 frm = new Form2(list,intersect_points);
            frm.Show();
        }

        
        //Intersection
        public void intersection()
        {
            string intersection_msg = "";
            int xmin=0, xmax=0, ymin=0, ymax=0, x1min=0, x1max=0, y1min=0, y1max=0,xi,xj,yi,yj;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j].valid == false||list[i].valid==false)
                    {
                        continue;
                    }
                    if (list[i].getA().X > list[j].getD().X ||
                        list[i].getD().X < list[j].getA().X ||
                        list[i].getA().Y > list[j].getD().Y ||
                        list[i].getD().Y < list[j].getA().Y)
                    {

                    }
                    else
                    {
                        intersection_msg += "Rectangle " + (i + 1) + " and " + (j + 1) + " are intersecting\n";


                        //This is to find out the intersected points

                        if (list[i].getA().X == list[i].getB().X && list[i].getC().X == list[i].getD().X)
                        {
                            xmin = Math.Min(list[i].getA().X, list[i].getC().X);
                            xmax = Math.Max(list[i].getA().X, list[i].getC().X);
                            ymin = Math.Min(list[i].getA().Y, list[i].getB().Y);
                            ymax = Math.Max(list[i].getA().Y, list[i].getB().Y);
                        }
                        else if (list[i].getA().X == list[i].getC().X && list[i].getB().X == list[i].getD().X)
                        {
                            xmin = Math.Min(list[i].getA().X, list[i].getD().X);
                            xmax = Math.Max(list[i].getA().X, list[i].getD().X);
                            ymin = Math.Min(list[i].getA().Y, list[i].getC().Y);
                            ymax = Math.Max(list[i].getA().Y, list[i].getC().Y);

                        }
                        else if (list[i].getA().X == list[i].getD().X && list[i].getB().X == list[i].getC().X)
                        {
                            xmin = Math.Min(list[i].getA().X, list[i].getB().X);
                            xmax = Math.Max(list[i].getA().X, list[i].getB().X);
                            ymin = Math.Min(list[i].getA().Y, list[i].getD().Y);
                            ymax = Math.Max(list[i].getA().Y, list[i].getD().Y);
                        }

                        if (list[j].getA().X == list[j].getB().X && list[j].getC().X == list[j].getD().X)
                        {
                            x1min = Math.Min(list[j].getA().X, list[j].getC().X);
                            x1max = Math.Max(list[j].getA().X, list[j].getC().X);
                            y1min = Math.Min(list[j].getA().Y, list[j].getB().Y);
                            y1max = Math.Max(list[j].getA().Y, list[j].getB().Y);
                        }
                        else if (list[j].getA().X == list[j].getC().X && list[j].getB().X == list[j].getD().X)
                        {
                            x1min = Math.Min(list[j].getA().X, list[j].getD().X);
                            x1max = Math.Max(list[j].getA().X, list[j].getD().X);
                            y1min = Math.Min(list[j].getA().Y, list[j].getC().Y);
                            y1max = Math.Max(list[j].getA().Y, list[j].getC().Y);

                        }
                        else if (list[j].getA().X == list[j].getD().X && list[j].getB().X == list[j].getC().X)
                        {
                            x1min = Math.Min(list[j].getA().X, list[j].getB().X);
                            x1max = Math.Max(list[j].getA().X, list[j].getB().X);
                            y1min = Math.Min(list[j].getA().Y, list[j].getD().Y);
                            y1max = Math.Max(list[j].getA().Y, list[j].getD().Y);
                        }


                        xi=Math.Max(xmin,x1min) ;
                        xj=Math.Min(xmax,x1max) ;
                        yi=Math.Max(ymin,y1min);
                        yj = Math.Min(ymax, y1max);
                       
                        intersect_points.Add( new Rectangle(new Point(xi, yi), new Point(xj, yi), new Point(xi, yj), new Point(xj, yj)));
                        
                                    
                    }
                }

            }
            if (intersection_msg.Equals("")&&list.Count>=2)
            {
                MessageBox.Show("No proper Intersection");
            }
            else
            {
                MessageBox.Show(intersection_msg);
             
            }
             
        }


        //This method is to check whether all rectangles are valid or not
        public void validateAllRectangles()
        {
            
            int returnedValue=0;

            for (int i = 0; i < list.Count; i++)
            {
                returnedValue=list[i].validateRectangle();
                if (returnedValue == 1)
                {
                    list[i].valid = true;
                }
                else
                {
                    list[i].valid = false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
