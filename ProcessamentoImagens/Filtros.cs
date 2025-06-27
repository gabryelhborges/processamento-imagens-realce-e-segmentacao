using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics.Tracing;

namespace ProcessamentoImagens
{
    class Filtros
    {
        //sem acesso direto a memoria
        public static void convert_to_gray(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int r, g, b;
            Int32 gs;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = imageBitmapSrc.GetPixel(x, y);

                    r = cor.R;
                    g = cor.G;
                    b = cor.B;
                    gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);
                    //gs = (Int32)(r+g+b)/3; //cores alteradas
                    if(gs <= 127)
                        gs = 0;
                    else
                        gs = 1;
                    //nova cor
                    Color newcolor = Color.FromArgb(gs, gs, gs);

                    imageBitmapDest.SetPixel(x, y, newcolor);
                }
            }
        }

        //sem acesso direito a memoria
        public static void negativo(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int r, g, b;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //obtendo a cor do pixel
                    Color cor = imageBitmapSrc.GetPixel(x, y);

                    r = cor.R;
                    g = cor.G;
                    b = cor.B;

                    //nova cor
                    Color newcolor = Color.FromArgb(255 - r, 255 - g, 255 - b);

                    imageBitmapDest.SetPixel(x, y, newcolor);
                }
            }
        }

        //com acesso direto a memória
        public static void convert_to_grayDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;
            Int32 gs;

            //lock dados bitmap origem
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //lock dados bitmap destino
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(src++); //está armazenado dessa forma: b g r 
                        g = *(src++);
                        r = *(src++);
                        gs = (Int32)(r * 0.2990 + g * 0.5870 + b * 0.1140);
                        *(dst++) = (byte)gs;
                        *(dst++) = (byte)gs;
                        *(dst++) = (byte)gs;
                    }
                    src += padding;//soma pra não mexer no padding, para o ponteiro ir para a proxima linha
                    dst += padding;
                }
            }
            //unlock imagem origem
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        //com acesso direito a memoria
        public static void negativoDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            //lock dados bitmap origem 
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //lock dados bitmap destino
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src1 = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b = *(src1++); //está armazenado dessa forma: b g r 
                        g = *(src1++);
                        r = *(src1++);

                        *(dst++) = (byte)(255 - b);
                        *(dst++) = (byte)(255 - g);
                        *(dst++) = (byte)(255 - r);
                    }
                    src1 += padding;
                    dst += padding;
                }
            }
            //unlock imagem origem 
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        public static void espelhoVerticalDMA(Bitmap imageBitmapSrc, Bitmap imageBitmapDest)
        {
            int width = imageBitmapSrc.Width;
            int height = imageBitmapSrc.Height;
            int pixelSize = 3;

            //lock dados bitmap origem 
            BitmapData bitmapDataSrc = imageBitmapSrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            //lock dados bitmap destino
            BitmapData bitmapDataDst = imageBitmapDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bitmapDataSrc.Stride - (width * pixelSize);

            unsafe
            {
                byte* src1 = (byte*)bitmapDataSrc.Scan0.ToPointer();
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();

                int r, g, b;
                for (int y = 0; y < height; y++)
                {
                    byte* aux = dst;
                    aux = aux + bitmapDataDst.Stride * (height - 1 - y);
                    for (int x = 0; x < width; x++)
                    {
                        b = *(src1++); //está armazenado dessa forma: b g r 
                        g = *(src1++);
                        r = *(src1++);

                        *(aux++) = (byte)b;
                        *(aux++) = (byte)g;
                        *(aux++) = (byte)r;
                    }
                    src1 += padding;
                }
            }
            //unlock imagem origem 
            imageBitmapSrc.UnlockBits(bitmapDataSrc);
            //unlock imagem destino
            imageBitmapDest.UnlockBits(bitmapDataDst);
        }

        // ---------------------------------------------------------------------------------------------------------------------------
        // ---------------------------------------------------------------------------------------------------------------------------
        // ----------------------------------------------------- Exercs Lista 3 ------------------------------------------------------
        // ---------------------------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------------------------- 

        // ----------------------------------------------------- Exercs Sem DMA ------------------------------------------------------
        public static void fatiamento_plano_bits(Bitmap bSrc)
        {
            int height = bSrc.Height;
            int width = bSrc.Width;

            Bitmap[] vetImgs = { new Bitmap(width, height), new Bitmap(width, height), new Bitmap(width, height), new Bitmap(width, height), new Bitmap(width, height), new Bitmap(width, height), new Bitmap(width, height), new Bitmap(width, height) };

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Color pixel = bSrc.GetPixel(x, y);
                    byte b = pixel.R;

                    for (int i = 0; i < 8; i++)
                    {
                        bool bit = (b & (1 << i)) != 0;
                        if (bit)
                            vetImgs[i].SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        else
                            vetImgs[i].SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                }
            }

            for(int i = 0; i < 8; i++)
            {
                vetImgs[i].Save($"fatiamento_bits_{i}.jpg");
            }
            //fica salvo na pasta bin/debug do projeto
        }

        public static int[] histograma(Bitmap bSrc)
        {
            //ja enviar imagem com tons de cinza(bSrc)
            int[] histograma = new int[256];//ja vem inicializado com zero
            int height = bSrc.Height;
            int width = bSrc.Width;
            
            for(int y = 0; y < height; y++)
            {
                for(int x= 0; x < width; x++)
                {
                    Color pixel = bSrc.GetPixel(x, y);
                    int pos = pixel.R;
                    histograma[pos]++;
                }
            }

            for(int i = 0; i < 256; i++)
            {
                Console.WriteLine("Nível de cinza: " + i + " Qtd Pixel: " + histograma[i]);
            }

            return histograma;
        }
        
        public static int[] equalizacao_histograma(int[] histograma, int height, int width)
        {
            //calculando CDF
            int g = 0;
            int n = height * width;//total pixel
            float[] cdf = new float[256];
            cdf[0] = (float)histograma[0];
            for(int i = 1; i < 256; i++)
            {
                cdf[i] = cdf[i - 1] + (float)histograma[i];//calculo cdf
                if (histograma[i] != 0)//calculo g
                {
                    g++;
                }
            }
            int numIdeal = (height * width) / g;//calculo I

            int[] q = new int[256];
            for(int i= 0; i < 256; i++)
            {
                int max = 0;
                if (histograma[i] != 0)
                {
                    int res = (int)Math.Round((cdf[i])/numIdeal) - 1;
                    if (res > max)
                        max = res;
                }
                q[i] = max;
            }

            for(int i = 0; i < 256; i++)
            {
                Console.WriteLine("Q -> Nível de cinza: " + i + " Resultado: " + q[i]);
            }
            return q;
        }
        
        /*
        public static int[] equalizacao_histograma(int[] histograma, int height, int width)
        {
            int n = height * width; // Total de pixels
            float[] cdf = new float[256];
            int[] q = new int[256];

            // Calcular a CDF
            cdf[0] = histograma[0];
            for (int i = 1; i < 256; i++)
            {
                cdf[i] = cdf[i - 1] + histograma[i];
            }

            // Normalizar a CDF para o intervalo [0, 255]
            for (int i = 0; i < 256; i++)
            {
                q[i] = (int)Math.Round((cdf[i] - cdf[0]) / (n - cdf[0]) * 255);
                q[i] = Math.Max(0, Math.Min(255, q[i])); // Restringir ao intervalo [0, 255]
            }

            // Exibir o mapeamento
            for (int i = 0; i < 256; i++)
            {
                Console.WriteLine("Q -> Nível de cinza: " + i + " Resultado: " + q[i]);
            }
            return q;
        }
        */

        public static void equaliza_imagem(Bitmap bSrc, Bitmap bDst, int[] histEqualizado)
        {
            int height = bSrc.Height;
            int width = bSrc.Width;

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Color pixel = bSrc.GetPixel(x, y);
                    int corOriginal = pixel.R;
                    int novaCor = histEqualizado[corOriginal];
                    bDst.SetPixel(x, y, Color.FromArgb(novaCor, novaCor, novaCor));
                }
            }
        }

        public static void exibirHistograma(int[] histograma, bool ehEqualizado)
        {
            int width = 256;
            int height = 200;
            Bitmap bitmap = new Bitmap(width, height);
            int max = 0;

            // Encontrar o valor máximo no histograma para normalização
            for (int i = 0; i < histograma.Length; i++)
            {
                if (histograma[i] > max)
                {
                    max = histograma[i];
                }
            }

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);

                for (int i = 0; i < histograma.Length; i++)
                {
                    int barHeight = (int)((histograma[i] / (float)max) * height);
                    g.DrawLine(Pens.Black, i, height, i, height - barHeight);
                }
            }
            if (ehEqualizado)
                bitmap.Save("HistogramaEqualizado.jpg");
            else
                bitmap.Save("HistogramaOriginal.jpg");
        }

        public static void media_vizinhanca_5x5(Bitmap bSrc, Bitmap bDst)
        {
            int height = bSrc.Height;
            int width = bSrc.Width;

            int[] vetX = { -2, -1,  0,  1,  2, -2, -1,  0,  1,  2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2 };
            int[] vetY = { -2, -2, -2, -2, -2, -1, -1, -1, -1, -1,  0,  0, 0, 0, 0,  1,  1, 1, 1, 1,  2,  2, 2, 2, 2 };

            for (int y = 2; y < height-2; y++)
            {
                for (int x = 2; x < width - 2; x++)
                {
                    int soma = 0;
                    for (int i = 0; i < vetX.Length; i++)
                    {
                        int novoX = x + vetX[i];
                        int novoY = y + vetY[i];
                        soma += bSrc.GetPixel(novoX, novoY).R;
                    }
                    int media = (int)Math.Round((float)soma / vetX.Length);
                    bDst.SetPixel(x, y, Color.FromArgb(media, media, media));
                }
            }
        }

        public static void mediana_5x5(Bitmap bSrc, Bitmap bDst)
        {
            int height = bSrc.Height;
            int width = bSrc.Width;

            int[] vetX = { -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2 };
            int[] vetY = { -2, -2, -2, -2, -2, -1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };

            for (int y = 2; y < height - 2; y++)
            {
                for (int x = 2; x < width - 2; x++)
                {
                    List<int> vetOrdenado = new List<int>();
                    for (int i = 0; i < vetX.Length; i++)
                    {
                        int novoX = x + vetX[i];
                        int novoY = y + vetY[i];
                        insere_ordenado_vet(vetOrdenado, bSrc.GetPixel(novoX, novoY).R);
                    }
                    int elemMediana = vetOrdenado[vetOrdenado.Count / 2];
                    bDst.SetPixel(x, y, Color.FromArgb(elemMediana, elemMediana, elemMediana));
                }
            }
        }

        public static void insere_ordenado_vet(List<int> vet, int valor)
        {
            int i = 0;
            while (i < vet.Count && valor > vet[i])
            {
                i++;
            }
            vet.Insert(i, valor);
        }

        public static void media_kVizinhos9(Bitmap bSrc, Bitmap bDst)
        {
            int height = bSrc.Height;
            int width = bSrc.Width;
            int k = 9;// num de vizinhos

            int[] vetX = { -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2 };
            int[] vetY = { -2, -2, -2, -2, -2, -1, -1, -1, -1, -1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };
            //vets sem pos 0,0

            for (int y = 2; y < height - 2; y++)
            {
                for (int x = 2; x < width - 2; x++)
                {
                    Color cor = bSrc.GetPixel(x, y);

                    List<int> vetOrdenado = new List<int>();
                    for(int i = 0; i < vetX.Length; i++)
                    {
                        int novoX = x + vetX[i];
                        int novoY = y + vetY[i];
                        insere_ordenado_vet(vetOrdenado, bSrc.GetPixel(novoX, novoY).R);
                    }

                    int soma = 0;
                    for(int i = 0; i < k; i++)
                    {
                        int posMenor = 0;
                        int menorDistancia = Math.Abs(vetOrdenado[0] - cor.R);
                        for(int j = 1; j < vetOrdenado.Count; j++)
                        {
                            int distancia = Math.Abs(vetOrdenado[i] - cor.R);
                            if(distancia < menorDistancia)
                            {
                                menorDistancia = distancia;
                                posMenor = j;
                            }
                        }
                        soma += vetOrdenado[posMenor];
                        vetOrdenado.RemoveAt(posMenor);
                    }
                    int media = soma / k;
                    bDst.SetPixel(x, y, Color.FromArgb(media, media, media));
                }
            }
        }

        // ----------------------------------------------------- Exercs Com DMA ------------------------------------------------------
        public static void fatiamento_plano_bitsDMA(Bitmap bsrc)
        {
            int height = bsrc.Height;
            int width = bsrc.Width;
            int pixelsize = 3;
            Bitmap[] vetImgs = new Bitmap[8];

            for (int i = 0; i < 8; i++)
            {
                vetImgs[i] = new Bitmap(width, height);
            }

            BitmapData bdatasrc = bsrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            BitmapData[] vetBmpData = new BitmapData[8];
            for(int i = 0; i < 8; i++)
            {
                vetBmpData[i] = vetImgs[i].LockBits(new Rectangle(0, 0, width, height),
                    ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            }

            unsafe
            {
                byte* src = (byte*)bdatasrc.Scan0.ToPointer();
                byte*[] ponteiros = new byte*[8];
                for(int i = 0; i < 8; i++)
                {
                    ponteiros[i] = (byte*)vetBmpData[i].Scan0.ToPointer();
                }

                for(int y = 0; y < height; y++)
                {
                    for(int x = 0; x < width; x++)
                    {
                        int pos = x * pixelsize + y * bdatasrc.Stride;
                        byte cor = src[pos];
                        for(int i = 0; i < 8; i++)
                        {
                            bool bit = (cor & (1 << i)) != 0;
                            byte* aux = ponteiros[i];
                            if (bit)
                                aux[pos] = aux[pos + 1] = aux[pos + 2] = 255;
                            else
                                aux[pos] = aux[pos + 1] = aux[pos + 2] = 0;
                        }
                    }
                }
            }
            bsrc.UnlockBits(bdatasrc);
            for(int i = 0; i < 8; i++)
            {
                vetImgs[i].UnlockBits(vetBmpData[i]);
            }

            for(int i = 0; i < 8; i++)
            {
                vetImgs[i].Save("Fatiamento_Plano_Bits_DMA_" + i + ".jpg");
            }
        }

        public static int[] histogramaDMA(Bitmap bsrc)
        {
            int height = bsrc.Height;
            int width = bsrc.Width;
            int pixelsize = 3;

            BitmapData bdatasrc = bsrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bdatasrc.Stride - width * pixelsize;

            int[] hist = new int[256];
            unsafe
            {
                byte* src = (byte*)bdatasrc.Scan0.ToPointer();
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int cor = *src;
                        hist[cor]++;
                        src += 3;
                    }
                    src += padding;
                }
            }
            bsrc.UnlockBits(bdatasrc);
            return hist;
        }

        public static int[] equalizacao_histogramaDMA(Bitmap bsrc, int[] hist)
        {
            int[] cdf = new int[256];
            int g = 0;//qtde niveis cinza
            int i = 0;//num ideal de pixels
            cdf[0] = hist[0];
            for (int j = 1; j < 256; j++)
            {
                cdf[j] = cdf[j - 1] + hist[j];
                if (hist[j] != 0)
                    g++;
            }
            i = (bsrc.Height * bsrc.Width) / g;// I = (n.x)/g
            int[] q = new int[256];//hist equalizado
            for (int j = 0; j < 256; j++)
            {
                if (hist[j] != 0)
                {
                    q[j] = Math.Max((int)Math.Round((float)cdf[j] / i - 1), 0);
                }
            }
            return q;
        }

        public static void equaliza_imagemDMA(Bitmap bsrc, Bitmap bdst, int[] histeq)
        {
            int width = bsrc.Width;
            int height = bsrc.Height;
            int pixelsize = 3;

            BitmapData bdatasrc = bsrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bdatadst = bdst.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int padding = bdatasrc.Stride - width * pixelsize;

            unsafe
            {
                byte* src = (byte*)bdatasrc.Scan0.ToPointer();
                byte* dst = (byte*)bdatadst.Scan0.ToPointer();

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int cor = *src;
                        *(dst++) = (byte)histeq[cor];
                        *(dst++) = (byte)histeq[cor];
                        *(dst++) = (byte)histeq[cor];
                        src += 3;
                    }
                    src += padding;
                    dst += padding;
                }
            }
            bsrc.UnlockBits(bdatasrc);
            bdst.UnlockBits(bdatadst);
        }

        public static void media_vizinhanca_5x5DMA(Bitmap bsrc, Bitmap bdst)
        {
            int width = bsrc.Width;
            int height = bsrc.Height;
            int pixelsize = 3;

            BitmapData bdatasrc = bsrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bdatadst = bdst.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int[] vetX = { -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2, -2, -1, 0, 1, 2 };
            int[] vetY = { -2, -2, -2, -2, -2, -1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 };

            unsafe
            {
                byte* src = (byte*)bdatasrc.Scan0.ToPointer();
                byte* dst = (byte*)bdatadst.Scan0.ToPointer();
                int pos;
                for (int y = 2; y < height-2; y++)
                {
                    for (int x = 2; x < width-2; x++)
                    {
                        int soma = 0;
                        for(int i = 0; i < vetX.Length; i++)
                        {
                            int novoX = x + vetX[i];
                            int novoY = y + vetY[i];
                            pos = novoX * pixelsize + novoY * bdatasrc.Stride;
                            int cor = src[pos];
                            soma += cor;
                        }
                        int media = (int)Math.Round((float)soma / vetX.Length);
                        pos = x * pixelsize + y * bdatasrc.Stride;
                        dst[pos] = dst[pos + 1] = dst[pos + 2] = (byte)media;
                    }
                }
            }
            bsrc.UnlockBits(bdatasrc);
            bdst.UnlockBits(bdatadst);
        }

        // ---------------------------------------------------------------------------------------------------------------------------
        // ---------------------------------------------------------------------------------------------------------------------------
        // ----------------------------------------------------- Exercs Lista 4 ------------------------------------------------------
        // ---------------------------------------------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------------------------------------------- 

        // ----------------------------------------------------- Exercs Sem DMA ------------------------------------------------------
        public static void detecta_borda_roberts(Bitmap bSrc, Bitmap bDst)
        {
            //pixel 'central' de roberts é o z4
            //  z1   z2
            //  z3   z4
            int height = bSrc.Height;
            int width = bSrc.Width;

            int[] h1 = { 1, 0, 0, -1 };
            int[] h2 = { 0, 1, -1, 0 };

            for(int y = 1; y < height; y++)
            {
                for(int x = 1; x < width; x++)
                {
                    int z1 = bSrc.GetPixel(x - 1, y - 1).R;
                    int z2 = bSrc.GetPixel(x, y - 1).R;
                    int z3 = bSrc.GetPixel(x - 1, y).R;
                    int z4 = bSrc.GetPixel(x, y).R;

                    int res1 = z1 * h1[0] + z2 * h1[1] + z3 * h1[2] + z4 * h1[3];
                    int res2 = z1 * h2[0] + z2 * h2[1] + z3 * h2[2] + z4 * h2[3];

                    int res = (int)Math.Sqrt(Math.Pow(res1, 2) + Math.Pow(res2,2));
                    res = Math.Min(255, Math.Max(0, res));
                    bDst.SetPixel(x, y, Color.FromArgb(res, res, res));
                }
            }
        }

        public static void detecta_borda_prewitt(Bitmap bSrc, Bitmap bDst)
        {
            int height = bSrc.Height;
            int width = bSrc.Width;

            //int[] h1 = { -1, -1, -1, 0, 0, 0, 1, 1, 1 };
            //int[] h2 = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };

            for(int y = 1; y < height-1; y++)
            {
                for(int x = 1; x < width-1; x++)
                {
                    int z1 = bSrc.GetPixel(x - 1, y - 1).R;
                    int z2 = bSrc.GetPixel(x, y - 1).R;
                    int z3 = bSrc.GetPixel(x + 1, y - 1).R;
                    int z4 = bSrc.GetPixel(x - 1, y).R;
                    //z5 nao eh utilizado
                    int z6 = bSrc.GetPixel(x + 1, y).R;
                    int z7 = bSrc.GetPixel(x - 1, y + 1).R;
                    int z8 = bSrc.GetPixel(x, y + 1).R;
                    int z9 = bSrc.GetPixel(x + 1, y + 1).R;

                    int res_h1 = (z7 + z8 + z9) - (z1 + z2 + z3);//linha de baixo - linha de cima
                    int res_h2 = (z3 + z6 + z9) - (z1 + z4 + z7);//coluna dir - coluna esq

                    int res = (int)Math.Sqrt(Math.Pow(res_h1, 2) + Math.Pow(res_h2, 2));
                    res = Math.Min(255, Math.Max(0, res));
                    bDst.SetPixel(x, y, Color.FromArgb(res, res, res));
                }
            }
        }

        public static void detecta_borda_sobel(Bitmap bSrc, Bitmap bDst)
        {
            int height = bSrc.Height;
            int width = bSrc.Width;

            for(int y = 1; y < height - 1; y++)
            {
                for(int x = 1; x < width - 1; x++)
                {
                    int z1 = bSrc.GetPixel(x - 1, y - 1).R;
                    int z2 = bSrc.GetPixel(x, y - 1).R;
                    int z3 = bSrc.GetPixel(x + 1, y - 1).R;
                    int z4 = bSrc.GetPixel(x - 1, y).R;
                    //z5 nao eh utilizado
                    int z6 = bSrc.GetPixel(x + 1, y).R;
                    int z7 = bSrc.GetPixel(x - 1, y + 1).R;
                    int z8 = bSrc.GetPixel(x, y + 1).R;
                    int z9 = bSrc.GetPixel(x + 1, y + 1).R;

                    int res_h1 = (z7 + 2 * z8 + z9) - (z1 + 2 * z2 + z3);
                    int res_h2 = (z3 + 2 * z6 + z9) - (z1 + 2 * z4 + z7);

                    int res = (int)Math.Sqrt(Math.Pow(res_h1, 2) + Math.Pow(res_h2, 2));
                    res = Math.Min(255, Math.Max(0, res));
                    bDst.SetPixel(x, y, Color.FromArgb(res, res, res));
                }
            }
        }

        // ----------------------------------------------------- Exercs Com DMA ------------------------------------------------------
        public static void detecta_borda_prewittDMA(Bitmap bsrc, Bitmap bdst)
        {
            int width = bsrc.Width;
            int height = bsrc.Height;
            int pixelsize = 3;

            BitmapData bdatasrc = bsrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bdatadst = bdst.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bdatasrc.Stride;

            // x1 x2 x3
            // x4 x5 x6
            // x7 x8 x9
            unsafe
            {
                byte* src = (byte*)bdatasrc.Scan0.ToPointer();
                byte* dst = (byte*)bdatadst.Scan0.ToPointer();
                int pos;
                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        int x1, x2, x3, x4, x6, x7, x8, x9;
                        x1 = src[(x - 1) * pixelsize + (y - 1) * stride];
                        x2 = src[x * pixelsize + (y - 1) * stride];
                        x3 = src[(x + 1) * pixelsize + (y - 1) * stride];

                        x4 = src[(x - 1) * pixelsize + y * stride];
                        x6 = src[(x + 1) * pixelsize + y * stride];

                        x7 = src[(x - 1) * pixelsize + (y + 1) * stride];
                        x8 = src[x * pixelsize + (y + 1) * stride];
                        x9 = src[(x + 1) * pixelsize + (y + 1) * stride];

                        int h1 = (x3 + x6 + x9) - (x1 + x4 + x7);
                        int h2 = (x7 + x8 + x9) - (x1 + x2 + x3);

                        int res = (int)Math.Sqrt(Math.Pow(h1, 2) + Math.Pow(h2, 2));
                        res = Math.Min(255, Math.Max(0, res));
                        pos = x * pixelsize + y * stride;
                        dst[pos] = dst[pos + 1] = dst[pos + 2] = (byte)res;
                    }
                }
            }
            bsrc.UnlockBits(bdatasrc);
            bdst.UnlockBits(bdatadst);
        }

        public static void detecta_borda_sobelDMA(Bitmap bsrc, Bitmap bdst)
        {
            int width = bsrc.Width;
            int height = bsrc.Height;
            int pixelsize = 3;

            BitmapData bdatasrc = bsrc.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bdatadst = bdst.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = bdatasrc.Stride;

            // x1 x2 x3
            // x4 x5 x6
            // x7 x8 x9
            unsafe
            {
                byte* src = (byte*)bdatasrc.Scan0.ToPointer();
                byte* dst = (byte*)bdatadst.Scan0.ToPointer();
                int pos;
                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        int x1, x2, x3, x4, x6, x7, x8, x9;
                        x1 = src[(x - 1) * pixelsize + (y - 1) * stride];
                        x2 = src[x * pixelsize + (y - 1) * stride];
                        x3 = src[(x + 1) * pixelsize + (y - 1) * stride];

                        x4 = src[(x - 1) * pixelsize + y * stride];
                        x6 = src[(x + 1) * pixelsize + y * stride];

                        x7 = src[(x - 1) * pixelsize + (y + 1) * stride];
                        x8 = src[x * pixelsize + (y + 1) * stride];
                        x9 = src[(x + 1) * pixelsize + (y + 1) * stride];

                        int h1 = (x3 + 2 * x6 + x9) - (x1 + 2 * x4 + x7);
                        int h2 = (x7 + 2 * x8 + x9) - (x1 + 2 * x2 + x3);

                        int res = (int)Math.Sqrt(Math.Pow(h1, 2) + Math.Pow(h2, 2));
                        res = Math.Min(255, Math.Max(0, res));
                        pos = x * pixelsize + y * stride;
                        dst[pos] = dst[pos + 1] = dst[pos + 2] = (byte)res;
                    }
                }
            }
            bsrc.UnlockBits(bdatasrc);
            bdst.UnlockBits(bdatadst);
        }


        // ----------------------------------------------------- Exercs Extras -------------------------------------------------------
        public static void passa_alta_centro4(Bitmap bsrc, Bitmap bdst)
        {
            int height = bsrc.Height;
            int width = bsrc.Width;

            for(int y = 1; y < height-1; y++)
            {
                for(int x = 1; x < width-1; x++)
                {
                    int p1 = bsrc.GetPixel(x, y).R;//central
                    int p2 = bsrc.GetPixel(x + 1, y).R;//direita
                    int p3 = bsrc.GetPixel(x, y + 1).R;//abaixo
                    int p4 = bsrc.GetPixel(x, y - 1).R;//acima
                    int p5 = bsrc.GetPixel(x - 1, y).R;

                    int novop = 4 * p1 - p2 - p3 - p4 - p5;
                    novop = Math.Min(255, Math.Max(0, novop));
                    bdst.SetPixel(x, y, Color.FromArgb(novop, novop, novop));
                }
            }
        }

        public static void passa_alta_centro8(Bitmap bsrc, Bitmap bdst)
        {
            int height = bsrc.Height;
            int width = bsrc.Width;

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int p1 = bsrc.GetPixel(x, y).R; // central
                    int p2 = bsrc.GetPixel(x + 1, y).R; // direita
                    int p3 = bsrc.GetPixel(x, y + 1).R; // abaixo
                    int p4 = bsrc.GetPixel(x, y - 1).R; // acima
                    int p5 = bsrc.GetPixel(x - 1, y).R; // esquerda
                    int p6 = bsrc.GetPixel(x + 1, y + 1).R; // diagonal direita abaixo
                    int p7 = bsrc.GetPixel(x - 1, y - 1).R; // diagonal esquerda acima
                    int p8 = bsrc.GetPixel(x + 1, y - 1).R; // diagonal direita acima
                    int p9 = bsrc.GetPixel(x - 1, y + 1).R; // diagonal esquerda abaixo

                    int novop = 8 * p1 - p2 - p3 - p4 - p5 - p6 - p7 - p8 - p9;
                    novop = Math.Min(255, Math.Max(0, novop));
                    bdst.SetPixel(x, y, Color.FromArgb(novop, novop, novop));
                }
            }
        }

        public static void subtrai(Bitmap bsrc, Bitmap bdst)
        {
            int height = bsrc.Height;
            int width = bsrc.Width;

            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    int origem = bsrc.GetPixel(x, y).R;
                    int dest = bdst.GetPixel(x, y).R;

                    int subtracao = origem - dest;
                    subtracao = Math.Max(0, subtracao);
                    bdst.SetPixel(x, y, Color.FromArgb(subtracao, subtracao, subtracao));
                }
            }
        }
    }
}

/*
 * pixel é armazenado como bgr 
 * pixel é 3 bytes
*/