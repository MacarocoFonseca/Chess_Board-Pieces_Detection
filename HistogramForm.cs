using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ZedGraph;

namespace SS_OpenCV
{
    public partial class HistogramForm : Form
    {
        public HistogramForm(int [] array)
        {
            InitializeComponent();

            GraphPane pane = zedGraphControl1.GraphPane;
            //Titulos
            pane.Title.Text = "Histograma";
            pane.XAxis.Title.Text = "Intensidade";
            pane.YAxis.Title.Text = "Numero Pixeis";

            PointPairList list1 = new PointPairList();
            for (int i = 0; i < array.Length; i++)
            {
                list1.Add(i, array[i]);
            }

            pane.AddBar("imagem", list1, Color.Gray);
            pane.XAxis.Scale.Min = 0;
            pane.XAxis.Scale.Max = array.Length;
            zedGraphControl1.AxisChange();

        }

        //public HistogramForm(int [,] array)
        //{
        //    InitializeComponent();

        //    // get a reference to the GraphPane
        //    GraphPane myPane = zedGraphControl1.GraphPane;

        //    // Set the Titles
        //    myPane.Title.Text = "Histograma";
        //    myPane.XAxis.Title.Text = "Intensidade";
        //    myPane.YAxis.Title.Text = "Numero Pixeis";

        //    //list points
        //    PointPairList list1 = new PointPairList();
        //    PointPairList list2 = new PointPairList();
        //    PointPairList list3 = new PointPairList();

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        list1.Add(i, array[0,i]);
        //    }

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        list2.Add(i, array[1,i]);
        //    }

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        list3.Add(i, array[2,i]);
        //    }

        //    //add bar series
        //    myPane.AddCurve("Red", list1, Color.Red, SymbolType.None);
        //    myPane.AddCurve("Green", list2, Color.Green, SymbolType.None);
        //    myPane.AddCurve("Blue", list3, Color.Blue, SymbolType.None);
        //    myPane.XAxis.Scale.Min = 0;
        //    myPane.XAxis.Scale.Max = array.Length;
        //    zedGraphControl1.AxisChange();

        //}

        //public void HistogramFormAll(int [,] array)
        //{
           

        //    // get a reference to the GraphPane
        //    GraphPane myPane = zedGraphControl1.GraphPane;

        //    // Set the Titles
        //    myPane.Title.Text = "Histograma";
        //    myPane.XAxis.Title.Text = "Intensidade";
        //    myPane.YAxis.Title.Text = "Numero Pixeis";

        //    //list points
        //    PointPairList list1 = new PointPairList();
        //    PointPairList list2 = new PointPairList();
        //    PointPairList list3 = new PointPairList();
        //    PointPairList list4 = new PointPairList();

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        list1.Add(i, array[0, i]);
        //    }

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        list2.Add(i, array[1, i]);
        //    }

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        list3.Add(i, array[2, i]);
        //    }

        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        list4.Add(i, array[3, i]);
        //    }
        //    //add bar series
        //    myPane.AddCurve("Red", list1, Color.Red, SymbolType.None);
        //    myPane.AddCurve("Green", list2, Color.Green, SymbolType.None);
        //    myPane.AddCurve("Blue", list3, Color.Blue, SymbolType.None);
        //    myPane.AddCurve("Gray", list4, Color.Gray, SymbolType.None);
        //    myPane.XAxis.Scale.Min = 0;
        //    myPane.XAxis.Scale.Max = array.Length;
        //    zedGraphControl1.AxisChange();


        
    }
}
