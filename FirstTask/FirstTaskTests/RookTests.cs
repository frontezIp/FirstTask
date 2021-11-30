using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLibrary;
using System.Collections.Generic;

namespace FirstTaskTests
{
    [TestClass]
    public class RookTests
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
        /// Tests calculatePossibleMoves method
        /// </summary>
        [TestMethod]
        public void CalculatePossibleMoves_ShouldCalculateAllPossibleMoves()
        {
            // Arrange
            List<(int, int)> expected = new List<(int, int)> { (7, 6), (7, 5), (7, 4), (7, 3), (7, 2), (7, 1), (7, 0), (6, 7), (5, 7), (4, 7), (3, 7), (2, 7), (1, 7), (0, 7) };
            int xRook = 7;
            int yRook = 7;
            Rook rook = new Rook(xRook, yRook, PlayerColor.White);
            List<(int, int)> actual = new List<(int, int)> { };
            rook.CalculatePossibleMoves(board);

            // Act
            foreach (var move in rook.GetPossibleMoves)
            {
                actual.Add(move);
            }

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Tests defineDirectionOfTheList Method
        /// </summary>
        [TestMethod]
        public void DefineDirectionOfTheList_ShouldReturnCorrectDirectionOfGivenListOfMoves()
        {
            // Arrange
            (int, int) expected = (0, -1);
            int xRook = 7;
            int yRook = 7;
            Rook rook = new Rook(xRook, yRook, PlayerColor.White);

            // Act
            (int, int) actual = rook.DefineDirectionOfTheList(rook.PossibleMoves[0]);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Tests IsBlockedIfMove method in scenario when figure is moving but still blocking the king
        /// </summary>
        [TestMethod]
        public void IsBlockedIfMove_FigureMovesButStillBlockingTheKing_ShouldReturnTrue()
        {
            // Arrange
            int xRook = 7;
            int yRook = 7;
            Rook rook = new Rook(xRook, yRook, PlayerColor.White);
            bool expected = true;
            board.CalculateLegalMoves(board);

            // Act
            bool actual = rook.IsBlockedIfMove((2, 5), (3, 6), (1, 4), board);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Tests IsBlockedIfMove method in scenario when figure is moving and not blocking the king
        /// </summary>
        [TestMethod]
        public void IsBlockedIfMove_FigureMovesAndDontBlockTheWayToTheKing_ShouldReturnFalse()
        {
            // Arrange
            int xRook = 1;
            int yRook = 6;
            Rook rook = new Rook(xRook, yRook, PlayerColor.White);
            bool expected = false;
            rook.CalculatePossibleMoves(board);

            // Act
            bool actual = rook.IsBlockedIfMove((2, 5), (0, 2), (1, 4), board);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
