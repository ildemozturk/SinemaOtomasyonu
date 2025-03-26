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
    public partial class BiletAl : Form
    {
        // KOLTUK LİSTESİ BURADA TANIMLANIYOR
        List<Button> koltuklar = new List<Button>();

        public BiletAl()
        {
            InitializeComponent();
        }

        private void BiletAl_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn && btn.Tag == null) // Sadece koltuk butonlarını al
                {
                    btn.BackColor = Color.White;
                    btn.Tag = false; // Koltuk boş
                    btn.Click += KoltukTiklandi;
                    koltuklar.Add(btn);
                }
            }
        }

        private void KoltukTiklandi(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                bool doluMu = (bool)btn.Tag;
                if (!doluMu)
                {
                    btn.BackColor = Color.Red; // Koltuk dolu
                    btn.Tag = true;
                }
                else
                {
                    btn.BackColor = Color.White; // Koltuk boş
                    btn.Tag = false;
                }
            }
        }
    }
}
