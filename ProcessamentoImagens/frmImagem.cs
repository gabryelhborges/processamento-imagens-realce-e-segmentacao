using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessamentoImagens
{
    public partial class frmImagem : Form
    {
        private Bitmap imageBitmap;
        public frmImagem(Bitmap imageBitmap)
        {
            InitializeComponent();

            this.imageBitmap = imageBitmap;
        }

        private void frmImagem_Load(object sender, EventArgs e)
        {
            pictureBox.Image = imageBitmap;
        }
    }
}
