using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess960
{
    public static class Chess
    {
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
    }
}
