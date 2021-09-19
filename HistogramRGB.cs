using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace SS_OpenCV
{
    public partial class HistogramRGB : Form
    {
        public HistogramRGB(int[,] array, int[] arrayRed, int[] arrayGreen, int[] arrayBlue)
        {
            InitializeComponent();

            // get a reference to the GraphPane
            GraphPane myPane = zedGraphControl1.GraphPane;

            // Set the Titles
            myPane.Title.Text = "Histograma";
            myPane.XAxis.Title.Text = "Intensidade";
            myPane.YAxis.Title.Text = "Numero Pixeis";

            //list points
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();

            for (int i = 0; i < 256; i++)
            {

                list1.Add(i, arrayBlue[i]);
                //list1.Add(i, array[0,i]);

                list2.Add(i, arrayGreen[i]);
           
                list3.Add(i, arrayRed[i]);
            }

            //add bar series
            myPane.AddCurve("Red", list1, Color.Red, SymbolType.None);
            myPane.AddCurve("Green", list2, Color.Green, SymbolType.None);
            myPane.AddCurve("Blue", list3, Color.Blue, SymbolType.None);
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 255;
            zedGraphControl1.AxisChange();
            
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
