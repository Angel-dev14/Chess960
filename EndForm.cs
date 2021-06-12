using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess960
{
    public partial class EndForm : Form
    {
        public EndForm()
        {
            InitializeComponent();
        }
        public void SetHeading(string text)
        {
            lblHeading.Text = text;
        }
        public void SetEnemyPlayer(string text)
        {
            lblPlayer.Text = text;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
