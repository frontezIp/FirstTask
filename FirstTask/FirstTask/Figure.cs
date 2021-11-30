using System;
using System.Collections.Generic;

namespace ChessLibrary
{
    public abstract class Figure
    {
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }

        /// <summary>
        /// Colour of the figure
        /// </summary>
        public PlayerColor Player { get; }

        public List<(int, int)> LegalMoves
        {
            private set;
            get;
        }

        private int _x;
        private int _y;

        public Figure(int x, int y, PlayerColor player)
        {
            _x = x;
            _y = y;
            Player = player;
            LegalMoves = new List<(int, int)>();
        }


        public abstract IEnumerable<(int, int)> GetPossibleMoves { get; }

        /// <summary>
        /// Calculate all possible moves
        /// </summary>
        /// <param name="board"></param>
        public abstract void CalculatePossibleMoves(ChessBoard board);

        /// <summary>
        /// Validates moving from to to doesnt put king in check
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <param name="King"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public abstract bool IsBlockedIfMove((int xFrom, int yFrom) From, (int xTo, int yTo) To, (int xKing, int yKing) King, ChessBoard board);
    }
}
