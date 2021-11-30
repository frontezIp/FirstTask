using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLibrary;
using System.Collections.Generic;

namespace FirstTaskTests
{
    [TestClass]
    public class QueenTests
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
            List<(int, int)> expected = new List<(int, int)> { (7,6),(7,5),(7,4),(7,3),(7,2),(7,1),(7,0),(6,7),(5,7),(4,7),(3,7),(2,7),(1,7),(0,7),(6,6),(5,5),(4,4),(3,3),(2,2),(1,1),(0,0) };
            int xQueen = 7;
            int yQueen = 7;
            Queen queen = new Queen(xQueen, yQueen, PlayerColor.White);
            List<(int, int)> actual = new List<(int, int)> { };
            queen.CalculatePossibleMoves(board);

            // Act
            foreach (var move in queen.GetPossibleMoves)
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
            int xQueen = 7;
            int yQueen = 7;
            Queen queen = new Queen(xQueen, yQueen, PlayerColor.White);

            // Act
            (int, int) actual = queen.DefineDirectionOfTheList(queen.PossibleMoves[0]);

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
            int xQueen = 7;
            int yQueen = 7;
            Queen queen = new Queen(xQueen, yQueen, PlayerColor.White);
            bool expected = true;
            board.CalculateLegalMoves(board);

            // Act
            bool actual = queen.IsBlockedIfMove((2, 5), (3, 6), (1, 4), board);

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
            int xQueen = 4;
            int yQueen = 7;
            Queen queen = new Queen(xQueen, yQueen, PlayerColor.White);
            bool expected = false;
            queen.CalculatePossibleMoves(board);

            // Act
            bool actual = queen.IsBlockedIfMove((2, 5), (0, 2), (1, 4), board);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
