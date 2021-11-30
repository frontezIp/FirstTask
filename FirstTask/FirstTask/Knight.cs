using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Knight : Figure
    {
        public List<(int, int)> PossibleMoves { get => possibleMoves; }

        private List<(int, int)> possibleMoves;

        public Knight(Figure figure) : base(figure.X, figure.Y, figure.Player)
        {
            possibleMoves = new List<(int, int)>();
        }

        public Knight(
          int x,
          int y,
          PlayerColor player
          ) : base(x, y, player) { possibleMoves = new List<(int, int)>(); }

        public override IEnumerable<(int, int)> GetPossibleMoves
        {
            get
            {
                foreach ((int, int) coords in possibleMoves)
                {
                    yield return (coords.Item1, coords.Item2);
                }

            }
        }

        /// <summary>
        /// Add move to the possible moves if its possible to move
        /// </summary>
        /// <param name="coords"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="board"></param>
        public void TypeOfPossibility(int x, int y, ChessBoard board)
        {
            if (board.GetCell(x, y).figure == null || board.GetCell(x, y).figure.Player != this.Player)
                possibleMoves.Add((x, y));
        }

        public override void CalculatePossibleMoves(ChessBoard board)
        {
            if (board.ValidateCoords(X + 2, Y + 1) != false)
            {
                board.GetCell(X + 2, Y + 1).possibleToHit.Add(this);
                TypeOfPossibility(X + 2, Y + 1, board);
            }
            if (board.ValidateCoords(X + 2, Y - 1) != false)
            {
                board.GetCell(X + 2, Y - 1).possibleToHit.Add(this);
                TypeOfPossibility(X + 2, Y - 1, board);
            }
            if (board.ValidateCoords(X - 2, Y + 1) != false)
            {
                board.GetCell(X - 2, Y + 1).possibleToHit.Add(this);
                TypeOfPossibility(X - 2, Y + 1, board);
            }
            if (board.ValidateCoords(X - 2, Y - 1) != false)
            {
                board.GetCell(X - 2, Y - 1).possibleToHit.Add(this);
                TypeOfPossibility(X - 2, Y - 1, board);
            }
            if (board.ValidateCoords(X + 1, Y + 2) != false)
            {
                board.GetCell(X + 1, Y + 2).possibleToHit.Add(this);
                TypeOfPossibility(X + 1, Y + 2, board);
            }
            if (board.ValidateCoords(X + 1, Y - 2) != false)
            {
                board.GetCell(X + 1, Y - 2).possibleToHit.Add(this);
                TypeOfPossibility(X + 1, Y - 2, board);
            }
            if (board.ValidateCoords(X - 1, Y + 2) != false)
            {
                board.GetCell(X - 1, Y + 2).possibleToHit.Add(this);
                TypeOfPossibility(X - 1, Y + 2, board);
            }
            if (board.ValidateCoords(X - 1, Y - 2) != false)
            {
                board.GetCell(X - 1, Y - 2).possibleToHit.Add(this);
                TypeOfPossibility(X - 1, Y - 2, board);
            }

        }
        public override bool IsBlockedIfMove((int xFrom, int yFrom) From, (int xTo, int yTo) To, (int xKing, int yKing) King, ChessBoard board)
        {

            return (!possibleMoves.Contains(King));
        }

        public override string ToString()
        {
            return "H";
        }

    }
}
