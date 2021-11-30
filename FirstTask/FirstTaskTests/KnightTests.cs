using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLibrary;
using System.Collections.Generic;

namespace FirstTaskTests
{
    [TestClass]
    public class KnightTests
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
            List<(int, int)> expected = new List<(int, int)> { (5, 6),(6,5)  };
            int xKnight = 7;
            int yKnight = 7;
            Knight knight = new Knight(xKnight, yKnight, PlayerColor.White);
            List<(int, int)> actual = new List<(int, int)> { };
            knight.CalculatePossibleMoves(board);

            // Act
            foreach (var move in knight.GetPossibleMoves)
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
        public void IsBlockedIfMove_KingIsOutOfDanger_ShouldReturnTrue()
        {
            // Arrange
            int xKnight = 7;
            int yKnight = 7;
            Knight knight = new Knight(xKnight, yKnight, PlayerColor.White);
            bool expected = true;
            board.CalculateLegalMoves(board);

            // Act
            bool actual = knight.IsBlockedIfMove((2, 5), (3, 6), (1, 4), board);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Tests IsBlockedIfMove method in scenario when king in check
        /// </summary>
        [TestMethod]
        public void IsBlockedIfMove_KingInCheckOfKnight_ShouldReturnFalse()
        {
            // Arrange
            int xKnight = 7;
            int yKnight = 7;
            Knight knight = new Knight(xKnight, yKnight, PlayerColor.White);
            bool expected = false;
            knight.CalculatePossibleMoves(board);

            // Act
            bool actual = knight.IsBlockedIfMove((2, 5), (0, 2), (6, 5), board);

            // Assert
            Assert.AreEqual(expected, actual);
        }


    }
}
