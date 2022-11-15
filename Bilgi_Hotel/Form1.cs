using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bilgi_Hotel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        AnaSayfa ana = new AnaSayfa();
        Musteriler mst = new Musteriler();
        Rezervasyon rezerv = new Rezervasyon();
        Misafir mis = new Misafir();
        SatisveFatura sat = new SatisveFatura();
        Kampanyalar kam = new Kampanyalar();
        Personeller pers = new Personeller();
        Odalar oda = new Odalar();
        Vardiyalar var = new Vardiyalar();

        Login lg = new Login();


        void FormDegis(Form acilacak)
        {

            switch (acilacak.Name)
            {
                case "AnaSayfa":
                    acilacak.MdiParent = this; acilacak.Show(); mst.Hide(); rezerv.Hide(); mis.Hide(); sat.Hide(); kam.Hide(); pers.Hide(); oda.Hide(); var.Hide(); break;
                case "Musteriler":
                    acilacak.MdiParent = this; acilacak.Show(); ana.Hide(); rezerv.Hide(); mis.Hide(); sat.Hide(); kam.Hide(); pers.Hide(); oda.Hide(); var.Hide(); break;
                case "Rezervasyon":
                    acilacak.MdiParent = this; acilacak.Show(); mst.Hide(); ana.Hide(); mis.Hide(); sat.Hide(); kam.Hide(); pers.Hide(); oda.Hide(); var.Hide(); break;
                case "Misafir":
                    acilacak.MdiParent= this; acilacak.Show(); mst.Hide(); rezerv.Hide(); sat.Hide(); kam.Hide(); pers.Hide(); oda.Hide(); var.Hide(); ana.Hide(); break;
                case "SatisveFatura":
                    acilacak.MdiParent = this; acilacak.Show(); mst.Hide(); rezerv.Hide(); mis.Hide(); ana.Hide(); kam.Hide(); pers.Hide(); oda.Hide(); var.Hide(); break;
                case "Kampanyalar":
                    acilacak.MdiParent = this; acilacak.Show(); mst.Hide(); rezerv.Hide(); mis.Hide(); sat.Hide(); ana.Hide(); pers.Hide(); oda.Hide(); var.Hide(); break;
                case "Personeller":
                    acilacak.MdiParent = this; acilacak.Show(); mst.Hide(); rezerv.Hide(); mis.Hide(); sat.Hide(); kam.Hide(); ana.Hide(); oda.Hide(); var.Hide(); break;
                case "Odalar":
                    acilacak.MdiParent = this; acilacak.Show(); mst.Hide(); rezerv.Hide(); mis.Hide(); sat.Hide(); kam.Hide(); pers.Hide(); ana.Hide(); var.Hide(); break;
                case "Vardiyalar":
                    acilacak.MdiParent = this; acilacak.Show(); mst.Hide(); rezerv.Hide(); mis.Hide(); sat.Hide(); kam.Hide(); pers.Hide(); oda.Hide(); ana.Hide(); break;

            }

        }

        private void tsAnaSayfa_Click(object sender, EventArgs e)
        {
            FormDegis(ana);
            
        }

        private void tsMusteriler_Click(object sender, EventArgs e)
        {
            FormDegis(mst);
        }

        private void tsRezervasyon_Click(object sender, EventArgs e)
        {
            FormDegis(rezerv);
        }

        private void tsMisafirler_Click(object sender, EventArgs e)
        {
            FormDegis(mis);
        }

        private void tsSatis_Click(object sender, EventArgs e)
        {
            FormDegis(sat);
        }

        private void tsKampanya_Click(object sender, EventArgs e)
        {
            FormDegis(kam);
        }

        private void tsPersonel_Click(object sender, EventArgs e)
        {
            FormDegis(pers);
        }

        private void tsOdalar_Click(object sender, EventArgs e)
        {
            FormDegis(oda);
        }

        private void tsVardiya_Click(object sender, EventArgs e)
        {
            FormDegis(var);
        }

        private void tsCıkıs_Click(object sender, EventArgs e)
        {
            this.Dispose();
            lg.Show();
        }
    }
}
