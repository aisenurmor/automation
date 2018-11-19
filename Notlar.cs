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
    public partial class Notlar : Form
    {
        public Notlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void Temizle()
        {
            txtBaslik.Text = "";
            txtOlusturan.Text = "";
            txtHitap.Text = "";
            mtxtTarih.Text = "";
            mtxtSaat.Text = "";
            txtId.Text = "";
            rtxtDetay.Text = "";

        }

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_NOTLAR", baglan.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void Notlar_Load(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_NOTLAR (TARIH,SAAT,BASLIK,DETAY,OLUSTURAN,HITAP) values (@P1,@P2,@P3,@P4,@P5,@P6)", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", mtxtTarih.Text);
            komut.Parameters.AddWithValue("@P2", mtxtSaat.Text);
            komut.Parameters.AddWithValue("@P3", txtBaslik.Text);
            komut.Parameters.AddWithValue("@P4", rtxtDetay.Text);
            komut.Parameters.AddWithValue("@P5", txtOlusturan.Text);
            komut.Parameters.AddWithValue("@P6", txtHitap.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Not bilgisi başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
            Temizle();
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
                SqlCommand komut = new SqlCommand("Delete From TBL_NOTLAR where ID=@p1", baglan.baglanti());
                komut.Parameters.AddWithValue("@p1", txtId.Text);
                komut.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Not bilgisi silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Listele();
                Temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr!=null)
            {
                txtId.Text = dr["ID"].ToString();
                mtxtTarih.Text = dr["TARIH"].ToString();
                mtxtSaat.Text = dr["SAAT"].ToString();
                txtBaslik.Text = dr["BASLIK"].ToString();
                txtOlusturan.Text = dr["OLUSTURAN"].ToString();
                txtHitap.Text = dr["HITAP"].ToString();
                rtxtDetay.Text = dr["DETAY"].ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update TBL_NOTLAR set TARIH=@P1, SAAT=@P2, BASLIK=@P3, DETAY=@P4, OLUSTURAN=@P5, HITAP=@P6 WHERE ID=@P7", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", mtxtTarih.Text);
            komut.Parameters.AddWithValue("@P2", mtxtSaat.Text);
            komut.Parameters.AddWithValue("@P3", txtBaslik.Text);
            komut.Parameters.AddWithValue("@P4", rtxtDetay.Text);
            komut.Parameters.AddWithValue("@P5", txtOlusturan.Text);
            komut.Parameters.AddWithValue("@P6", txtHitap.Text);
            komut.Parameters.AddWithValue("@P7", txtId.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Not bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
            Temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            NotDetay not = new NotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr!=null)
            {
                not.detay = rtxtDetay.Text;
                not.Show();
            }
        }
    }
}
