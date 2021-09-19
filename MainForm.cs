using System;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace SS_OpenCV
{
    public partial class MainForm : Form
    {
        Image<Bgr, Byte> img = null; // working image
        Image<Bgr, Byte> imgUndo = null; // undo backup image - UNDO
        string title_bak = "";

        public MainForm()
        {
            InitializeComponent();
            title_bak = Text;
        }

        /// <summary>
        /// Opens a new image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                img = new Image<Bgr, byte>(openFileDialog1.FileName);
                Text = title_bak + " [" +
                        openFileDialog1.FileName.Substring(openFileDialog1.FileName.LastIndexOf("\\") + 1) +
                        "]";
                imgUndo = img.Copy();
                ImageViewer.Image = img.Bitmap;
                ImageViewer.Refresh();
            }
        }

        /// <summary>
        /// Saves an image with a new name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImageViewer.Image.Save(saveFileDialog1.FileName);
            }
        }

        /// <summary>
        /// Closes the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// restore last undo copy of the working image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgUndo == null) // verify if the image is already opened
                return; 
            Cursor = Cursors.WaitCursor;
            img = imgUndo.Copy();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        /// <summary>
        /// Chaneg visualization mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zoom
            if (autoZoomToolStripMenuItem.Checked)
            {
                ImageViewer.SizeMode = PictureBoxSizeMode.Zoom;
                ImageViewer.Dock = DockStyle.Fill;
            }
            else // with scroll bars
            {
                ImageViewer.Dock = DockStyle.None;
                ImageViewer.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        /// <summary>
        /// Show authors form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthorsForm form = new AuthorsForm();
            form.ShowDialog();
        }

        /// <summary>
        /// Calculate the image negative
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Negative(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void evalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EvalForm eval = new EvalForm();
            eval.ShowDialog();
        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.ConvertToGray(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor 
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void redChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.RedChannel(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

        }



        private void brightnessContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("contraste?");
            form.ShowDialog();
            double contraste = Convert.ToDouble(form.ValueTextBox.Text);

            InputBox form2 = new InputBox("brilho?");
            form2.ShowDialog();
            int brighte = Convert.ToInt32(form2.ValueTextBox.Text);

            ImageClass.BrightContrast(img, brighte, contraste);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor


        }

        /*
        private void translacaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("Xs?");
            form.ShowDialog();
            int Xs = Convert.ToInt32(form.ValueTextBox.Text);

            InputBox form2 = new InputBox("Ys?");
            form2.ShowDialog();
            int Ys = Convert.ToInt32(form2.ValueTextBox.Text);

            ImageClass.Translation(img,imgUndo, Xs, Ys);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

        }

        private void rotacaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("scaleFactor?");
            form.ShowDialog();
            float Angles = Convert.ToSingle(form.ValueTextBox.Text);

            ImageClass.Rotation(img, imgUndo, Angles);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

        }
        */

       

        private void translationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("Xs?");
            form.ShowDialog();
            int Xs = Convert.ToInt32(form.ValueTextBox.Text);

            InputBox form2 = new InputBox("Ys?");
            form2.ShowDialog();
            int Ys = Convert.ToInt32(form2.ValueTextBox.Text);

            ImageClass.Translation(img, imgUndo, Xs, Ys);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

        }

        private void rotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("scaleFactor?");
            form.ShowDialog();
            float Angles = Convert.ToSingle(form.ValueTextBox.Text);

            ImageClass.Rotation(img, imgUndo, Angles);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("scale Factor?");
            form.ShowDialog();
            float scaleFactor = Convert.ToSingle(form.ValueTextBox.Text);

            ImageClass.Scale(img, imgUndo, scaleFactor);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor


        }

        //criar as variaveis do rato

        int mouseX, mouseY;
        bool mouseFlag = false;

       
        private void zoompointxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor

            //ficar coms as coordenadas do rato
            mouseFlag = true;
            while (mouseFlag) //espera pelo evento, click do mouse
                Application.DoEvents();
            

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox form = new InputBox("scale Factor?");
            form.ShowDialog();
            float scaleFactor = Convert.ToSingle(form.ValueTextBox.Text);

            ImageClass.Scale_point_xy(img, imgUndo, scaleFactor, mouseX, mouseY );

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor


        }

        

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if(mouseFlag)
            {
                mouseX = e.X; // get mouse coordinates
                mouseY = e.Y;

                mouseFlag = false; 
            }
        }

        

        private void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Mean(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void meanCToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            InputBox tamanho = new InputBox("Size?");
            tamanho.ShowDialog();
            int size = Convert.ToInt32(tamanho.ValueTextBox.Text);

            ImageClass.Mean_solutionC(img, imgUndo, size);


            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

        }
        

        private void nonUniformToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (img == null) // verify if the image is already opened
                return;
            
            float[,] Matrix = new float[3,3];
            Form1 form = new Form1();
            form.ShowDialog();

            Matrix[0, 0] = Convert.ToSingle(form.textBox1.Text);
            Matrix[0, 1] = Convert.ToSingle(form.textBox2.Text);
            Matrix[0, 2] = Convert.ToSingle(form.textBox3.Text);
            Matrix[1, 0] = Convert.ToSingle(form.textBox4.Text);
            Matrix[1, 1] = Convert.ToSingle(form.textBox5.Text);
            Matrix[1, 2] = Convert.ToSingle(form.textBox6.Text);
            Matrix[2, 0] = Convert.ToSingle(form.textBox7.Text);
            Matrix[2, 1] = Convert.ToSingle(form.textBox8.Text);
            Matrix[2, 2] = Convert.ToSingle(form.textBox9.Text);

            float MatrixWeight = Convert.ToSingle(form.textBox10);

            Cursor = Cursors.WaitCursor; // clock cursor 
            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.NonUniform(img, imgUndo, Matrix, MatrixWeight);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

        }

        private void chessRecognitionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            Chess form = new Chess();
            form.ShowDialog();



            Cursor = Cursors.WaitCursor; // clock cursor 
            //copy Undo Image
            imgUndo = img.Copy();

           
            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }


        private void chessRecognitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            System.Drawing.Rectangle BD_Location = new System.Drawing.Rectangle(520, 850, 500, 100);
            string Angle = 48.ToString();
            string[,] tmp = new string[8, 8];
            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Chess_Recognition(img, imgUndo,out BD_Location,out Angle,out tmp);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursorr

        }
        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Sobel(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void diferenciacaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Diferentiation(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

        }

        private void medianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 
            
            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Median(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor



        }

        private void histogramGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            int[] histogramGray = new int[255];

            histogramGray = ImageClass.Histogram_Gray(img);

            HistogramForm form = new HistogramForm(histogramGray);
            form.ShowDialog();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor
        }

        private void histogramRGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int[,] histogram_RGB = new int[3,256];
            int[] arrayRed = new int[256];
            int[] arrayGreen = new int[256];
            int[] arrayBlue = new int[256];
            int i;

            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 
            //copy Undo Image
            imgUndo = img.Copy();

            histogram_RGB = ImageClass.Histogram_RGB(img);

            for (i = 0; i < 256; i++)
            {
                arrayBlue[i] = histogram_RGB[0, i];
                arrayGreen[i] = histogram_RGB[1, i];
                arrayRed[i] = histogram_RGB[2, i];
                
            }
           
            HistogramRGB form = new HistogramRGB(histogram_RGB, arrayBlue, arrayGreen, arrayRed);

            form.ShowDialog();

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

        }       

        private void histogramAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            int[,] histogram_All = new int[255,255];
            histogram_All = ImageClass.Histogram_All(img);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

        }


        private void convertToBWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();
            InputBox form = new InputBox("Introduza o valor de Threshold?");
            form.ShowDialog();
            int threshold = Convert.ToInt32(form.ValueTextBox.Text);

            ImageClass.ConvertToBW(img, threshold);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor

        }

        private void convertToBWOtsuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

             ImageClass.ConvertToBW_Otsu(img);
                        
            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursor


        }

        private void ImageViewer_Click(object sender, EventArgs e)
        {

        }

        

        private void robertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (img == null) // verify if the image is already opened
                return;
            Cursor = Cursors.WaitCursor; // clock cursor 

            //copy Undo Image
            imgUndo = img.Copy();

            ImageClass.Roberts(img, imgUndo);

            ImageViewer.Image = img.Bitmap;
            ImageViewer.Refresh(); // refresh image on the screen

            Cursor = Cursors.Default; // normal cursorr


        }


    }
}