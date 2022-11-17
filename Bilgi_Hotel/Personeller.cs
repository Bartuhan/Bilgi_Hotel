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
        string personelID;
        string uyruk;

        List<KeyValuePair<int, string>> GorevGetir= new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> UlkeGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> SehirGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> IlceGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> CinsiyetGetir = new List<KeyValuePair<int, string>>();
        List<KeyValuePair<int, string>> KategoriGetir = new List<KeyValuePair<int, string>>();
        private void Personeller_Load(object sender, EventArgs e)
        {
            grpPersonel.Enabled = false;

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

            //Gorev Getirme
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmd.CommandType = CommandType.Text;
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
            cmbAraGorev.SelectedIndex = -1;

            //Ülke Getirme
            UlkeGetir.Clear();
            con.Open();
            cmd.CommandType = CommandType.Text;
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
            cmbUlke.SelectedIndex = -1;

            //Cinsiyet Getirme
            CinsiyetGetir.Clear();
            con.Open();
            cmd.CommandType = CommandType.Text;
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
            cmbCinsiyet.SelectedIndex = -1;

            //Personel Kategorisi Getirme
            KategoriGetir.Clear();
            con.Open();
            cmd.CommandType = CommandType.Text;
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
            cmbKategori.SelectedIndex = -1;

            cmbGorev.SelectedIndex = -1;

        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            lwPersonel.Items.Clear();
            cmd.Parameters.Clear();
            PersonelGetir();
        }

        void PersonelGetir()
        {
            cmd.Parameters.Clear();
            cmd.CommandType= CommandType.Text;
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
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select p.PersonelId,p.PersonelAd,p.PersonelSoyad,p.PersonelTcKimlik ,p.PersonelDogumTarihi, p.PersonelEposta , p.PersonelTelefon , g.GorevAd,p.PersonelAdres from Personel p JOIN Gorevler g ON p.GorevId=g.GorevId where p.PersonelTcKimlik= @TC OR g.GorevID = @Gorev";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@TC", Tc);
            cmd.Parameters.AddWithValue("@Gorev", cmbAraGorev.SelectedValue is null ? DBNull.Value: cmbAraGorev.SelectedValue) ;
             

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
            Temizle();
        }

        private void cmbUlke_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbDoldur(cmbSehir, SehirGetir, "Select s.IlId,s.IlAd from Ulke u JOIN Sehir s ON u.UlkeId=s.UlkeId Where u.UlkeId="+cmbUlke.SelectedValue);

            //cmd.Parameters.Clear();
            //SehirGetir.Clear();
            //con.Open();
            //cmd.CommandText = " Select s.IlId,s.IlAd from Ulke u JOIN Sehir s ON u.UlkeId=s.UlkeId Where u.UlkeId=@ulke";
            //cmd.Connection = con;
            //cmd.Parameters.AddWithValue("@ulke", cmbUlke.SelectedValue);

            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    SehirGetir.Add(new KeyValuePair<int, string>((int)dr[0], (string)dr[1]));
            //}
            //cmbSehir.DataSource = SehirGetir.ToList();
            //cmbSehir.ValueMember = "Key";
            //cmbSehir.DisplayMember = "Value";
            //dr.Close();
            //con.Close();
        }

        private void cmbSehir_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbDoldur(cmbIlce, IlceGetir, "select i.IlceId,i.IlceAd from Sehir il JOIN Ilce i ON i.IlId = il.IlId where il.IlId ="+cmbSehir.SelectedValue);

            //cmd.Parameters.Clear();
            //IlceGetir.Clear();
            //con.Open();
            //cmd.CommandText = "select i.IlceId,i.IlceAd from Sehir il JOIN Ilce i ON i.IlId = il.IlId where il.IlId =@ilce";
            //cmd.Parameters.AddWithValue("@ilce", cmbSehir.SelectedValue);
            //cmd.Connection = con;

            //SqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    IlceGetir.Add(new KeyValuePair<int, string>((int)dr[0], (string)dr[1]));
            //}
            //cmbIlce.DataSource = IlceGetir.ToList();
            //cmbIlce.ValueMember = "Key";
            //cmbIlce.DisplayMember = "Value";
            //dr.Close();
            //con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd.Parameters.Clear();
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.CommandText = "sp_UpdatePersonel";
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
            cmd.Parameters.Add("@PersonelSaatlikUcret",Convert.ToDecimal( txtSaatlik.Text));
            cmd.Parameters.Add("@PersonelMaas",Convert.ToDecimal( txtMaas.Text));
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
            cmd.Parameters.Add("@ResimId", txtResim.Text);
            cmd.Parameters.Add("@PersonelId",personelID);

            lblSonuc.Text = cmd.ExecuteNonQuery().ToString()+" Adet Kayıt Güncellendi";
            lblSonuc.ForeColor = Color.Orange;
            con.Close();


        }

        private void lwPersonel_Click(object sender, EventArgs e)
        {
            grpPersonel.Enabled = true;
            btnKaydet.Enabled = false;
            btnDelete.Enabled = true;
            btnUpdate.Enabled = true;

            CmbDoldur(cmbUlke, UlkeGetir, "Select UlkeId,UlkeAd from Ulke ");
            CmbDoldur(cmbSehir, SehirGetir, "Select IlID,IlAd from Sehir ");
            CmbDoldur(cmbIlce, IlceGetir, "Select IlceId,IlceAd from Ilce ");
            CmbDoldur(cmbKategori,KategoriGetir, "select PersonelKategoriId,PersonelKategoriTip from PersonelKategori");

            con.Open();
            cmd.Parameters.Clear();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText = "Select * From Personel Where PersonelId =" + lwPersonel.SelectedItems[0].SubItems[0].Text;
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string[] gelen = {dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), dr[12].ToString(), dr[13].ToString(), dr[14].ToString(), dr[15].ToString(), dr[16].ToString(), dr[17].ToString(), dr[18].ToString(), dr[19].ToString(), dr[20].ToString(), dr[21].ToString(), dr[22].ToString(), dr[23].ToString(), dr[24].ToString()};
            personelID = gelen[0];
            txtAd.Text = gelen[1];
            txtSoyad.Text = gelen[2];
            txtTc.Text = gelen[3];
            dtDogum.Value = Convert.ToDateTime( gelen[4]);
            uyruk = gelen[5];
            txtEposta.Text= gelen[6];
            txtTelefon.Text= gelen[7];
            txtPasaport.Text= gelen[8];
            cmbCinsiyet.SelectedIndex =Convert.ToInt32 (gelen[9])-1;
            dtIseGiris.Value = Convert.ToDateTime( gelen[10]);
            dtIstenCikis.Value = Convert.ToDateTime(gelen[11]);
            txtSaatlik.Text= gelen[12];
            txtMaas.Text= gelen[13];
            txtSicilNo.Text= gelen[14];
            cmbGorev.SelectedIndex= Convert.ToInt32(gelen[15]) - 1;
            cmbKategori.SelectedIndex=Convert.ToInt32 (gelen[16])-1;
            chcEngel.Checked = Convert.ToBoolean( gelen[17]);
            cmbSehir.SelectedIndex= Convert.ToInt32(gelen[18]) - 1;
            cmbIlce.SelectedIndex= Convert.ToInt32(gelen[19]) - 1;
            cmbUlke.SelectedIndex= Convert.ToInt32(gelen[20]) - 1;
            txtAdres.Text= gelen[21];
            txtAcilAd.Text= gelen[22];
            txtAcilNo.Text= gelen[23];
            txtResim.Text= gelen[24];
            dr.Close();
            con.Close();


        }
        void CmbDoldur(ComboBox ComboAd, List<KeyValuePair<int, string>> KeyListem,string cmdText) 
        {
            cmd.Parameters.Clear();
            KeyListem.Clear();
            con.Open();
            cmd.CommandText = cmdText;
            cmd.Connection = con;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                KeyListem.Add(new KeyValuePair<int, string>((int)dr[0], (string)dr[1]));
            }
            ComboAd.DataSource = KeyListem.ToList();
            ComboAd.ValueMember = "Key";
            ComboAd.DisplayMember = "Value";
            dr.Close();
            con.Close();
        }
        void Temizle() 
        {
            txtAd.Clear();
            txtSoyad.Clear();
            txtTc.Clear();
            dtDogum.Value = DateTime.Now;
            txtEposta.Clear();
            txtTelefon.Clear();
            txtPasaport.Clear();
            cmbCinsiyet.SelectedIndex = -1;
            dtIseGiris.Value = DateTime.Now;
            dtIstenCikis.Value = DateTime.Now;
            txtSaatlik.Clear();
            txtMaas.Clear();
            txtSicilNo.Clear();
            cmbGorev.SelectedIndex=-1;
            cmbKategori.SelectedIndex = -1;
            chcEngel.Checked = false;
            cmbSehir.SelectedIndex = -1;
            cmbIlce.SelectedIndex = -1;
            cmbUlke.SelectedIndex = -1;
            txtAdres.Clear();
            txtAcilAd.Clear();
            txtAcilNo.Clear();
            txtResim.Clear();

        }

        private void btnYeniPersonel_Click(object sender, EventArgs e)
        {
            Temizle();
            grpPersonel.Enabled = true;
            btnKaydet.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
    }
}
