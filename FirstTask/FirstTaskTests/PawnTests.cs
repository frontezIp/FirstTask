using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLibrary;
using System.Collections.Generic;


namespace FirstTaskTests
{
    [TestClass]
    public class PawnTests
    {
        private ChessBoard board;

        [TestInitialize]
        public void TestInitialize()
        {
            board = new ChessBoard();
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    board.Board[i, j].figure = null;
                }
            }
            board.figures.Clear();
        }

        /// <summary>
        /// Tests method CalculatePossibleMoves
        /// </summary>
        [TestMethod]
        public void CalculatePossibleMoves_ForwardCellClearRightAndLeftClear_ShouldAddForwardCellToPossibleMove()
        {
            // Arrange
            List<(int, int)> expected = new List<(int, int)> { (5,6),(4,6) };
            int xPawn = 6;
            int yPawn = 6;
            Pawn pawn = new Pawn(xPawn, yPawn, PlayerColor.Black);
            List<(int, int)> actual = new List<(int, int)> { };
            pawn.CalculatePossibleMoves(board);

            // Act
            foreach (var move in pawn.GetPossibleMoves)
            {
                actual.Add(move);
            }

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Tests method CalculatePossibleMoves
        /// </summary>
        [TestMethod]
        public void CalculatePossibleMoves_ForwardCellClearRightAndLeftAreNotClear_ShouldAddForwardCellRightCellAndLeftCellToPossibleMove()
        {
            // Arrange
            List<(int, int)> expected = new List<(int, int)> { (5, 6), (4, 6),(5,5),(5,7) };
            int xPawn = 6;
            int yPawn = 6;
            Pawn pawn = new Pawn(xPawn, yPawn, PlayerColor.Black);
            board.GetCell(5, 5).figure = new Bishop(5, 5, PlayerColor.White);
            board.GetCell(5, 7).figure = new Bishop(5, 7, PlayerColor.White);
            List<(int, int)> actual = new List<(int, int)> { };
            pawn.CalculatePossibleMoves(board);

            // Act
            foreach (var move in pawn.GetPossibleMoves)
            {
                actual.Add(move);
            }

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Tests IsBlockedIfMove method in scenario when figure is moving but still blocking the king
        /// </summary>
        [TestMethod]
        public void IsBlockedIfMove_KingIsOutOfDanger_ShouldReturnTrue()
        {
            // Arrange
            bool expected = true;
            int xPawn = 6;
            int yPawn = 6;
            int xKing = 1;
            int yKing = 4;
            King king = new King(xKing, yKing, PlayerColor.White);
            Pawn pawn = new Pawn(xPawn, yPawn, PlayerColor.Black);
            board.GetCell(xKing, yKing).figure = king;
            pawn.CalculatePossibleMoves(board);

            // Act
            bool actual = pawn.IsBlockedIfMove((2, 5), (3, 6), (xKing,yKing), board);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Tests IsBlockedIfMove method in scenario when figure is moving and not blocking the king
        /// </summary>
        [TestMethod]
        public void IsBlockedIfMove_KingInCheckOfKnight_ShouldReturnFalse()
        {
            // Arrange
            bool expected = false;
            int xPawn = 6;
            int yPawn = 6;
            int xKing = 5;
            int yKing = 7;
            King king = new King(xKing, yKing, PlayerColor.White);
            Pawn pawn = new Pawn(xPawn, yPawn, PlayerColor.Black);
            board.GetCell(xKing, yKing).figure = king;
            pawn.CalculatePossibleMoves(board);

            // Act
            bool actual = pawn.IsBlockedIfMove((2, 5), (3, 6), (xKing, yKing), board);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
