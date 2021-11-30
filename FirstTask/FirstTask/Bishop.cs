using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public class Bishop : Figure
    {
        private List<List<(int, int)>> possibleMoves;
        private List<(int, int)> possibleMovesRightUp;
        private List<(int, int)> possibleMovesRightDown;
        private List<(int, int)> possibleMovesLeftDown;
        private List<(int, int)> possibleMovesLeftUp;

        public List<List<(int, int)>> PossibleMoves { get => possibleMoves; }

        public Bishop(Figure figure) : base(figure.X, figure.Y, figure.Player)
        {
            possibleMovesRightDown = new List<(int, int)> { };
            possibleMovesRightUp = new List<(int, int)> { };
            possibleMovesLeftDown = new List<(int, int)> { };
            possibleMovesLeftUp = new List<(int, int)> { };
            possibleMoves = new List<List<(int, int)>> { possibleMovesLeftDown, possibleMovesLeftUp, possibleMovesRightDown, possibleMovesRightUp };
        }

        public Bishop(
          int x,
          int y,
          PlayerColor color
          ) : base(x, y, color)
        {
            possibleMovesRightDown = new List<(int, int)> { };
            possibleMovesRightUp = new List<(int, int)> { };
            possibleMovesLeftDown = new List<(int, int)> { };
            possibleMovesLeftUp = new List<(int, int)> { };
            possibleMoves = new List<List<(int, int)>> { possibleMovesLeftDown, possibleMovesLeftUp, possibleMovesRightDown, possibleMovesRightUp };
        }

        public override IEnumerable<(int, int)> GetPossibleMoves
        {
            get
            {
                foreach (var element in possibleMoves)
                {
                    foreach (var coords in element)
                    {
                        yield return (coords.Item1, coords.Item2);
                    }
                }
            }
        }

        
        /// <summary>
        /// Validates that move is possible to move
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

        /// <summary>
        /// Checks cell for any figure
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool CheckForPresence(int x, int y, ChessBoard board)
        {
            if (board.GetCell(x, y).figure == null)
                return true;
            else return false;
        }

        /// <summary>
        /// Checks cells in given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="possibleDirectionMoves"></param>
        /// <param name="board"></param>
        public void CheckLineOfSight((int, int) direction, List<(int, int)> possibleDirectionMoves, ChessBoard board)
        {
            bool flag = true;
            int counter = 1;
            while (flag)
            {
                flag = false;
                if (board.ValidateCoords(X + (counter * direction.Item1), Y + (counter * direction.Item2)))
                {
                    flag = CheckForPresence(X + (counter * direction.Item1), Y + (counter * direction.Item2), board);
                    board.GetCell(X + (counter * direction.Item1), Y + (counter * direction.Item2)).possibleToHit.Add(this);
                    TypeOfPossibility(possibleDirectionMoves, X + (counter * direction.Item1), Y + (counter * direction.Item2), board);
                }
                counter++;
            }
        }

        public override void CalculatePossibleMoves(ChessBoard board)
        {
            CheckLineOfSight((1, 1), possibleMovesRightDown, board);
            CheckLineOfSight((1, -1), possibleMovesLeftDown, board);
            CheckLineOfSight((-1, 1), possibleMovesRightUp, board);
            CheckLineOfSight((-1, -1), possibleMovesLeftUp, board);
        }


      
        /// <summary>
        /// DefinesDirectionOfTheList
        /// </summary>
        /// <param name="movesToDefine"></param>
        /// <returns></returns>
        public (int,int) DefineDirectionOfTheList(List<(int,int)> movesToDefine)
        {
            if (movesToDefine.GetHashCode() == possibleMovesLeftDown.GetHashCode())
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

        /// <summary>
        /// Validates that attacker cant hit king in this direction
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
                    }

                }
            }
            return true;
        }

        public override string ToString()
        {
            return "B";
        }


    }


}
