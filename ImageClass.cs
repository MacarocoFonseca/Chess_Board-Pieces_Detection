using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing;
using System.IO;
using System.Diagnostics;


namespace SS_OpenCV
{
    class ImageClass
    {


        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        /// 
        // Negative mais rapido
        public static void Negative(Image<Bgr, byte> img)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // numero de canais 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)
                int x, y;


                /* Versao mais lenta, com acesso à memoria
                    Bgr aux;
                    for (y = 0; y < img.Height; y++)
                    {
                        for (x = 0; x < img.Width; x++)
                        {
                            // acesso directo : mais lento 
                            aux = img[y, x];
                            img[y, x] = new Bgr(255 - aux.Blue, 255 - aux.Green, 255 - aux.Red);
                        }
                    } */

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];


                            // store in the image
                            dataPtr[0] = (byte)(255 - blue);
                            dataPtr[1] = (byte)(255 - green);
                            dataPtr[2] = (byte)(255 - red);

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }
        /// <summary>
        /// Convert to gray
        /// Direct access to memory
        /// </summary>
        /// <param name="img">image</param>
        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            gray = (byte)(((int)blue + green + red) / 3);

                            // store in the image
                            dataPtr[0] = gray;
                            dataPtr[1] = gray;
                            dataPtr[2] = gray;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            //obtém as 3 componentes
                            blue = dataPtr[0];
                            green = dataPtr[1];
                            red = dataPtr[2];

                            // convert to gray
                            //gray = (byte)(((int)blue + green + red) / 3);

                            // store in the image
                            //dataPtr[0] = red;
                            dataPtr[0] = red;
                            dataPtr[1] = red;

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }
            }
        }

        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {


                            dataPtr[0] = (byte)((dataPtr[0] * contrast) + bright);
                            dataPtr[1] = (byte)((dataPtr[1] * contrast) + bright);
                            dataPtr[2] = (byte)((dataPtr[2] * contrast) + bright);

                            if (dataPtr[0] > 255)
                                dataPtr[0] = (byte)255;
                            if (dataPtr[0] < 0)
                                dataPtr[0] = (byte)0;

                            if (dataPtr[1] > 255)
                                dataPtr[1] = (byte)255;
                            if (dataPtr[1] < 0)
                                dataPtr[1] = 0;

                            if (dataPtr[2] > 255)
                                dataPtr[2] = (byte)255;
                            if (dataPtr[2] < 0)
                                dataPtr[2] = (byte)0;

                          


                            dataPtr[0] = (byte)dataPtr[0];
                            dataPtr[1] = (byte)dataPtr[1];
                            dataPtr[2] = (byte)dataPtr[2];

                            // advance the pointer to the next pixel
                            dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        dataPtr += padding;
                    }
                }

            }
        }

        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                                                                       // byte* dataPtrOrigAux;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino, xOrigem, yOrigem;
                int widthStep = m.widthStep; //

                if (nChan == 3) // image in RGB
                {
                    for (yDestino = 0; yDestino < height; yDestino++)
                    {
                        for (xDestino = 0; xDestino < width; xDestino++)
                        {

                            xOrigem = xDestino - dx;
                            yOrigem = yDestino - dy;

                            //Verificar se o pixel está dentro dos limites da imagem Destino
                            if ((xOrigem < 0) || (yOrigem < 0) || xOrigem >= width || yOrigem >= height)
                            {
                                //por o pixel a preto
                                dataPtrDest[0] = (byte)0;
                                dataPtrDest[1] = (byte)0;
                                dataPtrDest[2] = (byte)0;
                            }
                            else
                            {

                                ////Colocar o Pixel(RGB) no ponto (x,y) definido
                                dataPtrDest[0] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[0];
                                dataPtrDest[1] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[1];
                                dataPtrDest[2] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[2];
                            }

                            // advance the pointer to the next pixel
                            dataPtrDest += nChan;

                            //at the end of the line advance the pointer by the aligment bytes (padding)
                        }
                        dataPtrDest += padding;
                    }

                }

            }

        }
        //otimizado
        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, double angle)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                double halfWidth = width / 2.0;
                double halfHeight = height / 2.0;
                double cosAngle = Math.Cos(angle); //angle
                double sinAngle = Math.Sin(angle);

                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino, xOrigem, yOrigem;
                int widthStep = m.widthStep;


                if (nChan == 3) // image in RGB
                {
                    for (yDestino = 0; yDestino < height; yDestino++)
                    {
                        for (xDestino = 0; xDestino < width; xDestino++)
                        {


                            xOrigem = (int)Math.Round((xDestino - halfWidth) * cosAngle - (halfHeight - yDestino) * sinAngle + (halfWidth));
                            yOrigem = (int)Math.Round(halfHeight - (xDestino - halfWidth) * sinAngle - (halfHeight - yDestino) * cosAngle);


                            if ((xOrigem < 0) || (yOrigem < 0) || xOrigem >= width || yOrigem >= height)
                            {
                                //por o pixel a preto
                                dataPtrDest[0] = (byte)0;
                                dataPtrDest[1] = (byte)0;
                                dataPtrDest[2] = (byte)0;
                            }
                            else
                            {

                                ////Colocar o Pixel(RGB) no ponto (x,y) definido
                                dataPtrDest[0] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[0];
                                dataPtrDest[1] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[1];
                                dataPtrDest[2] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[2];
                            }

                            // advance the pointer to the next pixel
                            dataPtrDest += nChan;

                            //at the end of the line advance the pointer by the aligment bytes (padding)
                        }
                        dataPtrDest += padding;
                    }

                }

            }


        }



        public static void Scale(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino, xOrigem, yOrigem;
                int widthStep = m.widthStep; //

                if (nChan == 3) // image in RGB
                {
                    for (yDestino = 0; yDestino < height; yDestino++)
                    {
                        for (xDestino = 0; xDestino < width; xDestino++)
                        {

                            xOrigem = (int)Math.Round(xDestino / scaleFactor);
                            yOrigem = (int)Math.Round(yDestino / scaleFactor);


                            if (xOrigem < 0 || yOrigem < 0 || xOrigem >= width || yOrigem >= height)
                            {
                                //por o pixel a preto
                                dataPtrDest[0] = (byte)0;
                                dataPtrDest[1] = (byte)0;
                                dataPtrDest[2] = (byte)0;
                            }
                            else
                            {

                                ////Colocar o Pixel(RGB) no ponto (x,y) definido
                                dataPtrDest[0] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[0];
                                dataPtrDest[1] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[1];
                                dataPtrDest[2] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[2];
                            }

                            // advance the pointer to the next pixel
                            dataPtrDest += nChan;

                            //at the end of the line advance the pointer by the aligment bytes (padding)
                        }
                        dataPtrDest += padding;
                    }

                }

            }

        }

        //otimizado
        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int mouseX, int mouseY)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino, xOrigem, yOrigem;
                int widthStep = m.widthStep; //
                float halfWidthxScale = (width / 2) / scaleFactor;
                float halfHeightxScale = (height / 2) / scaleFactor;

                if (nChan == 3) // image in RGB
                {
                    for (yDestino = 0; yDestino < height; yDestino++)
                    {
                        for (xDestino = 0; xDestino < width; xDestino++)
                        {

                            xOrigem = (int)Math.Round((xDestino / scaleFactor) + mouseX - halfWidthxScale);
                            yOrigem = (int)Math.Round((yDestino / scaleFactor) + mouseY - halfHeightxScale);


                            if (xOrigem < 0 || yOrigem < 0 || xOrigem >= width || yOrigem >= height)
                            {
                                //por o pixel a preto
                                dataPtrDest[0] = (byte)0;
                                dataPtrDest[1] = (byte)0;
                                dataPtrDest[2] = (byte)0;
                            }
                            else
                            {

                                ////Colocar o Pixel(RGB) no ponto (x,y) definido
                                dataPtrDest[0] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[0];
                                dataPtrDest[1] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[1];
                                dataPtrDest[2] = (dataPtrOrig + (yOrigem * widthStep) + (xOrigem * nChan))[2];
                            }

                            // advance the pointer to the next pixel
                            dataPtrDest += nChan;

                            //at the end of the line advance the pointer by the aligment bytes (padding)
                        }
                        dataPtrDest += padding;
                    }

                }

            }
        }


        //Versao mais rapida (completa)
        // Filtro 3x3, 
        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)

                int xDestino, yDestino;
                int widthStep = m.widthStep; //
                int somaB, somaG, somaR, i, j;


                dataPtrDest += widthStep + nChan;
                if (nChan == 3) // image in RGB
                {
                    //Processar os pixeis no interior 
                    for (yDestino = 1; yDestino < height - 1; yDestino++)
                    {
                        for (xDestino = 1; xDestino < width - 1; xDestino++)
                        {
                            somaB = 0;
                            somaG = 0;
                            somaR = 0;

                            dataPtrAux = dataPtrOrig + ((yDestino - 1) * widthStep) + ((xDestino - 1) * nChan);


                            for (j = 0; j < 3; j++) // Fitlro 3x3 ou outra dimensao qualquer
                            {
                                for (i = 0; i < 3; i++)
                                {
                                    somaB += (dataPtrAux + i * nChan)[0];
                                    somaG += (dataPtrAux + i * nChan)[1];
                                    somaR += (dataPtrAux + i * nChan)[2];
                                }
                                dataPtrAux += widthStep;
                            }

                            ////Colocar o Pixel(RGB) no ponto (x,y) definido pixel do meio
                            dataPtrDest[0] = (byte)Math.Round(somaB / 9.0);
                            dataPtrDest[1] = (byte)Math.Round(somaG / 9.0);
                            dataPtrDest[2] = (byte)Math.Round(somaR / 9.0);


                            // advance the pointer to the next pixel
                            dataPtrDest += nChan;

                        }
                        dataPtrDest += padding + 2 * nChan; //Por causa das margens

                    }

                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                    dataPtrAux = dataPtrOrig;

                    //canto (0,0)
                    dataPtrDest[0] = (byte)((4 * dataPtrAux[0] + 2 * (dataPtrAux + nChan)[0] + 2 * (dataPtrAux + widthStep)[0] + (dataPtrAux + nChan + widthStep)[0]) / 9.0);
                    dataPtrDest[1] = (byte)((4 * dataPtrAux[1] + 2 * (dataPtrAux + nChan)[1] + 2 * (dataPtrAux + widthStep)[1] + (dataPtrAux + nChan + widthStep)[1]) / 9.0);
                    dataPtrDest[2] = (byte)((4 * dataPtrAux[2] + 2 * (dataPtrAux + nChan)[2] + 2 * (dataPtrAux + widthStep)[2] + (dataPtrAux + nChan + widthStep)[2]) / 9.0);


                    //Linha de Topo
                    dataPtrDest += nChan;
                    dataPtrAux += nChan;
                    for (i = 1; i < (width - 1); i++)
                    {

                        dataPtrDest[0] = (byte)((2 * (dataPtrAux[0]) + 2 * (dataPtrAux - nChan)[0] + 2 * (dataPtrAux + nChan)[0] + (dataPtrAux + widthStep)[0] + (dataPtrAux + widthStep - nChan)[0] + (dataPtrAux + widthStep + nChan)[0]) / 9.0);
                        dataPtrDest[1] = (byte)((2 * (dataPtrAux[1]) + 2 * (dataPtrAux - nChan)[1] + 2 * (dataPtrAux + nChan)[1] + (dataPtrAux + widthStep)[1] + (dataPtrAux + widthStep - nChan)[1] + (dataPtrAux + widthStep + nChan)[1]) / 9.0);
                        dataPtrDest[2] = (byte)((2 * (dataPtrAux[2]) + 2 * (dataPtrAux - nChan)[2] + 2 * (dataPtrAux + nChan)[2] + (dataPtrAux + widthStep)[2] + (dataPtrAux + widthStep - nChan)[2] + (dataPtrAux + widthStep + nChan)[2]) / 9.0);

                        dataPtrDest += nChan;
                        dataPtrAux += nChan;
                    }


                    //canto (width,0)
                    dataPtrDest[0] = (byte)((4 * dataPtrAux[0] + 2 * (dataPtrAux - nChan)[0] + 2 * (dataPtrAux + widthStep)[0] + (dataPtrAux - nChan + widthStep)[0]) / 9.0);
                    dataPtrDest[1] = (byte)((4 * dataPtrAux[1] + 2 * (dataPtrAux - nChan)[1] + 2 * (dataPtrAux + widthStep)[1] + (dataPtrAux - nChan + widthStep)[1]) / 9.0);
                    dataPtrDest[2] = (byte)((4 * dataPtrAux[2] + 2 * (dataPtrAux - nChan)[2] + 2 * (dataPtrAux + widthStep)[2] + (dataPtrAux - nChan + widthStep)[2]) / 9.0);


                    //linha da direita
                    dataPtrDest += widthStep;
                    dataPtrAux += widthStep;
                    for (i = 1; i < (height - 1); i++)
                    {
                        dataPtrDest[0] = (byte)((2 * (dataPtrAux[0]) + 2 * (dataPtrAux - widthStep)[0] + 2 * (dataPtrAux + widthStep)[0] + (dataPtrAux - nChan)[0] + (dataPtrAux - nChan - widthStep)[0] + (dataPtrAux - nChan + widthStep)[0]) / 9.0);
                        dataPtrDest[1] = (byte)((2 * (dataPtrAux[1]) + 2 * (dataPtrAux - widthStep)[1] + 2 * (dataPtrAux + widthStep)[1] + (dataPtrAux - nChan)[1] + (dataPtrAux - nChan - widthStep)[1] + (dataPtrAux - nChan + widthStep)[1]) / 9.0);
                        dataPtrDest[2] = (byte)((2 * (dataPtrAux[2]) + 2 * (dataPtrAux - widthStep)[2] + 2 * (dataPtrAux + widthStep)[2] + (dataPtrAux - nChan)[2] + (dataPtrAux - nChan - widthStep)[2] + (dataPtrAux - nChan + widthStep)[2]) / 9.0);

                        dataPtrDest += widthStep;
                        dataPtrAux += widthStep;
                    }


                    //canto (width,height)
                    dataPtrDest[0] = (byte)((4 * dataPtrAux[0] + 2 * (dataPtrAux - nChan)[0] + 2 * (dataPtrAux - widthStep)[0] + (dataPtrAux - nChan - widthStep)[0]) / 9.0);
                    dataPtrDest[1] = (byte)((4 * dataPtrAux[1] + 2 * (dataPtrAux - nChan)[1] + 2 * (dataPtrAux - widthStep)[1] + (dataPtrAux - nChan - widthStep)[1]) / 9.0);
                    dataPtrDest[2] = (byte)((4 * dataPtrAux[2] + 2 * (dataPtrAux - nChan)[2] + 2 * (dataPtrAux - widthStep)[2] + (dataPtrAux - nChan - widthStep)[2]) / 9.0);

                    dataPtrDest -= nChan;
                    dataPtrAux -= nChan;

                    //linha de baixo
                    for (i = 1; i < (width - 1); i++)
                    {
                        dataPtrDest[0] = (byte)((2 * (dataPtrAux[0]) + 2 * (dataPtrAux - nChan)[0] + 2 * (dataPtrAux + nChan)[0] + (dataPtrAux - widthStep)[0] + (dataPtrAux - nChan - widthStep)[0] + (dataPtrAux + nChan - widthStep)[0]) / 9.0);
                        dataPtrDest[1] = (byte)((2 * (dataPtrAux[1]) + 2 * (dataPtrAux - nChan)[1] + 2 * (dataPtrAux + nChan)[1] + (dataPtrAux - widthStep)[1] + (dataPtrAux - nChan - widthStep)[1] + (dataPtrAux + nChan - widthStep)[1]) / 9.0);
                        dataPtrDest[2] = (byte)((2 * (dataPtrAux[2]) + 2 * (dataPtrAux - nChan)[2] + 2 * (dataPtrAux + nChan)[2] + (dataPtrAux - widthStep)[2] + (dataPtrAux - nChan - widthStep)[2] + (dataPtrAux + nChan - widthStep)[2]) / 9.0);

                        dataPtrDest -= nChan;
                        dataPtrAux -= nChan;
                    }

                    //canto (0,height)
                    dataPtrDest[0] = (byte)((4 * dataPtrAux[0] + 2 * (dataPtrAux + nChan)[0] + 2 * (dataPtrAux - widthStep)[0] + (dataPtrAux + nChan - widthStep)[0]) / 9.0);
                    dataPtrDest[1] = (byte)((4 * dataPtrAux[1] + 2 * (dataPtrAux + nChan)[1] + 2 * (dataPtrAux - widthStep)[1] + (dataPtrAux + nChan - widthStep)[1]) / 9.0);
                    dataPtrDest[2] = (byte)((4 * dataPtrAux[2] + 2 * (dataPtrAux + nChan)[2] + 2 * (dataPtrAux - widthStep)[2] + (dataPtrAux + nChan - widthStep)[2]) / 9.0);

                    dataPtrDest -= widthStep;
                    dataPtrAux -= widthStep;

                    //linha da esquerda
                    for (i = 1; i < (height - 1); i++)
                    {
                        dataPtrDest[0] = (byte)((2 * (dataPtrAux[0]) + 2 * (dataPtrAux - widthStep)[0] + 2 * (dataPtrAux + widthStep)[0] + (dataPtrAux + nChan)[0] + (dataPtrAux + nChan - widthStep)[0] + (dataPtrAux - nChan + widthStep)[0]) / 9.0);
                        dataPtrDest[1] = (byte)((2 * (dataPtrAux[1]) + 2 * (dataPtrAux - widthStep)[1] + 2 * (dataPtrAux + widthStep)[1] + (dataPtrAux + nChan)[1] + (dataPtrAux + nChan - widthStep)[1] + (dataPtrAux - nChan + widthStep)[1]) / 9.0);
                        dataPtrDest[2] = (byte)((2 * (dataPtrAux[2]) + 2 * (dataPtrAux - widthStep)[2] + 2 * (dataPtrAux + widthStep)[2] + (dataPtrAux + nChan)[2] + (dataPtrAux + nChan - widthStep)[2] + (dataPtrAux - nChan + widthStep)[2]) / 9.0);

                        dataPtrDest -= widthStep;
                        dataPtrAux -= widthStep;
                    }

                }

            }


        }

        //solution C with 7x7 filter
        //(incompleto)
        public static void Mean_solutionC(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int size)
        {
            //unsafe
            //{
            //    MIplImage m = img.MIplImage;
            //    MIplImage mOrig = imgCopy.MIplImage;

            //    byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
            //    byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
            //    byte* dataPtrAux;

            //    int width = img.Width;
            //    int height = img.Height; //numero de pixeis que a imagem ocupas
            //    int nChan = m.nChannels; // number of channels - 3
            //    int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)

            //    int i, j;
            //    int widthStep = m.widthStep;


            //    if (nChan == 3) // image in RGB
            //    {
            //        dataPtrAux = dataPtrOrig;
            //        //Processar o primeiro pixel 

            //        dataPtrDest[0] = (byte)(dataPtrAux * ((size + 1) / 2) * ((size + 1) / 2))[0];
            //        dataPtrDest[1] = (byte)(dataPtrAux * ((size + 1) / 2) * ((size + 1) / 2))[1];
            //        dataPtrDest[2] = (byte)(dataPtrAux * ((size + 1) / 2) * ((size + 1) / 2))[2];

            //        //Pixeis à direita do primeiro
            //        for (j = 1; j < (size / 2); j++)
            //        {
            //            dataPtrDest[0] += (byte)(dataPtrAux + (j * nChan) * ((size + 1) / 2))[0];
            //            dataPtrDest[1] += (byte)(dataPtrAux + (j * nChan) * ((size + 1) / 2))[1];
            //            dataPtrDest[2] += (byte)(dataPtrAux + (j * nChan) * ((size + 1) / 2))[2];

            //        }

            //        dataPtrAux = dataPtrOrig;
            //        //pixeis a baxo do primeiro

            //        for (j = 1; j < (size / 2); j++)
            //        {
            //            dataPtrDest[0] += (byte)(dataPtrAux + (j * widthStep) * ((size + 1) / 2))[0];
            //            dataPtrDest[1] += (byte)(dataPtrAux + (j * widthStep) * ((size + 1) / 2))[1];
            //            dataPtrDest[2] += (byte)(dataPtrAux + (j * widthStep) * ((size + 1) / 2))[2];

            //        }


            //        dataPtrAux = dataPtrOrig + nChan + widthStep;

            //        for (j = 1; j < (size / 2); j++)
            //        {
            //            for (i = 1; i < (size / 2); i++)
            //            {
            //                dataPtrDest[0] += (byte)(dataPtrAux + (i * nChan))[0];
            //                dataPtrDest[1] += (byte)(dataPtrAux + (i * nChan))[1];
            //                dataPtrDest[2] += (byte)(dataPtrAux + (i * nChan))[2];

            //            }
            //            dataPtrDest += widthStep;
            //            dataPtrAux += widthStep;
            //        }




            //    }
            //}
        }


        //Filtro Não uniforme(completo)
        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)

                int xDestino, yDestino;
                int widthStep = m.widthStep;
                double somaB, somaG, somaR, somaRGB, somaRGB2;
                int i, j, vermelho, azul, verde;

                dataPtrDest += widthStep + nChan;
                if (nChan == 3) // image in RGB
                {
                    //Processar os pixeis no interior 
                    for (yDestino = 1; yDestino < height - 1; yDestino++)
                    {
                        for (xDestino = 1; xDestino < width - 1; xDestino++)
                        {
                            somaB = 0;
                            somaG = 0;
                            somaR = 0;

                            dataPtrAux = dataPtrOrig + ((yDestino - 1) * widthStep) + ((xDestino - 1) * nChan);


                            for (j = 0; j < 3; j++) // Fitlro 3x3 ou outra dimensao qualquer
                            {
                                for (i = 0; i < 3; i++)
                                {
                                    somaB += (dataPtrAux + i * nChan)[0] * matrix[i, j];
                                    somaG += (dataPtrAux + i * nChan)[1] * matrix[i, j];
                                    somaR += (dataPtrAux + i * nChan)[2] * matrix[i, j];
                                }
                                dataPtrAux += widthStep;
                            }

                            ////Colocar o Pixel(RGB) no ponto (x,y) definindo os pixeis do meio

                            //dataPtrDest[0] = (byte)Math.Round(somaB / matrixWeight);
                            //dataPtrDest[1] = (byte)Math.Round(somaG / matrixWeight);
                            //dataPtrDest[2] = (byte)Math.Round(somaR / matrixWeight);

                            azul = (int)Math.Round(somaB / matrixWeight);
                            verde = (int)Math.Round(somaG / matrixWeight);
                            vermelho = (int)Math.Round(somaR / matrixWeight);



                            if (azul > 255)
                                azul = 255;
                            if (azul < 0)
                                azul = Math.Abs(azul);

                            if (verde > 255)
                                verde = 255;
                            if (verde < 0)
                                verde = Math.Abs(verde);

                            if (vermelho > 255)
                                vermelho = 255;
                            if (vermelho < 0)
                                vermelho = Math.Abs(vermelho);

                            dataPtrDest[0] = (byte)azul;
                            dataPtrDest[1] = (byte)verde;
                            dataPtrDest[2] = (byte)vermelho;


                            // advance the pointer to the next pixel
                            dataPtrDest += nChan;

                        }
                        dataPtrDest += padding + 2 * nChan; //Por causa das margens

                    }

                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    dataPtrAux = dataPtrOrig;


                    //canto (0,0)
                    somaRGB = 0;
                    for (i = 0; i < 3; i++)
                    {
                        somaRGB = (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) * (dataPtrAux)[i];
                        somaRGB += (matrix[2, 0] + matrix[2, 1]) * (dataPtrAux + nChan)[i];
                        somaRGB += (matrix[0, 2] + matrix[1, 2]) * (dataPtrAux + widthStep)[i];
                        somaRGB += (matrix[2, 2]) * (dataPtrAux + nChan + widthStep)[i];


                        somaRGB2 = (int)Math.Round(somaRGB / matrixWeight);

                        if (somaRGB2 > 255)
                            somaRGB2 = 255;
                        if (somaRGB2 < 0)
                            somaRGB2 = 0;

                        dataPtrDest[i] = (byte)somaRGB2;
                    }


                    //Linha de Topo
                    dataPtrDest += nChan;
                    dataPtrAux += nChan;
                    somaRGB = 0;

                    for (i = 1; i < (width - 1); i++)
                    {
                        for (j = 0; j < 3; j++)
                        {
                            somaRGB = (matrix[0, 0] + matrix[0, 1]) * (dataPtrAux - nChan)[j];
                            somaRGB += (matrix[2, 0] + matrix[2, 1]) * (dataPtrAux + nChan)[j];
                            somaRGB += (matrix[1, 0] + matrix[1, 1]) * (dataPtrAux)[j];
                            somaRGB += (matrix[1, 2]) * (dataPtrAux + widthStep)[j];
                            somaRGB += (matrix[0, 2]) * (dataPtrAux - nChan + widthStep)[j];
                            somaRGB += (matrix[2, 2]) * (dataPtrAux + nChan + widthStep)[j];

                            somaRGB2 = (int)Math.Round(somaRGB / matrixWeight);

                            if (somaRGB2 < 0)
                                somaRGB2 = Math.Abs(somaRGB2);
                            if (somaRGB2 > 255)
                                somaRGB2 = 255;



                            dataPtrDest[j] = (byte)somaRGB2;

                        }
                        dataPtrDest += nChan;
                        dataPtrAux += nChan;
                    }


                    //canto (width,0)
                    somaRGB = 0;

                    for (i = 0; i < 3; i++)
                    {
                        somaRGB = (matrix[1, 0] + matrix[2, 0] + matrix[1, 1] + matrix[2, 1]) * (dataPtrAux)[i];
                        somaRGB += (matrix[0, 0] + matrix[0, 1]) * (dataPtrAux - nChan)[i];
                        somaRGB += (matrix[1, 2] + matrix[2, 2]) * (dataPtrAux + widthStep)[i];
                        somaRGB += (matrix[0, 2]) * (dataPtrAux - nChan + widthStep)[i];

                        somaRGB2 = (int)Math.Round(somaRGB / matrixWeight);

                        if (somaRGB2 > 255)
                            somaRGB2 = 255;
                        if (somaRGB2 < 0)
                            somaRGB2 = 0;


                        //if (i == 2)
                        //    dataPtrDest[i] = 255;
                        //else
                        //    dataPtrDest[i] = 0;

                        dataPtrDest[i] = (byte)somaRGB2;

                    }


                    //linha da direita
                    dataPtrDest += widthStep;
                    dataPtrAux += widthStep;

                    for (i = 1; i < (height - 1); i++)
                    {
                        for (j = 0; j < 3; j++)
                        {
                            somaRGB = (matrix[1, 0] + matrix[2, 0]) * (dataPtrAux - widthStep)[j];
                            somaRGB += (matrix[1, 2] + matrix[2, 2]) * (dataPtrAux + widthStep)[j];
                            somaRGB += (matrix[1, 1] + matrix[2, 1]) * (dataPtrAux)[j];
                            somaRGB += (matrix[0, 0]) * (dataPtrAux - widthStep - nChan)[j];
                            somaRGB += (matrix[0, 1]) * (dataPtrAux - nChan)[j];
                            somaRGB += (matrix[0, 2]) * (dataPtrAux - nChan + widthStep)[j];

                            somaRGB2 = (int)Math.Round(somaRGB / matrixWeight);

                            if (somaRGB2 > 255)
                                somaRGB2 = 255;
                            if (somaRGB2 < 0)
                                somaRGB2 = 0;


                            //if (j == 2)
                            //    dataPtrDest[j] = 255;
                            //else
                            //    dataPtrDest[j] = 0;
                            dataPtrDest[j] = (byte)somaRGB2;

                        }
                        dataPtrDest += widthStep;
                        dataPtrAux += widthStep;
                    }


                    //canto (width,height) 
                    somaRGB = 0;

                    for (i = 0; i < 3; i++)
                    {
                        somaRGB = (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]) * (dataPtrAux)[i];
                        somaRGB += (matrix[0, 1] + matrix[0, 2]) * (dataPtrAux - nChan)[i];
                        somaRGB += (matrix[1, 0] + matrix[2, 0]) * (dataPtrAux - widthStep)[i];
                        somaRGB += (matrix[0, 0]) * (dataPtrAux - nChan - widthStep)[i];

                        somaRGB2 = (int)Math.Round(somaRGB / matrixWeight);

                        if (somaRGB2 > 255)
                            somaRGB2 = 255;
                        if (somaRGB2 < 0)
                            somaRGB2 = 0;

                        //if (i == 2)
                        //    dataPtrDest[i] = 255;
                        //else
                        //    dataPtrDest[i] = 0;

                        dataPtrDest[i] = (byte)somaRGB2;

                    }


                    //linha de baixo
                    dataPtrDest -= nChan;
                    dataPtrAux -= nChan;

                    for (i = 1; i < (width - 1); i++)
                    {
                        for (j = 0; j < 3; j++)
                        {
                            somaRGB = (matrix[2, 1] + matrix[2, 2]) * (dataPtrAux + nChan)[j];
                            somaRGB += (matrix[1, 1] + matrix[1, 2]) * (dataPtrAux)[j];
                            somaRGB += (matrix[0, 1] + matrix[0, 2]) * (dataPtrAux - nChan)[j];
                            somaRGB += (matrix[2, 0]) * (dataPtrAux - widthStep + nChan)[j];
                            somaRGB += (matrix[1, 0]) * (dataPtrAux - widthStep)[j];
                            somaRGB += (matrix[0, 0]) * (dataPtrAux - nChan - widthStep)[j];

                            somaRGB2 = (int)Math.Round(somaRGB / matrixWeight);

                            if (somaRGB2 > 255)
                                somaRGB2 = 255;
                            if (somaRGB2 < 0)
                                somaRGB2 = 0;

                            //if (j == 2)
                            //    dataPtrDest[j] = 255;
                            //else
                            //    dataPtrDest[j] = 0;
                            dataPtrDest[j] = (byte)somaRGB2;

                        }
                        dataPtrDest -= nChan;
                        dataPtrAux -= nChan;
                    }

                    //canto (0,height)
                    somaRGB = 0;

                    for (i = 0; i < 3; i++)
                    {
                        somaRGB = (matrix[0, 1] + matrix[1, 1] + matrix[0, 2] + matrix[1, 2]) * (dataPtrAux)[i];
                        somaRGB += (matrix[2, 1] + matrix[2, 2]) * (dataPtrAux + nChan)[i];
                        somaRGB += (matrix[0, 0] + matrix[1, 0]) * (dataPtrAux - widthStep)[i];
                        somaRGB += (matrix[2, 0]) * (dataPtrAux + nChan - widthStep)[i];

                        somaRGB2 = (int)Math.Round(somaRGB / matrixWeight);

                        if (somaRGB2 > 255)
                            somaRGB2 = 255;
                        if (somaRGB2 < 0)
                            somaRGB2 = 0;


                        //if (i == 2)
                        //    dataPtrDest[i] = 255;
                        //else
                        //    dataPtrDest[i] = 0;
                        dataPtrDest[i] = (byte)somaRGB2;

                    }

                    //linha da esquerda
                    dataPtrDest -= widthStep;
                    dataPtrAux -= widthStep;

                    for (i = 1; i < (height - 1); i++)
                    {
                        for (j = 0; j < 3; j++)
                        {
                            somaRGB = (matrix[0, 0] + matrix[1, 0]) * (dataPtrAux - widthStep)[j];
                            somaRGB += (matrix[0, 1] + matrix[1, 1]) * (dataPtrAux)[j];
                            somaRGB += (matrix[0, 2] + matrix[1, 2]) * (dataPtrAux + widthStep)[j];
                            somaRGB += (matrix[2, 0]) * (dataPtrAux - widthStep + nChan)[j];
                            somaRGB += (matrix[2, 1]) * (dataPtrAux + nChan)[j];
                            somaRGB += (matrix[2, 2]) * (dataPtrAux + nChan + widthStep)[j];

                            somaRGB2 = (int)Math.Round(somaRGB / matrixWeight);

                            if (somaRGB2 > 255)
                                somaRGB2 = 255;
                            if (somaRGB2 < 0)
                                somaRGB2 = 0;

                            //if (j == 2)
                            //    dataPtrDest[j] = 255;
                            //else
                            //    dataPtrDest[j] = 0;

                            dataPtrDest[j] = (byte)somaRGB2;

                        }
                        dataPtrDest -= widthStep;
                        dataPtrAux -= widthStep;
                    }
                    //    */
                }

            }

        }

        // Filtro Sobel, Versão Simplificada (\completo)
        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep; //
                int i, j;
                int Sx = 0, Sy = 0, Stot = 0;

                dataPtrDest += widthStep + nChan;
                if (nChan == 3) // image in RGB
                {
                    //Processar os pixeis no interior 
                    for (yDestino = 1; yDestino < height - 1; yDestino++)
                    {
                        for (xDestino = 1; xDestino < width - 1; xDestino++)
                        {

                            dataPtrAux = dataPtrOrig + ((yDestino - 1) * widthStep) + ((xDestino - 1) * nChan);

                            for (i = 0; i < 3; i++)
                            {
                                Sx = (byte)(dataPtrAux)[i] + 2 * (byte)(dataPtrAux + widthStep)[i] + (byte)(dataPtrAux + 2 * widthStep)[i];
                                Sx -= (byte)(dataPtrAux + 2 * nChan)[i] + 2 * (byte)(dataPtrAux + 2 * nChan + widthStep)[i] + (byte)(dataPtrAux + 2 * nChan + (2 * widthStep))[i];

                                Sy = (byte)(dataPtrAux + (2 * widthStep))[i] + 2 * (byte)(dataPtrAux + (2 * widthStep) + nChan)[i] + (byte)(dataPtrAux + (2 * widthStep) + 2 * nChan)[i];
                                Sy -= (byte)(dataPtrAux)[i] + 2 * (byte)(dataPtrAux + nChan)[i] + (byte)(dataPtrAux + 2 * nChan)[i];

                                Stot = Math.Abs(Sx) + Math.Abs(Sy);

                                if (Stot > 255)
                                    Stot = 255;


                                dataPtrDest[i] = (byte)Stot;

                            }
                            dataPtrDest += nChan;
                        }
                        dataPtrDest += padding + 2 * nChan;
                    }

                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                    dataPtrAux = dataPtrOrig;

                    //canto (0,0)
                    for (i = 0; i < 3; i++)
                    {

                        Sx = 3 * (byte)(dataPtrAux)[i] + (byte)(dataPtrAux + widthStep)[i];
                        Sx -= 3 * (byte)(dataPtrAux + nChan)[i] + (byte)(dataPtrAux + widthStep + nChan)[i];

                        Sy = 3 * (byte)(dataPtrAux + widthStep)[i] + (byte)(dataPtrAux + nChan + widthStep)[i];
                        Sy -= 3 * (byte)(dataPtrAux)[i] + (byte)(dataPtrAux + nChan)[i];

                        Stot = Math.Abs(Sx) + Math.Abs(Sy);

                        if (Stot > 255)
                            Stot = 255;


                        dataPtrDest[i] = (byte)Stot;
                    }

                    dataPtrDest += nChan;
                    dataPtrAux += nChan;

                    //Linha de Topo
                    for (j = 1; j < (width - 1); j++)
                    {
                        for (i = 0; i < 3; i++)
                        {
                            Sx = 3 * (byte)(dataPtrAux - nChan)[i] + (byte)(dataPtrAux - nChan + widthStep)[i];
                            Sx -= 3 * (byte)(dataPtrAux + nChan)[i] + (byte)(dataPtrAux + nChan + widthStep)[i];

                            Sy = (byte)(dataPtrAux - nChan + widthStep)[i] + 2 * (byte)(dataPtrAux + widthStep)[i] + (byte)(dataPtrAux + widthStep + nChan)[i];
                            Sy -= (byte)(dataPtrAux - nChan)[i] + 2 * (byte)(dataPtrAux)[i] + (byte)(dataPtrAux + nChan)[i];

                            Stot = Math.Abs(Sx) + Math.Abs(Sy);

                            if (Stot > 255)
                                Stot = 255;

                            dataPtrDest[i] = (byte)Stot;

                        }

                        dataPtrDest += nChan;
                        dataPtrAux += nChan;
                    }


                    //canto (width,0)
                    for (i = 0; i < 3; i++)
                    {
                        Sx = 3 * (byte)(dataPtrAux - nChan)[i] + (byte)(dataPtrAux + widthStep - nChan)[i];
                        Sx -= 3 * (byte)(dataPtrAux)[i] + (byte)(dataPtrAux + widthStep)[i];

                        Sy = (byte)(dataPtrAux - nChan + widthStep)[i] + 3 * (byte)(dataPtrAux + widthStep)[i];
                        Sy -= (byte)(dataPtrAux - nChan)[i] + 3 * (byte)(dataPtrAux)[i];

                        Stot = Math.Abs(Sx) + Math.Abs(Sy);


                        if (Stot > 255)
                            Stot = 255;

                        dataPtrDest[i] = (byte)Stot;

                    }

                    dataPtrDest += widthStep;
                    dataPtrAux += widthStep;

                    //linha da direita
                    for (j = 1; j < (height - 1); j++)
                    {
                        for (i = 0; i < 3; i++)
                        {

                            Sx = (byte)(dataPtrAux - nChan - widthStep)[i] + 2 * (byte)(dataPtrAux - nChan)[i] + (byte)(dataPtrAux - nChan + widthStep)[i];
                            Sx -= (byte)(dataPtrAux - widthStep)[i] + 2 * (byte)(dataPtrAux)[i] + (byte)(dataPtrAux + widthStep)[i];

                            Sy = (byte)(dataPtrAux - nChan + widthStep)[i] + 3 * (byte)(dataPtrAux + widthStep)[i];
                            Sy -= (byte)(dataPtrAux - nChan - widthStep)[i] + 3 * (byte)(dataPtrAux - widthStep)[i];

                            Stot = Math.Abs(Sx) + Math.Abs(Sy);


                            if (Stot > 255)
                                Stot = 255;

                            //if (i == 2)
                            //    dataPtrDest[i] = 255;// (byte)Stot;
                            //else
                            //    dataPtrDest[i] = 0;

                            dataPtrDest[i] = (byte)Stot;

                        }
                        dataPtrDest += widthStep;
                        dataPtrAux += widthStep;
                    }

                    //canto (width,height)
                    for (i = 0; i < 3; i++)
                    {
                        Sx = (byte)(dataPtrAux - nChan - widthStep)[i] + 3 * (byte)(dataPtrAux - nChan)[i];
                        Sx -= (byte)(dataPtrAux - widthStep)[i] + 3 * (byte)(dataPtrAux)[i];

                        Sy = (byte)(dataPtrAux - nChan)[i] + 3 * (byte)(dataPtrAux)[i];
                        Sy -= (byte)(dataPtrAux - nChan - widthStep)[i] + 3 * (byte)(dataPtrAux - widthStep)[i];

                        // Stot = 0;

                        Stot = Math.Abs(Sx) + Math.Abs(Sy);


                        if (Stot > 255)
                            Stot = 255;

                        dataPtrDest[i] = (byte)Stot;

                    }

                    dataPtrDest -= nChan;
                    dataPtrAux -= nChan;

                    //linha de baixo
                    for (j = 1; j < (width - 1); j++)
                    {
                        for (i = 0; i < 3; i++)
                        {

                            Sx = (byte)(dataPtrAux - nChan - widthStep)[i] + 3 * (byte)(dataPtrAux - nChan)[i];
                            Sx -= (byte)(dataPtrAux + nChan - widthStep)[i] + 3 * (byte)(dataPtrAux + nChan)[i];

                            Sy = (byte)(dataPtrAux - nChan)[i] + 2 * (byte)(dataPtrAux)[i] + (byte)(dataPtrAux + nChan)[i];
                            Sy -= (byte)(dataPtrAux - nChan - widthStep)[i] + 2 * (byte)(dataPtrAux - widthStep)[i] + (byte)(dataPtrAux - widthStep + nChan)[i];


                            Stot = Math.Abs(Sx) + Math.Abs(Sy);


                            if (Stot > 255)
                                Stot = 255;

                            dataPtrDest[i] = (byte)Stot;

                        }
                        dataPtrDest -= nChan;
                        dataPtrAux -= nChan;
                    }

                    //canto (0,height)
                    for (i = 0; i < 3; i++)
                    {
                        Sx = (byte)(dataPtrAux - widthStep)[i] + 3 * (byte)(dataPtrAux)[i];
                        Sx -= (byte)(dataPtrAux + nChan - widthStep)[i] + 3 * (byte)(dataPtrAux + nChan)[i];

                        Sy = 3 * (byte)(dataPtrAux)[i] + (byte)(dataPtrAux + nChan)[i];
                        Sy -= 3 * (byte)(dataPtrAux - widthStep)[i] + (byte)(dataPtrAux + nChan - widthStep)[i];

                        Stot = Math.Abs(Sx) + Math.Abs(Sy);


                        if (Stot > 255)
                            Stot = 255;

                        //if (i == 2)
                        //    dataPtrDest[i] = 255;// (byte)Stot;
                        //else
                        //   dataPtrDest[i] = 0;

                        dataPtrDest[i] = (byte)Stot;
                    }

                    dataPtrDest -= widthStep;
                    dataPtrAux -= widthStep;



                    //linha da esquerda
                    for (j = 1; j < (height - 1); j++)
                    {
                        for (i = 0; i < 3; i++)
                        {

                            Sx = (byte)(dataPtrAux - widthStep)[i] + 2 * (byte)(dataPtrAux)[i] + (byte)(dataPtrAux + widthStep)[i];
                            Sx -= (byte)(dataPtrAux - widthStep + nChan)[i] + 2 * (byte)(dataPtrAux + nChan)[i] + (byte)(dataPtrAux + nChan + widthStep)[i];

                            Sy = 3 * (byte)(dataPtrAux + widthStep)[i] + (byte)(dataPtrAux + nChan + widthStep)[i];
                            Sy -= 3 * (byte)(dataPtrAux - widthStep)[i] + (byte)(dataPtrAux + nChan - widthStep)[i];

                            Stot = Math.Abs(Sx) + Math.Abs(Sy);


                            if (Stot > 255)
                                Stot = 255;

                            dataPtrDest[i] = (byte)Stot;

                        }
                        dataPtrDest -= widthStep;
                        dataPtrAux -= widthStep;
                    }

                }
            }
        }

        //Filtro Roberts (completo)
        public static void Roberts(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep;
                int i, j;
                int diagonalEsquerda = 0, diagonalDireita = 0, Stot = 0;


                if (nChan == 3) // image in RGB
                {
                    for (yDestino = 0; yDestino < height - 1; yDestino++)
                    {
                        for (xDestino = 0; xDestino < width - 1; xDestino++)
                        {
                            dataPtrAux = dataPtrOrig + ((yDestino) * widthStep) + ((xDestino) * nChan);

                            for (i = 0; i < 3; i++) //RGB
                            {
                                diagonalEsquerda = (int)(dataPtrAux)[i] - (dataPtrAux + nChan + widthStep)[i];
                                diagonalDireita = (int)(dataPtrAux + nChan)[i] - (dataPtrAux + widthStep)[i];

                                Stot = Math.Abs(diagonalEsquerda) + Math.Abs(diagonalDireita);

                                if (Stot > 255)
                                    Stot = (byte)255;

                                dataPtrDest[i] = (byte)Stot;

                            }
                            dataPtrDest += nChan;
                        }
                        dataPtrDest += padding + nChan;
                    }

                    //pixeis da linha da direita 
                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();

                    dataPtrDest += (width - 1) * nChan;
                    dataPtrAux = dataPtrOrig + (width - 1) * nChan;

                    for (j = 0; j < height - 1; j++)
                    {
                        for (i = 0; i < 3; i++)
                        {
                            // diagonalDireita = (byte)(2*(dataPtrAux[i] - (dataPtrAux + widthStep)[i]));

                            Stot = Math.Abs((byte)(2 * (dataPtrAux[i] - (dataPtrAux + widthStep)[i])));

                            if (Stot > 255)
                                Stot = 255;

                            dataPtrDest[i] = (byte)Stot;
                        }
                        dataPtrDest += widthStep;
                        dataPtrAux += widthStep;
                    }

                    //canto (height, width)
                    dataPtrDest[0] = (byte)0;
                    dataPtrDest[1] = (byte)0;
                    dataPtrDest[2] = (byte)0;

                    dataPtrDest -= nChan;
                    dataPtrAux -= nChan;

                    //linha de baixo
                    for (j = 1; j < width; j++)
                    {
                        for (i = 0; i < 3; i++)
                        {

                            Stot = Math.Abs((byte)(2 * (dataPtrAux[i] - (dataPtrAux + nChan)[i])));

                            if (Stot > 255)
                                Stot = 255;

                            dataPtrDest[i] = (byte)Stot;
                        }
                        dataPtrDest -= nChan;
                        dataPtrAux -= nChan;

                    }


                }

            }

        }

        //Filtro Diferenciacao (completo)
        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep; //
                int i, j;
                int Sx = 0, Sy = 0, Stot = 0;

                if (nChan == 3) // image in RGB
                {
                    for (yDestino = 0; yDestino < height - 1; yDestino++)
                    {
                        for (xDestino = 0; xDestino < width - 1; xDestino++)
                        {
                            dataPtrAux = dataPtrOrig + ((yDestino) * widthStep) + ((xDestino) * nChan);

                            for (i = 0; i < 3; i++) //RGB
                            {
                                Sx = dataPtrAux[i] - (dataPtrAux + nChan)[i];
                                Sy = dataPtrAux[i] - (dataPtrAux + widthStep)[i];

                                Stot = Math.Abs(Sx) + Math.Abs(Sy);

                                if (Stot > 255)
                                    Stot = (byte)255;

                                dataPtrDest[i] = (byte)Stot;

                            }
                            dataPtrDest += nChan;
                        }
                        dataPtrDest += padding + nChan;
                    }

                    //pixeis da linha da direita 
                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrOrig = (byte*)mOrig.imageData.ToPointer();

                    dataPtrDest += (width - 1) * nChan;
                    dataPtrAux = dataPtrOrig + (width - 1) * nChan;

                    for (j = 0; j < height - 1; j++)
                    {
                        for (i = 0; i < 3; i++)
                        {
                            // diagonalDireita = (byte)(2*(dataPtrAux[i] - (dataPtrAux + widthStep)[i]));
                            Sx = (byte)0;
                            Sy = dataPtrAux[i] - (dataPtrAux + widthStep)[i];

                            Stot = Math.Abs(Sx) + Math.Abs(Sy);

                            if (Stot > 255)
                                Stot = 255;


                            dataPtrDest[i] = (byte)Stot;
                        }
                        dataPtrDest += widthStep;
                        dataPtrAux += widthStep;
                    }

                    //canto (height, width)
                    dataPtrDest[0] = (byte)0;
                    dataPtrDest[1] = (byte)0;
                    dataPtrDest[2] = (byte)0;

                    dataPtrDest -= nChan;
                    dataPtrAux -= nChan;

                    //linha de baixo
                    for (j = 1; j < width; j++)
                    {
                        for (i = 0; i < 3; i++)
                        {
                            Sx = dataPtrAux[i] - (dataPtrAux + nChan)[i];
                            Sy = (byte)0;

                            Stot = Math.Abs(Sx) + Math.Abs(Sy);

                            if (Stot > 255)
                                Stot = 255;

                            dataPtrDest[i] = (byte)Stot;
                        }
                        dataPtrDest -= nChan;
                        dataPtrAux -= nChan;

                    }


                }
            }
        }

        //Por acabar
        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep;

                int dist_anterior = 0;
                int dist_actual = 0;
                int[] position_pointer = new int[9];

                if (nChan == 3) // imagem em RGB
                {
                    dataPtrDest = dataPtrDest + m.widthStep + nChan;
                    dataPtrAux = dataPtrOrig + m.widthStep + nChan;


                    for (yDestino = 0; yDestino < height - 2; yDestino++)
                    {
                        for (xDestino = 0; xDestino < width - 2; xDestino++)
                        {

                            //position_pointer
                            //0 1 2
                            //3 4 5
                            //6 7 8
                            //posições do apontador
                            position_pointer[0] = (int)(dataPtrAux - m.widthStep - nChan);
                            position_pointer[1] = (int)(dataPtrAux - m.widthStep);
                            position_pointer[2] = (int)(dataPtrAux - m.widthStep + nChan);
                            position_pointer[3] = (int)(dataPtrAux - nChan);
                            position_pointer[4] = (int)(dataPtrAux);
                            position_pointer[5] = (int)(dataPtrAux + nChan);
                            position_pointer[6] = (int)(dataPtrAux + m.widthStep - nChan);
                            position_pointer[7] = (int)(dataPtrAux + m.widthStep);
                            position_pointer[8] = (int)(dataPtrAux + m.widthStep + nChan);


                            dist_anterior = int.MaxValue;
                            for (int a = 0; a < 9; a++)
                            {
                                dist_actual = 0;
                                for (int b = 0; b < 9; b++)
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        dist_actual += Math.Abs(((byte*)position_pointer[a])[i] - ((byte*)position_pointer[b])[i]);
                                    }
                                }
                                if (dist_actual < dist_anterior)
                                {
                                    dist_anterior = dist_actual;
                                    dataPtrDest[0] = ((byte*)position_pointer[a])[0];
                                    dataPtrDest[1] = ((byte*)position_pointer[a])[1];
                                    dataPtrDest[2] = ((byte*)position_pointer[a])[2];
                                }

                            }
                            // avança apontador para próximo pixel
                            dataPtrDest += nChan;
                            dataPtrAux += nChan;
                        }
                        dataPtrDest += padding + 2 * nChan;
                        dataPtrAux += padding + 2 * nChan;
                    }
                }
            }

        }

        //Feito
        public static int[] Histogram_Gray(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                //MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                                                                    //byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                                                                    // byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep;
                byte grey;

                int[] vecCinzentos = new int[256];

                for (yDestino = 0; yDestino < height; yDestino++)
                {
                    for (xDestino = 0; xDestino < width; xDestino++)
                    {
                        grey = (byte)Math.Round(((double)dataPtrDest[0] + dataPtrDest[1] + dataPtrDest[2]) / 3);
                        vecCinzentos[grey]++;

                        dataPtrDest += nChan;
                    }
                    dataPtrDest += padding;
                }
                return vecCinzentos;
            }


        }

        //Feito
        public static int[,] Histogram_RGB(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                // MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                //byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                //byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep;
                // byte grey;

                int[,] vecRGB = new int[3, 256];

                for (yDestino = 0; yDestino < height; yDestino++)
                {
                    for (xDestino = 0; xDestino < width; xDestino++)
                    {
                        vecRGB[0, dataPtrDest[0]]++;
                        vecRGB[1, dataPtrDest[1]]++;
                        vecRGB[2, dataPtrDest[2]]++;

                        dataPtrDest += nChan;
                    }
                    dataPtrDest += padding;
                }
                return vecRGB;
            }


        }

        public static int[,] Histogram_All(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                // MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                                                                    //byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                                                                    // byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep;

                int[,] vecAll = new int[4, 256];

                for (yDestino = 0; yDestino < height; yDestino++)
                {
                    for (xDestino = 0; xDestino < width; xDestino++)
                    {
                        vecAll[0, (int)Math.Round((double)(dataPtrDest[0] + dataPtrDest[1] + dataPtrDest[2]) / 3.0)]++;
                        vecAll[1, dataPtrDest[0]]++;
                        vecAll[2, dataPtrDest[1]]++;
                        vecAll[3, dataPtrDest[2]]++;



                        dataPtrDest += nChan;
                    }
                    dataPtrDest += padding;
                }
                return vecAll;
            }

        }

        //Feito
        public static void ConvertToBW(Emgu.CV.Image<Bgr, byte> img, int threshold)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int widthStep = m.widthStep;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                byte gray;


                for (yDestino = 0; yDestino < height; yDestino++)
                {
                    for (xDestino = 0; xDestino < width; xDestino++)
                    {
                        gray = (byte)Math.Round((((double)dataPtrDest[0] + dataPtrDest[1] + dataPtrDest[2]) / 3.0));
                        if (gray > threshold)
                        {
                            dataPtrDest[0] = (byte)255;
                            dataPtrDest[1] = (byte)255;
                            dataPtrDest[2] = (byte)255;

                        }
                        else
                        {
                            dataPtrDest[0] = (byte)0;
                            dataPtrDest[1] = (byte)0;
                            dataPtrDest[2] = (byte)0;
                        }


                        dataPtrDest += nChan;
                    }
                    dataPtrDest += padding;
                }

            }

        }

        //Feito
        private static int Otsu(int[] grayHist)
        {
            int i = 0, t = 0, totalPixel = 0, threshold = 0;
            double q1 = 0, q2 = 0, u1 = 0, u2 = 0;
            double variancia = 0, varAux = 0;

            for (i = 0; i < 256; i++)
            {
                totalPixel += grayHist[i];
            }

            for (t = 0; t < 256; t++)
            {
                q1 = 0;
                u1 = 0;
                for (i = 0; i <= t; i++)
                {
                    q1 += grayHist[i];
                    u1 += i * grayHist[i];
                }
                q1 = q1 / totalPixel;
                u1 = (u1 / totalPixel) / q1;

                q2 = 0;
                u2 = 0;
                for (i = t + 1; i < 256; i++)
                {
                    q2 += grayHist[i];
                    u2 += i * (grayHist[i]);
                }
                q2 = q2 / totalPixel;
                u2 = (u2 / totalPixel) / q2;

                variancia = q1 * q2 * Math.Pow((u1 - u2), 2);

                if (variancia > varAux)
                {
                    varAux = variancia;
                    threshold = t;
                }
            }
            return threshold;
        }

        //Feito
        public static void ConvertToBW_Otsu(Image<Bgr, byte> img)
        {
            int[] grayHist = Histogram_Gray(img);
            int threshold = Otsu(grayHist);
            ConvertToBW(img, threshold);
        }


        //dataPtrDest[0] = 0;
        //dataPtrDest[1] = 0;
        //dataPtrDest[2] = 255;

        //----------------------------------------trabalho Final---------------------------------------------------------------------------------///

        public static unsafe void Rotation_Bilinear(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, double angle)
        {
            MIplImage m = img.MIplImage;
            MIplImage mc = imgCopy.MIplImage;
            byte* imgPtrOrigem = (byte*)m.imageData.ToPointer();
            byte* imgCopyPtr = (byte*)mc.imageData.ToPointer();


            int width = img.Width;
            int widthStep = m.widthStep;
            int nChannels = m.nChannels;
            int height = img.Height;
            int padding = widthStep - nChannels * m.width;

           
           
            double xOrigem, yOrigem, factor;
            byte[] destX1 = new byte[3], destX2 = new byte[3];

            double halfWidth = width / 2.0;
            double halfHeight = height / 2.0;
            double cosAngle = Math.Cos(angle); //angle
            double sinAngle = Math.Sin(angle);
            int xDestino = 0, yDestino = 0, auxX1, auxX2, auxY1, auxY2 ;

            for (yDestino = 0; yDestino < height; yDestino++)
            {
                for (xDestino = 0; xDestino < width; xDestino++)
                {
                    xOrigem = ((xDestino - (halfWidth)) * cosAngle - ((halfHeight) - yDestino) * sinAngle + (halfWidth));
                    yOrigem = ((halfHeight) - (xDestino - (halfWidth)) * sinAngle - ((halfHeight) - yDestino) * cosAngle);


                    if (xOrigem < 0 || yOrigem < 0 || Math.Ceiling(xOrigem) >= width || Math.Ceiling(yOrigem) >= height)
                    {
                        imgPtrOrigem[0] = 0;
                        imgPtrOrigem[1] = 0;
                        imgPtrOrigem[2] = 0;
                    }
                    else
                    {
                        auxY1 = (int)Math.Floor(yOrigem);
                        auxY2 = (int)Math.Ceiling(yOrigem);
                        auxX1 = (int)Math.Floor(xOrigem);
                        auxX2 = (int)Math.Ceiling(xOrigem);
                       

                        factor = xOrigem - auxX1;

                        destX1[0] = (byte)((1 - factor) * (imgCopyPtr + auxY1 * widthStep + auxX1 * nChannels)[0] + factor * (imgCopyPtr + auxY1 * widthStep + auxX2 * nChannels)[0]);
                        destX1[1] = (byte)((1 - factor) * (imgCopyPtr + auxY1 * widthStep + auxX1 * nChannels)[1] + factor * (imgCopyPtr + auxY1 * widthStep + auxX2 * nChannels)[1]);
                        destX1[2] = (byte)((1 - factor) * (imgCopyPtr + auxY1 * widthStep + auxX1 * nChannels)[2] + factor * (imgCopyPtr + auxY1 * widthStep + auxX2 * nChannels)[2]);

                        destX2[0] = (byte)((1 - factor) * (imgCopyPtr + auxY2 * widthStep + auxX1 * nChannels)[0] + factor * (imgCopyPtr + auxY2 * widthStep + auxX2 * nChannels)[0]);
                        destX2[1] = (byte)((1 - factor) * (imgCopyPtr + auxY2 * widthStep + auxX1 * nChannels)[1] + factor * (imgCopyPtr + auxY2 * widthStep + auxX2 * nChannels)[1]);
                        destX2[2] = (byte)((1 - factor) * (imgCopyPtr + auxY2 * widthStep + auxX1 * nChannels)[2] + factor * (imgCopyPtr + auxY2 * widthStep + auxX2 * nChannels)[2]);

                        factor = yOrigem - auxY1;

                        imgPtrOrigem[0] = (byte)Math.Round((1 - factor) * destX1[0] + factor * destX2[0]);
                        imgPtrOrigem[1] = (byte)Math.Round((1 - factor) * destX1[1] + factor * destX2[1]);
                        imgPtrOrigem[2] = (byte)Math.Round((1 - factor) * destX1[2] + factor * destX2[2]);
                    }
                    imgPtrOrigem += nChannels;
                }
                imgPtrOrigem += padding;
            }
        }

        public static int imgComp(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;
                //MIplImage mOrig = imgCopy.MIplImage;
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();
                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrAux;
                byte* dataPtrAux2;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep;
                int i = 0;

                int auxnotE = 0;

                for (xDestino = 0; xDestino < width; xDestino++)
                {

                    for (yDestino = 0; yDestino < height; yDestino++)
                    {
                        dataPtrAux = dataPtrOrig + ((yDestino) * widthStep) + ((xDestino) * nChan);
                        dataPtrAux2 = dataPtrDest + ((yDestino) * widthStep) + ((xDestino) * nChan);

                        // tmp[yDestino, xDestino] = "K_w";
                        if (dataPtrAux[0] != dataPtrAux2[0] || dataPtrAux[1] != dataPtrAux2[1] || dataPtrAux[2] != dataPtrAux2[2])
                        {
                            auxnotE++;

                            //dataPtrDest[0] = 0;
                            //dataPtrDest[1] = 0;
                            //dataPtrDest[2] = 255;


                        }
                        dataPtrDest += widthStep;
                    }
                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrDest += nChan * i;
                    i++;

                }
                return auxnotE;
            }
        }

        public static int[] Histogram_x(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                //MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep;
                int i = 0;

                int[] vecBrancosX = new int[width];

                for (xDestino = 0; xDestino < width; xDestino++)
                {

                    for (yDestino = 0; yDestino < height; yDestino++)
                    {

                        // tmp[yDestino, xDestino] = "K_w";
                        if (dataPtrDest[0] == 255 && dataPtrDest[1] == 255 && dataPtrDest[2] == 255)
                        {
                            vecBrancosX[xDestino]++;

                        }
                        dataPtrDest += widthStep;
                    }
                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrDest += nChan * i;
                    i++;

                }
                return vecBrancosX;
            }
        }

        public static int[] HistogramXBlack(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                //MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep;
                int i = 0;

                int[] vecBrancosX = new int[width];

                for (xDestino = 0; xDestino < width; xDestino++)
                {

                    for (yDestino = 0; yDestino < height; yDestino++)
                    {

                        // tmp[yDestino, xDestino] = "K_w";
                        if (dataPtrDest[0] == 0 && dataPtrDest[1] == 0 && dataPtrDest[2] == 0)
                        {
                            vecBrancosX[xDestino]++;


                        }
                        dataPtrDest += widthStep;
                    }
                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrDest += nChan * i;
                    i++;

                }
                return vecBrancosX;
            }
        }

        public static int[] Histogram_y(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                //MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep;
                int i = 0;

                int[] vecBrancosY = new int[height];

                for (yDestino = 0; yDestino < height; yDestino++)
                {

                    for (xDestino = 0; xDestino < width; xDestino++)
                    {

                        // tmp[yDestino, xDestino] = "K_w";
                        if (dataPtrDest[0] == 255 && dataPtrDest[1] == 255 && dataPtrDest[2] == 255)
                        {
                            vecBrancosY[yDestino]++;

                            //dataPtrDest[0] = 0;
                            //dataPtrDest[1] = 0;
                            //dataPtrDest[2] = 255;


                        }
                        dataPtrDest += nChan;
                    }
                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrDest += widthStep * i;
                    i++;

                }
                return vecBrancosY;
            }
        }

        public static int[] HistogramYBlack(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                //MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep;
                int i = 0;

                int[] vecBrancosY = new int[height];

                for (yDestino = 0; yDestino < height; yDestino++)
                {

                    for (xDestino = 0; xDestino < width; xDestino++)
                    {

                        // tmp[yDestino, xDestino] = "K_w";
                        if (dataPtrDest[0] == 0 && dataPtrDest[1] == 0 && dataPtrDest[2] == 0)
                        {
                            vecBrancosY[yDestino]++;



                        }
                        dataPtrDest += nChan;
                    }
                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrDest += widthStep * i;
                    i++;

                }
                return vecBrancosY;
            }
        }

        public static int pixeisTotal(int[] hist)
        {
            int sum = 0;
            for (int i = 0; i < hist.Length; i++)
            {
                sum += hist[i];

            }
            return sum;
        }

        public static void Bin_HSV_Black(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int w = img.Width;
                int h = img.Height;
                int nC = m.nChannels;
                int widthstep = m.widthStep;
                int padding = widthstep - nC * w;
                int x, y;
                double hue, saturation, value;
                Color rgbColor;

                for (y = 0; y < h; y++)
                {
                    for (x = 0; x < w; x++)
                    {
                        // Convert color to HSV to select a range of intensities to be black
                        rgbColor = Color.FromArgb(dataPtr[2], dataPtr[1], dataPtr[0]);
                        ColorToHSV(rgbColor, out hue, out saturation, out value);

                        // All the pixels close to black stay black, the rest go to white
                        if (value <= 0.5)
                        {
                            dataPtr[0] = 0;
                            dataPtr[1] = 0;
                            dataPtr[2] = 0;
                        }
                        else
                        {
                            dataPtr[0] = 255;
                            dataPtr[1] = 255;
                            dataPtr[2] = 255;
                        }

                        dataPtr += nC;
                    }
                    dataPtr += padding;
                }
            }
        }

        public static void Bin_Hsv_Blue(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr_write = (byte*)m.imageData.ToPointer(); // obter apontador do inicio da imagem
                double hue, saturation, value;
                Color original;

                int w = img.Width; // largura da imagem
                int h = img.Height; // altura da imagem
                int nC = m.nChannels; // numero de canais - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhamento (padding)

                for (int i = 0; i < h; i++)
                {
                    for (int j = 0; j < w; j++)
                    {
                        original = Color.FromArgb(dataPtr_write[2], dataPtr_write[1], dataPtr_write[0]);
                        ColorToHSV(original, out hue, out saturation, out value);

                        if ((hue < 280 && hue > 190) && (saturation < 0.3 && saturation > 0.1) && (value < 0.68 && value > 0.33))
                        {
                            dataPtr_write[0] = 255;
                            dataPtr_write[1] = 255;
                            dataPtr_write[2] = 255;
                        }
                        else
                        {
                            dataPtr_write[0] = 0;
                            dataPtr_write[1] = 0;
                            dataPtr_write[2] = 0;
                        }
                        dataPtr_write += nC;

                    }
                    dataPtr_write += padding;

                }
            }
        }

        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static float tratarBaseDados(Bitmap img1, Bitmap img2)
        {

            unsafe
            {


                if (img1.Size != img2.Size)
                    return 0;

                float dif = 0;

                for (int i = 0; i < img1.Height; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {
                        dif += (float)Math.Abs((img1.GetPixel(i, j).R - img2.GetPixel(i, j).R) / 255);
                        dif += (float)Math.Abs((img1.GetPixel(i, j).B - img2.GetPixel(i, j).B) / 255);
                        dif += (float)Math.Abs((img1.GetPixel(i, j).G - img2.GetPixel(i, j).G) / 255);


                    }
                }
                return (100 * dif / (img1.Width * img1.Height * 3));
            }



        }   

        public static int[] pos_Cut(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;

                //MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image


                int[] vecCut = new int[4];
                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;
                int widthStep = m.widthStep;
                int i = 0;

                for (yDestino = 0; yDestino < height; yDestino++)
                {
                    for (xDestino = 0; xDestino < width; xDestino++)
                    {

                        if ((dataPtrDest[0] + dataPtrDest[1] + dataPtrDest[2]) == 0)
                        {

                            vecCut[0] = yDestino;

                            yDestino = height;
                            xDestino = width;

                        }
                        dataPtrDest += nChan;
                    }
                    dataPtrDest += padding;

                }

                //Encontrar o canto inferior direito do tabuleiro, comecando a varrer os pixeis pelo fim

                dataPtrDest = (byte*)m.imageData.ToPointer();
                i = 0;
                for (xDestino = 0; xDestino < width; xDestino++)
                {

                    for (yDestino = 0; yDestino < height; yDestino++)
                    {

                        // tmp[yDestino, xDestino] = "K_w";
                        if ((dataPtrDest[0] + dataPtrDest[1] + dataPtrDest[2]) == 0)
                        {

                            vecCut[1] = xDestino;

                            yDestino = height;
                            xDestino = width;


                        }
                        dataPtrDest += widthStep;
                    }
                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrDest += nChan * i;
                    i++;

                }

                dataPtrDest = (byte*)m.imageData.ToPointer();
                dataPtrDest += (width - 1) * nChan;
                i = 0;
                for (xDestino = width; xDestino > 0; xDestino--)
                {

                    for (yDestino = 0; yDestino < height; yDestino++)
                    {

                        // tmp[yDestino, xDestino] = "K_w";
                        if ((dataPtrDest[0] + dataPtrDest[1] + dataPtrDest[2]) == 0)
                        {
                            vecCut[2] = xDestino;




                            xDestino = 0;
                            yDestino = height;

                        }
                        dataPtrDest += widthStep;
                    }
                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrDest += (width - 1) * nChan;
                    dataPtrDest -= nChan * i;
                    i++;

                }

                dataPtrDest = (byte*)m.imageData.ToPointer();
                dataPtrDest += (width - 1) * nChan + (height - 1) * widthStep;
                i = 0;

                for (yDestino = height; yDestino > 0; yDestino--)
                {

                    for (xDestino = width; xDestino > 0; xDestino--)
                    {


                        if ((dataPtrDest[0] + dataPtrDest[1] + dataPtrDest[2]) == 0)
                        {

                            vecCut[3] = yDestino;

                            yDestino = 0;
                            xDestino = 0;
                        }
                        dataPtrDest -= nChan;
                    }
                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrDest += (width - 1) * nChan + (height - 1) * widthStep;
                    dataPtrDest -= widthStep * i;
                    i++;
                }


                return vecCut;
            }
        }


        public static void CopyImg(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image
                byte* dataPtrAux;

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)

                int xDestino, yDestino, i=0;
                int widthStep = m.widthStep; 
                


               // dataPtrDest += widthStep + nChan;
                if (nChan == 3) // image in RGB
                {
                    //Processar os pixeis no interior 
                    for (yDestino = 0; yDestino < height ; yDestino++)
                    {
                        for (xDestino = 0; xDestino < width ; xDestino++)
                        {
   
                            dataPtrAux = dataPtrOrig + ((yDestino) * widthStep) + ((xDestino) * nChan);

                            dataPtrDest[0] = (byte)dataPtrAux[0];
                            dataPtrDest[1] = (byte)dataPtrAux[1];
                            dataPtrDest[2] = (byte)dataPtrAux[2];


                            // advance the pointer to the next pixel
                            dataPtrDest += nChan;

                        }
                         dataPtrDest += padding ; //Por causa das margens

                        //dataPtrDest = (byte*)m.imageData.ToPointer();
                        //dataPtrDest += (height - 1) * widthStep;
                        //dataPtrDest += widthStep * i;
                        //i++;
                    }

            

                    
                }

            }


        }


        public static void Chess_Recognition(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, out Rectangle BD_Location, out string Angle, out string[,] Pieces)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage mOrig = imgCopy.MIplImage;

                byte* dataPtrDest = (byte*)m.imageData.ToPointer(); // Pointer to the begining of the destiny image
                byte* dataPtrOrig = (byte*)mOrig.imageData.ToPointer();// Pointer to the begining of the original image

                int width = img.Width;
                int height = img.Height; //numero de pixeis que a imagem ocupas
                int widthStep = m.widthStep;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes, acerto (padding)
                int xDestino, yDestino;

                int x11 = 0, y11 = 0, x22 = 0, y22 = 0, comprimento = 0, largura = 0, xDest11, yDest11, xDest22, yDest22;
                int i = 0, flag = 0;
                double angulo, anguloRodado;

                BD_Location = new Rectangle(520, 850, 500, 100);
                Angle = 48.ToString();
                string[,] tmp = new string[8, 8];

                Image<Bgr, Byte> imgOriginal = imgCopy;
                Image<Bgr, Byte>[] allImages = new Image<Bgr, Byte>[12];
                Image<Bgr, Byte>[] blackImage = new Image<Bgr, Byte>[6];
                Image<Bgr, Byte>[] allImage = new Image<Bgr, Byte>[12];
                Image<Bgr, Byte> tabuleiroRodado;


                String folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                String[] split = folder.Split('\\');
                folder = "";

                foreach (String s in split)
                {
                    folder += s + "\\";
                    if (s.Contains("SS_OpenCV"))
                        break;
                }


                folder += "SS_OpenCV\\ImagensTeste\\";


                allImages[0] = new Image<Bgr, byte>(folder + "bispoBranco.png");
                allImages[1] = new Image<Bgr, byte>(folder + "bispoPreto.png");

                allImages[2] = new Image<Bgr, byte>(folder + "cavaloBranco.png");
                allImages[3] = new Image<Bgr, byte>(folder + "cavaloPreto.png");

                allImages[4] = new Image<Bgr, byte>(folder + "peaoBranco.png");
                allImages[5] = new Image<Bgr, byte>(folder + "peaoPreto.png");

                allImages[6] = new Image<Bgr, byte>(folder + "rainhaBranco.png");
                allImages[7] = new Image<Bgr, byte>(folder + "rainhaPreto.png");

                allImages[8] = new Image<Bgr, byte>(folder + "reiBranco.png");
                allImages[9] = new Image<Bgr, byte>(folder + "reiPreto.png");

                allImages[10] = new Image<Bgr, byte>(folder + "torreBranco.png");
                allImages[11] = new Image<Bgr, byte>(folder + "torrePreto.png");


                Bin_Hsv_Blue(img);
                img.Save("BinHSVBlue.png");
                if (nChan == 3)
                {
                    /****************************************************************************************************************/
                    /****************************************************Encontrar o Tabuleiro e o Angulo******************************************/
                    /****************************************************************************************************************/


                    //Canto Superior Direito
                    dataPtrDest += (width - 1) * nChan;
                    for (yDestino = 0; yDestino < height; yDestino++)
                    {

                        for (xDestino = width; xDestino > 0; xDestino--)
                        {

                            // tmp[yDestino, xDestino] = "K_w";
                            if ((dataPtrDest[0] + dataPtrDest[1] + dataPtrDest[2]) != 0)
                            {
                                x11 = xDestino;
                                y11 = yDestino;


                                xDestino = 0;
                                yDestino = height;

                                dataPtrDest[0] = 0;
                                dataPtrDest[1] = 0;
                                dataPtrDest[2] = 255;

                            }
                            dataPtrDest -= nChan;
                        }
                        dataPtrDest = (byte*)m.imageData.ToPointer();
                        dataPtrDest += (width - 1) * nChan;
                        dataPtrDest += widthStep * i;
                        i++;
                    }

                    //Canto Inferior Esquerdo
                    dataPtrDest = (byte*)m.imageData.ToPointer();
                    dataPtrDest += (height - 1) * widthStep;
                    i = 0;
                    for (yDestino = height; yDestino > 0; yDestino--)
                    {

                        for (xDestino = 0; xDestino < width; xDestino++)
                        {

                            // tmp[yDestino, xDestino] = "K_w";
                            if ((dataPtrDest[0] + dataPtrDest[1] + dataPtrDest[2]) != 0)
                            {
                                x22 = xDestino;
                                y22 = yDestino;

                                xDestino = width;
                                yDestino = 0;
                                dataPtrDest[0] = 0;
                                dataPtrDest[1] = 0;
                                dataPtrDest[2] = 255;

                            }
                            dataPtrDest += nChan;
                        }
                        dataPtrDest = (byte*)m.imageData.ToPointer();
                        dataPtrDest += (height - 1) * widthStep;
                        dataPtrDest -= widthStep * i;
                        i++;

                    }

                    img = img.Copy();
                    img.Save("BinHSVBlue2.png");

                    largura = x11 - x22;
                    comprimento = y22 - y11;

                    angulo = (Math.Atan2((double)comprimento, (double)largura) * (180.0 / Math.PI));

                    if ((angulo <= 45.9) && (angulo >= 45.0))
                    {
                        //Mean(img, imgCopy);
                        //img = imgCopy.Copy();
                        CopyImg(img, imgCopy);
                        BD_Location = new Rectangle((int)x22, (int)y11, (int)largura, (int)comprimento);
                        tabuleiroRodado = img.Copy(BD_Location);
                    }

                    else
                    {
                        anguloRodado = (angulo) - 45.00; //if angulo positivo
                        anguloRodado = (Math.PI / 180.0) * anguloRodado;// em radianos

                        angulo = (int)angulo;
                        Angle = angulo.ToString();
                                               
                        Rotation_Bilinear(img, imgCopy, anguloRodado);

                        Image<Bgr, Byte> tabuleiroPrim = img.Copy(); 

                        int widthRod = tabuleiroPrim.Width;
                        int heightRod = tabuleiroPrim.Height;

                        double halfWidth = widthRod / 2.0;
                        double halfHeight = heightRod / 2.0;

                        xDest22 = x22;
                        yDest11 = y11;

                        //Rotacao inversa
                        xDest11 = (int)Math.Round(((x11 - halfWidth) * Math.Cos(-anguloRodado)) - (halfHeight - yDest11) * (Math.Sin(-anguloRodado)) + (halfWidth));
                        y11 = (int)Math.Round(halfHeight - (x11 - halfWidth) * (Math.Sin(-anguloRodado)) - ((halfHeight - yDest11) * Math.Cos(-anguloRodado)));

                        x22 = (int)Math.Round(((xDest22 - halfWidth) * Math.Cos(-anguloRodado)) - (halfHeight - y22) * (Math.Sin(-anguloRodado)) + (halfWidth));
                        yDest22 = (int)Math.Round(halfHeight - (xDest22 - halfWidth) * (Math.Sin(-anguloRodado)) - ((halfHeight - y22) * Math.Cos(-anguloRodado)));


                        largura = xDest11 - x22;
                        comprimento = yDest22 - y11;

                        BD_Location = new Rectangle((int)x22, (int)y11, (int)largura, (int)comprimento);
                        tabuleiroRodado = img.Copy(BD_Location);
                        Bin_HSV_Black(tabuleiroRodado);
                        tabuleiroRodado.Save("RodadoTabuuuuu.png");
                        flag = 1;

                    }

                    /****************************************************************************************************************
                    ****************************************************Encontrar as pecas******************************************
                    ****************************************************************************************************************/


                    /*
                    int wid = 0, heig = 0;
                    int yCima, yBaixo, xEsq, xDireita;
                    int largFinal, compFinal, x, y;
                    int[] histX;
                    int[] histY;
                    float total = 0;
                    */
                    int v = 0 ;
                    int w = 0;
                    int l =0;
                                        
                    
                    for (v = 0; v < 8; v++)
                    {
                        for (w = 0; w < 8; w++)
                        {

                            for (l = 0; l < allImages.Length; l++)
                            {
                                //Para qual dos casos da Rotaçao
                                if(flag == 1)
                                {
                                    img = tabuleiroRodado;
                                    BD_Location = new Rectangle(0 + v * (largura / 8), 0 + w * (comprimento / 8), largura / 8, comprimento / 8);
                                }

                                else
                                {
                                    img = imgCopy;
                                    BD_Location = new Rectangle(x22 + v * (largura / 8), y11 + w * (comprimento / 8), largura / 8, comprimento / 8);

                                }


                                img = img.Copy(BD_Location);
                                img.Save("RenatoOut.png");

                                int[] pretos = HistogramXBlack(img);

                                int totall = pixeisTotal(pretos);

                                if (totall > 10)
                                {
                                    Bitmap bitmap1 = img.ToBitmap();
                                    Bitmap resizedImage = new Bitmap(bitmap1, new Size(300, 300));
                                    //resizedImage.Save("Teste do Resized.png");
                                    img = new Image<Bgr, byte>(resizedImage);

                                    Bin_HSV_Black(img);

                                    img.Resize(allImages[l].Width, allImages[l].Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

                                    int[] posicoes = pos_Cut(img);
                                    //int[] posicoes = pos_cut2(img);

                                    BD_Location = new Rectangle(posicoes[1], posicoes[0], posicoes[2] - posicoes[1], posicoes[3] - posicoes[0]);
                                    

                                    Image<Bgr, byte> pecaTratada = img.Copy(BD_Location);

                                    bitmap1 = pecaTratada.ToBitmap();

                                    resizedImage = new Bitmap(bitmap1, new Size(300, 300));
                                    //resizedImage.Save("filename1.png");
                                    //   int []pretos= Histogramx_black(pecaTabRez);


                                    img = allImages[l];
                                    Bin_HSV_Black(img);
                                    BD_Location = new Rectangle(0, 0, img.Width, img.Height);
                                    img = img.Copy(BD_Location);
                                    Bitmap bitmap2 = img.ToBitmap();
                                    Bitmap resized2 = new Bitmap(bitmap2, new Size(300, 300));
                                    img = new Image<Bgr, byte>(resized2);
                                    Bin_HSV_Black(img);
                                    posicoes = pos_Cut(img);

                                    BD_Location = new Rectangle(posicoes[1], posicoes[0], posicoes[2] - posicoes[1], posicoes[3] - posicoes[0]);
                                    Image<Bgr, byte> pecaBaseD = img.Copy(BD_Location);
                                    // pecaBaseD.Save("filename2.png"); //peca da base de dados tratada

                                    bitmap1 = pecaBaseD.ToBitmap();
                                    resized2 = new Bitmap(bitmap1, new Size(300, 300));
                                    //resized2.Save("filename2.png");


                                    float pErro = tratarBaseDados(resizedImage, resized2);


                                    if (pErro < 15)
                                    {

                                        if (l == 0)
                                        {
                                            tmp[v, w] = "B_w";
                                            l = allImages.Length;
                                        }
                                        if (l == 1)
                                        {
                                            tmp[v, w] = "B_b";
                                            l = allImages.Length;
                                        }
                                        if (l == 2)
                                        {
                                            tmp[v, w] = "H_w";
                                            l = allImages.Length;
                                        }
                                        if (l == 3)
                                        {
                                            tmp[v, w] = "H_b";
                                            l = allImages.Length;
                                        }
                                        if (l == 4)
                                        {
                                            tmp[v, w] = "P_w";
                                            l = allImages.Length;
                                        }
                                        if (l == 5)
                                        {
                                            tmp[v, w] = "P_b";
                                            l = allImages.Length;
                                        }
                                        if (l == 6)
                                        {
                                            tmp[v, w] = "Q_w";
                                            l = allImages.Length;
                                        }
                                        if (l == 7)
                                        {
                                            tmp[v, w] = "Q_b";
                                            l = allImages.Length;
                                        }
                                        if (l == 8)
                                        {
                                            tmp[v, w] = "K_w";
                                            l = allImages.Length;
                                        }
                                        if (l == 9)
                                        {
                                            tmp[v, w] = "K_b";
                                            l = allImages.Length;
                                        }
                                        if (l == 10)
                                        {
                                            tmp[v, w] = "R_w";
                                            l = allImages.Length;
                                        }
                                        if (l == 11)
                                        {
                                            tmp[v, w] = "R_b";
                                            l = allImages.Length;
                                        }

                                    }
                                }
                                 
                                else
                                    tmp[v, w] = "E_p";


                            }
                        }
                    }
                    img = tabuleiroRodado;
                    BD_Location = new Rectangle(x22, y11, largura, comprimento);
                    img.Save("ImagemXadrez.png");



            }



                Pieces = tmp;
            }

        }





    }
}

