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
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        string text1 = "Kullanıcı adı";
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == text1)
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = text1;
            }
        }

        string text2 = "Şifre";

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == text2)
            {
                textBox2.Text = "";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = text2;
            }
        }

        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_ADMIN", baglan.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        public string kullanici;

        void ListeleSart()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_ADMIN where KULLANICIADI='"+ kullanici +"'", baglan.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void Temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        
        private void Ayarlar_Load(object sender, EventArgs e)
        {
            if(kullanici=="admin")
            {
                Listele();
                Temizle();
                textBox1.Text = text1;
                textBox2.Text = text2;
            }
            else
            {
                ListeleSart();
            }
            
        }

        private void btnIslem_Click(object sender, EventArgs e)
        {
            if (btnIslem.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_ADMIN values (@p1,@p2)", baglan.baglanti());
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", textBox2.Text);
                komut.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Kullanıcı başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }

            if(btnIslem.Text=="Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("update TBL_ADMIN set SIFRE=@p1 where KULLANICIADI=@p2", baglan.baglanti());
                komut1.Parameters.AddWithValue("@p1", textBox2.Text);
                komut1.Parameters.AddWithValue("@p2", textBox1.Text);
                komut1.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Kullanıcı şifresi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListeleSart();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr!=null)
            {
                textBox1.Text = dr["KULLANICIADI"].ToString();
                textBox2.Text = dr["SIFRE"].ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(kullanici=="admin")
            {
                if (textBox1.Text != text1)
                {
                    if (textBox1.Text != "")
                    {
                        btnIslem.Text = "Güncelle";
                        btnIslem.BackColor = Color.LightCoral;
                    }
                }
                else
                {
                    btnIslem.Text = "Kaydet";
                    btnIslem.BackColor = Color.WhiteSmoke;
                }
            }

            else
            {
                btnIslem.Text = "Güncelle";
                btnIslem.BackColor = Color.LightCoral;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (kullanici == "admin")
            {
                Listele();
                Temizle();
                textBox1.Text = text1;
                textBox2.Text = text2;
            }
            else
            {
                ListeleSart();
            }
        }
    }
}
