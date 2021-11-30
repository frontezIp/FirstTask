using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class King:Figure
    {
        public List<List<(int, int)>> PossibleMoves { get => possibleMoves; }

        private List<List<(int, int)>> possibleMoves;
        private List<(int, int)> possibleMovesRight;
        private List<(int, int)> possibleMovesDown;
        private List<(int, int)> possibleMovesLeft;
        private List<(int, int)> possibleMovesUp;
        private List<(int, int)> possibleMovesRightDown;
        private List<(int, int)> possibleMovesRightUp;
        private List<(int, int)> possibleMovesLeftUp;
        private List<(int, int)> possibleMovesLeftDown;

        public King(
          int x,
          int y,
          PlayerColor color
          ) : base(x, y, color)
        {
            possibleMovesRight = new List<(int, int)> { };
            possibleMovesUp = new List<(int, int)> { };
            possibleMovesLeft = new List<(int, int)> { };
            possibleMovesDown = new List<(int, int)> { };
            possibleMovesRightUp = new List<(int, int)> { };
            possibleMovesRightDown = new List<(int, int)> { };
            possibleMovesLeftUp = new List<(int, int)> { };
            possibleMovesLeftDown = new List<(int, int)> { };
            possibleMoves = new List<List<(int, int)>> { possibleMovesLeft, possibleMovesUp, possibleMovesRight, possibleMovesDown, possibleMovesLeftUp, possibleMovesLeftDown, possibleMovesRightUp, possibleMovesRightDown };
        }

        public override IEnumerable<(int, int)> GetPossibleMoves
        {
            get
            {
                foreach(var moves in possibleMoves)
                {
                    foreach (var move in moves)
                        yield return move;
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
        public void TypeOfPossibility(List<(int, int)> coords, int x, int y, ChessBoard board)
        {
            if (board.GetCell(x, y).figure == null || board.GetCell(x, y).figure.Player != this.Player)
                coords.Add((x, y));
        }

        
        public override void CalculatePossibleMoves(ChessBoard board)
        {
            if(board.ValidateCoords(X+1,Y+1)!=false)
            {
                board.GetCell(X + 1, Y + 1).possibleToHit.Add(this);
                TypeOfPossibility(possibleMovesRightDown,X + 1, Y + 1,board);
            }
            if (board.ValidateCoords(X + 1, Y) != false)
            {
                board.GetCell(X + 1, Y).possibleToHit.Add(this);
                TypeOfPossibility(possibleMovesDown,X + 1, Y,board);
            }
            if (board.ValidateCoords(X + 1, Y - 1) != false)
            {
                board.GetCell(X + 1, Y - 1).possibleToHit.Add(this);
                TypeOfPossibility(possibleMovesLeftDown,X + 1, Y - 1,board);
            }
            if (board.ValidateCoords(X, Y + 1) != false)
            {
                board.GetCell(X, Y + 1).possibleToHit.Add(this);
                TypeOfPossibility(possibleMovesRight,X, Y + 1, board);
            }
            if (board.ValidateCoords(X, Y - 1) != false)
            {
                board.GetCell(X, Y - 1).possibleToHit.Add(this);
                TypeOfPossibility(possibleMovesLeft,X, Y - 1, board);
            }
            if (board.ValidateCoords(X - 1, Y + 1) != false)
            {
                board.GetCell(X - 1, Y + 1).possibleToHit.Add(this);
                TypeOfPossibility(possibleMovesRightUp,X - 1, Y + 1, board);
            }
            if (board.ValidateCoords(X - 1, Y) != false)
            {
                board.GetCell(X - 1, Y).possibleToHit.Add(this);
                TypeOfPossibility(possibleMovesUp,X - 1, Y, board);
            }
            if (board.ValidateCoords(X - 1, Y - 1) != false)
            {
                board.GetCell(X - 1, Y - 1).possibleToHit.Add(this);
                TypeOfPossibility(possibleMovesLeftUp,X - 1, Y - 1, board);
            }
        }

        /// <summary>
        /// Defines direction of the list of possible moves
        /// </summary>
        /// <param name="movesToDefine"></param>
        /// <returns></returns>
        public (int, int) DefineDirectionOfTheList(List<(int, int)> movesToDefine)
        {
            if (movesToDefine.GetHashCode() == possibleMovesDown.GetHashCode())
                return (-1, 0);
            else if (movesToDefine.GetHashCode() == possibleMovesLeft.GetHashCode())
                return (0, -1);
            else if (movesToDefine.GetHashCode() == possibleMovesRight.GetHashCode())
                return (0, 1);
            else if (movesToDefine.GetHashCode() == possibleMovesUp.GetHashCode())
                return (1, 0);
            else if (movesToDefine.GetHashCode() == possibleMovesLeftDown.GetHashCode())
                return (1, -1);
            else if (movesToDefine.GetHashCode() == possibleMovesLeftUp.GetHashCode())
                return (-1, -1);
            else if (movesToDefine.GetHashCode() == possibleMovesRightDown.GetHashCode())
                return (1, 1);
            else if (movesToDefine.GetHashCode() == possibleMovesRightUp.GetHashCode())
                return (-1, 1);
            return (0, 0);
        }

        public override bool IsBlockedIfMove((int xFrom, int yFrom) From, (int xTo, int yTo) To, (int xKing, int yKing) King, ChessBoard board)
        {

            foreach (List<(int, int)> coords in possibleMoves)
            {
                if (!IsBlockedIfMove(coords, From, To, King, board)) return false;
            }
            return true;
        }

        public override string ToString()
        {
            return "K";
        }

       
        /// <summary>
        /// Overloaded method that validates that attacker cant hit king in this direction
        /// </summary>
        /// <param name="coord"></param>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <param name="King"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool IsBlockedIfMove(List<(int, int)> coord, (int xFrom, int yFrom) From, (int xTo, int yTo) To, (int xKing, int yKing) King, ChessBoard board)
        {
            if (coord.Contains(King) && !coord.Contains(To))
            {
                return false;
            }
            else if (coord.Contains(From))
            {
                int toIndex = coord.IndexOf(To);
                if (0 <= toIndex && toIndex < coord.Count)
                    return true;
                else
                {
                    (int, int) direct = DefineDirectionOfTheList(coord);
                    foreach ((int, int) move in board.GoInGivenDirection(From.xFrom, From.yFrom, direct.Item1, direct.Item2))
                    {
                        if (move == King)
                            return false;
                        else return true;
                    }

                }
            }
            return true;
        }
    }

    


}
