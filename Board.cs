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
    public partial class Board : Form
    {
        public Block[,] Blocks { get; set; }
        public int CurrentPlayer { get; set; }

        public Board()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            CurrentPlayer = 1;

            Blocks = new Block[8, 8];

            CreateBoard();
        }
        public void CreateBoard()
        {
            int pointer = 0;
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                    pointer = 0;
                else
                    pointer = 1;

                for (int j = 0; j < 8; j++)
                {
                    // Create new block
                    Block block = new Block();
                    block.Size = new Size(100, 100);
                    block.Location = new Point(j * 100, i * 100);

                    // Generate chess board colors
                    if (pointer % 2 == 0)
                    {
                        block.BackColor = Color.White;
                        block.Color = block.BackColor;
                    }
                    else
                    {
                        block.BackColor = Color.DarkGray;
                        block.Color = block.BackColor;

                    }

                    // Get the type (white,black) and the position (figure type)
                    int color = GetBlockColorType(i, j);
                    int type = GetFigurePosition(i, j);

                    if (type != 0) // Put the figure
                    {
                        // Create new figure and set icon and type
                        Figure figure = new Figure();
                        figure.Type = type;
                        figure.Icon = GenerateFigure(color, type);

                        Graphics graphics = Graphics.FromImage(figure.Icon);
                        graphics.DrawImage(figure.Icon, block.Location);
                        block.BackgroundImage = figure.Icon;
                        block.Figure = figure;
                    }

                    //block.Click += new EventHandler(OnFigureClick); // ne e implementirano

                    Blocks[i, j] = block; // Add block to blocks matrix

                    this.Controls.Add(block); // Add to this form

                    pointer++;  // increase block pointer for next block color position
                }
            }
        }
    }
}
