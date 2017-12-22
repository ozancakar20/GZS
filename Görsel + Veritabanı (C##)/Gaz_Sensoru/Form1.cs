using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;

namespace Gaz_Sensoru
{
    public partial class butonLedYak : Form
    {
        public butonLedYak()
        {
            InitializeComponent();
        }

        SqlConnection baglan = new SqlConnection("data source=ACER-BILGISAYAR;initial catalog=gazSensoru;integrated security=True;");
        Veritabanı vrtbn = new Veritabanı();
        public void verileriGoster()
        {
            vrtbn.listView1.Items.Clear();
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *from Kayitlar2",baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while(oku.Read())
            {
                
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["tarih"].ToString();
                ekle.SubItems.Add(oku["veri"].ToString());
                vrtbn.listView1.Items.Add(ekle);
            }
            baglan.Close();

        }


        private void Form1_Load(object sender, EventArgs e)
        {         
            string [] portlar = SerialPort.GetPortNames();
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
            }
            CheckForIllegalCrossThreadCalls = false;
            verileriGoster();
        }

        private void btnBaglan_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    MessageBox.Show("Bağlantı daha önce Kurulmuş!!!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button3);
                }

                else
                {
                    serialPort1.BaudRate = 9600;
                    serialPort1.DataBits = 8;
                    serialPort1.StopBits = System.IO.Ports.StopBits.One;
                    serialPort1.Parity = Parity.None;
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    MessageBox.Show("Bağlantı Kuruldu");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Cihazın Bağlı olduğu portu seçiniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
            }
        }
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[1];
            serialPort1.Read(buffer, 0, 0);
            string gelen = serialPort1.ReadExisting();
           // MessageBox.Show(gelen);
            string uyari="";
            if (gelen == "a")
            {
                uyari = "Gaz Durumu Normal";
                listBox1.Items.Add(uyari);
                this.BackColor = Color.Turquoise;
            }
            if(gelen=="b")
            {
                uyari = "Dikkat Gaz Kaçağı var";
                listBox1.Items.Add(uyari);
                this.BackColor = Color.Red;
            }
         
            string tarih = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            baglan.Open();
            SqlCommand komut = new SqlCommand("insert into Kayitlar2 (tarih,veri) Values('" + tarih + "','" +uyari+ "')", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hakkında hkn = new Hakkında();
            hkn.ShowDialog();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            vrtbn.ShowDialog();
            verileriGoster();
        }
        long id = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (vrtbn.listView1.SelectedItems.Count != 0)
            {
                id = long.Parse(vrtbn.listView1.SelectedItems[0].Text);

                {
                    DialogResult sonuc = MessageBox.Show("Veri Silinsin mi?", "Silinecek", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3);
                    if (sonuc == DialogResult.OK)
                    {
                        baglan.Open();
                        SqlCommand komut = new SqlCommand("Delete From Kayitlar2  where sira=(" + id + ")", baglan);
                        komut.ExecuteNonQuery();
                        baglan.Close();
                        verileriGoster();
                    }

                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string tarih = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            lblTarih.Text = tarih;
        }
        int sayi = 0;
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (sayi == 1)
            {
                serialPort1.Write("b");
                lblLed.Text = "Led Sönük";
                btnLedYak.Text = "Yak";
                sayi--;
                return;
            }
            if (sayi == 0)
            {
                serialPort1.Write("a");
                lblLed.Text = "Led Yanıyor";
                btnLedYak.Text = "Söndür";
                sayi++;
            }           
        }

        private void butonLedYak_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(5);
        }
    }
}
