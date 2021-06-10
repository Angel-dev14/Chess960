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
        public Block PressedBlock { get; set; }
        public Block PreviousBlock { get; set; }
        public bool ToBeMoved { get; set; }

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

                    block.Click += new EventHandler(OnFigureClick);

                    Blocks[i, j] = block; // Add block to blocks matrix

                    this.Controls.Add(block); // Add to this form

                    pointer++;  // increase block pointer for next block color position
                }
            }
        }
        public int GetBlockColorType(int i, int j) // 1 white 2 black
        {
            return Chess.Map[i, j] / 10; // 1 = white // 2 = black
        }
        public int GetFigurePosition(int i, int j)
        {
            return Chess.Map[i, j] % 10; // first digit
        }
        // Generate figure
        public Image GenerateFigure(int color, int type)
        {
            Image figure = null;
            string fullPath = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);

            switch (color)
            {
                case 1:
                    figure = new Bitmap(fullPath + "\\figures\\white\\" + type + ".png");
                    break;
                case 2:
                    figure = new Bitmap(fullPath + "\\figures\\black\\" + type + ".png");
                    break;
            }
            Image formatted = new Bitmap(figure, 100, 100);
            return formatted;
        }
        public void OnFigureClick(object sender, EventArgs e)
        {
            if (PreviousBlock != null)
            {
                PreviousBlock.BackColor = PreviousBlock.Color;
            }

            PressedBlock = sender as Block;

            int x = PressedBlock.Location.X / 100;
            int y = PressedBlock.Location.Y / 100;

            int location = Chess.Map[y, x];

            if (location != 0 && location / 10 == CurrentPlayer) // make sure that an enemy figure is clicked
            {
                //ClearBoard();

                if (PressedBlock.BackColor != Color.Purple)
                    PressedBlock.BackColor = Color.Red; // Show selected block color (on click)

                //DisableBlocks();

                PressedBlock.Enabled = true;

                //ShowSteps(y, x, PressedBlock.Figure.Type % 10);

                if (ToBeMoved)
                {
                    //ClearBoard();
                    PressedBlock.BackColor = PressedBlock.Color;
                    //EnableBlocks();
                    ToBeMoved = false;
                }
                else
                {
                    ToBeMoved = true;
                }
            }
            else
            {
                if (ToBeMoved)
                {
                    // CheckIfKingIsTagged(y, x) ;
                    SwapFigurePosition(y, x);
                    
                    //KingWarned = false;
                    // To this pressed block add the previous figure
                    PressedBlock.AddFigure(PreviousBlock.Figure);

                    // set the previous block figure to empty
                    PreviousBlock.ClearFigure();

                    // reset the moving process
                    ToBeMoved = false;

                    //ClearBoard();

                    //EnableBlocks();

                    //CheckIfKingIsTagged(y, x); // not finished implementation

                    //CheckIfGameEnded();

                    //PawnPromotion(y, x);

                    //Test(); //testing a new algorithm

                    //ChangePlayer();

                }
            }
            PreviousBlock = PressedBlock;
        }
        public void SwapFigurePosition(int y, int x)
        {
            int prevX = PreviousBlock.Location.X / 100;
            int prevY = PreviousBlock.Location.Y / 100;
            Chess.Map[y, x] = Chess.Map[prevY, prevX];

            Chess.Map[prevY, prevX] = 0; // Set to 0, because the figure is moved
        }
        public bool FindPath(int i, int j)
        {
            if (Chess.Map[i, j] == 0)
            {
                Blocks[i, j].Available();
                Blocks[i, j].Enabled = true;
            }
            else
            {
                if (Chess.Map[i, j] / 10 != CurrentPlayer && Chess.Map[i, j] % 10 != 1) // Dokolku imame figura od sprotiven igrac treba da sopre algoritmot i da ovozmozi deka moze da se zeme taa figura
                {
                    Blocks[i, j].Available();
                    Blocks[i, j].Enabled = true;
                }
                return false; // end the search
            }
            return true;
        }
        public int GetDirection()
        {
            return CurrentPlayer == 1 ? -1 : 1;
        }
        // ============= FIGURE MOVES =============
    }

}
