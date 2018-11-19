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
    public partial class AltAnaSayfa : Form
    {
        public AltAnaSayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();
        void Stoklar()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select URUNAD,sum(ADET) as 'ADET' from TBL_URUNLER group by URUNAD having sum(ADET)<=20 order by sum(ADET)", baglan.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlStok.DataSource = dt;
        }

        void Ajanda()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("Select TOP 8 TARIH,SAAT,BASLIK from TBL_NOTLAR order by ID desc", baglan.baglanti());
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            gridControlAjanda.DataSource = dt1;
        }

        void FirmaHareketler()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("Exec FirmaHareketleri2", baglan.baglanti());
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            gridControlHareket.DataSource = dt2;
        }

        void Rehber()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select AD,TELEFON1 from TBL_FIRMALAR", baglan.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControlFirma.DataSource = dt;
        }

        private void AltAnaSayfa_Load(object sender, EventArgs e)
        {
            Stoklar();
            Ajanda();
            FirmaHareketler();
            Rehber();
            webBrowser1.Navigate("http://www.tcmb.gov.tr/kurlar/today.xml");
            webBrowser2.Navigate("https://www.youtube.com/");
            webBrowser3.Navigate("https://www.ntv.com.tr/");
        }
    }
}
