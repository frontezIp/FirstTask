using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLibrary;
using System.Collections.Generic;

namespace FirstTaskTests
{
    [TestClass]
    public class PlayChessTests
    {
        private PlayChess play;

        [TestInitialize]
        public void TestInitialize()
        {
            play = new PlayChess(@"C:\Users\User\source\repos\FirstTask\FirstTask\Logs.txt",true);
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    play.Board.Board[i, j].figure = null;
                }
            }
            play.Board.figures.Clear();
        }

        /// <summary>
        /// Tests ChoosePromotion method
        /// </summary>
        [TestMethod]
        public void ChoosePromotion_ShouldInitializeChoosedPromotion()
        {
            // Arrange
            Promotion expected = Promotion.Queen;

            // Act
            play.ChoosePromotion(Promotion.Queen);
            Promotion actual = play.Promotion;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests TurnMessage method on any errors
        /// </summary>
        [TestMethod]
        public void TurnMessage_ShouldNotThrowException()
        {
            // Arrange

            bool expected = true;
            bool actual = true;

            // Act
            play.TurnMessage();

            // Assert
            Assert.AreEqual(expected, actual);       
        }
        /// <summary>
        /// Tests LooserMessage method on any errors
        /// </summary>

        [TestMethod]
        public void LooserMessage_ShouldNotThrowException()
        {
            // Arrange

            bool expected = true;
            bool actual = true;

            // Act
            play.LooserMessage();

            // Assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Tests MoveMessage method on any errors
        /// </summary>
        [TestMethod]
        public void MoveMessage_ShouldNotThrowException()
        {
            // Arrange

            bool expected = true;
            bool actual = true;

            // Act
            play.MoveMessage(play.Board.GetCell(4,4), play.Board.GetCell(5,5));

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests Interactionm method in scenario when in parameters was send coords of the figure that has diifer colour from the current player
        /// </summary>
        [TestMethod]
        public void Interaction_CoordsOfTheFigureThatHaveDifferentColourFromPlayerOne_ShouldNotMoveTheFigure()
        {
            // Arrange
            int xRook = 7;
            int yRook = 7;
            int xTo = 6;
            int yTo = 7;
            play.PlayerColor = PlayerColor.White;
            Rook rook = new Rook(xRook, yRook, PlayerColor.Black);
            play.Board.AddFigure(play.Board.GetCell(xRook, yRook), rook);
            object expected = null;
            play.Board.PrepareFigures(play.Board);
            play.Board.CalculateLegalMoves(play.Board);

            // Act
            play.Interaction(xRook, yRook, xTo, yTo);
            object actual = play.Board.GetCell(xTo, yTo).figure;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests Interactionm method in scenario when chosen figure cant move in given place
        /// </summary>
        [TestMethod]
        public void Interaction_MovingFigureToGivenCoordsIsNotPossibleDueToInAbilityToMoveThere_ShouldNotMoveTheFigure()
        {
            // Arrange
            int xRook = 7;
            int yRook = 7;
            int xTo = 3;
            int yTo = 2;
            play.PlayerColor = PlayerColor.White;
            Rook rook = new Rook(xRook, yRook, PlayerColor.White);
            play.Board.AddFigure(play.Board.GetCell(xRook, yRook), rook);
            object expected = null;
            play.Board.PrepareFigures(play.Board);
            play.Board.CalculateLegalMoves(play.Board);

            // Act
            play.Interaction(xRook, yRook, xTo, yTo);
            object actual = play.Board.GetCell(xTo, yTo).figure;

            // Assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Tests Interaction method
        /// </summary>
        [TestMethod]
        public void Interaction_MovingFigureToLegalCoords_ShouldMoveFigureToGivenCoords()
        {
            // Arrange
            int xRook = 7;
            int yRook = 7;
            int xTo = 6;
            int yTo = 7;
            play.PlayerColor = PlayerColor.White;
            Rook rook = new Rook(xRook, yRook, PlayerColor.White);
            play.Board.AddFigure(play.Board.GetCell(xRook, yRook), rook);
            Rook expected = rook;
            play.Board.PrepareFigures(play.Board);
            play.Board.CalculateLegalMoves(play.Board);

            // Act
            play.Interaction(xRook, yRook, xTo, yTo);
            Figure actual = play.Board.GetCell(xTo, yTo).figure;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests TurnOver method
        /// </summary>
        [TestMethod]
        public void TurnOver_ShouldChangeColourOfTheCurrentPlayerFromWhiteToBlack()
        {

            // Arrange
            int xRook = 7;
            int yRook = 7;
            int xTo = 6;
            int yTo = 7;
            play.PlayerColor = PlayerColor.White;
            Rook rook = new Rook(xRook, yRook, PlayerColor.White);
            play.Board.AddFigure(play.Board.GetCell(xRook, yRook), rook);
            PlayerColor expected = PlayerColor.Black;
            play.Board.PrepareFigures(play.Board);
            play.Board.CalculateLegalMoves(play.Board);

            // Act
            play.Interaction(xRook, yRook, xTo, yTo);
            PlayerColor actual = play.PlayerColor;

            // Assert
            Assert.AreEqual(expected, actual);

        }

    }
}
