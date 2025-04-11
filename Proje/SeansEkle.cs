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
    public partial class SeansEkle : Form
    {
        private readonly SinemaOtomasyonEntities1 db = new SinemaOtomasyonEntities1();

        public SeansEkle()
        {
            InitializeComponent();
        }

        private void SeansEkle_Load(object sender, EventArgs e)
        {
            LoadFilmler();
            SetupDateTimePicker();
            LoadSalonlar();
            SetupSeansComboBox();
        }

        private void SetupSeansComboBox()
        {
            // Seans saatlerini ComboBox'a ekle
            guna2ComboBox4.Items.Clear();
            guna2ComboBox4.Items.AddRange(new string[] {
                "09:00",
                "12:45",
                "16:00",
                "21:30",
                "00:00"
            });
        }

        private void SetupDateTimePicker()
        {
            // Sadece bugün ve sonraki günlerin seçilebilmesi için
            dateTimePicker1.MinDate = DateTime.Today;
            dateTimePicker1.Format = DateTimePickerFormat.Short;
        }

        private void LoadSalonlar()
        {
            try
            {
                // Salon ComboBox'ını temizle ve yeniden doldur
                guna2ComboBox3.Items.Clear();
                var salonlar = db.Salon.ToList();
                foreach (var salon in salonlar)
                {
                    guna2ComboBox3.Items.Add(salon.id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Salonlar yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void LoadFilmler()
        {
            try
            {
                // Film isimlerini ComboBox'a çekiyoruz
                var filmler = db.Film.ToList();
                
                if (filmler != null && filmler.Any())
                {
                    guna2ComboBox1.DataSource = filmler;
                    guna2ComboBox1.DisplayMember = "film_adi";
                    guna2ComboBox1.ValueMember = "id";
                    guna2ComboBox1.SelectedIndex = -1; // Hiçbir şey seçili olmasın
                }
                else
                {
                    MessageBox.Show("Veritabanında film bulunamadı! Önce film ekleyiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filmler yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Gerekli alanların kontrolü
                if (guna2ComboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen bir film seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (guna2ComboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen bir salon seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (guna2ComboBox4.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen bir seans saati seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Seçilen değerleri al
                int filmId = (int)guna2ComboBox1.SelectedValue;
                int salonId = Convert.ToInt32(guna2ComboBox3.SelectedItem);
                DateTime seansTarihi = dateTimePicker1.Value.Date;
                string seansStr = guna2ComboBox4.SelectedItem.ToString();
                TimeSpan seansSaati = TimeSpan.Parse(seansStr);

                // Seçilen tarih ve saatte, seçilen salonda başka seans var mı kontrol et
                var mevcutSeans = db.Seans.FirstOrDefault(s => 
                    s.salon_id == salonId && 
                    s.seans_tarihi == seansTarihi &&
                    s.seans_saati == seansSaati);

                if (mevcutSeans != null)
                {
                    MessageBox.Show("Bu salon, tarih ve saatte başka bir seans bulunmaktadır!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Film ve salon varlığını kontrol et
                var film = db.Film.Find(filmId);
                var salon = db.Salon.Find(salonId);

                if (film == null)
                {
                    MessageBox.Show("Seçilen film bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (salon == null)
                {
                    MessageBox.Show("Seçilen salon bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Yeni seans oluştur
                var yeniSeans = new Seans
                {
                    film_id = filmId,
                    salon_id = salonId,
                    seans_tarihi = seansTarihi,
                    seans_saati = seansSaati,
                    ogrenci_fiyati = 120M, // Sabit öğrenci fiyatı
                    tam_fiyat = 150M // Sabit tam fiyat
                };

                // Veritabanına ekle
                db.Seans.Add(yeniSeans);
                db.SaveChanges();

                MessageBox.Show("Seans başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Formu temizle
                guna2ComboBox1.SelectedIndex = -1;
                guna2ComboBox3.SelectedIndex = -1;
                guna2ComboBox4.SelectedIndex = -1;
                dateTimePicker1.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seans eklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcıya çıkış onayı sor
            var result = MessageBox.Show("Programı kapatmak istediğinizden emin misiniz?", "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Eğer kullanıcı "Evet" derse, uygulamayı kapat
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Programı kapatır
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
    }
}
