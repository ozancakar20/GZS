using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Gaz_Sensoru
{
    public partial class Veritabanı : Form
    {
        public Veritabanı()
        {
            InitializeComponent();
        }
        
      //  SqlConnection baglan = new SqlConnection("Data Source=casper\\sqlexpress;Initial Catalog=gazSensoru;Integrated Security=True");
        private void Veritabanı_Load(object sender, EventArgs e)
        {
            butonLedYak frm = new butonLedYak();
            frm.verileriGoster();
        }

        private void Veritabanı_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

    }
}
