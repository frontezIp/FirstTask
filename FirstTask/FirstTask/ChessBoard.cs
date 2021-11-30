using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChessLibrary
{
    public class ChessBoard
    {
        private Cell[,] board;

        public Cell[,] Board { get => board; }

        public ChessBoard()
        {
            board = new Cell[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = new Cell(i, j);
                }
            }
            for (int i = 0; i < 8; i++)
            {
                AddFigure(board[1, i], new Pawn(1, i, PlayerColor.White));
            }
            AddFigure(board[0, 0], new Rook(0, 0, PlayerColor.White));
            AddFigure(board[0, 7], new Rook(0, 7, PlayerColor.White));
            AddFigure(board[0, 1], new Knight(0, 1, PlayerColor.White));
            AddFigure(board[0, 6], new Knight(0, 6, PlayerColor.White));
            AddFigure(board[0, 2], new Bishop(0, 2, PlayerColor.White));
            AddFigure(board[0, 5], new Bishop(0, 5, PlayerColor.White));
            AddFigure(board[0, 3], new Queen(0, 3, PlayerColor.White));
            AddFigure(board[0, 4], BlackKing = new King(0, 4, PlayerColor.White));
            for (int i = 0; i < 8; i++)
            {
                AddFigure(board[6, i], new Pawn(6, i, PlayerColor.Black));
            }

            AddFigure(board[7, 0], new Rook(7, 0, PlayerColor.Black));
            AddFigure(board[7, 7], new Rook(7, 7, PlayerColor.Black));
            AddFigure(board[7, 1], new Knight(7, 1, PlayerColor.Black));
            AddFigure(board[7, 6], new Knight(7, 6, PlayerColor.Black));
            AddFigure(board[7, 2], new Bishop(7, 2, PlayerColor.Black));
            AddFigure(board[7, 5], new Bishop(7, 5, PlayerColor.Black));
            AddFigure(board[7, 3], new Queen(7, 3, PlayerColor.Black));
            AddFigure(board[7, 4], WhiteKing = new King(7, 4, PlayerColor.Black));
        }

        public class Cell
        {
            public int X;

            public int Y;

            public Cell(int x , int y)
            {
                possibleToHit = new List<Figure>();
                X = x;
                Y = y;
            }
            public Figure figure
            {
                get; set;
            }
     

            public List<Figure> possibleToHit;

        }

        private PlayerColor color;

        public PlayerColor Color { get => color; set => color = value; }
        public Figure BlackKing { get => blackKing; set => blackKing = value; }
        public Figure WhiteKing { get => whiteKing; set => whiteKing = value; }
        public bool InCheck { get => inCheck; }

        /// <summary>
        /// Return cell with given coords
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Cell GetCell(int x, int y)
        {
            if (x >= 0 && x <= 7 && y >= 0 && y <= 7) return board[x, y];

            else return null;
        }

        /// <summary>
        /// Go in given direction trough all cells until the edge 
        /// </summary>
        /// <param name="xFigure"></param>
        /// <param name="yFigure"></param>
        /// <param name="xDirection"></param>
        /// <param name="yDirection"></param>
        /// <returns></returns>
        public IEnumerable<(int,int)> GoInGivenDirection(int xFigure,int yFigure,int xDirection,int yDirection)
        {
            for(int i = 1; i <= 7; i++)
            {
                if (!ValidateCoords(xFigure + xDirection * i, yFigure + yDirection * i))
                    yield break;
                yield return (xFigure + xDirection * i, yFigure + yDirection * i);

                if (GetCell(xFigure + xDirection * i, yFigure + yDirection * i).figure != null)
                    yield break;
            }
        }

        /// <summary>
        /// Add given figure to the given cell
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="figure"></param>
        public void AddFigure(Cell cell, Figure figure)
        {
            cell.figure = figure;
            figures.Add(figure);
        }

        public List<Figure> figures = new List<Figure>();

        private bool inCheck;

        /// <summary>
        /// Checks wheter king in check or not
        /// </summary>
        /// <param name="color"></param>
        public void IsCheck(PlayerColor color)
        {
            if (color==PlayerColor.White)
            {
                foreach(Figure hit in board[WhiteKing.X, WhiteKing.Y].possibleToHit)
                {
                    if (hit.Player != WhiteKing.Player)
                    {
                        inCheck = true;
                        return;
                    }
                    
                }
            }
            else
            {
                foreach (Figure hit in board[BlackKing.X, BlackKing.Y].possibleToHit)
                {
                    if (hit.Player != WhiteKing.Player)
                    {
                        inCheck = true;
                        return;
                    }
                }
            }
            inCheck = false;

        }

        private Figure blackKing;

        private Figure whiteKing;


        /// <summary>
        /// Validate if coords is legal
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool ValidateCoords(int x,int y)
        {
            if (x >= 0 && x <= 7 && y >= 0 && y <= 7) return true;

            else return false;
        }

        /// <summary>
        /// Start at every turn and prepare figures
        /// Return false if current player dont have any legal move which means he lost
        /// </summary>
        /// <param name="player"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool StartMove(PlayerColor player, ChessBoard board)
        {
            color = player;
            IsCheck(color);
            ClearHits(board);
            PrepareFigures(board);
            bool isLoose = CalculateLegalMoves(board);
            return isLoose;
        }

        /// <summary>
        /// Cleal all hits from cells
        /// </summary>
        /// <param name="board"></param>
        public void ClearHits(ChessBoard board)
        {
            for(int i= 0; i<8; i++)
            {
                for(int j =0; j<8; j++)
                {
                    this.board[i, j].possibleToHit.Clear();
                }
            }
        }

        /// <summary>
        /// Calculate all legal moves of the figures
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool CalculateLegalMoves(ChessBoard board)
        {
            bool anyMoveToMake = false;
            foreach(Figure figure in figures)
            {
                figure.LegalMoves.Clear();
                if(figure.Player == color)
                {
                    foreach(var move in figure.GetPossibleMoves)
                    {
                        if (LegalMove(move.Item1, move.Item2, figure, board))
                        {
                            figure.LegalMoves.Add(move);
                            anyMoveToMake = true;
                        }
                    }
                }
            }
            return anyMoveToMake;
        }


        /// <summary>
        /// Calculate all possible moves of the figures
        /// </summary>
        /// <param name="board"></param>
        public void PrepareFigures(ChessBoard board)
        {
            foreach (Figure figure in figures)
                figure.CalculatePossibleMoves(board);
        }

        /// <summary>
        /// Move figure from one cell to another
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="promotionOption"></param>
        /// <param name="board"></param>
        public void Move(Cell from, Cell to,Promotion promotionOption,ChessBoard board)
        {
            if (to.figure != null)
                figures.Remove(to.figure);

            to.figure = from.figure;
            from.figure = null;
            to.figure.X = to.X;
            to.figure.Y = to.Y; 
            if(to.figure.GetType() == typeof(Pawn) && to.X == (to.figure.Player == PlayerColor.White ? 7 :0))
            {
                Figure promoted = null;
                switch (promotionOption)
                {
                    case Promotion.Queen:
                        promoted = new Queen(to.figure);
                        break;
                    case Promotion.Rook:
                        promoted = new Rook(to.figure);
                        break;
                    case Promotion.Bishop:
                        promoted = new Bishop(to.figure);
                        break;
                    case Promotion.Knight:
                        promoted = new Knight(to.figure);
                        break;
                    
                }
                figures.Remove(to.figure);
                to.figure = promoted;
                figures.Add(promoted);
            }          
            to.figure.CalculatePossibleMoves(board);
        }

        /// <summary>
        /// Checks whether move is legal or not
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="figure"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool LegalMove(int x, int y,Figure figure,ChessBoard board)
        {
            Figure currentKing = CurrentKing();
            if (figure.GetType() == typeof(King))
            {
                foreach (Figure hit in this.board[x, y].possibleToHit)
                {
                    if ((hit.X,hit.Y) != (x,y) && hit.Player != figure.Player)
                        return false;
                }
            }
            else
            {
                if (inCheck)
                {
                    foreach(Figure hit in this.board[currentKing.X, currentKing.Y].possibleToHit)
                    {
                        if (this.board[hit.X, hit.Y] == this.board[x, y]) continue;
                        if (hit.IsBlockedIfMove((figure.X, figure.Y),(x, y), (currentKing.X, currentKing.Y), board)) continue;
                        if (hit.Player == currentKing.Player) continue;
                        return false;
                    }
                }
                foreach(Figure hit in this.board[figure.X, figure.Y].possibleToHit)
                {
                    if (this.board[hit.X, hit.Y] == this.board[x, y]) continue;
                    if (hit.Player == currentKing.Player) continue;
                    if (!hit.IsBlockedIfMove((figure.X, figure.Y), (x, y), (currentKing.X, currentKing.Y), board))
                        return false;
                }
            }
            return true;
        }

        
        /// <summary>
        /// Return current king
        /// </summary>
        /// <returns></returns>
        public Figure CurrentKing()
        {
            if (color == PlayerColor.White)
                return WhiteKing;
            else
                return BlackKing;
        }
    }
}
