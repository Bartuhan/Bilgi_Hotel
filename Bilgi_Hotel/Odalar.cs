using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bilgi_Hotel
{
    public partial class Odalar : Form
    {
        public Odalar()
        {
            InitializeComponent();
        }
        SqlConnection con =new SqlConnection("Server=.;Database=db_Bilgi_Hotel;Trusted_Connection=True;");
        SqlCommand cmd = new SqlCommand();
        List<KeyValuePair<int, string>> OdaTipiGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> DurumGetir = new List<KeyValuePair<int, string>>();

        string OdaID;

        private void Odalar_Load(object sender, EventArgs e)
        {
            listView1.Clear();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("Oda Numarası", 120);
            listView1.Columns.Add("Oda Yatak Tipi", 120);
            listView1.Columns.Add("Oda Fiyatı", 120);
            listView1.Columns.Add("Oda Durumu", 120);
            listView1.Columns.Add("Oda Açıklaması", 200);
            listView1.Columns.Add("Müşteri Ad Soyad", 250);

            CmbDoldur(cmbOdaTipi, OdaTipiGetir, "Select OdaTipiId,OdaTipiAd From OdaTipi");
            CmbDoldur(cmbOdaDurum, DurumGetir, "Select DurumKategoriId,DurumKategoriAd From DurumKategori");

            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select OdaTipiAciklama from OdaTipi ";
            cmd.Connection = con;
            
            SqlDataReader dr2 = cmd.ExecuteReader();
            while (dr2.Read())
            {
                cmbYatakTipi.Items.Add(dr2[0].ToString());
            }
            dr2.Close();
            con.Close();

            cmbYatakTipi.SelectedIndex = -1;


            con.Open();
            cmd.Parameters.Clear();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText = "select o.OdaNo , o.OdaYatakTipi , o.OdaFiyat , d.DurumKategoriAd , d.DurumKategoriAciklama , m.MisafirAd + ' ' + m.MisafirSoyad AS AdSoyad from Odalar O LEFT JOIN OdaDurum od ON o.OdaId = od.OdaId LEFT JOIN DurumKategori d ON od.DurumKategoriId = d.DurumKategoriId LEFT JOIN MisafirOda mo ON o.OdaId = mo.OdaId LEFT JOIN Misafir m ON mo.MisafirId = m.MisafirId Order by o.OdaNo";
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString() };
                var satir = new ListViewItem(row);
                listView1.Items.Add(satir);
            }
            dr.Close();
            con.Close();

        }
        void CmbDoldur(ComboBox Cmbadi, List<KeyValuePair<int, string>> KeyValueadi,string Text)
        {
            cmd.Parameters.Clear();
            KeyValueadi.Clear();
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = Text;
            cmd.Connection = con;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                KeyValueadi.Add(new KeyValuePair<int, string>((int)dr[0], (string)dr[1]));
            }
            Cmbadi.DataSource=KeyValueadi.ToList();
            Cmbadi.ValueMember = "Key";
            Cmbadi.DisplayMember = "Value";
            Cmbadi.SelectedIndex = -1;
            dr.Close();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_InsertOdalar";
            cmd.Connection = con;
            cmd.Parameters.Add("@OdaEbatMsqr",txtEbat.Text);
            cmd.Parameters.Add("@OdaTipiId", cmbOdaTipi.SelectedValue);
            cmd.Parameters.Add("@OdaFiyat",Convert.ToDecimal( txtOdaFiyat.Text));
            cmd.Parameters.Add("@OdaYatakTipi", cmbYatakTipi.SelectedItem);
            cmd.Parameters.Add("@OdaMiniBarOk", chcMiniBar.Checked);
            cmd.Parameters.Add("@OdaKlimaOk", chcKlima.Checked);
            cmd.Parameters.Add("@OdaKurutmaOk", chcKurutma.Checked);
            cmd.Parameters.Add("@OdaWifiOk", chcWifi.Checked);
            cmd.Parameters.Add("@OdaKasaOk", chcKasa.Checked);
            cmd.Parameters.Add("@OdaBalkonOk", chcBalkon.Checked);
            cmd.Parameters.Add("@OdaTvOk", chcTV.Checked);
            cmd.Parameters.Add("@OdaAciklama", txtAciklama.Text);
            cmd.Parameters.Add("@OdaEbatBoyut", txtEbat.Text);
            cmd.Parameters.Add("@OdaNo", txtOdaNo.Text);
            cmd.Parameters.Add("@OdaKat", txtKat.Text);

            lblSonuc.Text = cmd.ExecuteNonQuery().ToString() + " Adet Kayıt Eklendi";
            lblSonuc.ForeColor = Color.Green;
            con.Close();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            con.Open();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText = "Select * from Odalar where OdaNo = " + listView1.SelectedItems[0].SubItems[0].Text;
            cmd.Connection = con;

            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string[] gelen = { dr[0].ToString() , dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), dr[12].ToString(), dr[13].ToString(), dr[14].ToString(), dr[15].ToString() };

            OdaID = gelen[0];
            txtEbat.Text = gelen[1];
            cmbOdaTipi.SelectedIndex = Convert.ToInt32(gelen[2]) - 1;
            txtOdaFiyat.Text= gelen[3];
            cmbYatakTipi.SelectedItem = gelen[4];
            chcMiniBar.Checked = Convert.ToBoolean(gelen[5]);
            chcKlima.Checked = Convert.ToBoolean(gelen[6]);
            chcKurutma.Checked = Convert.ToBoolean(gelen[7]);
            chcWifi.Checked = Convert.ToBoolean(gelen[8]);
            chcKasa.Checked = Convert.ToBoolean(gelen[9]);
            chcBalkon.Checked = Convert.ToBoolean(gelen[10]);
            chcTV.Checked = Convert.ToBoolean(gelen[11]);
            txtAciklama.Text = gelen[12];
            txtOdaNo.Text = gelen[14];
            txtAraOdaNo.Text = gelen[14];
            txtKat.Text=gelen[15];
            dr.Close();
            con.Close();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_UpdateOdalar";
            cmd.Connection = con;
            cmd.Parameters.Add("@OdaEbatMsqr", txtEbat.Text);
            cmd.Parameters.Add("@OdaTipiId", cmbOdaTipi.SelectedValue);
            cmd.Parameters.Add("@OdaFiyat", Convert.ToDecimal(txtOdaFiyat.Text));
            cmd.Parameters.Add("@OdaYatakTipi", cmbYatakTipi.SelectedItem);
            cmd.Parameters.Add("@OdaMiniBarOk", chcMiniBar.Checked);
            cmd.Parameters.Add("@OdaKlimaOk", chcKlima.Checked);
            cmd.Parameters.Add("@OdaKurutmaOk", chcKurutma.Checked);
            cmd.Parameters.Add("@OdaWifiOk", chcWifi.Checked);
            cmd.Parameters.Add("@OdaKasaOk", chcKasa.Checked);
            cmd.Parameters.Add("@OdaBalkonOk", chcBalkon.Checked);
            cmd.Parameters.Add("@OdaTvOk", chcTV.Checked);
            cmd.Parameters.Add("@OdaAciklama", txtAciklama.Text);
            cmd.Parameters.Add("@OdaEbatBoyut", txtEbat.Text);
            cmd.Parameters.Add("@OdaNo", txtOdaNo.Text);
            cmd.Parameters.Add("@OdaKat", txtKat.Text);
            cmd.Parameters.Add("@OdaId", OdaID);

            lblSonuc.Text = cmd.ExecuteNonQuery().ToString() + " Adet Kayıt Güncellendi";
            lblSonuc.ForeColor = Color.Orange;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            con.Open();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select o.OdaNo , o.OdaYatakTipi , o.OdaFiyat , d.DurumKategoriAd , d.DurumKategoriAciklama , m.MisafirAd + ' ' + m.MisafirSoyad AS AdSoyad from Odalar O LEFT JOIN OdaDurum od ON o.OdaId = od.OdaId LEFT JOIN DurumKategori d ON od.DurumKategoriId = d.DurumKategoriId LEFT JOIN MisafirOda mo ON o.OdaId = mo.OdaId LEFT JOIN Misafir m ON mo.MisafirId = m.MisafirId Order by o.OdaNo";
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString() };
                var satir = new ListViewItem(row);
                listView1.Items.Add(satir);
            }
            dr.Close();
            con.Close();
        }

        private void btnDurumKaydet_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_OdaDurum";
            cmd.Connection=con;

            cmd.Parameters.Add("@odaID",OdaID);
            cmd.Parameters.Add("@durumKategoriID", cmbOdaDurum.SelectedValue);
            lblSonuc2.Text=cmd.ExecuteNonQuery().ToString()+ " Adet Durum Düzenlendi";
            lblSonuc2.ForeColor=Color.Green;
            con.Close();
        }
    }
}
