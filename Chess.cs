using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;


namespace SS_OpenCV
{
    public partial class Chess : Form
    {

        Image<Bgr, Byte> img = null; // working image
        Image<Bgr, Byte> imgUndo = null; // undo backup image - UNDO
        
        string title_bak = "";

        public Chess()
        {
            InitializeComponent();
            title_bak = Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog chess = new OpenFileDialog();
            ////chess.ShowDialog();

            //chess.Filter = "Image Files(*.png)|*.PNG";

            //if (chess.ShowDialog() == DialogResult.OK)
            //{
            //    pictureBoxResult.ImageLocation = chess.FileName;
            //    pictureBoxResult.SizeMode = PictureBoxSizeMode.StretchImage;

            //}

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(openFileDialog1.FileName);
                Text = title_bak + " [" +
                        openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\", StringComparison.Ordinal) + 1) +
                        "]";
                imgUndo = img.Copy();
                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh();
            }

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PictureBox pb3 = new PictureBox();
            pb3.Image = Image.FromFile("C:\\Users\\tmzfo\\Desktop\\Sistemas Sensoriais\\SS_OpenCV_Base\\SS_OpenCV\bin\\Debug\\ImagemXadrez.png");
            pb3.SizeMode = PictureBoxSizeMode.StretchImage;
            System.Drawing.Rectangle BD_Location = new System.Drawing.Rectangle(520, 850, 500, 100);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 


            System.Drawing.Rectangle BD_Location = new System.Drawing.Rectangle(520, 850, 500, 100);
            string Angle = 48.ToString();
            string[,] tmp = new string[8, 8];
            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Chess_Recognition(img, imgUndo, out BD_Location, out Angle, out tmp);

            

            pictureBoxResult.Image = img.Bitmap;
            pictureBoxResult.Refresh(); // refresh image on the screen
            Cursor = Cursors.Default; // normal cursorr
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

      

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
