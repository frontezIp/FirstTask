using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary
{
    public enum Promotion
    {
        Queen = 0,
        Rook = 1,
        Knight = 2,
        Bishop = 3
    }

    public enum PlayerColor
    {
        White,Black
    }

        
    public class PlayChess
    {
        private ChessBoard board;

        private PlayerColor playerColor;

        private Promotion promotion;

        private bool isRunning;

        private ChessBoard.Cell _currentCell;

        private ChessBoard.Cell _moveToCell;

        public ChessBoard Board { get => board; }

        public PlayerColor PlayerColor { get => playerColor; set => playerColor = value; }
        public Promotion Promotion { get => promotion; }
        

        private Logger logger;

        public PlayChess(string path,bool run = false)
        {
            board = new ChessBoard();
            isRunning = true;
            playerColor = PlayerColor.White;
            logger = new Logger(path);
            logger.StartLog();

        }

        /// <summary>
        /// Sets chosen promotion to upcoming pawn
        /// </summary>
        /// <param name="promotion"></param>
        public void ChoosePromotion(Promotion promotion)
        {
            this.promotion = promotion;
        }

        /// <summary>
        /// Send log when new turn start
        /// </summary>
        public void TurnMessage()
        {
            if (playerColor == PlayerColor.White)
                logger.Log("White's turn");
            else
                logger.Log("Black's turn");
        }

        /// <summary>
        /// Send log when game is ended
        /// </summary>
        public void LooserMessage()
        {
            string message = playerColor == PlayerColor.White ? "White loose" : "Black loose";
            logger.Log(message);
        }

        /// <summary>
        /// Send log when someone move
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void MoveMessage(ChessBoard.Cell from,ChessBoard.Cell to)
        {
            if (to.figure != null)
            {
                logger.Log($"{Convert.ToChar(from.Y + 97)}x{Convert.ToChar(to.Y + 97)}{to.X}");
            }
            else
            {
                logger.Log($"{Convert.ToChar(to.Y + 97)}{to.X}");
            }
        }


        /// <summary>
        /// Move figures according to given coords if its possible
        /// </summary>
        /// <param name="xFrom"></param>
        /// <param name="yFrom"></param>
        /// <param name="xTo"></param>
        /// <param name="yTo"></param>
        public void Interaction(int xFrom, int yFrom, int xTo, int yTo)
        {
            if(board.ValidateCoords(xFrom,yFrom) && board.ValidateCoords(xTo, yTo))
            {
                _currentCell = board.GetCell(xFrom, yFrom);

                if (_currentCell.figure.Player != playerColor || _currentCell.figure == null || _currentCell.figure.LegalMoves.Count == 0)
                    return;

                if (_currentCell.figure.GetType() == typeof(Pawn) && (xTo == 0 || xTo == 7))
                    ChoosePromotion(Promotion.Queen);

                if (!_currentCell.figure.LegalMoves.Contains((xTo, yTo)))
                    return;

                _moveToCell = board.GetCell(xTo, yTo);

                TurnOver();
            }
            return;
        }


        /// <summary>
        /// End every turn
        /// </summary>
        public void TurnOver()
        {
            MoveMessage(_currentCell, _moveToCell);
            board.Move(_currentCell, _moveToCell, promotion, board);
            _currentCell = null;
            _moveToCell = null;
            playerColor = playerColor == PlayerColor.White ? PlayerColor.Black : PlayerColor.White;
        }
    }
}
