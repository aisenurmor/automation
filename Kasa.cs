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
using DevExpress.Charts;

namespace TicariOtomasyon
{
    public partial class Kasa : Form
    {
        public Kasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_GIDERLER", baglan.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        void musteriHListele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketleri", baglan.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void firmaHListele()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("Execute FirmaHareketleri", baglan.baglanti());
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            gridControl3.DataSource = dt1;
        }

        void ToplamTutar()
        {
            SqlCommand komut = new SqlCommand("Select sum(TUTAR) from TBL_FATURADETAY", baglan.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                lblToplamT.Text = dr[0].ToString() + " TL";
            }
            baglan.baglanti().Close();
        }

        void Giderler()
        {
            SqlCommand komut1 = new SqlCommand("Select (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) from TBL_GIDERLER order by ID asc", baglan.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while(dr1.Read())
            {
                lblOdemeT.Text = dr1[0].ToString() + " TL";
            }
            baglan.baglanti().Close();
        }

        void Maaslar()
        {
            SqlCommand komut2 = new SqlCommand("Select MAASLAR from TBL_GIDERLER order by ID asc", baglan.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lblMaas.Text = dr2[0].ToString() + " TL";
            }
            baglan.baglanti().Close();
        }

        void MusteriSay()
        {
            SqlCommand komut3 = new SqlCommand("select Count(ID) from TBL_MUSTERILER", baglan.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                lblMusteri.Text = dr3[0].ToString();
            }
            baglan.baglanti().Close();
        }

        void FirmaSay()
        {
            SqlCommand komut4 = new SqlCommand("Select Count(ID) from TBL_FIRMALAR", baglan.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblFirma.Text = dr4[0].ToString();
            }
            baglan.baglanti().Close();
        }

        void FSehir()
        {
            SqlCommand komut5 = new SqlCommand("Select Count(Distinct(IL)) from TBL_FIRMALAR", baglan.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblFsehir.Text = dr5[0].ToString();
            }
            baglan.baglanti().Close();
        }

        void MSehir()
        {
            SqlCommand komut6 = new SqlCommand("Select Count(Distinct(IL)) from TBL_MUSTERILER", baglan.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblMsehir.Text = dr6[0].ToString();
            }
            baglan.baglanti().Close();
        }

        void PersonelSay()
        {
            SqlCommand komut7 = new SqlCommand("Select Count(ID) from TBL_PERSONELLER", baglan.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                lblPersonel.Text = dr7[0].ToString();
            }
            baglan.baglanti().Close();
        }

        void StokSay()
        {
            SqlCommand komut8 = new SqlCommand("Select sum(ADET) from TBL_URUNLER", baglan.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                lblStok.Text = dr8[0].ToString();
            }
            baglan.baglanti().Close();
        }

        void Elektrik()
        {
            SqlCommand komut9 = new SqlCommand("Select top 5 AY,ELEKTRIK from TBL_GIDERLER order by ID desc", baglan.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while(dr9.Read())
            {
                chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr9[0], dr9[1]));
            }
            baglan.baglanti().Close();
        }

        void Su()
        {
            SqlCommand komut10 = new SqlCommand("Select top 5 AY,SU from TBL_GIDERLER order by ID desc", baglan.baglanti());
            SqlDataReader dr10 = komut10.ExecuteReader();
            while (dr10.Read())
            {
                chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
            }
            baglan.baglanti().Close();
        }

        void Dogalgaz()
        {
            SqlCommand komut11 = new SqlCommand("Select top 5 AY,DOGALGAZ from TBL_GIDERLER order by ID desc", baglan.baglanti());
            SqlDataReader dr11 = komut11.ExecuteReader();
            while (dr11.Read())
            {
                chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
            }
            baglan.baglanti().Close();
        }

        void Internet()
        {
            SqlCommand komut12 = new SqlCommand("Select top 5 AY,INTERNET from TBL_GIDERLER order by ID desc", baglan.baglanti());
            SqlDataReader dr12 = komut12.ExecuteReader();
            while (dr12.Read())
            {
                chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr12[0], dr12[1]));
            }
            baglan.baglanti().Close();
        }

        void Ekstra()
        {
            SqlCommand komut13 = new SqlCommand("Select top 5 AY,EKSTRA from TBL_GIDERLER order by ID desc", baglan.baglanti());
            SqlDataReader dr13 = komut13.ExecuteReader();
            while (dr13.Read())
            {
                chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr13[0], dr13[1]));
            }
            baglan.baglanti().Close();
        }


        public string kullanici;
        private void Kasa_Load(object sender, EventArgs e)
        {
            lblKullanici.Text = kullanici;
            musteriHListele();
            firmaHListele();
            ToplamTutar();
            Giderler();
            Maaslar();
            MusteriSay();
            FirmaSay();
            FSehir();
            MSehir();
            PersonelSay();
            StokSay();
            Listele();
        }

        public int sayac;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            //Elektrik faturaları için
            if(sayac>0 && sayac<=5)
            {
                chartControl1.Series["Aylar"].Points.Clear();
                groupControl10.Text = "Elektrik";
                Elektrik();
            }
            //Su faturaları için
            if(sayac>5 && sayac<=10)
            {
                chartControl1.Series["Aylar"].Points.Clear();
                groupControl10.Text = "Su";
                Su();
            }
            //Doğalgaz faturaları için
            if (sayac > 10 && sayac <= 15)
            {
                chartControl1.Series["Aylar"].Points.Clear();
                groupControl10.Text = "Doğalgaz";
                Dogalgaz();
            }

            if(sayac==16)
            {
                sayac = 0;
            }
        }
        public int sayac2;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            //Doğalgaz faturaları için
            if (sayac2 > 0 && sayac2 <= 5)
            {
                chartControl2.Series["Aylar"].Points.Clear();
                groupControl11.Text = "Doğalgaz";
                SqlCommand komut14 = new SqlCommand("Select top 5 AY,DOGALGAZ from TBL_GIDERLER order by ID desc", baglan.baglanti());
                SqlDataReader dr14 = komut14.ExecuteReader();
                while (dr14.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr14[0], dr14[1]));
                }
                baglan.baglanti().Close();
            }
            //İnternet faturaları için
            if (sayac2 > 5 && sayac2 <= 10)
            {
                chartControl2.Series["Aylar"].Points.Clear();
                groupControl11.Text = "İnternet";
                Internet();
            }
            //Ekstralar için
            if (sayac2 > 10 && sayac2 <= 15)
            {
                chartControl2.Series["Aylar"].Points.Clear();
                groupControl11.Text = "Ekstra";
                Ekstra();
            }

            if (sayac2 == 16)
            {
                sayac2 = 0;
            }
        }
    }
}
