using System;
using System.Linq;
using System.Windows.Forms;
using Proje.Models;

namespace Proje
{
    public partial class SeansGuncelle : Form
    {
        private readonly SinemaOtomasyonEntities1 db = new SinemaOtomasyonEntities1();
        private readonly int seansId;

        public SeansGuncelle(int seansId, string filmAdi, int salonNo, DateTime tarih, TimeSpan saat)
        {
            InitializeComponent();
            this.seansId = seansId;

            // Form yüklendiğinde mevcut bilgileri göster
            this.Load += (s, e) =>
            {
                // Film adını göster
                labelFilmAdi.Text = filmAdi;

                // Salon numarasını göster
                labelSalonNo.Text = salonNo.ToString();

                // Tarihi göster
                dateTimePicker1.Value = tarih;

                // Saati göster
                comboBox1.Text = saat.ToString(@"hh\:mm");
            };
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                // Seansı bul
                var seans = db.Seans.Find(seansId);
                if (seans == null)
                {
                    MessageBox.Show("Seans bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Yeni tarih ve saat bilgilerini al
                DateTime yeniTarih = dateTimePicker1.Value;
                TimeSpan yeniSaat = TimeSpan.Parse(comboBox1.Text);

                // Seansı güncelle
                seans.seans_tarihi = yeniTarih;
                seans.seans_saati = yeniSaat;

                // Değişiklikleri kaydet
                db.SaveChanges();

                // Formu kapat
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seans güncellenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
} 