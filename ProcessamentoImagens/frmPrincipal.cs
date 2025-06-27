using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ProcessamentoImagens
{
    public partial class frmPrincipal : Form
    {
        private Image image;
        private Bitmap imageBitmap;

        public frmPrincipal()
        {
            InitializeComponent();
            pictBoxImg1.SizeMode = PictureBoxSizeMode.Zoom;
            pictBoxImg2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnAbrirImagem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Arquivos de Imagem (*.jpg;*.png)|*.jpg;*.gif;*.bmp;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                pictBoxImg1.Image = image;
                imageBitmap = (Bitmap)image;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            pictBoxImg1.Image = null;
            pictBoxImg2.Image = null;
        }

        private void btnLuminanciaSemDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_gray(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;

            //frmImagem form = new frmImagem(imgDest);
        }

        private void btnLuminanciaComDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void btnNegativoSemDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.negativo(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void btnNegativoComDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.negativoDMA(imageBitmap, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        private void bttFatiamentoPlanoBits_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);
            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.fatiamento_plano_bits(novaimg);
            Console.WriteLine("Fatiamento plano de bits realizado com exito!");
        }

        private void bttHistograma_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);

            int[] histograma = Filtros.histograma(novaimg);
            int[] histEqualizado = Filtros.equalizacao_histograma(histograma, novaimg.Height, novaimg.Width);

            Filtros.exibirHistograma(histograma, false);
            Filtros.exibirHistograma(histEqualizado, true);
            Console.WriteLine("Gráfico dos histogramas salvos na pasta bin/debug do projeto!");

            Filtros.equaliza_imagem(novaimg, imgDest, histEqualizado);
            pictBoxImg2.Image = imgDest;
        }

        private void bttMediaVizinhanca5x5_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.media_vizinhanca_5x5(novaimg, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttMediana5x5_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.mediana_5x5(novaimg, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttMediaKvizinhos9_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.media_kVizinhos9(novaimg, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttBordasRoberts_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.detecta_borda_roberts(novaimg, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttBordasPrewitt_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.detecta_borda_prewitt(novaimg, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttBordasSobel_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.detecta_borda_sobel(novaimg, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttHistogramaDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);

            int[] histograma = Filtros.histogramaDMA(novaimg);
            int[] histEqualizado = Filtros.equalizacao_histogramaDMA(novaimg, histograma);

            Filtros.exibirHistograma(histograma, false);
            Filtros.exibirHistograma(histEqualizado, true);
            Console.WriteLine("Gráfico dos histogramas salvos na pasta bin/debug do projeto!");

            Filtros.equaliza_imagemDMA(novaimg, imgDest, histEqualizado);

            pictBoxImg2.Image = imgDest;
        }

        private void bttFatiamentoPlanoBitsDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);
            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.fatiamento_plano_bitsDMA(novaimg);
            Console.WriteLine("Fatiamento plano de bits DMA realizado com exito!");
        }

        private void bttMediaVizinhanca5x5DMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.media_vizinhanca_5x5DMA(novaimg, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttBordasPrewittDMA_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.detecta_borda_prewittDMA(novaimg, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttPassaAlta_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.passa_alta_centro4(novaimg, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttPassaAltaCentro8_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);
            Filtros.passa_alta_centro8(novaimg, imgDest);
            pictBoxImg2.Image = imgDest;
        }

        private void bttSubtracao_Click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            imageBitmap = (Bitmap)image;
            Filtros.convert_to_grayDMA(imageBitmap, imgDest);

            Bitmap novaimg = new Bitmap(imgDest);
            Bitmap aux = new Bitmap(imgDest);

            Filtros.media_vizinhanca_5x5DMA(novaimg, imgDest);

            Bitmap nova = new Bitmap(aux);
            Filtros.subtrai(nova, imgDest);
            pictBoxImg2.Image = imgDest;
        }
    }
}
