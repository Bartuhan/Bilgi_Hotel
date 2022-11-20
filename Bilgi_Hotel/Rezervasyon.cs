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

namespace Bilgi_Hotel
{
    public partial class Rezervasyon : Form
    {
        public Rezervasyon()
        {
            InitializeComponent();
        }
        SqlConnection con =new SqlConnection("Server=.;Database=db_Bilgi_Hotel;Trusted_Connection=True;");
        SqlCommand cmd = new SqlCommand();

        string odaid="";
        string musteriID = "";
        int fiyat;
        string rezervasyonID = "";

        List<KeyValuePair<int,string>> OdaTipiGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> OdaNoGetir = new List<KeyValuePair<int, string>>();
        

        private void Rezervasyon_Load(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            btnDetay.Enabled = false;
            btnUpdate.Enabled = false;

            lwRezervasyon.Clear();
            lwRezervasyon.View = View.Details;
            lwRezervasyon.GridLines = true;
            lwRezervasyon.FullRowSelect = true;

            lwRezervasyon.Columns.Add("Ad Soyad", 200);
            lwRezervasyon.Columns.Add("Başlangıç Tarihi", 200);
            lwRezervasyon.Columns.Add("Bitiş Tarihi", 200);
            lwRezervasyon.Columns.Add("Telefon", 200);
            lwRezervasyon.Columns.Add("Oda Numarası", 200);
            //lwRezervasyon.Columns.Add("Yetkili Ad Soyad", 130);   
            //lwRezervasyon.Columns.Add("Vergi Dairesi", 140);
            //lwRezervasyon.Columns.Add("Vergi No", 140);

            if (con.State==ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.CommandText = "Select OdaTipiId,OdaTipiAd From OdaTipi";
            cmd.Connection = con;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                OdaTipiGetir.Add(new KeyValuePair<int, string>((int)dr[0], (string)dr[1].ToString()));
            }
            cmbOdaTipi.DataSource = OdaTipiGetir.ToList();
            cmbOdaTipi.ValueMember = "Key";
            cmbOdaTipi.DisplayMember = "Value";
            dr.Close();
            con.Close();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.CommandText = "select m.MusteriAd + ' ' + m.MusteriSoyad AS AdSoyad , r.RezervasyonGecerlilikTarihi , r.RezervasyonGecerlilikSonTarihi ,m.MusteriTelefon ,o.OdaNo from Rezervasyon r JOIN MusteriRezervasyon mr ON r.RezervasyonId=mr.RezervasyonId JOIN Musteriler m ON mr.MusteriId=m.MusteriID JOIN Odalar o ON mr.OdaId = o.OdaId where RezervasyonGecerlilikTarihi>= GETDATE()";
            cmd.Connection = con;
            SqlDataReader dr2 = cmd.ExecuteReader();
            while (dr2.Read())
            {
                string[] row = { dr2[0].ToString(), dr2[1].ToString(), dr2[2].ToString(), dr2[3].ToString(), dr2[4].ToString() };
                var satir = new ListViewItem(row);
                lwRezervasyon.Items.Add(satir);
            }
            con.Close();
        }


        private void cmbOdaTipi_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            try
            {
                OdaNoGetir.Clear();
                cmd.CommandText = "select o.OdaId,o.OdaNo From Odalar o JOIN OdaTipi ot ON o.OdaTipiId = ot.OdaTipiId where ot.OdaTipiID =" + cmbOdaTipi.SelectedValue;
                cmd.Connection = con;
                
                SqlDataReader dr2 = cmd.ExecuteReader();
                while (dr2.Read())
                {
                    OdaNoGetir.Add(new KeyValuePair<int , string>((int)dr2[0],(string)dr2[1]));
                }
                cmbOdaNo.DataSource = OdaNoGetir.ToList();
                cmbOdaNo.ValueMember = "Key";
                cmbOdaNo.DisplayMember = "Value"; 
                dr2.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

            
            con.Close();
        }

