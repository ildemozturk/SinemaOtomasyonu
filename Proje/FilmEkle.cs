using Proje.Models;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Proje
{
    public partial class FilmEkle : Form
    {
        public FilmEkle()
        {
            InitializeComponent();
        }

        SinemaOtomasyonEntities1 db = new SinemaOtomasyonEntities1();

        private void FilmEkle_Load(object sender, EventArgs e)
        {
            LoadFilmTurleri();
            LoadFilmler();
        }

        private void LoadFilmTurleri()
        {
            try
            {
                // Film türlerini ComboBox'a çekiyoruz
                var filmTurleri = db.FilmTurleri.ToList();
                
                if (filmTurleri != null && filmTurleri.Any())
                {
                    filmTur.DataSource = filmTurleri;
                    filmTur.DisplayMember = "tur_adi";
                    filmTur.ValueMember = "id";
                    filmTur.SelectedIndex = -1; // Hiçbir şey seçili olmasın
                }
                else
                {
                    MessageBox.Show("Film türleri bulunamadı! Lütfen veritabanını kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Film türleri yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFilmler()
        {
            try
            {
                // ComboBox'ı temizle
                comboBox1.Items.Clear();

                // Veritabanından tüm filmleri çek
                var filmler = db.Film.OrderBy(f => f.film_adi).ToList();

                // ComboBox'a film adlarını ekle
                foreach (var film in filmler)
                {
                    comboBox1.Items.Add(film.film_adi);
                }

                // ComboBox'ın varsayılan seçim modunu ayarla
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filmler yüklenirken bir hata oluştu: " + ex.Message, "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fotoDow_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                    openFileDialog.Title = "Film Fotoğrafı Seçin";
                    openFileDialog.Multiselect = false;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Seçilen dosyanın boyutunu kontrol et (max 5MB)
                        FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                        if (fileInfo.Length > 5 * 1024 * 1024)
                        {
                            MessageBox.Show("Seçilen resim dosyası 5MB'dan büyük olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Mevcut resmi temizle
                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image.Dispose();
                            pictureBox1.Image = null;
                        }

                        // Yeni resmi yükle
                        using (var img = Image.FromFile(openFileDialog.FileName))
                        {
                            pictureBox1.Image = new Bitmap(img);
                        }
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox1.Tag = openFileDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fotoğraf yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilmAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(filmAdi.Text) ||
                filmTur.SelectedItem == null ||
                string.IsNullOrWhiteSpace(filmSure.Text) ||
                pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Film Türü ID kontrolü
            int filmTuruId;
            if (!int.TryParse(filmTur.SelectedValue?.ToString(), out filmTuruId))
            {
                MessageBox.Show("Geçerli bir film türü seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Film Süresi kontrolü
            int filmSuresi;
            if (!int.TryParse(filmSure.Text, out filmSuresi))
            {
                MessageBox.Show("Lütfen geçerli bir film süresi giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Fotoğraf kontrolü
            if (pictureBox1.Tag == null)
            {
                MessageBox.Show("Lütfen bir görsel seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Seçilen fotoğrafı kaydetme
                string dosyaAdi = Guid.NewGuid().ToString() + Path.GetExtension(pictureBox1.Tag.ToString());
                string hedefKlasor = Path.Combine(Application.StartupPath, "FilmFotograflari");

                if (!Directory.Exists(hedefKlasor))
                    Directory.CreateDirectory(hedefKlasor);

                string tamDosyaYolu = Path.Combine(hedefKlasor, dosyaAdi);
                File.Copy(pictureBox1.Tag.ToString(), tamDosyaYolu);

                // Yeni film nesnesi oluştur
                Film yeniFilm = new Film
                {
                    film_adi = filmAdi.Text,
                    film_turu_id = filmTuruId,
                    film_suresi = filmSuresi,
                    film_gorsel = dosyaAdi
                };

                // Veritabanına ekleme
                db.Film.Add(yeniFilm);
                db.SaveChanges();

                MessageBox.Show("Film başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Formu temizleme
                filmAdi.Text = "";
                filmTur.SelectedIndex = 0;
                filmSure.Text = "";
                pictureBox1.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + (ex.InnerException?.Message ?? ex.Message), "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen silinecek filmi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string secilenFilmAdi = comboBox1.SelectedItem.ToString();
                var film = db.Film.FirstOrDefault(f => f.film_adi == secilenFilmAdi);
                
                if (film == null)
                {
                    MessageBox.Show("Seçilen film bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = MessageBox.Show($"{film.film_adi} filmini silmek istediğinizden emin misiniz?\nBu işlem geri alınamaz ve filme ait tüm seanslar ve biletler de silinecektir!", 
                    "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    int silinecekBiletSayisi = 0;
                    int silinecekSeansSayisi = 0;

                    // Önce bu filme ait seansları sil
                    var seanslar = db.Seans.Where(s => s.film_id == film.id).ToList();
                    silinecekSeansSayisi = seanslar.Count;

                    foreach (var seans in seanslar)
                    {
                        // Seansa ait biletleri sil
                        var biletler = db.Bilet.Where(b => b.seans_id == seans.id).ToList();
                        silinecekBiletSayisi += biletler.Count;
                        db.Bilet.RemoveRange(biletler);
                        db.Seans.Remove(seans);
                    }

                    // Sonra filmi sil
                    db.Film.Remove(film);
                    db.SaveChanges();

                    string mesaj = $"{film.film_adi} filmi başarıyla silindi!\n\n" +
                                 $"Silinen Kayıtlar:\n" +
                                 $"- Film: 1 adet\n" +
                                 $"- Seans: {silinecekSeansSayisi} adet\n" +
                                 $"- Bilet: {silinecekBiletSayisi} adet";

                    MessageBox.Show(mesaj, "Silme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // ComboBox'ı güncelle
                    LoadFilmler();
                }
                else
                {
                    MessageBox.Show("Silme işlemi iptal edildi.", "İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Film silinirken bir hata oluştu:\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
