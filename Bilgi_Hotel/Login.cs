using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bilgi_Hotel
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            grpSifreDegis.Visible = false;
        }

        
        
        SqlConnection con = new SqlConnection("Server=.;Database=db_Bilgi_Hotel;Trusted_Connection=True;");
        string yetki;

      //  [Obsolete]
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("sp_Login",con);
            if (con.State==ConnectionState.Closed)
            {
                con.Open();
            }
            if (txtKullaniciAdi.Text == string.Empty && txtKullaniciParola.Text == string.Empty)
            {
                MessageBox.Show("Kullanıcı Adı Ve Parola Boş Olamaz");
            }
            else
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KullaniciAdi", txtKullaniciAdi.Text);
                cmd.Parameters.Add("@KullaniciParola", txtKullaniciParola.Text);

                Form1 yonetici = new Form1();
                Form2 muhasebe = new Form2();
                //Login lg = new Login();
                yetki = cmd.ExecuteScalar().ToString();
;              
                if (yetki!="0")
                {
                    switch (yetki)
                    {
                        case "1": yonetici.Show(); break;
                        case "2": muhasebe.Show(); break;
                        default: break;

                    }
                    this.Hide();
                }
                else MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı ! !");
                
            }
            con.Close();

            
        }

        private void BtnSifreUnuttum_Click(object sender, EventArgs e)
        {
            grpGiris.Visible = false;
            grpSifreDegis.Visible = true;

        }

        [Obsolete]
        private void btnSifreOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("sp_SifreUnuttum", con);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("KullaniciEposta",txtEposta.Text);
            cmd.Parameters.Add("@EpostaOnayKodu", txtOnayKodu.Text);
            cmd.Parameters.Add("@KullaniciYeniSifre", TxtYeniSifre.Text);

            MessageBox.Show(cmd.ExecuteNonQuery().ToString()+" Güncelleme Başarılı");
            con.Close();

            grpGiris.Visible = true;
            grpSifreDegis.Visible = false;
        }
    }
}
