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

namespace Gaz_Sensoru
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection("data source=ACER-BILGISAYAR;initial catalog=gazSensoru;integrated security=True;");
        butonLedYak frm = new butonLedYak();
        

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("Select *from giris where ad=@adi AND sifre=@sifresi", baglan);
                komut.Parameters.AddWithValue("@adi", textBox1.Text.Trim());
                komut.Parameters.AddWithValue("@sifresi", textBox2.Text.Trim());
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    butonLedYak af = new butonLedYak();
                    af.Show();
                    this.Hide();
                }
                else
                {
                    if (textBox1.Text != "A")
                        MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı");
                    baglan.Close();
                }
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
          
        }

        private class anaForm : butonLedYak
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
