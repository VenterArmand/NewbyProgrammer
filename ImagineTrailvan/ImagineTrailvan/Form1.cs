using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImagineTrailvan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }//end of public Form1()

        private void button1_Click(object sender, EventArgs e)
        {
            frmInventory obj = new frmInventory();
            this.Hide();
            obj.ShowDialog();
        }//end of private void button1_Click(object sender, EventArgs e)
    }//end of public partial class Form1 : Form
}//end of namespace ImagineTrailvan
