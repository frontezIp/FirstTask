using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLibrary;
using System.Collections.Generic;

namespace FirstTaskTests
{
    [TestClass]
    public class BishopTests
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
            List<(int, int)> expected = new List<(int, int)> {  (5, 6), (6, 5), (7, 4),(3, 6), (2, 5), (1, 4), (0, 3) };
            int xBishop = 4;
            int yBishop = 7;
            Bishop bishop = new Bishop(xBishop, yBishop, PlayerColor.White);
            List<(int, int)> actual = new List<(int, int)> { };
            bishop.CalculatePossibleMoves(board);

            // Act
            foreach(var move in bishop.GetPossibleMoves)
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
            (int, int) expected = (1, -1);
            int xBishop = 4;
            int yBishop = 7;
            Bishop bishop = new Bishop(xBishop, yBishop, PlayerColor.White);

            // Act
            (int, int) actual = bishop.DefineDirectionOfTheList(bishop.PossibleMoves[0]);

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
            int xBishop = 4;
            int yBishop = 7;
            Bishop bishop = new Bishop(xBishop, yBishop, PlayerColor.White);
            bool expected = true;
            board.CalculateLegalMoves(board);

            // Act
            bool actual = bishop.IsBlockedIfMove((2, 5), (3, 6), (1, 4), board);

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
            int xBishop = 4;
            int yBishop = 7;
            Bishop bishop = new Bishop(xBishop, yBishop, PlayerColor.White);
            bool expected = false;
            bishop.CalculatePossibleMoves(board);

            // Act
            bool actual = bishop.IsBlockedIfMove((2, 5), (0, 2), (1, 4), board);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
