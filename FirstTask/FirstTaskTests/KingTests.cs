using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLibrary;
using System.Collections.Generic;

namespace FirstTaskTests
{
    [TestClass]
    public class KingTests
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
            List<(int, int)> expected = new List<(int, int)> { (7,6),(6,7),(6,6) };
            int xKing = 7;
            int yKing = 7;
            King king = new King(xKing, yKing, PlayerColor.White);
            List<(int, int)> actual = new List<(int, int)> { };
            king.CalculatePossibleMoves(board);

            // Act
            foreach (var move in king.GetPossibleMoves)
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
            int xKing = 7;
            int yKing = 7;
            King king = new King(xKing, yKing, PlayerColor.White);

            // Act
            (int, int) actual = king.DefineDirectionOfTheList(king.PossibleMoves[0]);

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
            int xKing = 4;
            int yKing = 7;
            King king = new King(xKing, yKing, PlayerColor.White);
            bool expected = true;
            board.CalculateLegalMoves(board);

            // Act
            bool actual = king.IsBlockedIfMove((2, 5), (3, 6), (1, 4), board);

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
            int xKing = 1;
            int yKing = 5;
            King king = new King(xKing, yKing, PlayerColor.White);
            bool expected = false;
            king.CalculatePossibleMoves(board);

            // Act
            bool actual = king.IsBlockedIfMove((2, 5), (0, 2), (1, 4), board);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
