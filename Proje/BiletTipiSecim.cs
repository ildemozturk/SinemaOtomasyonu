using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Proje
{
    public partial class BiletTipiSecim : Form
    {
        private List<string> secilenKoltuklar;
        private Dictionary<string, string> secilenBiletTipleri;
        private Dictionary<string, ComboBox> biletTipiComboBoxlar = new Dictionary<string, ComboBox>();
        private decimal toplamTutar = 0;
        private Label lblToplamTutar;

        public BiletTipiSecim(List<string> koltuklar, Dictionary<string, string> mevcutBiletTipleri = null)
        {
            InitializeComponent();
            secilenKoltuklar = koltuklar;
            secilenBiletTipleri = mevcutBiletTipleri ?? new Dictionary<string, string>();

            this.Size = new Size(400, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Bilet Tipi Seçimi";

            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            this.Controls.Add(mainPanel);

            Label baslik = new Label
            {
                Text = "Bilet Tipi Seçimi",
                Font = new Font("Century Gothic", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };
            mainPanel.Controls.Add(baslik);

            int yPos = 60;
            foreach (string koltuk in secilenKoltuklar)
            {
                Label koltukLabel = new Label
                {
                    Text = $"Koltuk {koltuk}:",
                    Location = new Point(20, yPos + 5),
                    AutoSize = true,
                    Font = new Font("Century Gothic", 10)
                };
                mainPanel.Controls.Add(koltukLabel);

                ComboBox biletTipiCombo = new ComboBox
                {
                    Location = new Point(150, yPos),
                    Size = new Size(200, 30),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Century Gothic", 10)
                };

                biletTipiCombo.Items.AddRange(new string[] { 
                    "Öğrenci / 120TL",
                    "Tam / 150TL"
                });

                if (secilenBiletTipleri.ContainsKey(koltuk))
                {
                    biletTipiCombo.SelectedItem = secilenBiletTipleri[koltuk];
                }

                biletTipiCombo.SelectedIndexChanged += BiletTipiCombo_SelectedIndexChanged;
                mainPanel.Controls.Add(biletTipiCombo);
                biletTipiComboBoxlar[koltuk] = biletTipiCombo;

                yPos += 40;
            }

            lblToplamTutar = new Label
            {
                Text = "Toplam Tutar: 0 TL",
                Location = new Point(20, yPos + 20),
                AutoSize = true,
                Font = new Font("Century Gothic", 12, FontStyle.Bold)
            };
            mainPanel.Controls.Add(lblToplamTutar);

            Button btnOnayla = new Button
            {
                Text = "Onayla",
                Location = new Point(150, yPos + 60),
                Size = new Size(100, 40),
                BackColor = Color.Maroon,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Century Gothic", 10)
            };
            btnOnayla.Click += BtnOnayla_Click;
            mainPanel.Controls.Add(btnOnayla);

            // Form yüklendiğinde toplam tutarı hesapla
            HesaplaToplamTutar();
        }

        private void BiletTipiCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            HesaplaToplamTutar();
        }

        private void HesaplaToplamTutar()
        {
            toplamTutar = 0;
            foreach (var combo in biletTipiComboBoxlar)
            {
                if (combo.Value.SelectedItem != null)
                {
                    if (combo.Value.SelectedItem.ToString().Contains("Öğrenci"))
                    {
                        toplamTutar += 120;
                    }
                    else
                    {
                        toplamTutar += 150;
                    }
                }
            }
            lblToplamTutar.Text = $"Toplam Tutar: {toplamTutar} TL";
        }

        private void BtnOnayla_Click(object sender, EventArgs e)
        {
            // Tüm koltuklar için bilet tipi seçildiğinden emin ol
            if (biletTipiComboBoxlar.Any(x => x.Value.SelectedIndex == -1))
            {
                MessageBox.Show("Lütfen tüm koltuklar için bilet tipi seçiniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçilen bilet tiplerini kaydet
            foreach (var combo in biletTipiComboBoxlar)
            {
                secilenBiletTipleri[combo.Key] = combo.Value.SelectedItem.ToString();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public Dictionary<string, string> GetSecilenBiletTipleri()
        {
            return secilenBiletTipleri;
        }

        public decimal GetToplamTutar()
        {
            return toplamTutar;
        }
    }
} 