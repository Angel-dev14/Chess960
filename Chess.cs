using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess960
{
    public static class Chess
    {
        public static int[] FigurePositions = { 5, 4, 3, 2, 1, 3, 4, 5 }; // starting positions
        //5 - rook, 4 - horse, 3 - bishop, 2 - queen, 1 king, 6 pawn
        public static int[,] Map = new int[8, 8]   //Chess Map
        {
            {25,24,23,22,21,23,24,25 },
            {26,26,26,26,26,26,26,26 }, //black
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {16,16,16,16,16,16,16,16 },
            {15,14,13,12,11,13,14,15 }, //white

        };
        public static int[,] Moves = new int[8, 8] // PREDICTION OF CHECKMATE AND FUTURE MOVES
        {
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0 }
        };
        //public static int[,] Map = new int[8, 8];
        public static readonly Random RANDOM = new Random();
        public static void RandomizeMap()
        {
            FigurePositions = Shuffle(FigurePositions);
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == 0)
                        Map[i, j] = 20 + FigurePositions[j];
                    else if (i == 1)
                        Map[i, j] = 26;
                    else if (i == 7)
                        Map[i, j] = 10 + FigurePositions[j];
                    else if (i == 6)
                        Map[i, j] = 16;
                    else
                        Map[i, j] = 0;
                }
            }
        }
        // FISHER'S ALGORITHM 
        public static int[] Shuffle(int[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = RANDOM.Next(n + 1);
                int temp = array[k];
                array[k] = array[n];
                array[n] = temp;
            }
            return array;
        }
        public static void Reset()
        {
            Map = new int[8, 8]
            {
                { 25,24,23,22,21,23,24,25 },
                { 26,26,26,26,26,26,26,26},
                { 0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0 },
                { 16,16,16,16,16,16,16,16},
                { 15,14,13,12,11,13,14,15 },

            };
            Moves = new int[8, 8]
            {
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,0,0,0,0,0 }
            };
        }
    }
}
