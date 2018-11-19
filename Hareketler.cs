using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TicariOtomasyon
{
    public partial class Hareketler : Form
    {
        public Hareketler()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void FirmaListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec FirmaHareketleri", baglan.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        void MusteriListele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec MusteriHareketleri", baglan.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void Hareketler_Load(object sender, EventArgs e)
        {
            FirmaListele();
            MusteriListele();
        }
    }
}
