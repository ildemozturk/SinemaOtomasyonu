using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    public partial class FilmEkle : Form
    {
        public FilmEkle()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
           
               
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Bir fotoğraf seçin";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Seçilen fotoğrafı PictureBox içine yükle
                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // Fotoğrafı PictureBox'a sığdır
                }
            

        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
