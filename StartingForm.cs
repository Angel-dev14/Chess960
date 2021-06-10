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
	public partial class StartingForm : Form
	{
		public StartingForm()
		{
			InitializeComponent();
		}


		private void button2_Click(object sender, EventArgs e)
		{
			string message = "About Chess960:\n" +
							"Chess960, or Fischer Random, is a chess variant  where the pieces have been randomly shuffled on each player's back rank.\n" +
							"The game is based on a simple check variant with one clear task in mind: Check the king 3 times to win.\n\n\n" +
							"Figure movements:\n" +
							"-> King can move exactly one square horizontally, vertically, or diagonally. At most once in every game, each king is allowed to make a special move, known as castling.\n" +
							"-> Queen can move any number of vacant squares diagonally, horizontally, or vertically.\n" +
							"-> Rook can move any number of vacant squares vertically or horizontally.It also is moved while castling.\n" +
							"-> Bishop can move any number of vacant squares in any diagonal direction.\n" +
							"-> Knight can move one square along any rank or file and then at an angle.The knight´s movement can also be viewed as an “L” or “7″ laid out at any horizontal or vertical angle.\n" +
							"-> Pawns can move forward one square, if that square is unoccupied.If it has not yet moved, the pawn has the option of moving two squares forward provided both squares in front of the pawn are unoccupied. A pawn cannot move backward.Pawns are the only pieces that capture differently from how they move.They can capture an enemy piece on either of the two spaces adjacent to the space in front of them(i.e., the two squares diagonally in front of them) but cannot move to these spaces if they are vacant. The pawn is also involved in the two special moves en passant and promotion.\n\n\n" +
							"End of game:\n" +
							"The game ends if the king has been warned by the opposite side three times";

			string title = "Game Rules";
			MessageBox.Show(message, title);
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			Board gameBoard = new Board();
			gameBoard.Show();
		}

		private void Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
