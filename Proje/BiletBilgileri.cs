using System;
using System.Windows.Forms;
using System.Drawing;
using Proje.Models;
using System.Data.Entity;
using System.Linq;
using System.IO;

namespace Proje
{
    public partial class BiletBilgileri : Form
    {
        private readonly SinemaOtomasyonEntities1 db = new SinemaOtomasyonEntities1();
        private int biletId;

        public BiletBilgileri(int biletId)
        {
            InitializeComponent();
            this.biletId = biletId;
        }

        private void BiletBilgileri_Load(object sender, EventArgs e)
        {
            try
            {
                var bilet = db.Bilet
                    .Include("Seans")
                    .Include("Seans.Film")
                    .FirstOrDefault(b => b.id == biletId);

                if (bilet != null)
                {
                    label8.Text = $"{bilet.musteri_adi} {bilet.musteri_soyadi}";
                    label9.Text = $"{bilet.Seans.seans_tarihi.ToShortDateString()} {bilet.Seans.seans_saati}";
                    label10.Text = bilet.Seans.salon_id.ToString();
                    label11.Text = bilet.bilet_tipi;
                    label13.Text = bilet.Seans.Film.film_adi;
                    label12.Text = bilet.koltuk_numarasi;

                    // Film görselini yükle
                    if (!string.IsNullOrEmpty(bilet.Seans.Film.film_gorsel))
                    {
                        string filmGorselYolu = Path.Combine(Application.StartupPath, "FilmFotograflari", bilet.Seans.Film.film_gorsel);
                        if (File.Exists(filmGorselYolu))
                        {
                            picture.Image = Image.FromFile(filmGorselYolu);
                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bilet bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilet bilgileri getirilirken bir hata oluştu: " + ex.Message, "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AnaSayfa anaSayfa = new AnaSayfa();
            anaSayfa.Show();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                AnaSayfa anaSayfa = new AnaSayfa();
                anaSayfa.Show();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            // Kullanıcıya çıkış onayı sor
            var result = MessageBox.Show("Programı kapatmak istediğinizden emin misiniz?", "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Eğer kullanıcı "Evet" derse, uygulamayı kapat
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Programı kapatır
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Ana sayfaya dön
                AnaSayfa anaSayfa = new AnaSayfa();
                anaSayfa.Show();
                this.Hide(); // Mevcut formu gizle
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ana sayfaya dönüşte bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picture_Click(object sender, EventArgs e)
        {

        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (int.TryParse(textBox1.Text, out int biletId))
        //        {
        //            var bilet = db.Bilet
        //                .Include("Seans")
        //                .Include("Seans.Film")
        //                .FirstOrDefault(b => b.id == biletId);
        //            if (bilet != null)
        //            {
        //                // Bilet bilgilerini göster
        //                label8.Text = $"{bilet.musteri_adi} {bilet.musteri_soyadi}";
        //                label9.Text = $"{bilet.Seans.seans_tarihi.ToShortDateString()} {bilet.Seans.seans_saati}";
        //                label10.Text = bilet.Seans.salon_id.ToString();
        //                label11.Text = bilet.bilet_tipi;
        //                label13.Text = bilet.Seans.Film.film_adi;
        //                label12.Text = bilet.koltuk_numarasi;
        //                label16.Text = bilet.id.ToString();
        //            }
        //            else
        //            {
        //                MessageBox.Show("Bu ID'ye sahip bir bilet bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                this.Hide();
        //                AnaSayfa anaSayfa = new AnaSayfa();
        //                anaSayfa.Show();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        this.Hide();
        //        AnaSayfa anaSayfa = new AnaSayfa();
        //        anaSayfa.Show();
        //    }
        //}
    }
}


