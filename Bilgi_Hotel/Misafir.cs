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
    public partial class Misafir : Form
    {
        public Misafir()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Server=.;Database=db_Bilgi_Hotel;Trusted_Connection=True;");
        SqlCommand cmd = new SqlCommand();

        string odaNo = "";
        public static DateTime odaGiris,odaCikis;
        public static int odaID;

        List<KeyValuePair<int, string>> OdaTipiGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> OdaNoGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> UlkeGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> CinsiyetGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> SehirGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> IlceGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> DilGetir = new List<KeyValuePair<int, string>>();

        private void Misafir_Load(object sender, EventArgs e)
        {
            // ListView'i Satır ve Sütunlara Bölme
            listView1.Clear();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("Oda Numarası", 200);
            listView1.Columns.Add("Oda Yatak Tipi", 200);
            listView1.Columns.Add("Oda Fiyatı", 200);
            listView1.Columns.Add("Oda Durumu", 200);
            listView1.Columns.Add("Oda Açıklaması", 200);
            listView1.Columns.Add("Müşteri Ad Soyad", 250);

            // OdaTipi ve Oda Numarasını Veri Tabanından Çekme
            groupBox1.Enabled = false;
            if (con.State == ConnectionState.Closed)
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

            //Odalar Ve Durumlarını Veri Tabanından Çekip ListViewde Gösterme
            listView1.Items.Clear();
            con.Open();
            cmd.CommandText = "select o.OdaNo , o.OdaYatakTipi , o.OdaFiyat , d.DurumKategoriAd , d.DurumKategoriAciklama , m.MisafirAd+' '+m.MisafirSoyad AS AdSoyad from Odalar O LEFT JOIN OdaDurum od ON o.OdaId=od.OdaId LEFT JOIN DurumKategori d ON od.DurumKategoriId=d.DurumKategoriId LEFT JOIN MisafirOda mo ON o.OdaId=mo.OdaId LEFT JOIN Misafir m ON mo.MisafirId=m.MisafirId Order by o.OdaNo ";
            cmd.Connection = con;

            SqlDataReader dr2 = cmd.ExecuteReader();
            while (dr2.Read())
            {
                string[] row = { dr2[0].ToString(), dr2[1].ToString(), dr2[2].ToString(), dr2[3].ToString(), dr2[4].ToString(), dr2[5].ToString() };
                var satir = new ListViewItem(row);
                listView1.Items.Add(satir);
            }
            dr2.Close();
            con.Close();

            //Ülke , Cinsiyet , Dil ComboBoxların Verilerini Doldurma
            //Ülke Doldurma
            UlkeGetir.Clear();
            con.Open();
            cmd.CommandText = " Select UlkeId,UlkeAd from Ulke ";
            cmd.Connection = con;

            SqlDataReader dr3 = cmd.ExecuteReader();
            while (dr3.Read())
            {
                UlkeGetir.Add(new KeyValuePair<int, string>((int)dr3[0], (string)dr3[1]));
            }
            cmbUlke.DataSource = UlkeGetir.ToList();
            cmbUlke.ValueMember = "Key";
            cmbUlke.DisplayMember = "Value";
            dr3.Close();
            con.Close();

            // Cinsiyet Doldurma
            CinsiyetGetir.Clear();
            con.Open();
            cmd.CommandText = " Select CinsiyetId,CinsiyetAd From Cinsiyet";
            cmd.Connection = con;

            SqlDataReader dr4 = cmd.ExecuteReader();
            while (dr4.Read())
            {
                CinsiyetGetir.Add(new KeyValuePair<int, string>((int)dr4[0], (string)dr4[1]));
            }
            cmbCinsiyet.DataSource = CinsiyetGetir.ToList();
            cmbCinsiyet.ValueMember = "Key";
            cmbCinsiyet.DisplayMember = "Value";
            dr4.Close();
            con.Close();

            //Anadil Doldurma
            DilGetir.Clear();
            con.Open();
            cmd.CommandText = " select DilId,DilAd from Diller";
            cmd.Connection = con;

            SqlDataReader dr5 = cmd.ExecuteReader();
            while (dr5.Read())
            {
                DilGetir.Add(new KeyValuePair<int, string>((int)dr5[0], (string)dr5[1]));
            }
            cmbDil.DataSource = DilGetir.ToList();
            cmbDil.ValueMember = "Key";
            cmbDil.DisplayMember = "Value";
            dr5.Close();
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
                    OdaNoGetir.Add(new KeyValuePair<int, string>((int)dr2[0], (string)dr2[1]));
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

        private void btnOda_Click(object sender, EventArgs e)
        {
            odaID = (int)cmbOdaNo.SelectedValue;
            groupBox1.Enabled = true;
            odaGiris = dtOdaGiris.Value;
            odaCikis = dtOdaCikis.Value;
        }

        private void btnBosOda_Click(object sender, EventArgs e)
        {
            OdaDurumu("Müsait");
        }

        private void btnTemizlik_Click(object sender, EventArgs e)
        {
            OdaDurumu("Temizleniyor");
        }

        void OdaDurumu(string gelen)
        {
            cmd.Parameters.Clear();
            listView1.Items.Clear();
            con.Open();
            cmd.CommandText = "select o.OdaNo , o.OdaYatakTipi , o.OdaFiyat , d.DurumKategoriAd , d.DurumKategoriAciklama from Odalar O  JOIN OdaDurum od ON o.OdaId=od.OdaId JOIN DurumKategori d ON od.DurumKategoriId=d.DurumKategoriId where d.DurumKategoriAd=@durum Order by o.OdaNo ";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@durum", gelen);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString() };
                var satir = new ListViewItem(row);
                listView1.Items.Add(satir);
            }
            dr.Close();
            con.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_InsertMisafirler";
            cmd.Parameters.Add("@MisafirAd", txtad.Text);
            cmd.Parameters.Add("@MisafirSoyad", txtsoyad.Text);
            cmd.Parameters.Add("@MisafirTcKimlik", txttc.Text);
            cmd.Parameters.Add("@MisafirDogumTarihi", dtDogumTarih.Value);
            cmd.Parameters.Add("@MisafirUyrukId", 1);
            cmd.Parameters.Add("@MisafirEposta", txteposta.Text);
            cmd.Parameters.Add("@dilId", cmbDil.SelectedValue);
            cmd.Parameters.Add("@MisafirTelefon", txtTelefon.Text);
            cmd.Parameters.Add("@MisafirPasaportNo", txtpasaport.Text);
            cmd.Parameters.Add("@CinsiyetId", cmbCinsiyet.SelectedValue);
            cmd.Parameters.Add("@MisafirAdres", txtAdres.Text);
            cmd.Parameters.Add("@IlId", cmbSehir.SelectedValue);
            cmd.Parameters.Add("@IlceId", cmbIlce.SelectedValue);
            cmd.Parameters.Add("@UlkeId", cmbUlke.SelectedValue);
            cmd.Parameters.Add("@MisafirAciklama", txtAciklama.Text);

            lblSonuc.Text= cmd.ExecuteNonQuery().ToString() + " Adet Kayıt Eklendi";
            lblSonuc.ForeColor = Color.Green;
            con.Close();
            
        }

        private void cmbUlke_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SehirGetir.Clear();
            con.Open();
            cmd.CommandText = " Select s.IlId,s.IlAd from Ulke u JOIN Sehir s ON u.UlkeId=s.UlkeId Where u.UlkeId=@ulke";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ulke",cmbUlke.SelectedValue);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SehirGetir.Add(new KeyValuePair<int, string>((int)dr[0], (string)dr[1]));
            }
            cmbSehir.DataSource = SehirGetir.ToList();
            cmbSehir.ValueMember = "Key";
            cmbSehir.DisplayMember = "Value";
            dr.Close();
            con.Close();
        }

        private void cmbSehir_SelectionChangeCommitted(object sender, EventArgs e)
        {
            IlceGetir.Clear();
            con.Open();
            cmd.CommandText = "select i.IlceId,i.IlceAd from Sehir il JOIN Ilce i ON i.IlId = il.IlId where il.IlId =@ilce";
            cmd.Parameters.AddWithValue("@ilce", cmbSehir.SelectedValue);
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

        private void button1_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            listView1.Items.Clear();
            con.Open();
            cmd.CommandText = "select o.OdaNo , o.OdaYatakTipi , o.OdaFiyat , d.DurumKategoriAd , d.DurumKategoriAciklama , m.MisafirAd+' '+m.MisafirSoyad AS AdSoyad from Odalar O LEFT JOIN OdaDurum od ON o.OdaId=od.OdaId LEFT JOIN DurumKategori d ON od.DurumKategoriId=d.DurumKategoriId LEFT JOIN MisafirOda mo ON o.OdaId=mo.OdaId LEFT JOIN Misafir m ON mo.MisafirId=m.MisafirId Order by o.OdaNo";
            cmd.Connection = con;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString() };
                var satir = new ListViewItem(row);
                listView1.Items.Add(satir);
            }
            dr.Close();
            con.Close();
        }
    }
}
