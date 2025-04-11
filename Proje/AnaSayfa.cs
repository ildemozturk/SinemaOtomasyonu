using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proje.Models;
using System.Data.Entity;

namespace Proje
{
    public partial class AnaSayfa : Form
    {
        private readonly SinemaOtomasyonEntities1 db = new SinemaOtomasyonEntities1();

        public AnaSayfa()
        {
            InitializeComponent();
        }

        private string ShowInputDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            TextBox textBox = new TextBox() { Left = 50, Top = 20, Width = 200 };
            Button confirmation = new Button() { Text = "Tamam", Left = 50, Width = 70, Top = 50, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "İptal", Left = 180, Width = 70, Top = 50, DialogResult = DialogResult.Cancel };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Programı kapatmak istediğinizden emin misiniz?", "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnFilmEkle_Click(object sender, EventArgs e)
        {
            FilmEkle filmEkle = new FilmEkle();
            filmEkle.Show();
            this.Hide();
        }

        private void btnSeansEkle_Click(object sender, EventArgs e)
        {
            SeansEkle seansEkle = new SeansEkle();
            seansEkle.Show();
            this.Hide();
        }

        private void btnSeans_Click(object sender, EventArgs e)
        {
            try
            {
                SeansListele seansListele = new SeansListele();
                seansListele.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seanslar sayfası açılırken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AnaSayfa_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            FilmEkle filmEkle = new FilmEkle();
            filmEkle.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            BiletAl biletAl = new BiletAl();
            biletAl.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            SeansEkle seansEkle = new SeansEkle();
            seansEkle.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            while (true)
            {
                string input = ShowInputDialog("Lütfen bilet ID'sini girin:", "Bilet Bilgileri");
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                if (int.TryParse(input, out int biletId))
                {
                    using (var db = new SinemaOtomasyonEntities1())
                    {
                        var bilet = db.Bilet
                            .Include("Seans")
                            .Include("Seans.Film")
                            .FirstOrDefault(b => b.id == biletId);

                        if (bilet != null)
                        {
                            this.Hide();
                            BiletBilgileri biletBilgileriForm = new BiletBilgileri(biletId);
                            biletBilgileriForm.Show();
                            break;
                        }
                        else
                        {
                            var result = MessageBox.Show("Bu ID'ye sahip bir bilet bulunamadı. Geçerli bir ID girmek ister misiniz?", "Hata", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (result == DialogResult.No)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    var result = MessageBox.Show("Geçersiz ID formatı. Geçerli bir ID girmek ister misiniz?", "Hata", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (result == DialogResult.No)
                    {
                        break;
                    }
                }
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
