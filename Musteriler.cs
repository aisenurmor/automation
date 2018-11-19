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
    public partial class Musteriler : Form
    {
        public Musteriler()
        {
            InitializeComponent();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_MUSTERILER", baglan.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void illeriGetir()
        {
            SqlCommand komut = new SqlCommand("Select SEHIR From TBL_ILLER", baglan.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                cbil.Properties.Items.Add(dr[0]);   //normal cb olsaydı properties yazmaya gerek yoktu.
            }
            baglan.baglanti().Close();
        }

        void Temizle()
        {
            txtId.Text = "";
            txtAd.Text = "";
            txtSoyad.Text = "";
            mtxtTel1.Text = "";
            mtxtTel2.Text = "";
            mtxtTC.Text = "";
            txtMail.Text = "";
            cbil.Text = "";     //selecteditem yaparsak temizlemiyor.
            cbilce.Text = "";
            rtxtAdres.Text = "";
            txtVergi.Text = "";
        }

        private void Musteriler_Load(object sender, EventArgs e)
        {
            Listele();
            illeriGetir();
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert into TBL_MUSTERILER (AD, SOYAD, TELEFON, TELEFON2, TC, MAIL, IL, ILCE, ADRES, VERGIDAIRE) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10)", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mtxtTel1.Text);
            komut.Parameters.AddWithValue("@p4", mtxtTel2.Text);
            komut.Parameters.AddWithValue("@p5", mtxtTC.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", cbil.SelectedItem);        //cbil.Text de olur.
            komut.Parameters.AddWithValue("@p8", cbilce.SelectedItem);
            komut.Parameters.AddWithValue("@p9", rtxtAdres.Text);
            komut.Parameters.AddWithValue("@p10", txtVergi.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Müşteri başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
        }

        private void cbilce_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbilce.Properties.Items.Clear();
            cbilce.Text = "";

            SqlCommand komut = new SqlCommand("Select ILCE From TBL_ILCELER where SEHIR=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", cbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                cbilce.Properties.Items.Add(dr[0]);
            }
            baglan.baglanti().Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult Secim = new DialogResult();

            Secim = MessageBox.Show("Gerçekten silmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (Secim == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete From TBL_MUSTERILER where ID=@p1", baglan.baglanti());
                komut.Parameters.AddWithValue("@p1", txtId.Text);
                komut.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Müşteri silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Listele();
                Temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);      //tıklanan satırdaki veriyi getir
            if(dr!=null)
            {
                txtId.Text = dr["ID"].ToString();
                txtAd.Text = dr["AD"].ToString();
                txtSoyad.Text = dr["SOYAD"].ToString();
                mtxtTel1.Text = dr["TELEFON"].ToString();
                mtxtTel2.Text = dr["TELEFON2"].ToString();
                mtxtTC.Text = dr["TC"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cbil.Text = dr["IL"].ToString();
                cbilce.Text = dr["ILCE"].ToString();
                rtxtAdres.Text = dr["ADRES"].ToString();
                txtVergi.Text = dr["VERGIDAIRE"].ToString();
            }
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_MUSTERILER set AD=@p1, SOYAD=@p2, TELEFON=@p3, TELEFON2=@p4, TC=@p5, MAIL=@p6, IL=@p7, ILCE=@p8, ADRES=@p9, VERGIDAIRE=@p10 where ID=@p11", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", txtAd.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mtxtTel1.Text);
            komut.Parameters.AddWithValue("@p4", mtxtTel2.Text);
            komut.Parameters.AddWithValue("@p5", mtxtTC.Text);
            komut.Parameters.AddWithValue("@p6", txtMail.Text);
            komut.Parameters.AddWithValue("@p7", cbil.SelectedItem);
            komut.Parameters.AddWithValue("@p8", cbil.SelectedItem);
            komut.Parameters.AddWithValue("@p9", rtxtAdres.Text);
            komut.Parameters.AddWithValue("@p10", txtVergi.Text);
            komut.Parameters.AddWithValue("@p11", txtId.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Müşteri bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }
    }
}
