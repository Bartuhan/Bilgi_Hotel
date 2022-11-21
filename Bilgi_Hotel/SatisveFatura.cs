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
    public partial class SatisveFatura : Form
    {
        public SatisveFatura()
        {
            InitializeComponent();
        }
        public string sOdaID;
        public string sMusteriID;
        public DateTime sBaslangic;
        public DateTime sBitis;
        int toplamgun;
        SqlConnection con =new SqlConnection("Server=.;Database=db_Bilgi_Hotel;Trusted_Connection=True;");
        SqlCommand cmd = new SqlCommand();
        private void SatisveFatura_Load(object sender, EventArgs e)
        {
            if (sBitis>DateTime.Today)
            {
                dtBaslangıc.Value = sBaslangic;
                dtBitis.Value = sBitis;
            }
            con.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select OdaSatisTipAd from OdaSatisTip";
            cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbPansiyon.Items.Add(dr[0].ToString());
            }
            cmbPansiyon.SelectedIndex = -1;
            dr.Close();
            con.Close();
        }

        private void dtBitis_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan ts = dtBitis.Value-dtBaslangıc.Value;

            txtToplam.Text = ts.Days.ToString();
            toplamgun = Convert.ToInt32(txtToplam.Text);

        }

        
    }
}
