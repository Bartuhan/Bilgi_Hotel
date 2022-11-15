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
        private void Personeller_Load(object sender, EventArgs e)
        {
            cmd.CommandText = "Select * From Personel";
            cmd.Connection = con;
            con.Open();

            lwPersonel.Clear();
            lwPersonel.View = View.Details;
            lwPersonel.GridLines = true;
            lwPersonel.FullRowSelect = true;
           
            lwPersonel.Columns.Add("ID", 200);
            lwPersonel.Columns.Add("Adı ", 200);
            lwPersonel.Columns.Add("Soyadı ", 200);
            lwPersonel.Columns.Add("Tc Kimlik No", 200);
            lwPersonel.Columns.Add("Doğum Tarihi", 200);
            lwPersonel.Columns.Add("Eposta", 200);
            lwPersonel.Columns.Add("Telefon", 200);
            lwPersonel.Columns.Add("Adres ", 200);
            lwPersonel.Columns.Add("Görevi ", 200);
            
        }
    }
}
