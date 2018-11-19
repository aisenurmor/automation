using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace TicariOtomasyon
{
    public partial class Mail : Form
    {
        public Mail()
        {
            InitializeComponent();
        }

        public string mail;

        private void Mail_Load(object sender, EventArgs e)
        {
            txtAdres.Text = mail;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mesaj = new MailMessage();
                SmtpClient istemci = new SmtpClient();
                istemci.Credentials = new System.Net.NetworkCredential("aisenurmor@gmail.com", "41201881.");
                istemci.Port = 587;
                istemci.Host = "smtp.gmail.com";
                istemci.EnableSsl = true;
                mesaj.To.Add(txtAdres.Text);
                mesaj.From = new MailAddress("aisenurmor@gmail.com");
                mesaj.Subject = txtKonu.Text;
                mesaj.Body = rtxtMesaj.Text;
                istemci.Send(mesaj);
                MessageBox.Show("Mesajınız gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Mesajınız gönderilemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
