using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Proje.Models;

namespace Proje
{
    public partial class SeansListele : Form
    {
        private readonly SinemaOtomasyonEntities1 db = new SinemaOtomasyonEntities1();

        public SeansListele()
        {
            InitializeComponent();
        }

        private void SeansListele_Load(object sender, EventArgs e)
        {
            SeanslariListele();
        }

        private void SeanslariListele()
        {
            try
            {
                // Seans bilgilerini film adı ve salon numarası ile birlikte çek
                var seanslar = from s in db.Seans
                              join f in db.Film on s.film_id equals f.id
                              join sl in db.Salon on s.salon_id equals sl.id
                              select new
                              {
                                  SeansID = s.id,
                                  Film = f.film_adi,
                                  Salon = sl.id,
                                  Tarih = s.seans_tarihi,
                                  Saat = s.seans_saati,
                                  OgrenciFiyat = s.ogrenci_fiyati,
                                  TamFiyat = s.tam_fiyat
                              };

                dataGridView1.DataSource = seanslar.ToList();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                
                // Kolon başlıklarını Türkçeleştir
                dataGridView1.Columns["SeansID"].HeaderText = "Seans No";
                dataGridView1.Columns["Film"].HeaderText = "Film Adı";
                dataGridView1.Columns["Salon"].HeaderText = "Salon No";
                dataGridView1.Columns["Tarih"].HeaderText = "Seans Tarihi";
                dataGridView1.Columns["Saat"].HeaderText = "Seans Saati";
                dataGridView1.Columns["OgrenciFiyat"].HeaderText = "Öğrenci Bilet";
                dataGridView1.Columns["TamFiyat"].HeaderText = "Tam Bilet";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seanslar listelenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSeansGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen güncellenecek seansı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Seçili satırdan seans bilgilerini al
                int seansId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SeansID"].Value);
                string filmAdi = dataGridView1.SelectedRows[0].Cells["Film"].Value.ToString();
                int salonNo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Salon"].Value);
                DateTime tarih = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Tarih"].Value);
                TimeSpan saat = (TimeSpan)dataGridView1.SelectedRows[0].Cells["Saat"].Value;

                // Güncelleme formunu aç
                SeansGuncelle guncelleForm = new SeansGuncelle(seansId, filmAdi, salonNo, tarih, saat);
                if (guncelleForm.ShowDialog() == DialogResult.OK)
                {
                    // Listeyi güncelle
                    SeanslariListele();
                    MessageBox.Show("Seans başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seans güncellenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSeansSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen silinecek seansı seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Seçili satırdan seans ID'sini al
                int seansId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["SeansID"].Value);

                // Kullanıcıya silme onayı sor
                var result = MessageBox.Show("Seçili seansı silmek istediğinizden emin misiniz?", 
                    "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Seansı bul
                    var seans = db.Seans.Find(seansId);

                    if (seans != null)
                    {
                        // Seansı sil
                        db.Seans.Remove(seans);
                        db.SaveChanges();

                        MessageBox.Show("Seans başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Listeyi güncelle
                        SeanslariListele();
                    }
                    else
                    {
                        MessageBox.Show("Seans bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seans silinirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AnaSayfa anaSayfa = new AnaSayfa();
            anaSayfa.Show();
            this.Hide();
        }
    }
} 