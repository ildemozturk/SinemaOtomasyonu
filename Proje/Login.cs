using Proje.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Proje
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        // DbContext'i bir kere tanımlayıp, tüm metodlar boyunca kullanabiliriz
        SinemaOtomasyonEntities db = new SinemaOtomasyonEntities();

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde herhangi bir işlem yapılması gerekmediyse, boş bırakılabilir
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Textbox'lar temizlenmeden önce gerekli bilgiler alınmalı
            string kullaniciAdi = textBox1.Text.Trim();
            string parola = textBox2.Text.Trim();

            // Eğer kullanıcı adı ve parola boşsa uyarı ver
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(parola))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz.");
                return;
            }

            // Kullanıcı adıyla veritabanında sorgulama yap
            var kullanici = db.Kullanici.FirstOrDefault(x => x.kullanici_adi == kullaniciAdi);

            if (kullanici != null)
            {
                // Kullanıcı bulunduysa, şifreyi kontrol et
                if (kullanici.sifre == parola)
                {
                    // Şifre doğruysa, AnaSayfa formunu aç ve login formunu kapat
                    AnaSayfa anasayfa = new AnaSayfa();
                    anasayfa.Show();
                    this.Hide(); // Bu formu gizle
                }
                else
                {
                    // Şifre hatalı
                    MessageBox.Show("Kullanıcı adı veya parola hatalı.");
                }
            }
            else
            {
                // Kullanıcı adı bulunamadı
                MessageBox.Show("Kullanıcı adı veya parola hatalı.");
            }
        }

        // Reset butonunun click eventi
        private void button2_Click(object sender, EventArgs e)
        {
            // Textbox'ları temizle
            textBox1.Clear(); // Kullanıcı adı kutusunu temizle
            textBox2.Clear(); // Parola kutusunu temizle
        }
    }
}