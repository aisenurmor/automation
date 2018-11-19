using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicariOtomasyon
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        AltAnaSayfa anasayfa;
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(anasayfa==null)
            {
                anasayfa = new AltAnaSayfa();
                anasayfa.MdiParent = this;
                anasayfa.Show();
            }
        }

        Urunler urunler;

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (urunler == null)
            {      //eğer urunler açılmadıysa aç yani urunler nesnesi alttaki satırdaki gibi tanımlanmadıysa aç.
                urunler = new Urunler();
                urunler.MdiParent = this;
                urunler.Show();
            }
        }

        Firmalar firmalar;

        private void btnFirmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (firmalar == null)
            {
                firmalar = new Firmalar();
                firmalar.MdiParent = this;
                firmalar.Show();
            }
        }

        Musteriler musteriler;
        private void btnMusteriler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (musteriler == null)
            {
                musteriler = new Musteriler();
                musteriler.MdiParent = this;
                musteriler.Show();
            }
        }

        Rehber rehber;

        private void btnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (rehber == null)
            {
                rehber = new Rehber();
                rehber.MdiParent = this;
                rehber.Show();
            }
        }

        Personeller personeller;

        private void btnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (personeller == null)
            {
                personeller = new Personeller();
                personeller.MdiParent = this;
                personeller.Show();
            }
        }

        Giderler giderler;

        private void btnGiderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (giderler == null)
            {
                giderler = new Giderler();
                giderler.MdiParent = this;
                giderler.Show();
            }
        }

        Bankalar banka;

        private void btnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(banka==null)
            {
                banka = new Bankalar();
                banka.MdiParent = this;
                banka.Show();
            }
        }

        Faturalar faturalar;

        private void btnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (faturalar==null)
            {
                faturalar = new Faturalar();
                faturalar.MdiParent = this;
                faturalar.Show();
            }
        }

        Notlar not;
        private void btnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(not==null)
            {
                not = new Notlar();
                not.MdiParent = this;
                not.Show();
            }
        }
        Hareketler hareket;
        private void btnHareketler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(hareket==null)
            {
                hareket = new Hareketler();
                hareket.MdiParent = this;
                hareket.Show();
            }
        }
        Raporlar rapor;
        private void btnRaporlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(rapor==null)
            {
                rapor = new Raporlar();
                rapor.MdiParent = this;
                rapor.Show();
            }
        }
        Stoklar stok;
        private void btnStok_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(stok==null)
            {
                stok = new Stoklar();
                stok.MdiParent = this;
                stok.Show();
            }
        }
        public string kullanici;
        Ayarlar ayar;
        private void btnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ayar = new Ayarlar();
            ayar.kullanici = kullanici;
            ayar.Show();
        }
        Kasa kasa;
        private void btnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (kasa == null)
            {
                kasa = new Kasa();
                kasa.kullanici = kullanici;
                kasa.MdiParent = this;
                kasa.Show();
            }
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            if (anasayfa == null)
            {
                anasayfa = new AltAnaSayfa();
                anasayfa.MdiParent = this;
                anasayfa.Show();
            }
        }
    }
}
