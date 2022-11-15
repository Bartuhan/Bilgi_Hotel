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
    public partial class Musteriler : Form
    {
        public Musteriler()
        {
            InitializeComponent();
        }

        List<KeyValuePair<int,string>> SehirGetir = new List<KeyValuePair<int,string>>();
        List<KeyValuePair<int, string>> IlceGetir = new List<KeyValuePair<int, string>>();

        SqlConnection con = new SqlConnection("Server=.;Database=db_Bilgi_Hotel;Trusted_Connection=True;");
        SqlCommand cmd = new SqlCommand("");
        string MusteriID ="";
        
        private void Musteriler_Load_1(object sender, EventArgs e)
        {
            lwMusteriler.Clear();
            lwMusteriler.View = View.Details;
            lwMusteriler.GridLines = true;
            lwMusteriler.FullRowSelect = true;

            lwMusteriler.Columns.Add("Ad", 120);
            lwMusteriler.Columns.Add("Soyad", 120);
            lwMusteriler.Columns.Add("Tc Kimlik No", 120);
            lwMusteriler.Columns.Add("Telefon", 120);
            lwMusteriler.Columns.Add("EPosta", 120);
            lwMusteriler.Columns.Add("Yetkili Ad Soyad", 130);
            lwMusteriler.Columns.Add("Vergi Dairesi", 140);
            lwMusteriler.Columns.Add("Vergi No", 140);

            btnMusteriUpdate.Enabled= false;

            con.Open();
            cmd.CommandText = "select IlId,IlAd from Sehir";
            cmd.Connection = con;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SehirGetir.Add(new  KeyValuePair <int, string>((int)dr[0], (string)dr[1]));
            }
            cmbSehir.DataSource = SehirGetir.ToList();
            cmbSehir.ValueMember = "Key";
            cmbSehir.DisplayMember = "Value";
            dr.Close();
            con.Close();

            con.Open();
            cmd.CommandText = "sp_musteriGetir";
            cmd.Connection = con;

            SqlDataReader dr2 = cmd.ExecuteReader();
            while(dr2.Read())
            {
                string[] row = { dr2[0].ToString(), dr2[1].ToString(), dr2[2].ToString(), dr2[3].ToString(), dr2[4].ToString(), dr2[5].ToString(), dr2[6].ToString(), dr2[7].ToString() };
                var satir = new ListViewItem(row);
                lwMusteriler.Items.Add(satir);
            }
            dr2.Close();
            con.Close();
            btnkytGuncelle.Enabled = false;
        }

        private void cmbSehir_SelectionChangeCommitted(object sender, EventArgs e)
        {
            IlceGetir.Clear();
            con.Open();
            cmd.CommandText = "select i.IlceId,i.IlceAd from Sehir il JOIN Ilce i ON i.IlId = il.IlId where il.IlId =" + cmbSehir.SelectedValue;
            cmd.Connection = con;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                IlceGetir.Add(new KeyValuePair<int, string>((int)dr[0], (string)dr[1]));
            }
            cmbIlce.DataSource = IlceGetir.ToList();
            cmbIlce.ValueMember = "Key";
            cmbIlce.DisplayMember = "Value";
            dr.Close();
            con.Close();
        }

        [Obsolete]
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.CommandText = "sp_MusteriEkle";
            cmd.Parameters.Add("@MusteriAd",txtad.Text);
            cmd.Parameters.Add("@MusteriSoyad", txtSoyad.Text);
            cmd.Parameters.Add("@MusteriTCKimlik", txtTc.Text);
            cmd.Parameters.Add("@MusteriPasaportNo", txtPasaport.Text);
            cmd.Parameters.Add("@MusteriUnvan", txtUnvan.Text);
            cmd.Parameters.Add("@MusteriYetkiliAdSoyad", txtYetkili.Text);
            cmd.Parameters.Add("@MusteriVergiNo", txtVergiNo.Text);
            cmd.Parameters.Add("@MusteriVergiDairesi", txtVergiD.Text);
            cmd.Parameters.Add("@MusteriTelefon", txtTelefon.Text);
            cmd.Parameters.Add("@MusteriPosta", txtPosta.Text);
            cmd.Parameters.Add("@IlID", cmbSehir.SelectedValue);
            cmd.Parameters.Add("@IlceID", cmbIlce.SelectedValue);
            cmd.Parameters.Add("@MusteriAdres", txtAdres.Text);
            cmd.Parameters.Add("@UlkeID", 1);
            cmd.Parameters.Add("@MusteriAciklama", txtaciklama.Text);
            cmd.Parameters.Add("@DilID ", 1);
            cmd.Parameters.Add("@MusteriKurumsalOK", chcKurumsal.Checked);

            MessageBox.Show(cmd.ExecuteNonQuery()+" Kişi Başarıyla Kaydedildi");
            con.Close();
            Temizle();

        }



        void Temizle()
        {
            txtad.Clear();
            txtSoyad.Clear();
            txtTc.Clear();
            txtPasaport.Clear();
            txtUnvan.Clear();
            txtYetkili.Clear();
            txtVergiNo.Clear();
            txtVergiD.Clear();
            txtTelefon.Clear();
            txtPosta.Clear();
            txtAdres.Clear();
            txtaciklama.Clear();

        }

        private void btnMusteriBul_Click(object sender, EventArgs e)
        {

            cmd.Parameters.Clear();
            cmd.Connection = con;
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_TcyeGoreMusteri";
            cmd.Parameters.Add("@tc",txtTcAra.Text);

            string karar= cmd.ExecuteScalar().ToString();
            if (karar == "0")
            {
                MessageBox.Show("Müşteri Kayıtlı Değil");
                con.Close();
            }
            else
            {
                con.Close();
                cmd.Parameters.Clear();

                lwMusteriler.Items.Clear();
                con.Open();
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.CommandText = "sp_TCMusteriGetir";
                cmd.Connection = con;
                cmd.Parameters.Add("@tc", txtTcAra.Text);
                
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString() };
                    var satir = new ListViewItem(row);
                    lwMusteriler.Items.Add(satir);
                    MusteriID = dr[8].ToString();
                }

                dr.Close();
                btnMusteriUpdate.Enabled = true;
            }
            con.Close();
            
        }

        private void btnMusteriUpdate_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            con.Open();
            cmd.CommandText = "sp_MusteriIDyeGetir";
            cmd.Connection = con;
            cmd.Parameters.Add("@musteriID", Convert.ToInt32(MusteriID));
             SqlDataReader dr = cmd.ExecuteReader();
             dr.Read();
             string[] gelen = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(),dr [8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), dr[12].ToString(), dr[13].ToString(), dr[14].ToString(), dr[15].ToString(), dr[16].ToString(), dr [17].ToString() };
        
            txtad.Text = gelen[1];
            txtSoyad.Text = gelen[2];
            txtTc.Text = gelen[3];
            txtPasaport.Text = gelen[4];
            txtUnvan.Text = gelen[5];
            txtYetkili.Text = gelen[6];
            txtVergiNo.Text = gelen[7];
            txtVergiD.Text = gelen[8];
            txtTelefon.Text = gelen[9];
            txtPosta.Text = gelen[10];
            txtAdres.Text = gelen[11];
            cmbSehir.SelectedIndex = Convert.ToInt32(gelen[12])-1;
            txtaciklama.Text = gelen[15];
            chcKurumsal.Checked = Convert.ToBoolean(gelen[16]);

            dr.Close();
               
            con.Close();
            btnKaydet.Enabled = false;
            btnkytGuncelle.Enabled = true;
            grpYenikayit.Text = "Kayıt Güncelle";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnKaydet.Enabled = true;
            btnkytGuncelle.Enabled = false;
            grpYenikayit.Text = "Yeni Kayıt Ekle";
            btnMusteriUpdate.Enabled = false;
            Temizle();

        }

        [Obsolete]
        private void btnkytGuncelle_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_UptadeMusteriler";
            cmd.Connection = con;
            cmd.Parameters.Add("@MusteriID", MusteriID);
            cmd.Parameters.Add("@MusteriAd", txtad.Text);
            cmd.Parameters.Add("@MusteriSoyad", txtSoyad.Text);
            cmd.Parameters.Add("@MusteriTCKimlik", txtTc.Text);
            cmd.Parameters.Add("@MusteriPasaportNo", txtPasaport.Text);
            cmd.Parameters.Add("@MusteriUnvan", txtUnvan.Text);
            cmd.Parameters.Add("@MusteriYetkiliAdSoyad", txtYetkili.Text);
            cmd.Parameters.Add("@MusteriVergiNo", txtVergiNo.Text);
            cmd.Parameters.Add("@MusteriVergiDairesi", txtVergiD.Text);
            cmd.Parameters.Add("@MusteriTelefon", txtTelefon.Text);
            cmd.Parameters.Add("@MusteriPosta", txtPosta.Text);
            cmd.Parameters.Add("@IlID", cmbSehir.SelectedValue);
            cmd.Parameters.Add("@IlceID", cmbIlce.SelectedValue);
            cmd.Parameters.Add("@MusteriAdres", txtAdres.Text);
            cmd.Parameters.Add("@UlkeID", 1);
            cmd.Parameters.Add("@MusteriAciklama", txtaciklama.Text);
            cmd.Parameters.Add("@DilID ", 1);
            cmd.Parameters.Add("@MusteriKurumsalOK", chcKurumsal.Checked);
            
            MessageBox.Show(cmd.ExecuteNonQuery() + " Kayıt Güncellendi !");
            con.Close();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            lwMusteriler.Items.Clear();
            con.Open();
            cmd.CommandText = "sp_musteriGetir";
            cmd.Connection = con;

            SqlDataReader dr2 = cmd.ExecuteReader();
            while (dr2.Read())
            {
                string[] row = { dr2[0].ToString(), dr2[1].ToString(), dr2[2].ToString(), dr2[3].ToString(), dr2[4].ToString(), dr2[5].ToString(), dr2[6].ToString(), dr2[7].ToString() };
                var satir = new ListViewItem(row);
                lwMusteriler.Items.Add(satir);
            }
            dr2.Close();
            con.Close();
        }
    }
}
