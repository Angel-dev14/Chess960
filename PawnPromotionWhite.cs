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
    public partial class PawnPromotionWhite : Form
    {
        public int FigureType { get; set; }

        public PawnPromotionWhite()
        {
            InitializeComponent();
            this.AcceptButton = btnConfirm;
            FigureType = 0;
        }

        private void Queen_Click(object sender, EventArgs e)
        {
            Queen.BackColor = Color.Yellow;
            Rook.BackColor = Color.White;
            Horse.BackColor = Color.White;
            Bishop.BackColor = Color.White;

            FigureType = 2;
        }

        private void Rook_Click(object sender, EventArgs e)
        {
            Queen.BackColor = Color.White;
            Rook.BackColor = Color.Yellow;
            Horse.BackColor = Color.White;
            Bishop.BackColor = Color.White;

            FigureType = 5;
        }

        private void Bishop_Click(object sender, EventArgs e)
        {
            Queen.BackColor = Color.White;
            Rook.BackColor = Color.White;
            Horse.BackColor = Color.White;
            Bishop.BackColor = Color.Yellow;
            
            FigureType = 3;

        }

        private void Horse_Click(object sender, EventArgs e)
        {
            Queen.BackColor = Color.White;
            Rook.BackColor = Color.White;
            Horse.BackColor = Color.Yellow;
            Bishop.BackColor = Color.White;

            FigureType = 4;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
