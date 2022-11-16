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
    public partial class Personeller : Form
    {
        public Personeller()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Server=.;Database=db_Bilgi_Hotel;Trusted_Connection=True;");
        SqlCommand cmd = new SqlCommand();

        List<KeyValuePair<int, string>> GorevGetir= new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> UlkeGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> SehirGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> IlceGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> CinsiyetGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> KategoriGetir = new List<KeyValuePair<int, string>>();
        private void Personeller_Load(object sender, EventArgs e)
        {

            lwPersonel.Clear();
            lwPersonel.View = View.Details;
            lwPersonel.GridLines = true;
            lwPersonel.FullRowSelect = true;

            lwPersonel.Columns.Add("ID", 50);
            lwPersonel.Columns.Add("Adı ", 200);
            lwPersonel.Columns.Add("Soyadı ", 200);
            lwPersonel.Columns.Add("Tc Kimlik No", 200);
            lwPersonel.Columns.Add("Doğum Tarihi", 200);
            lwPersonel.Columns.Add("Eposta", 200);
            lwPersonel.Columns.Add("Telefon", 200);
            lwPersonel.Columns.Add("Görevi ", 200);
            lwPersonel.Columns.Add("Adres ", 200);

            PersonelGetir();

            //Grev Getirme
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.CommandText = "select GorevId,GorevAd from Gorevler";
            cmd.Connection = con;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                GorevGetir.Add(new KeyValuePair<int, string>((int)dr[0], (string)dr[1].ToString()));
            }
            cmbAraGorev.DataSource = GorevGetir.ToList();
            cmbAraGorev.ValueMember = "Key";
            cmbAraGorev.DisplayMember = "Value";

            cmbGorev.DataSource = GorevGetir.ToList();
            cmbGorev.ValueMember = "Key";
            cmbGorev.DisplayMember = "Value";
            dr.Close();
            con.Close();

            //Ülke Getirme
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

            //Cinsiyet Getirme
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
            
            //Personel Kategorisi Getirme
            KategoriGetir.Clear();
            con.Open();
            cmd.CommandText = " select PersonelKategoriId,PersonelKategoriTip from PersonelKategori";
            cmd.Connection = con;

            SqlDataReader dr5 = cmd.ExecuteReader();
            while (dr5.Read())
            {
                KategoriGetir.Add(new KeyValuePair<int, string>((int)dr5[0], (string)dr5[1]));
            }
            cmbKategori.DataSource = KategoriGetir.ToList();
            cmbKategori.ValueMember = "Key";
            cmbKategori.DisplayMember = "Value";
            dr5.Close();
            con.Close();



        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            PersonelGetir();
        }

        void PersonelGetir()
        {
            cmd.CommandText = "select p.PersonelId,p.PersonelAd,p.PersonelSoyad,p.PersonelTcKimlik ,p.PersonelDogumTarihi, p.PersonelEposta , p.PersonelTelefon , g.GorevAd,p.PersonelAdres from Personel p JOIN Gorevler g ON p.GorevId=g.GorevId";
            cmd.Connection = con;
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString() };
                var satir = new ListViewItem(row);
                lwPersonel.Items.Add(satir);
            }
            dr.Close();
            con.Close();
        }
        void PersonelGetir(string Tc)
        {
             cmd.Parameters.Clear();
             lwPersonel.Items.Clear();
             cmd.CommandText = "select p.PersonelId,p.PersonelAd,p.PersonelSoyad,p.PersonelTcKimlik ,p.PersonelDogumTarihi, p.PersonelEposta , p.PersonelTelefon , g.GorevAd,p.PersonelAdres from Personel p JOIN Gorevler g ON p.GorevId=g.GorevId where PersonelTcKimlik= @TC OR g.GorevID = 1";
             cmd.Connection = con;
             cmd.Parameters.AddWithValue("@TC", Tc);
             cmd.Parameters.AddWithValue("@Gorev", cmbAraGorev.SelectedValue);
             con.Open();

             SqlDataReader dr = cmd.ExecuteReader();

             while (dr.Read())
             {
                 string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(),dr[8].ToString() };
                 var satir = new ListViewItem(row);
                 lwPersonel.Items.Add(satir);
             }
             dr.Close();
            
             con.Close();
            
           
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            string TcNO = "";

            if (txtAraTc.Text == string.Empty)
            {
                TcNO = "a";
            }
            else TcNO = txtAraTc.Text;

            PersonelGetir(TcNO);
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_PersonelEkle";
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Add("@PersonelAd",txtAd.Text);
            cmd.Parameters.Add("@PersonelSoyad", txtSoyad.Text);
            cmd.Parameters.Add("@PersonelTcKimlik", txtTc.Text);
            cmd.Parameters.Add("@PersonelDogumTarihi", dtDogum.Value);
            cmd.Parameters.Add("@PersonelUyrukId", 1);
            cmd.Parameters.Add("@PersonelEposta", txtEposta.Text);
            cmd.Parameters.Add("@PersonelTelefon", txtTelefon.Text);
            cmd.Parameters.Add("@PersonelPasaportNo", txtPasaport.Text);
            cmd.Parameters.Add("@CinsiyetId", cmbCinsiyet.SelectedValue);
            cmd.Parameters.Add("@PersonelIseGirisTarihi", dtIseGiris.Value);
            cmd.Parameters.Add("@PersonelIstenCikisTarihi", dtIstenCikis.Value);
            cmd.Parameters.Add("@PersonelSaatlikUcret", txtSaatlik.Text);
            cmd.Parameters.Add("@PersonelMaas", txtMaas.Text);
            cmd.Parameters.Add("@PersonelSicilNo", txtSicilNo.Text);
            cmd.Parameters.Add("@GorevId", cmbGorev.SelectedValue);
            cmd.Parameters.Add("@PersonelKategoriID", cmbKategori.SelectedValue);
            cmd.Parameters.Add("@PersonelEngelDurumu", chcEngel.Checked);
            cmd.Parameters.Add("@IlId", cmbSehir.SelectedValue);
            cmd.Parameters.Add("@IlceId", cmbIlce.SelectedValue);
            cmd.Parameters.Add("@UlkeId", cmbUlke.SelectedValue);
            cmd.Parameters.Add("@PersonelAdres", txtAdres.Text);
            cmd.Parameters.Add("@PersonelAcilDurumKisiAd", txtAcilAd.Text);
            cmd.Parameters.Add("@PersonelAcilDurumKisiTelefon", txtAcilNo.Text);
            cmd.Parameters.Add("@ResimId", Convert.ToInt32(txtResim.Text));

            lblSonuc.Text=cmd.ExecuteNonQuery().ToString()+" Adet Kayıt Eklendi";
            lblSonuc.ForeColor = Color.Green;
            con.Close();
        }

        private void cmbUlke_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            SehirGetir.Clear();
            con.Open();
            cmd.CommandText = " Select s.IlId,s.IlAd from Ulke u JOIN Sehir s ON u.UlkeId=s.UlkeId Where u.UlkeId=@ulke";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ulke", cmbUlke.SelectedValue);

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
            cmd.Parameters.Clear();
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
    }
}
