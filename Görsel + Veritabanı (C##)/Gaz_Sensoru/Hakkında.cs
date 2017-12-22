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
    public partial class Hakkında : Form
    {
        public Hakkında()
        {
            InitializeComponent();
        }

        private void Hakkında_Load(object sender, EventArgs e)
        {
            label1.Text = "Bu program Bir ortamdaki gaz kaçağını kontrol ederek insanları kaçak durumunda bilgilendirme amaçlı yazılmıştır.  ÇAĞDAŞ ...";
        }
    }
}
