namespace Proje
{
    partial class SeansListele
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSeansSil = new System.Windows.Forms.Button();
            this.btnSeansGuncelle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(501, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 40);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tüm Seanslar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(489, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 39);
            this.label1.TabIndex = 6;
            this.label1.Text = "CineMekan";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 122);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1253, 456);
            this.dataGridView1.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Maroon;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(1148, 586);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(117, 51);
            this.button4.TabIndex = 20;
            this.button4.Text = "Geri";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(1221, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 48);
            this.button1.TabIndex = 21;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSeansSil
            // 
            this.btnSeansSil.BackColor = System.Drawing.Color.Maroon;
            this.btnSeansSil.FlatAppearance.BorderSize = 0;
            this.btnSeansSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeansSil.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSeansSil.ForeColor = System.Drawing.Color.White;
            this.btnSeansSil.Location = new System.Drawing.Point(12, 586);
            this.btnSeansSil.Name = "btnSeansSil";
            this.btnSeansSil.Size = new System.Drawing.Size(117, 51);
            this.btnSeansSil.TabIndex = 22;
            this.btnSeansSil.Text = "Sil";
            this.btnSeansSil.UseVisualStyleBackColor = false;
            this.btnSeansSil.Click += new System.EventHandler(this.btnSeansSil_Click);
            // 
            // btnSeansGuncelle
            // 
            this.btnSeansGuncelle.BackColor = System.Drawing.Color.Maroon;
            this.btnSeansGuncelle.FlatAppearance.BorderSize = 0;
            this.btnSeansGuncelle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeansGuncelle.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSeansGuncelle.ForeColor = System.Drawing.Color.White;
            this.btnSeansGuncelle.Location = new System.Drawing.Point(135, 586);
            this.btnSeansGuncelle.Name = "btnSeansGuncelle";
            this.btnSeansGuncelle.Size = new System.Drawing.Size(117, 51);
            this.btnSeansGuncelle.TabIndex = 23;
            this.btnSeansGuncelle.Text = "Güncelle";
            this.btnSeansGuncelle.UseVisualStyleBackColor = false;
            this.btnSeansGuncelle.Click += new System.EventHandler(this.btnSeansGuncelle_Click);
            // 
            // SeansListele
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 649);
            this.Controls.Add(this.btnSeansGuncelle);
            this.Controls.Add(this.btnSeansSil);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SeansListele";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SeansListele";
            this.Load += new System.EventHandler(this.SeansListele_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSeansSil;
        private System.Windows.Forms.Button btnSeansGuncelle;
    }
} 