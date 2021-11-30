using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Pawn : Figure
    {
        private List<(int, int)> possibleMoves;

        private (int, int)[] possibleHits;

        public Pawn(
            int x,
            int y,
            PlayerColor color
            ): base(x,y,color) { possibleMoves = new List<(int, int)> { }; possibleHits = new (int, int)[2];}



        public override IEnumerable<(int, int)> GetPossibleMoves
        {
            get
            {
                foreach ((int, int) coords in possibleMoves)
                {
                    yield return (coords.Item1, coords.Item2);
                }
                if (possibleHits[0] != (-1, -1))
                    yield return possibleHits[0];
                if (possibleHits[1] != (-1, -1))
                    yield return possibleHits[1];

            }

        }
        /// <summary>
        /// Add move to the possible moves if its possible to move
        /// </summary>
        /// <param name="coords"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="board"></param>
        public void TypeOfPossibility(int x, int y,ChessBoard board)
        {
            if (board.GetCell(x, y).figure == null)
                possibleMoves.Add((x, y));
            possibleHits[0] = (-1, -1);
            possibleHits[1] = (-1, -1);
            if (this.Player == PlayerColor.Black)
            {
                if (Y >= 1)
                {
                    if (board.GetCell(X - 1, Y - 1).figure != null && board.GetCell(X - 1, Y - 1).figure.Player != this.Player)
                        possibleHits[0] = (X - 1, Y - 1);
                }
                if (Y <= 6)
                {
                    if (board.GetCell(X - 1, Y + 1).figure != null && board.GetCell(X - 1, Y + 1).figure.Player != this.Player)
                        possibleHits[1] = (X - 1, Y + 1);
                }
            }
            else
            {
                if (Y >= 1)
                {
                    if (board.GetCell(X + 1, Y - 1).figure != null && board.GetCell(X + 1, Y - 1).figure.Player != this.Player)
                        possibleHits[0] = (X + 1, Y - 1);
                }
                if (Y <= 6)
                {
                    if (board.GetCell(X + 1, Y + 1).figure != null && board.GetCell(X + 1, Y + 1).figure.Player != this.Player)
                        possibleHits[1] = (X + 1, Y + 1);
                }
            }
        }

        public override void CalculatePossibleMoves(ChessBoard board)
        {
            if (this.Player == PlayerColor.White)
            {   
                board.GetCell(X + 1, Y).possibleToHit.Add(this);
                TypeOfPossibility(X + 1, Y, board);
                if (X == 1)
                {
                    board.GetCell(X + 2, Y).possibleToHit.Add(this);
                    TypeOfPossibility(X + 2, Y, board);
                }
            }
            else
            {
                board.GetCell(X - 1, Y).possibleToHit.Add(this);
                TypeOfPossibility(X - 1, Y, board);
                if (X == 6)
                {
                    board.GetCell(X - 2, Y).possibleToHit.Add(this);
                    TypeOfPossibility(X - 2, Y, board);
                }
            }
        }

        public override bool IsBlockedIfMove((int xFrom, int yFrom) From, (int xTo, int yTo) To, (int xKing, int yKing) King, ChessBoard board)
        {
            return possibleHits[0] != King && possibleHits[1] != King;

        }

        public override string ToString()
        {
            return "P";
        }

    }
}
