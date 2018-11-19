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
    public partial class Faturalar : Form
    {
        public Faturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        double ToplamTutar(int adet)
        {
            double tutar = 0;
            double fiyat = Convert.ToDouble(txtFiyat.Text);
            tutar = Convert.ToDouble(adet) * fiyat;
            return tutar;
        }

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURABILGI", baglan.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void Temizle()
        {
            txtId.Text = "";
            txtSira.Text = "";
            txtSeri.Text = "";
            txtAlici.Text = "";
            mtxtTarih.Text = "";
            mtxtSaat.Text = "";
            txtVergiD.Text = "";
            txtTeslimAlan.Text = "";
            txtUrunid.Text = "";
            txtUAd.Text = "";     //selecteditem yapınca temizlemiyor.
            txtFaturaId.Text = "";
            txtFiyat.Text = "";
            txtTutar.Text = "";
            txtMiktar.Text = "";
            txtPersonel.Text = "";
            txtFirma.Text = "";
            txtTeslimEden.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int miktar = Convert.ToInt32(txtMiktar.Text);
            double toplam = ToplamTutar(miktar);
            txtTutar.Text = toplam.ToString();
        }

        private void Faturalar_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
            txtId.Enabled = false;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtFaturaId.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_FATURABILGI (SERI, SIRANO, TARIH, SAAT, VERGIDAIRE, ALICI, TESLIMEDEN, TESLIMALAN) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", baglan.baglanti());
                komut.Parameters.AddWithValue("@P1", txtSeri.Text);
                komut.Parameters.AddWithValue("@P2", txtSira.Text);
                komut.Parameters.AddWithValue("@P3", mtxtTarih.Text);
                komut.Parameters.AddWithValue("@P4", mtxtSaat.Text);
                komut.Parameters.AddWithValue("@P5", txtVergiD.Text);
                komut.Parameters.AddWithValue("@P6", txtAlici.Text);
                komut.Parameters.AddWithValue("@P7", txtTeslimEden.Text);
                komut.Parameters.AddWithValue("@P8", txtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Firma faturası başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }

            //Firma carisi
            if(txtFaturaId.Text!="" && comboBox1.Text=="Firma")
            {
                int miktar = Convert.ToInt32(txtMiktar.Text);
                double toplam = ToplamTutar(miktar);
                txtTutar.Text = toplam.ToString();

                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD, MIKTAR, FIYAT, TUTAR,FATURAID) VALUES (@P1,@P2,@P3,@P4,@P5)", baglan.baglanti());
                komut2.Parameters.AddWithValue("@P1", txtUAd.Text);
                komut2.Parameters.AddWithValue("@P2", txtMiktar.Text);
                komut2.Parameters.AddWithValue("@P3", decimal.Parse(txtFiyat.Text));
                komut2.Parameters.AddWithValue("@P4", decimal.Parse(txtTutar.Text));
                komut2.Parameters.AddWithValue("@P5", txtFaturaId.Text);
                komut2.ExecuteNonQuery();
                baglan.baglanti().Close();

                //Hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into TBL_FIRMAHAREKETLER (Urunıd,adet,personel,fırma,fıyat,toplam,faturaıd,tarıh) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", baglan.baglanti());
                komut3.Parameters.AddWithValue("@h1", txtUrunid.Text);
                komut3.Parameters.AddWithValue("@h2", txtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", txtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", txtFirma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(txtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(txtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", txtFaturaId.Text);
                komut3.Parameters.AddWithValue("@h8", mtxtTarih.Text);
                komut3.ExecuteNonQuery();
                baglan.baglanti().Close();

                //Stoktan düşme
                SqlCommand komut4 = new SqlCommand("update TBL_URUNLER set adet=adet-@s1 where ID=@s2", baglan.baglanti());
                komut4.Parameters.AddWithValue("@s1", txtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", txtUrunid.Text);
                komut4.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Fatura detay bilgileri başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
            
            //Müşteri carisi
            if (txtFaturaId.Text != "" && comboBox1.Text=="Müşteri")
            {
                int miktar = Convert.ToInt32(txtMiktar.Text);
                double toplam = ToplamTutar(miktar);
                txtTutar.Text = toplam.ToString();

                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD, MIKTAR, FIYAT, TUTAR,FATURAID) VALUES (@P1,@P2,@P3,@P4,@P5)", baglan.baglanti());
                komut2.Parameters.AddWithValue("@P1", txtUAd.Text);
                komut2.Parameters.AddWithValue("@P2", txtMiktar.Text);
                komut2.Parameters.AddWithValue("@P3", decimal.Parse(txtFiyat.Text));
                komut2.Parameters.AddWithValue("@P4", decimal.Parse(txtTutar.Text));
                komut2.Parameters.AddWithValue("@P5", txtFaturaId.Text);
                komut2.ExecuteNonQuery();
                baglan.baglanti().Close();

                //Hareket tablosuna veri girişi
                SqlCommand komut3 = new SqlCommand("insert into TBL_MUSTERIHAREKETLER (Urunıd,adet,personel,musterı,fiyat,toplam,faturaıd,tarıh) values (@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", baglan.baglanti());
                komut3.Parameters.AddWithValue("@h1", txtUrunid.Text);
                komut3.Parameters.AddWithValue("@h2", txtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", txtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", txtFirma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(txtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(txtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", txtFaturaId.Text);
                komut3.Parameters.AddWithValue("@h8", mtxtTarih.Text);
                komut3.ExecuteNonQuery();
                baglan.baglanti().Close();

                //Stoktan düşme
                SqlCommand komut4 = new SqlCommand("update TBL_URUNLER set adet=adet-@s1 where ID=@s2", baglan.baglanti());
                komut4.Parameters.AddWithValue("@s1", txtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", txtUrunid.Text);
                komut4.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Fatura detay bilgileri başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr!=null)
            {
                txtId.Text = dr["FATURABILGIID"].ToString();
                txtSeri.Text = dr["SERI"].ToString();
                txtSira.Text = dr["SIRANO"].ToString();
                mtxtTarih.Text = dr["TARIH"].ToString();
                mtxtSaat.Text = dr["SAAT"].ToString();
                txtVergiD.Text = dr["VERGIDAIRE"].ToString();
                txtAlici.Text = dr["ALICI"].ToString();
                txtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                txtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult Secim = new DialogResult();

            Secim = MessageBox.Show("Gerçekten silmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_FATURABILGI where FATURABILGIID=@p1", baglan.baglanti());
                komut.Parameters.AddWithValue("@p1", txtId.Text);
                komut.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Fatura başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Listele();
                Temizle();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_FATURABILGI set SERI=@p1, SIRANO=@p2, TARIH=@p3, SAAT=@p4, VERGIDAIRE=@p5, ALICI=@p6, TESLIMEDEN=@p7, TESLIMALAN=@p8 where FATURABILGIID=@p9", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", txtSeri.Text);
            komut.Parameters.AddWithValue("@p2", txtSira.Text);
            komut.Parameters.AddWithValue("@p3", mtxtTarih.Text);
            komut.Parameters.AddWithValue("@p4", mtxtSaat.Text);
            komut.Parameters.AddWithValue("@p5", txtVergiD.Text);
            komut.Parameters.AddWithValue("@p6", txtAlici.Text);
            komut.Parameters.AddWithValue("@p7", txtTeslimEden.Text);
            komut.Parameters.AddWithValue("@p8", txtTeslimAlan.Text);
            komut.Parameters.AddWithValue("@p9", txtId.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Fatura bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
            Temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FaturaUrunler fatura = new FaturaUrunler();

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr!=null)
            {
                fatura.id = dr["FATURABILGIID"].ToString();
            }
            fatura.Show();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select URUNAD, SATISFIYAT from TBL_URUNLER where ID=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", txtUrunid.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                txtFiyat.Text = dr[1].ToString();
                txtUAd.Text = dr[0].ToString();
            }
            baglan.baglanti().Close();
        }
    }
}
