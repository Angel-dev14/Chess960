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
        public bool KingWarned { get; set; }
        public int WhitePlayerChecks { get; set; }
        public int BlackPlayerChecks { get; set; }


        public Board()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            Chess.Reset();

            Chess.RandomizeMap();

            CurrentPlayer = 1;

            Blocks = new Block[8, 8];

            KingWarned = false;

            PreviousBlock = new Block();

            ToBeMoved = false;

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
                        block.BackColor = Color.FromArgb(232,235,239);
                        block.Color = block.BackColor;
                    }
                    else
                    {
                        block.BackColor = Color.FromArgb(125,135,150);
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
                ClearBoard();

                if (PressedBlock.BackColor != Color.Purple)
                    PressedBlock.BackColor = Color.Red; // Show selected block color (on click)

                DisableBlocks();

                PressedBlock.Enabled = true;

                ShowSteps(y, x, PressedBlock.Figure.Type % 10);

                if (ToBeMoved)
                {
                    ClearBoard();
                    PressedBlock.BackColor = PressedBlock.Color;
                    EnableBlocks();
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
                    SwapFigurePosition(y, x);

                    KingWarned = false;

                    // To this pressed block add the previous figure
                    PressedBlock.AddFigure(PreviousBlock.Figure);

                    // set the previous block figure to empty
                    PreviousBlock.ClearFigure();

                    // reset the moving process
                    ToBeMoved = false;

                    ClearBoard();

                    EnableBlocks();

                    CheckIfKingIsTagged(y, x); 

                    CheckIfGameEnded();

                    PawnPromotion(y, x);

                    ClearBoard();

                    ChangePlayer();

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
        public void CheckIfGameEnded()
        {
            if (BlackPlayerChecks == 3)
            {

                EndForm endForm = new EndForm();
                endForm.SetHeading("WHITE PLAYER WINS");
                endForm.SetEnemyPlayer("black");
                endForm.ShowDialog();
                this.Close();

            }
            if (WhitePlayerChecks == 3)
            {
                EndForm endForm = new EndForm();
                endForm.SetHeading("BLACK PLAYER WINS");
                endForm.SetEnemyPlayer("white");
                endForm.ShowDialog();
                this.Close();
            }
        }
        public void PawnPromotion(int i, int j)
        {
            if (i == 0 && Chess.Map[i, j] % 10 == 6)
            {
                WhitePawnPromotion(i, j);
            }
            if (i == 7 && Chess.Map[i, j] % 10 == 6)
            {
                BlackPawnPromotion(i, j);
            }
        }
        public void WhitePawnPromotion(int i, int j)
        {
            PawnPromotionWhite promotion = new PawnPromotionWhite();
            promotion.ShowDialog();
            int type = promotion.FigureType;
            Figure figure = new Figure();
            figure.Icon = GenerateFigure(1, type);
            figure.Type = type;
            Blocks[i, j].Figure = figure;
            Blocks[i, j].BackgroundImage = figure.Icon;
            Chess.Map[i, j] = 10 + type;
        }
        public void BlackPawnPromotion(int i, int j)
        {
            PawnPromotionBlack promotion = new PawnPromotionBlack();
            promotion.ShowDialog();
            int type = promotion.FigureType;
            Figure figure = new Figure();
            figure.Icon = GenerateFigure(2, type);
            figure.Type = type;
            Blocks[i, j].Figure = figure;
            Blocks[i, j].BackgroundImage = figure.Icon;
            Chess.Map[i, j] = 20 + type;
        }
        public void ShowSteps(int i, int j, int figureType)
        {

            switch (figureType)
            {
                // MOVES
                case 6:
                    if (CheckDoubleMoves(i))
                        PawnDoubleMoves(i, j);
                    else PawnMoves(i, j);
                    break;
                case 5:
                    RookMoves(i, j);
                    break;
                case 4:
                    KnightMoves(i, j);
                    break;
                case 3:
                    BishopMoves(i, j);
                    break;
                case 2:
                    QueenMoves(i, j);
                    break;
                case 1:
                    KingMoves(i, j);
                    break;
            }
        }
        public bool CheckBounds(int i, int j)
        {
            if (i >= 8 || j >= 8 || i < 0 || j < 0)
                return false;
            return true;
        }
        public void ChangePlayer()
        {
            if (CurrentPlayer == 1)
                CurrentPlayer = 2;
            else CurrentPlayer = 1;
        }
        public void ClearBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (KingWarned && Blocks[i, j].BackColor == Color.Purple)
                        continue;
                    else
                        Blocks[i, j].BackColor = Blocks[i, j].Color;
                }
            }

        }
        public void DisableBlocks()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Blocks[i, j].Enabled = false;
                }
            }
        }
        public void EnableBlocks()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Blocks[i, j].Enabled = true;
                }
            }
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

        public bool CheckDoubleMoves(int currentRow)
        {
            if (currentRow == 1)
                return true;
            if (currentRow == 6)
                return true;
            return false;
        }
        public void PawnDoubleMoves(int currentRow, int currentColumn)
        {
            int i = currentRow;
            int j = currentColumn;
            int direction = GetDirection();
            if (CheckBounds(i + 2 * direction, j))
            {
                if (Chess.Map[i + 2 * direction, j] == 0)
                {
                    Blocks[i + 1 * direction, j].Available();
                    Blocks[i + 2 * direction, j].Available();
                    Blocks[i + 1 * direction, j].Enabled = true;
                    Blocks[i + 2 * direction, j].Enabled = true;
                    PawnMoves(currentRow, currentColumn);
                }
                else
                {
                    PawnMoves(currentRow, currentColumn);
                }
            }
            else
            {
                PawnMoves(currentRow, currentColumn);
            }
        }
        public void PawnMoves(int currentRow, int currentColumn, bool invisible = false)
        {
            int i = currentRow;
            int j = currentColumn;
            int direction = GetDirection();
            if (invisible)
            {
                if (CheckBounds(i + 1 * direction, j + 1))
                {
                    FindInvisiblePath(i + 1 * direction, j + 1);
                }
                if (CheckBounds(i + 1 * direction, j - 1))
                {
                    FindInvisiblePath(i + 1 * direction, j - 1);
                }
                return;

            }
            if (CheckBounds(i + 1 * direction, j))
            {
                if (Chess.Map[i + 1 * direction, j] == 0)
                {
                    Blocks[i + 1 * direction, j].Available();
                    Blocks[i + 1 * direction, j].Enabled = true;
                }
            }
            if (CheckBounds(i + 1 * direction, j + 1))
            {

                if (Chess.Map[i + 1 * direction, j + 1] != 0 && Chess.Map[i + 1 * direction, j + 1] / 10 != CurrentPlayer)
                {
                    Blocks[i + 1 * direction, j + 1].Available();
                    Blocks[i + 1 * direction, j + 1].Enabled = true;
                }
            }
            if (CheckBounds(i + 1 * direction, j - 1))
            {
                if (Chess.Map[i + 1 * direction, j - 1] != 0 && Chess.Map[i + 1 * direction, j - 1] / 10 != CurrentPlayer && CheckBounds(i + 1 * direction, j - 1))
                {
                    Blocks[i + 1 * direction, j - 1].Available();
                    Blocks[i + 1 * direction, j - 1].Enabled = true;
                }
            }
        }
        public void RookMoves(int currentRow, int currentColumn, bool invisible = false)
        {
            // DIRECTION DOWN
            for (int i = currentRow + 1; i < 8; i++)
            {
                if (CheckBounds(i, currentColumn))
                {
                    if (invisible)
                    {
                        if (!FindInvisiblePath(i, currentColumn))
                            break;
                    }
                    else
                    {
                        if (!FindPath(i, currentColumn))
                            break;
                    }
                }
            }
            // DIRECTION UP
            for (int i = currentRow - 1; i >= 0; i--)
            {
                if (CheckBounds(i, currentColumn))
                {
                    if (invisible)
                    {
                        if (!FindInvisiblePath(i, currentColumn))
                            break;
                    }
                    else
                    {
                        if (!FindPath(i, currentColumn))
                            break;
                    }
                }
            }
            // DIRECTION RIGHT
            for (int j = currentColumn + 1; j < 8; j++)
            {
                if (CheckBounds(currentRow, j))
                {
                    if (invisible)
                    {
                        if (!FindInvisiblePath(currentRow, j))
                            break;
                    }
                    else
                    {
                        if (!FindPath(currentRow, j))
                            break;
                    }
                }
            }
            // DIRECTION LEFT
            for (int j = currentColumn - 1; j >= 0; j--)
            {
                if (CheckBounds(currentRow, j))
                {
                    if (invisible)
                    {
                        if (!FindInvisiblePath(currentRow, j))
                            break;
                    }
                    else
                    {
                        if (!FindPath(currentRow, j))
                            break;
                    }
                }
            }
        }
        public void BishopMoves(int currentRow, int currentColumn, bool invisible = false)
        {
            // RIGHT DIAGONAL UP
            int j = currentColumn + 1;
            for (int i = currentRow - 1; i >= 0; i--)
            {
                if (CheckBounds(i, j))
                {
                    if (invisible)
                    {
                        if (!FindInvisiblePath(i, j++))
                            break;
                    }
                    else
                    {
                        if (!FindPath(i, j++))
                            break;
                    }
                }
            }
            // LEFT DIAGONAL UP
            j = currentColumn - 1;
            for (int i = currentRow - 1; i >= 0; i--)
            {
                if (CheckBounds(i, j))
                {
                    if (invisible)
                    {
                        if (!FindInvisiblePath(i, j--))
                            break;
                    }
                    else
                    {
                        if (!FindPath(i, j--))
                            break;
                    }
                }
            }
            // RIGHT DIAGONAL DOWN
            j = currentColumn + 1;
            for (int i = currentRow + 1; i < 8; i++)
            {
                if (CheckBounds(i, j))
                {
                    if (invisible)
                    {
                        if (!FindInvisiblePath(i, j++))
                            break;
                    }
                    else
                    {
                        if (!FindPath(i, j++))
                            break;
                    }
                }
            }
            // LEFT DIAGONAL DOWN
            j = currentColumn - 1;
            for (int i = currentRow + 1; i < 8; i++)
            {
                if (CheckBounds(i, j))
                {
                    if (invisible)
                    {
                        if (!FindInvisiblePath(i, j--))
                            break;
                    }
                    else
                    {
                        if (!FindPath(i, j--))
                            break;
                    }
                }
            }
        }
        public void QueenMoves(int currentRow, int currentColumn, bool invisible = false)
        {
            EnableBlocks();
            RookMoves(currentRow, currentColumn, invisible);
            BishopMoves(currentRow, currentColumn, invisible);
        }
        public void KingMoves(int currentRow, int currentColumn)
        {
            int i = currentRow;
            int j = currentColumn;

            if (CheckBounds(i + 1, j))
            {
                if (IsAvailable(i + 1, j))
                    FindPath(i + 1, j);
            }
            if (CheckBounds(i - 1, j))
            {
                if (IsAvailable(i - 1, j))
                    FindPath(i - 1, j);
            }
            if (CheckBounds(i + 1, j + 1))
            {
                if (IsAvailable(i + 1, j + 1))
                    FindPath(i + 1, j + 1);
            }
            if (CheckBounds(i + 1, j - 1))
            {
                if (IsAvailable(i + 1, j - 1))
                    FindPath(i + 1, j - 1);
            }
            if (CheckBounds(i - 1, j + 1))
            {
                if (IsAvailable(i - 1, j + 1))
                    FindPath(i - 1, j + 1);
            }
            if (CheckBounds(i - 1, j - 1))
            {
                if (IsAvailable(i - 1, j - 1))
                    FindPath(i - 1, j - 1);
            }
            if (CheckBounds(i, j + 1))
            {
                if (IsAvailable(i, j + 1))
                    FindPath(i, j + 1);
            }
            if (CheckBounds(i, j - 1))
            {
                if (IsAvailable(i, j - 1))
                    FindPath(i, j - 1);
            }
        }
        public void KnightMoves(int currentRow, int currentColumn, bool invisible = false)
        {
            int i = currentRow;
            int j = currentColumn;

            if (CheckBounds(i - 2, j - 1))
            {
                if (invisible)
                    FindInvisiblePath(i - 2, j - 1);
                else FindPath(i - 2, j - 1);
            }
            if (CheckBounds(i - 2, j + 1))
            {
                if (invisible)
                    FindInvisiblePath(i - 2, j + 1);
                else FindPath(i - 2, j + 1);
            }
            if (CheckBounds(i - 1, j - 2))
            {
                if (invisible)
                    FindInvisiblePath(i - 1, j - 2);
                else FindPath(i - 1, j - 2);
            }
            if (CheckBounds(i - 1, j + 2))
            {
                if (invisible)
                    FindInvisiblePath(i - 1, j + 2);
                else FindPath(i - 1, j + 2);
            }
            if (CheckBounds(i + 2, j - 1))
            {
                if (invisible)
                    FindInvisiblePath(i + 2, j - 1);
                else FindPath(i + 2, j - 1);
            }
            if (CheckBounds(i + 2, j + 1))
            {
                if (invisible)
                    FindInvisiblePath(i + 2, j + 1);
                else FindPath(i + 2, j + 1);
            }
            if (CheckBounds(i + 1, j - 2))
            {
                if (invisible)
                    FindInvisiblePath(i + 1, j - 2);
                else FindPath(i + 1, j - 2);
            }
            if (CheckBounds(i + 1, j + 2))
            {
                if (invisible)
                    FindInvisiblePath(i + 1, j + 2);
                else FindPath(i + 1, j + 2);
            }
        }
        //================================== CHECK ALGORITHM ===========================
        public bool EmptyBlock(int i, int j)
        {
            return Chess.Moves[i, j] == 0;
        }
        public void CheckIfKingIsTagged(int i, int j)
        {
            ResetMarkedBlocks();

            // THIS LOOKS FOR DIRECT TREATS TO THE KING
            int type = Chess.Map[i, j] % 10;
            switch (type)
            {
                // MOVES

                case 6:
                    PawnMoves(i, j, true);
                    break;
                case 5:
                    RookMoves(i, j, true);
                    break;
                case 4:
                    KnightMoves(i, j, true);
                    break;
                case 3:
                    BishopMoves(i, j, true);
                    break;
                case 2:
                    QueenMoves(i, j, true);
                    break;
                case 1:
                    KingMoves(i, j);
                    break;
            }
        }
        public void ResetMarkedBlocks()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Chess.Moves[i, j] = 0;
                }
            }
        }
        public bool FindInvisiblePath(int i, int j)
        {
            if (Chess.Map[i, j] == 0)
            {
                if (EmptyBlock(i, j) || Chess.Moves[i, j] != CurrentPlayer)
                {
                    Chess.Moves[i, j] = CurrentPlayer * -1; 
                }
                return true; // the loop can continue
            }
            if (KingWarning(i, j))
            {
                return false; // the loop can end
            }

            return false;
        }
        public bool IsAvailable(int i, int j)
        {
            return EmptyBlock(i, j) || Chess.Moves[i, j] == CurrentPlayer;
        }
        public bool KingWarning(int i, int j)
        {
            if (Chess.Map[i, j] % 10 == 1 && Chess.Map[i, j] / 10 != CurrentPlayer) // != opponent
            {
                // TREBA DA SE PROVERI DALI ZAKANATA DOVAGJA OD SPORIVNATA FIGURA
                if (CheckIfKingIsTargetedByEnemy(i, j))
                {
                    if (CurrentPlayer == 1)
                        BlackPlayerChecks++;
                    else
                        WhitePlayerChecks++;
                    Blocks[i, j].BackColor = Color.Purple;
                }
                else
                    Blocks[i, j].BackColor = Blocks[i, j].Color;
                
                KingWarned = true;

                return true;
            }
            return false;
        }
        public bool EnemyIsNear(int i, int j)
        {
            return Chess.Map[i, j] / 10 == CurrentPlayer;
        }
        public bool Targeted(int i, int j)
        {
            return (Chess.Moves[i, j] == CurrentPlayer * -1) && (Chess.Moves[i, j] != 0);
        }
        public bool CheckIfKingIsTargetedByEnemy(int i, int j)
        {
            int type = PressedBlock.Figure.Type;
            switch (type)
            {
                case 6:
                    return CheckByPawn(i, j);
                case 5:
                    return CheckByRook(i, j);
                case 4:
                    return CheckByKnight(i, j);
                case 3:
                    return CheckByBishop(i, j);
                case 2:
                    return CheckByQueen(i, j);
            }
            return false;
        }
        public bool CheckByPawn(int i, int j)
        {
            if (CheckBounds(i + 1, j - 1))
            {
                if (EnemyIsNear(i + 1, j - 1))
                    return true;
            }
            if (CheckBounds(i + 1, j + 1))
            {
                if (EnemyIsNear(i + 1, j + 1))
                    return true;
            }
            if (CheckBounds(i - 1, j - 1))
            {
                if (EnemyIsNear(i - 1, j - 1))
                    return true;
            }
            if (CheckBounds(i - 1, j + 1))
            {
                if (EnemyIsNear(i - 1, j + 1))
                    return true;
            }
            return false;
        }
        public bool CheckByQueen(int i, int j)
        {
            return CheckByBishop(i, j) || CheckByRook(i, j);
        }

        public bool CheckByBishop(int i, int j)
        {

            if (CheckBounds(i + 1, j + 1))
            {
                if (Targeted(i + 1, j + 1) || EnemyIsNear(i + 1, j + 1))
                {
                    return true;
                }
            }
            if (CheckBounds(i + 1, j - 1))
            {
                if (Targeted(i + 1, j - 1) || EnemyIsNear(i + 1, j - 1))
                {
                    return true;
                }
            }
            if (CheckBounds(i - 1, j + 1))
            {
                if (Targeted(i - 1, j + 1) || EnemyIsNear(i - 1, j + 1))
                {
                    return true;
                }
            }
            if (CheckBounds(i - 1, j - 1))
            {
                if (Targeted(i - 1, j - 1) || EnemyIsNear(i - 1, j - 1))
                {
                    return true;
                }
            }

            return false;
        }
        public bool CheckByRook(int i, int j)
        {
            if (CheckBounds(i + 1, j))
            {
                if (Targeted(i + 1, j) || EnemyIsNear(i + 1, j))
                {
                    return true;
                }
            }
            if (CheckBounds(i - 1, j))
            {
                if (Targeted(i - 1, j) || EnemyIsNear(i - 1, j))
                {
                    return true;
                }
            }
            if (CheckBounds(i, j - 1))
            {
                if (Targeted(i, j - 1) || EnemyIsNear(i, j - 1))
                {
                    return true;
                }
            }
            if (CheckBounds(i, j + 1))
            {
                if (Targeted(i, j + 1) || EnemyIsNear(i, j + 1))
                {
                    return true;
                }
            }
            return false;

        }
        public bool CheckByKnight(int i, int j)
        {
            if (CheckBounds(i - 2, j - 1))
            {
                if (Targeted(i - 2, j - 1) || EnemyIsNear(i - 2, j - 1))
                {
                    return true;
                }
            }
            if (CheckBounds(i - 2, j + 1))
            {
                if (Targeted(i - 2, j + 1) || EnemyIsNear(i - 2, j + 1))
                {
                    return true;
                }
            }
            if (CheckBounds(i - 1, j - 2))
            {
                if (Targeted(i - 1, j - 2) || EnemyIsNear(i - 1, j - 2))
                {
                    return true;
                }
            }
            if (CheckBounds(i - 1, j + 2))
            {
                if (Targeted(i - 1, j + 2) || EnemyIsNear(i - 1, j + 2))
                {
                    return true;
                }
            }
            if (CheckBounds(i + 2, j - 1))
            {
                if (Targeted(i + 2, j - 1) || EnemyIsNear(i + 2, j - 1))
                {
                    return true;
                }
            }
            if (CheckBounds(i + 2, j + 1))
            {
                if (Targeted(i + 2, j + 1) || EnemyIsNear(i + 2, j + 1))
                {
                    return true;
                }
            }
            if (CheckBounds(i + 1, j - 2))
            {
                if (Targeted(i + 1, j - 2) || EnemyIsNear(i + 1, j - 2))
                {
                    return true;
                }
            }
            if (CheckBounds(i + 1, j + 2))
            {
                if (Targeted(i + 1, j + 2) || EnemyIsNear(i + 1, j + 2))
                {
                    return true;
                }
            }
            return false;
        }

    }

}