        private void cmbOdaNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            
            cmd.CommandText = "Select * from Odalar where OdaId="+cmbOdaNo.SelectedValue;
            cmd.Connection=con;

            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string[] gelen = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), dr[12].ToString(), dr[13].ToString(), dr[14].ToString(), dr[15].ToString() };

            odaid = gelen[0];
            lblebat.Text = gelen[1].ToString();
            lblfiyat.Text = gelen[3].ToString() + " TL";
            lblyatak.Text = gelen[4].ToString();
            chcmini.Checked = Convert.ToBoolean(gelen[5]);
            chcklima.Checked = Convert.ToBoolean(gelen[6]);
            chckurutma.Checked = Convert.ToBoolean(gelen[7]);
            chcwifi.Checked = Convert.ToBoolean(gelen[8]);
            chckasa.Checked = Convert.ToBoolean(gelen[9]);
            chcbalkon.Checked = Convert.ToBoolean(gelen[10]);
            chctv.Checked= Convert.ToBoolean(gelen[11]);
            lblkat.Text = gelen[15].ToString();
            dr.Close();
            con.Close();

        }

        private void btnRezervasyonkaydet_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            musteriID = "";
            if (txtTC.Text == String.Empty)
            {
                MessageBox.Show("Tc Numarasını Giriniz !");
            }
            else
            {
                
                cmd.CommandText = "Select MusteriID From Musteriler where MusteriTCKimlik="+txtTC.Text;
                cmd.Connection = con;
                con.Open();
                musteriID = cmd.ExecuteScalar().ToString();
                con.Close();
                

                con.Open();



                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertRezervasyon";
                cmd.Connection = con;
                cmd.Parameters.Add("@MusteriId", musteriID);
                cmd.Parameters.Add("@RezervasyonGecerlilikTarihi", DateBaslangic.Value);
                cmd.Parameters.Add("@RezervasyonGecerlilikSonTarihi", dateBitis.Value);
                cmd.Parameters.Add("@ErkenRezervasyonIndirim", Convert.ToInt32(txtErken.Text));
                cmd.Parameters.Add("@RezervasyonTipiId", 1);
                cmd.Parameters.Add("@RezervasyonAciklama", txtaciklama.Text);
                cmd.Parameters.Add("@RezervasyonIptalOk", "0");
                cmd.Parameters.Add("@odaID",odaid);
                lblSonuc.Text = cmd.ExecuteNonQuery().ToString() + " Adet Kayıt Eklendi";
                lblSonuc.ForeColor = Color.Green;
                
                con.Close();
                Temizle();
                
            }
            
        }

        void Temizle()
        {
            txtTC.Clear();
            txtaciklama.Clear();
            txtErken.Clear();
            dateBitis.Refresh();
            DateBaslangic.Refresh();
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            lwRezervasyon.Items.Clear();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.CommandText = "select m.MusteriAd + ' ' + m.MusteriSoyad AS AdSoyad , r.RezervasyonGecerlilikTarihi , r.RezervasyonGecerlilikSonTarihi ,m.MusteriTelefon ,o.OdaNo from Rezervasyon r JOIN MusteriRezervasyon mr ON r.RezervasyonId=mr.RezervasyonId JOIN Musteriler m ON mr.MusteriId=m.MusteriID JOIN Odalar o ON mr.OdaId = o.OdaId where RezervasyonGecerlilikTarihi>= GETDATE()";
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString() , dr[4].ToString() };
                var satir = new ListViewItem(row);
                lwRezervasyon.Items.Add(satir);
            }
            dr.Close();
            con.Close();
        }

        private void btnKisiAra_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            lwRezervasyon.Items.Clear();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.CommandText = "select m.MusteriAd + ' ' + m.MusteriSoyad AS AdSoyad , r.RezervasyonGecerlilikTarihi , r.RezervasyonGecerlilikSonTarihi ,m.MusteriTelefon ,o.OdaNo from Rezervasyon r JOIN MusteriRezervasyon mr ON r.RezervasyonId=mr.RezervasyonId JOIN Musteriler m ON mr.MusteriId=m.MusteriID JOIN Odalar o ON mr.OdaId = o.OdaId where RezervasyonGecerlilikTarihi>= GETDATE() AND MusteriTCKimlik='"+txtkisiTc.Text+"'";
            cmd.Connection = con;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString() , dr[4].ToString() };
                var satir = new ListViewItem(row);
                lwRezervasyon.Items.Add(satir);
            }
            dr.Close();
            con.Close();
            btnDetay.Enabled = true;
        }

        private void btnTarihAra_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            lwRezervasyon.Items.Clear();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.CommandText = "select m.MusteriAd + ' ' + m.MusteriSoyad AS AdSoyad , r.RezervasyonGecerlilikTarihi , r.RezervasyonGecerlilikSonTarihi ,m.MusteriTelefon ,o.OdaNo from Rezervasyon r JOIN MusteriRezervasyon mr ON r.RezervasyonId=mr.RezervasyonId JOIN Musteriler m ON mr.MusteriId=m.MusteriID JOIN Odalar o ON mr.OdaId = o.OdaId where RezervasyonGecerlilikTarihi between @baslangicTarihi AND @bitisTarihi";
            cmd.Parameters.AddWithValue("@baslangicTarihi",dateArabaslangic.Value);
            cmd.Parameters.AddWithValue("@bitisTarihi",dateArabitis.Value);
            cmd.Connection = con;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString() };
                var satir = new ListViewItem(row);
                lwRezervasyon.Items.Add(satir);
            }
            dr.Close();
            con.Close();
        }

        private void btnDetay_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            btnUpdate.Enabled = true;
            btnRezervasyonkaydet.Enabled = false;
            con.Open();
            cmd.CommandText = "select m.MusteriTCKimlik , o.OdaTipiId , o.OdaNo ,r.RezervasyonGecerlilikTarihi,r.RezervasyonGecerlilikSonTarihi,r.ErkenRezervasyonIndirim,r.RezervasyonAciklama , r.RezervasyonId , m.MusteriId from Rezervasyon r  JOIN MusteriRezervasyon mr ON r.RezervasyonId=mr.RezervasyonId  JOIN Musteriler m ON mr.MusteriId=m.MusteriID  JOIN Odalar o ON mr.OdaId = o.OdaId where m.MusteriTCKimlik = @TC";
            cmd.Parameters.AddWithValue("@tc",txtkisiTc.Text);
            SqlDataReader dr =cmd.ExecuteReader();
            dr.Read();
            string[] gelen = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString() };
            txtTC.Text = gelen[0];
            cmbOdaTipi.SelectedIndex=Convert.ToInt32( gelen[1])-1;
            DateBaslangic.Value = Convert.ToDateTime(gelen[3]);
            dateBitis.Value = Convert.ToDateTime(gelen[4]);
            txtErken.Text = gelen[5];
            txtaciklama.Text = gelen[6];
            rezervasyonID=gelen[7];
            musteriID=gelen[8];

            dr.Close();
            con.Close() ;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_UpdateRezervasyon";
            cmd.Connection = con;
            cmd.Parameters.Add("@MusteriID",musteriID);
            cmd.Parameters.Add("@RezervasyonGecerlilikTarihi", DateBaslangic.Value);
            cmd.Parameters.Add("@RezervasyonGecerlilikSonTarihi",dateBitis.Value);
            cmd.Parameters.Add("@ErkenRezervasyonIndirim",txtErken.Text);
            cmd.Parameters.Add("@RezervasyonTipiId", 1);
            cmd.Parameters.Add("@RezervasyonAciklama",txtaciklama.Text);
            cmd.Parameters.Add("@RezervasyonIptalOk", false);
            cmd.Parameters.Add("@RezervasyonId",rezervasyonID);
            lblSonuc.Text = cmd.ExecuteNonQuery().ToString() + " Adet Kayıt Güncellendi";
            lblSonuc.ForeColor = Color.Orange;
            con.Close();
        }

        private void lwRezervasyon_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select OdaId from Odalar where odano=" + lwRezervasyon.SelectedItems[0].SubItems[4].Text;
            cmd.Connection=con;
            SqlDataReader dr =cmd.ExecuteReader();
            dr.Read();
            odaid = dr[0].ToString();
            dr.Close();
            con.Close();
        }

        private void btnSatis_Click(object sender, EventArgs e)
        {
            SatisveFatura sf = new SatisveFatura();
            sf.sOdaID = odaid;

        }
    }
}
