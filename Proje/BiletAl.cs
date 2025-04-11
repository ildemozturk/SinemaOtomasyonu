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
    public partial class BiletAl : Form
    {
        private readonly SinemaOtomasyonEntities1 db = new SinemaOtomasyonEntities1();
        private string musteriAdi = "";
        private string musteriSoyadi = "";
        private List<string> secilenKoltuklar = new List<string>();
        private Dictionary<string, string> secilenBiletTipleri = new Dictionary<string, string>();
        private int maxBiletSayisi = 0;
        private bool formYuklendi = false;

        // KOLTUK LİSTESİ BURADA TANIMLANIYOR
        List<Button> koltuklar = new List<Button>();

        public BiletAl()
        {
            InitializeComponent();
            this.Load += new EventHandler(BiletAl_Load);
        }

        private void BiletAl_Load(object sender, EventArgs e)
        {
            try
            {
                // NumericUpDown ayarları
                numericUpDown2.Minimum = 0; // Öğrenci bilet sayısı
                numericUpDown3.Minimum = 0; // Tam bilet sayısı
                numericUpDown2.Maximum = 10;
                numericUpDown3.Maximum = 10;
                
                // Başlangıç değerlerini ayarla
                numericUpDown2.Value = 0;
                numericUpDown3.Value = 0;
                maxBiletSayisi = 0;

                // Event handlers'ı ekle
                numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
                numericUpDown3.ValueChanged += numericUpDown3_ValueChanged;

                // Veritabanından filmleri çek
                var filmler = db.Film.ToList();
                
                // Combo box'ı temizle
                guna2ComboBox1.Items.Clear();
                
                // Her filmi combo box'a ekle
                foreach (var film in filmler)
                {
                    guna2ComboBox1.Items.Add(film.film_adi);
                }

                // Koltukları listeye ekle
                koltuklar.Clear();
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Button btn)
                    {
                        if (btn.Name != "button67" && btn.Name != "button68" && btn.Name != "button1" && btn.Name != "iptal")
                        {
                            btn.BackColor = Color.White;
                            btn.Click -= KoltukTiklandi;
                            btn.Click += KoltukTiklandi;
                            koltuklar.Add(btn);
                        }
                    }
                }

                if (Controls["button67"] is Button geriButon)
                {
                    geriButon.Click += button67_Click;
                }

                // Label'ları sıfırla
                label10.Text = "Toplam Tutar: 0 TL";
                label11.Text = "Seçilen Koltuklar:";

                formYuklendi = true;
                UpdateMaxBiletSayisi(); // Form yüklendiğinde bir kez çağır
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (formYuklendi)
            {
                UpdateMaxBiletSayisi();
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (formYuklendi)
            {
                UpdateMaxBiletSayisi();
            }
        }

        private void UpdateMaxBiletSayisi()
        {
            maxBiletSayisi = (int)(numericUpDown2.Value + numericUpDown3.Value);
            
            // Seçili koltuk sayısı toplam bilet sayısından fazlaysa, fazla seçimleri temizle
            while (secilenKoltuklar.Count > maxBiletSayisi)
            {
                string sonKoltuk = secilenKoltuklar.Last();
                Button koltukButton = Controls.Find(sonKoltuk, true).FirstOrDefault() as Button;
                if (koltukButton != null)
                {
                    koltukButton.BackColor = Color.White;
                }
                secilenKoltuklar.RemoveAt(secilenKoltuklar.Count - 1);
                secilenBiletTipleri.Remove(sonKoltuk);
            }

            // Toplam tutarı güncelle
            decimal toplamTutar = (numericUpDown2.Value * 120) + (numericUpDown3.Value * 150);
            label10.Text = $"Toplam Tutar: {toplamTutar} TL";
            
            label11.Text = "Seçilen Koltuklar: " + string.Join(", ", secilenKoltuklar);

            // Eğer hiç bilet seçilmediyse tüm koltukları beyaza çevir
            if (maxBiletSayisi == 0)
            {
                foreach (Button koltuk in koltuklar)
                {
                    if (koltuk.BackColor == Color.Red)
                    {
                        koltuk.BackColor = Color.White;
                    }
                }
                secilenKoltuklar.Clear();
                secilenBiletTipleri.Clear();
            }
        }

        private void KoltukTiklandi(object sender, EventArgs e)
        {
            Button tiklananKoltuk = (Button)sender;
            
            // Eğer koltuk doluysa işlem yapma
            if (tiklananKoltuk.BackColor == Color.Green)
            {
                MessageBox.Show("Bu koltuk dolu!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Eğer hiç bilet seçilmediyse uyarı ver
            if (maxBiletSayisi == 0)
            {
                MessageBox.Show("Lütfen önce almak istediğiniz bilet sayısını belirleyin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Eğer koltuk zaten seçiliyse, seçimi kaldır
            if (tiklananKoltuk.BackColor == Color.Red)
            {
                tiklananKoltuk.BackColor = Color.White;
                secilenKoltuklar.Remove(tiklananKoltuk.Name);
                secilenBiletTipleri.Remove(tiklananKoltuk.Name);
                label11.Text = "Seçilen Koltuklar: " + string.Join(", ", secilenKoltuklar);
                return;
            }

            if (secilenKoltuklar.Count >= maxBiletSayisi)
            {
                MessageBox.Show($"En fazla {maxBiletSayisi} bilet seçebilirsiniz!", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Yeni koltuk seçimi yap
            tiklananKoltuk.BackColor = Color.Red;
            secilenKoltuklar.Add(tiklananKoltuk.Name);

            // Bilet tipini otomatik ata (önce öğrenci biletlerini, sonra tam biletleri)
            int ogrenciKoltukSayisi = secilenBiletTipleri.Count(x => x.Value != null && x.Value.Contains("Öğrenci"));
            if (ogrenciKoltukSayisi < numericUpDown2.Value)
            {
                secilenBiletTipleri[tiklananKoltuk.Name] = "Öğrenci / 120TL";
            }
            else
            {
                secilenBiletTipleri[tiklananKoltuk.Name] = "Tam / 150TL";
            }

            label11.Text = "Seçilen Koltuklar: " + string.Join(", ", secilenKoltuklar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (secilenKoltuklar.Count == 0)
            {
                MessageBox.Show("Lütfen önce koltuk seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var biletTipiForm = new BiletTipiSecim(secilenKoltuklar, secilenBiletTipleri))
            {
                if (biletTipiForm.ShowDialog() == DialogResult.OK)
                {
                    secilenBiletTipleri = biletTipiForm.GetSecilenBiletTipleri();
                    decimal toplamTutar = biletTipiForm.GetToplamTutar();
                    label10.Text = $"Toplam Tutar: {toplamTutar} TL";
                }
            }
        }

        private void button68_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(musteriAdi) || string.IsNullOrEmpty(musteriSoyadi))
                {
                    MessageBox.Show("Lütfen müşteri adı ve soyadını giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (secilenKoltuklar.Count == 0)
                {
                    MessageBox.Show("Lütfen en az bir koltuk seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (secilenKoltuklar.Count != maxBiletSayisi)
                {
                    MessageBox.Show($"Lütfen {maxBiletSayisi} koltuk seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (guna2ComboBox1.SelectedItem == null || guna2ComboBox3.SelectedItem == null || guna2ComboBox4.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen film, tarih ve saat seçimlerini yapınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string secilenFilm = guna2ComboBox1.SelectedItem.ToString();
                DateTime secilenTarih = DateTime.Parse(guna2ComboBox3.SelectedItem.ToString());
                TimeSpan secilenSaat = TimeSpan.Parse(guna2ComboBox4.SelectedItem.ToString());

                var seans = db.Seans
                    .FirstOrDefault(s => s.Film.film_adi == secilenFilm &&
                                       s.seans_tarihi.Day == secilenTarih.Day &&
                                       s.seans_tarihi.Month == secilenTarih.Month &&
                                       s.seans_tarihi.Year == secilenTarih.Year &&
                                       s.seans_saati == secilenSaat);

                if (seans == null)
                {
                    MessageBox.Show("Seçilen seans bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (string koltukNo in secilenKoltuklar)
                {
                    var bilet = new Bilet
                    {
                        musteri_adi = musteriAdi,
                        musteri_soyadi = musteriSoyadi,
                        koltuk_numarasi = koltukNo,
                        seans_id = seans.id,
                        bilet_tipi = secilenBiletTipleri[koltukNo].Contains("Öğrenci") ? "Öğrenci" : "Tam"
                    };

                    db.Bilet.Add(bilet);
                }

                db.SaveChanges();

                MessageBox.Show($"{secilenKoltuklar.Count} adet bilet başarıyla oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Formu temizle
                secilenKoltuklar.Clear();
                secilenBiletTipleri.Clear();
                textBox1.Clear();
                textBox18.Clear();
                musteriAdi = "";
                musteriSoyadi = "";
                numericUpDown2.Value = 0;
                numericUpDown3.Value = 0;
                label11.Text = "Seçilen Koltuklar:";
                label10.Text = "Toplam Tutar: 0 TL";
                KoltuklariGuncelle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilet oluşturulurken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button67_Click(object sender, EventArgs e)
        {
            // Ana sayfaya dön
            AnaSayfa anaSayfa = new AnaSayfa();
            anaSayfa.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            musteriAdi = textBox1.Text;
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            musteriSoyadi = textBox18.Text;
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Seçilen filmi al
                string secilenFilm = guna2ComboBox1.SelectedItem.ToString();
                
                // Tarih combo box'ını temizle
                guna2ComboBox3.Items.Clear();
                guna2ComboBox4.Items.Clear();
                
                // Seçilen filme ait seansları bul
                var seanslar = db.Seans
                    .Where(s => s.Film.film_adi == secilenFilm)
                    .Select(s => s.seans_tarihi)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();
                
                // Tarihleri combo box'a ekle
                foreach (var tarih in seanslar)
                {
                    guna2ComboBox3.Items.Add(tarih.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tarihler yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Seçilen film ve tarihi al
                string secilenFilm = guna2ComboBox1.SelectedItem.ToString();
                DateTime secilenTarih = DateTime.Parse(guna2ComboBox3.SelectedItem.ToString());
                
                // Saat combo box'ını temizle
                guna2ComboBox4.Items.Clear();
                
                // Seçilen filme ve tarihe ait seansları bul
                var seanslar = db.Seans
                    .Where(s => s.Film.film_adi == secilenFilm && 
                               s.seans_tarihi.Day == secilenTarih.Day &&
                               s.seans_tarihi.Month == secilenTarih.Month &&
                               s.seans_tarihi.Year == secilenTarih.Year)
                    .Select(s => s.seans_saati)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();
                
                // Saatleri combo box'a ekle
                foreach (var saat in seanslar)
                {
                    guna2ComboBox4.Items.Add(saat.ToString(@"hh\:mm"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Saatler yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Saat seçildiğinde koltukları güncelle
            KoltuklariGuncelle();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            // Kullanıcıya çıkış onayı sor
            var result = MessageBox.Show("Programı kapatmak istediğinizden emin misiniz?", "Çıkış Onayı", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Eğer kullanıcı "Evet" derse, uygulamayı kapat
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void KoltuklariGuncelle()
        {
            try
            {
                if (guna2ComboBox1.SelectedItem == null || guna2ComboBox3.SelectedItem == null || 
                    guna2ComboBox4.SelectedItem == null)
                    return;

                string secilenFilm = guna2ComboBox1.SelectedItem.ToString();
                DateTime secilenTarih = DateTime.Parse(guna2ComboBox3.SelectedItem.ToString());
                TimeSpan secilenSaat = TimeSpan.Parse(guna2ComboBox4.SelectedItem.ToString());

                // Seçilen seansa ait dolu koltukları getir
                var doluKoltuklar = db.Bilet
                    .Where(b => b.Seans.Film.film_adi == secilenFilm &&
                               b.Seans.seans_tarihi.Day == secilenTarih.Day &&
                               b.Seans.seans_tarihi.Month == secilenTarih.Month &&
                               b.Seans.seans_tarihi.Year == secilenTarih.Year &&
                               b.Seans.seans_saati == secilenSaat)
                    .Select(b => b.koltuk_numarasi)
                    .ToList();

                // Tüm koltukları beyaz yap
                foreach (Button koltuk in koltuklar)
                {
                    koltuk.BackColor = Color.White;
                }

                // Dolu koltukları yeşil yap
                foreach (string koltukNo in doluKoltuklar)
                {
                    var koltuk = koltuklar.FirstOrDefault(k => k.Name == koltukNo);
                    if (koltuk != null)
                    {
                        koltuk.BackColor = Color.Green;
                    }
                }

                // Seçili koltukları kırmızı yap
                foreach (string koltukNo in secilenKoltuklar)
                {
                    var koltuk = koltuklar.FirstOrDefault(k => k.Name == koltukNo);
                    if (koltuk != null)
                    {
                        koltuk.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koltuklar güncellenirken bir hata oluştu: " + ex.Message, "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iptal_Click(object sender, EventArgs e)
        {
            BiletIptalEt();
        }

        private void BiletIptalEt()
        {
            try
            {
                if (string.IsNullOrEmpty(textBox19.Text))
                {
                    MessageBox.Show("Lütfen silinecek biletin ID'sini girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBox19.Text, out int biletId))
                {
                    MessageBox.Show("Geçerli bir bilet ID'si girin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var bilet = db.Bilet.FirstOrDefault(b => b.id == biletId);
                if (bilet == null)
                {
                    MessageBox.Show("Bu ID'ye sahip bir bilet bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox19.Clear();
                    return;
                }

                var result = MessageBox.Show("Bu bileti silmek istediğinizden emin misiniz?", "Silme Onayı", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    db.Bilet.Remove(bilet);
                    db.SaveChanges();
                    MessageBox.Show("Bilet başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox19.Clear();
                    KoltuklariGuncelle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilet silinirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
