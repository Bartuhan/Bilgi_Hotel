namespace Bilgi_Hotel
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnGiris = new System.Windows.Forms.Button();
            this.BtnSifreUnuttum = new System.Windows.Forms.Button();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.txtKullaniciParola = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grpGiris = new System.Windows.Forms.GroupBox();
            this.grpSifreDegis = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOnayKodu = new System.Windows.Forms.TextBox();
            this.txtEposta = new System.Windows.Forms.TextBox();
            this.btnSifreOlustur = new System.Windows.Forms.Button();
            this.TxtYeniSifre = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpGiris.SuspendLayout();
            this.grpSifreDegis.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(468, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(260, 260);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnGiris
            // 
            this.btnGiris.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGiris.Location = new System.Drawing.Point(42, 212);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(123, 46);
            this.btnGiris.TabIndex = 2;
            this.btnGiris.Text = "Giriş Yap";
            this.btnGiris.UseVisualStyleBackColor = true;
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);
            // 
            // BtnSifreUnuttum
            // 
            this.BtnSifreUnuttum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnSifreUnuttum.Location = new System.Drawing.Point(209, 212);
            this.BtnSifreUnuttum.Name = "BtnSifreUnuttum";
            this.BtnSifreUnuttum.Size = new System.Drawing.Size(138, 46);
            this.BtnSifreUnuttum.TabIndex = 3;
            this.BtnSifreUnuttum.Text = "Şifremi Unuttum";
            this.BtnSifreUnuttum.UseVisualStyleBackColor = true;
            this.BtnSifreUnuttum.Click += new System.EventHandler(this.BtnSifreUnuttum_Click);
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtKullaniciAdi.Location = new System.Drawing.Point(137, 91);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(210, 26);
            this.txtKullaniciAdi.TabIndex = 0;
            // 
            // txtKullaniciParola
            // 
            this.txtKullaniciParola.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtKullaniciParola.Location = new System.Drawing.Point(137, 145);
            this.txtKullaniciParola.Name = "txtKullaniciParola";
            this.txtKullaniciParola.PasswordChar = '*';
            this.txtKullaniciParola.Size = new System.Drawing.Size(210, 26);
            this.txtKullaniciParola.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(38, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Kullanıcı Adı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(38, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Şifre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(374, 55);
            this.label3.TabIndex = 4;
            this.label3.Text = "Bilgi Hotel Panel";
            // 
            // grpGiris
            // 
            this.grpGiris.Controls.Add(this.label3);
            this.grpGiris.Controls.Add(this.label2);
            this.grpGiris.Controls.Add(this.label1);
            this.grpGiris.Controls.Add(this.txtKullaniciParola);
            this.grpGiris.Controls.Add(this.txtKullaniciAdi);
            this.grpGiris.Controls.Add(this.BtnSifreUnuttum);
            this.grpGiris.Controls.Add(this.btnGiris);
            this.grpGiris.Location = new System.Drawing.Point(39, 92);
            this.grpGiris.Name = "grpGiris";
            this.grpGiris.Size = new System.Drawing.Size(423, 265);
            this.grpGiris.TabIndex = 5;
            this.grpGiris.TabStop = false;
            // 
            // grpSifreDegis
            // 
            this.grpSifreDegis.Controls.Add(this.label4);
            this.grpSifreDegis.Controls.Add(this.label7);
            this.grpSifreDegis.Controls.Add(this.label5);
            this.grpSifreDegis.Controls.Add(this.label6);
            this.grpSifreDegis.Controls.Add(this.TxtYeniSifre);
            this.grpSifreDegis.Controls.Add(this.txtOnayKodu);
            this.grpSifreDegis.Controls.Add(this.txtEposta);
            this.grpSifreDegis.Controls.Add(this.btnSifreOlustur);
            this.grpSifreDegis.Location = new System.Drawing.Point(39, 92);
            this.grpSifreDegis.Name = "grpSifreDegis";
            this.grpSifreDegis.Size = new System.Drawing.Size(423, 265);
            this.grpSifreDegis.TabIndex = 5;
            this.grpSifreDegis.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(374, 55);
            this.label4.TabIndex = 4;
            this.label4.Text = "Bilgi Hotel Panel";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(12, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "Eposta Onay Kodu";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(12, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 20);
            this.label6.TabIndex = 3;
            this.label6.Text = "Kullanıcı Eposta";
            // 
            // txtOnayKodu
            // 
            this.txtOnayKodu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtOnayKodu.Location = new System.Drawing.Point(160, 123);
            this.txtOnayKodu.Name = "txtOnayKodu";
            this.txtOnayKodu.Size = new System.Drawing.Size(210, 26);
            this.txtOnayKodu.TabIndex = 1;
            // 
            // txtEposta
            // 
            this.txtEposta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtEposta.Location = new System.Drawing.Point(160, 91);
            this.txtEposta.Name = "txtEposta";
            this.txtEposta.Size = new System.Drawing.Size(210, 26);
            this.txtEposta.TabIndex = 0;
            // 
            // btnSifreOlustur
            // 
            this.btnSifreOlustur.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSifreOlustur.Location = new System.Drawing.Point(160, 201);
            this.btnSifreOlustur.Name = "btnSifreOlustur";
            this.btnSifreOlustur.Size = new System.Drawing.Size(210, 46);
            this.btnSifreOlustur.TabIndex = 3;
            this.btnSifreOlustur.Text = "Yeni Şifre Oluştur";
            this.btnSifreOlustur.UseVisualStyleBackColor = true;
            this.btnSifreOlustur.Click += new System.EventHandler(this.btnSifreOlustur_Click);
            // 
            // TxtYeniSifre
            // 
            this.TxtYeniSifre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.TxtYeniSifre.Location = new System.Drawing.Point(160, 155);
            this.TxtYeniSifre.Name = "TxtYeniSifre";
            this.TxtYeniSifre.PasswordChar = '*';
            this.TxtYeniSifre.Size = new System.Drawing.Size(210, 26);
            this.TxtYeniSifre.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(12, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 20);
            this.label7.TabIndex = 3;
            this.label7.Text = "Yeni Şifre";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(814, 459);
            this.Controls.Add(this.grpSifreDegis);
            this.Controls.Add(this.grpGiris);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpGiris.ResumeLayout(false);
            this.grpGiris.PerformLayout();
            this.grpSifreDegis.ResumeLayout(false);
            this.grpSifreDegis.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnGiris;
        private System.Windows.Forms.Button BtnSifreUnuttum;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.TextBox txtKullaniciParola;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpGiris;
        private System.Windows.Forms.GroupBox grpSifreDegis;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtYeniSifre;
        private System.Windows.Forms.TextBox txtOnayKodu;
        private System.Windows.Forms.TextBox txtEposta;
        private System.Windows.Forms.Button btnSifreOlustur;
    }
}