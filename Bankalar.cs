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
    public partial class Bankalar : Form
    {
        public Bankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri", baglan.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void illeriGetir()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", baglan.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cbil.Properties.Items.Add(dr[0]);   //normal cb olsaydı properties yazmaya gerek yoktu.
            }
            baglan.baglanti().Close();
        }

        void Temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSube.Text = "";
            mtxtTel.Text = "";
            mtxtTarih.Text = "";
            txthTuru.Text = "";
            txtIban.Text = "";
            cbil.Text = "";     //selecteditem yaparsak temizlemiyor.
            cbilce.Text = "";
            txtHesap.Text = "";
            txtYetkili.Text = "";
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        void firmagetir()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD from TBL_FIRMALAR", baglan.baglanti());
            da.Fill(dt);
            lkeFirma.Properties.ValueMember = "ID";
            lkeFirma.Properties.DisplayMember = "AD";
            lkeFirma.Properties.DataSource = dt;
        }

        private void Bankalar_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
            illeriGetir();
            firmagetir();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_BANKALAR (ILBANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", cbil.SelectedItem);
            komut.Parameters.AddWithValue("@p3", cbilce.SelectedItem);
            komut.Parameters.AddWithValue("@p4", txtSube.Text);
            komut.Parameters.AddWithValue("@p5", txtIban.Text);
            komut.Parameters.AddWithValue("@p6", txtHesap.Text);
            komut.Parameters.AddWithValue("@p7", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", mtxtTel.Text);
            komut.Parameters.AddWithValue("@p9", mtxtTarih.Text);
            komut.Parameters.AddWithValue("@p10", txthTuru.Text);
            komut.Parameters.AddWithValue("@p11", lkeFirma.EditValue);
            komut.ExecuteNonQuery();
            Listele();
            baglan.baglanti().Close();
            MessageBox.Show("Banka bilgisi başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();
        }

        private void cbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbilce.Properties.Items.Clear();
            cbilce.Text = "";

            SqlCommand komut = new SqlCommand("Select ILCE From TBL_ILCELER where SEHIR=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", cbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cbilce.Properties.Items.Add(dr[0]);
            }
            baglan.baglanti().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);      //tıklanan satırdaki veriyi getir
            if (dr != null)
            {
                txtId.Text = dr["ID"].ToString();
                txtAd.Text = dr["ILBANKAADI"].ToString();
                cbil.Text = dr["IL"].ToString();
                cbilce.Text = dr["ILCE"].ToString();
                txtSube.Text = dr["SUBE"].ToString();
                txtIban.Text = dr["IBAN"].ToString();
                txtHesap.Text = dr["HESAPNO"].ToString();
                txtYetkili.Text = dr["YETKILI"].ToString();
                mtxtTel.Text = dr["TELEFON"].ToString();
                mtxtTarih.Text = dr["TARIH"].ToString();
                txthTuru.Text = dr["HESAPTURU"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult Secim = new DialogResult();

            Secim = MessageBox.Show("Gerçekten silmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_BANKALAR where ID=@p1", baglan.baglanti());
                komut.Parameters.AddWithValue("@p1", txtId.Text);
                komut.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Banka başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Listele();
                Temizle();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_BANKALAR set ILBANKAADI=@p1, IL=@p2, ILCE=@p3, SUBE=@p4, IBAN=@p5, HESAPNO=@p6, YETKILI=@p7, TELEFON=@p8, TARIH=@p9, HESAPTURU=@p10, FIRMAID=@p11 where ID=@p12", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", cbil.Text);
            komut.Parameters.AddWithValue("@p3", cbilce.Text);
            komut.Parameters.AddWithValue("@p4", txtSube.Text);
            komut.Parameters.AddWithValue("@p5", txtIban.Text);
            komut.Parameters.AddWithValue("@p6", txtHesap.Text);
            komut.Parameters.AddWithValue("@p7", txtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", mtxtTel.Text);
            komut.Parameters.AddWithValue("@p9", mtxtTarih.Text);
            komut.Parameters.AddWithValue("@p10", txthTuru.Text);
            komut.Parameters.AddWithValue("@p11", lkeFirma.EditValue);
            komut.Parameters.AddWithValue("@p12", txtId.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Müşteri bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
            Temizle();

        }

    }
}
