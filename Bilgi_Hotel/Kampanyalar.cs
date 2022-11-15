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
    public partial class Kampanyalar : Form
    {
        public Kampanyalar()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Server=.;Database=db_Bilgi_Hotel;Trusted_Connection=True;");
        SqlCommand cmd = new SqlCommand();

        int kampanyaID;
        private void Kampanyalar_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;

            lwKampanyalar.Clear();
            lwKampanyalar.View = View.Details;
            lwKampanyalar.GridLines = true;
            lwKampanyalar.FullRowSelect = true;

            lwKampanyalar.Columns.Add("Kampanya ID", 200);
            lwKampanyalar.Columns.Add("Kampanya Adı", 200);
            lwKampanyalar.Columns.Add("Kampanya Oranı", 200);
            lwKampanyalar.Columns.Add("Başlangıç Tarihi", 200);
            lwKampanyalar.Columns.Add("Bitiş Tarihi", 200);
            lwKampanyalar.Columns.Add("Kampanya Tanımı", 200);


            con.Open();
            cmd.CommandText = "select * From Kampanyalar";
            cmd.Connection=con;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString() };
                var satir = new ListViewItem(row);
                lwKampanyalar.Items.Add(satir);
            }
            dr.Close();
            con.Close();
        }

        private void lwKampanyalar_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            groupBox1.Enabled = false;

            con.Open();
            cmd.CommandText= "Select * from Kampanyalar where KampanyaId =" + lwKampanyalar.SelectedItems[0].SubItems[0].Text;
            cmd.Connection=con;
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            string[] gelen = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString() };
            kampanyaID=Convert.ToInt32( gelen[0]);
            txtUKampanyaAd.Text= gelen[1];
            txtUkampanyaOran.Text= gelen[2];
            dtUbaslangic.Text= gelen[3];
            dtUbitis.Text= gelen[4];
            txtUtanım.Text= gelen[5];

            dr.Close() ;
            con.Close() ;

        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_InsertKampanyalar";
            cmd.Connection = con;

            cmd.Parameters.Add("@KampanyaBilgileri", txtAd.Text);
            cmd.Parameters.Add("@KampanyaIndirimOran", txtOran.Text);
            cmd.Parameters.Add("@KampanyaBaslangicZaman", dtBaslangic.Value);
            cmd.Parameters.Add("@KampanyaBitisTarihi", dtBitis.Value);
            cmd.Parameters.Add("@KampanyaTanim", txtTanim.Text);

            lblSonuc.Text = cmd.ExecuteNonQuery().ToString() + " Adet Kayıt Eklendi";
            lblSonuc.ForeColor = Color.Green;
            con.Close();

            txtAd.Clear();
            txtOran.Clear();
            txtTanim.Clear();
            dtBaslangic.Text = string.Empty;
            dtBitis.Text = string.Empty;
        }

        private void btnYKampanyaOlustur_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;

            txtUKampanyaAd.Clear();
            txtUkampanyaOran.Clear();
            txtUtanım.Clear();
            dtUbaslangic.Text = string.Empty;
            dtUbitis.Text = string.Empty;
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            lwKampanyalar.Items.Clear();
            con.Open();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText = "select * From Kampanyalar";
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string[] row = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString() };
                var satir = new ListViewItem(row);
                lwKampanyalar.Items.Add(satir);
            }
            dr.Close();
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_UpdateKampanyalar";
            cmd.Connection = con;

            cmd.Parameters.Add("@KampanyaId",kampanyaID);
            cmd.Parameters.Add("@KampanyaBilgileri", txtUKampanyaAd.Text);
            cmd.Parameters.Add("@KampanyaIndirimOran", txtUkampanyaOran.Text);
            cmd.Parameters.Add("@KampanyaBaslangicZaman", dtUbaslangic.Value);
            cmd.Parameters.Add("@KampanyaBitisTarihi", dtUbitis.Value);
            cmd.Parameters.Add("@KampanyaTanim", txtUtanım.Text);

            lblSonuc.Text = cmd.ExecuteNonQuery().ToString() + " Adet Kayıt Güncellendi";
            lblSonuc.ForeColor = Color.Orange;
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_DeleteKampanyalar";
            cmd.Connection = con;

            cmd.Parameters.Add("@KampanyaId", kampanyaID);
            lblSonuc.Text = cmd.ExecuteNonQuery().ToString() + " Adet Kayıt Silindi";
            lblSonuc.ForeColor = Color.Red;
            con.Close();
        }
    }
}
