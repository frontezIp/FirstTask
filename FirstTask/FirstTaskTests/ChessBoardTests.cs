using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessLibrary;
using System.Collections.Generic;

namespace FirstTaskTests
{
    [TestClass]
    public class ChessBoardTests
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
        /// Test method GetCell with valid coords
        /// </summary>
        [TestMethod]
        public void GetCell_WithValidCoords_ShouldReturnCellOfTheBoardAccordingToGivingCoords()
        {
            // Arrange
            int x = 0;
            int y = 0;
            ChessBoard.Cell exptected = board.Board[x, y];

            // Act
            ChessBoard.Cell actual = board.GetCell(x, y);

            // Assert
            Assert.AreEqual(exptected, actual);
        }

        /// <summary>
        /// Test method GetCell with Invalid coords
        /// </summary>
        [TestMethod]
        public void GetCell_WithInvalidCoords_ShouldReturnNull()
        {
            // Arrange
            int x = 8;
            int y = 8;
            ChessBoard.Cell exptected = null;

            // Act
            ChessBoard.Cell actual = board.GetCell(x, y);

            // Assert
            Assert.AreEqual(exptected, actual);
        }

        /// <summary>
        /// Tests method GoInGivenDirection
        /// </summary>
        [TestMethod]
        public void GoInGivenDirection_ShouldIterateTroughExpectedCells()
        {
            // Arrange
            List<(int, int)> expected = new List<(int, int)> { (5, 5), (6, 6), (7, 7) };
            List<(int, int)> actual = new List<(int, int)> { };
            Bishop bishop = new Bishop(4, 4, PlayerColor.White);

            // Act
            foreach (var coords in board.GoInGivenDirection(bishop.X, bishop.Y, 1, 1))
                actual.Add(coords);

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests addFigure method
        /// </summary>
        [TestMethod]
        public void AddFigure_ShouldAddFigure()
        {
            // Arrange
            Bishop expected = new Bishop(4, 4, PlayerColor.White);

            // Act
            board.AddFigure(board.Board[4, 4], expected);
            Figure actual = board.figures[0];

            // Arrange
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests IsCheckMethod in scenario where king is sorrounded by enemies figures
        /// </summary>
        [TestMethod]
        public void IsCheck_KingSurroundByEnemyFigures_ShouldReturnTrue()
        {
            // Arrange
            int xKing = 5;
            int yKing = 5;
            int xRook = 5;
            int yRook = 7;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            board.AddFigure(board.GetCell(xKing, yKing), king);
            board.AddFigure(board.GetCell(xRook, yRook), new Rook(xRook, yRook, PlayerColor.Black));
            board.PrepareFigures(board);
            bool expected = true;

            // Act
            board.IsCheck(PlayerColor.White);
            bool actual = board.InCheck;


            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests IsCheckMethod in scenario where king is not sorrounded by enemies figures
        /// </summary>
        [TestMethod]
        public void IsCheck_KingNotSurrouned_ShouldReturnFalse()
        {
            int xKing = 5;
            int yKing = 5;
            int xRook = 4;
            int yRook = 7;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            board.AddFigure(board.GetCell(xKing, yKing), king);
            board.AddFigure(board.GetCell(xRook, yRook), new Rook(xRook, yRook, PlayerColor.Black));
            board.PrepareFigures(board);
            bool expected = false;

            // Act
            board.IsCheck(PlayerColor.White);
            bool actual = board.InCheck;


            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test method ValidateCoords with valid coords
        /// </summary>
        [TestMethod]
        public void ValidateCoords_WithValidCoords_ShouldReturnTrue()
        {
            // Arrange
            int x = 0;
            int y = 0;
            bool exptected = true;

            // Act
            bool actual = board.ValidateCoords(x, y);

            // Assert
            Assert.AreEqual(exptected, actual);
        }

        /// <summary>
        /// Test method ValidateCoords with Invalid coords
        /// </summary>
        [TestMethod]
        public void ValidateCoords_WithInvalidCoords_ShouldReturnFalse()
        {
            // Arrange
            int x = 8;
            int y = 8;
            bool exptected = false;

            // Act
            bool actual = board.ValidateCoords(x, y);

            // Assert
            Assert.AreEqual(exptected, actual);
        }

        /// <summary>
        /// Test method StartMove that return true if figures of current side
        /// have any legal move and return false if figures dont have any legal moves
        /// </summary>
        [TestMethod]
        public void StartMove_ShouldReturnFalse()
        {
            int xKing = 5;
            int yKing = 5;
            int xRook = 5;
            int yRook = 7;
            int x2Rook = 4;
            int x3Rook = 6;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            board.AddFigure(board.GetCell(xKing, yKing), king);
            board.AddFigure(board.GetCell(xRook, yRook), new Rook(xRook, yRook, PlayerColor.Black));
            board.AddFigure(board.GetCell(x2Rook, yRook), new Rook(x2Rook, yRook, PlayerColor.Black));
            board.AddFigure(board.GetCell(x3Rook, yRook), new Rook(x3Rook, yRook, PlayerColor.Black));
            board.AddFigure(board.GetCell(xRook, 0), new Rook(xRook, 0, PlayerColor.Black));
            board.PrepareFigures(board);
            bool expected = false;

            // Act
            bool actual = board.StartMove(PlayerColor.White, board);


            // Assert
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// Test method StartMove that return true if figures of current side
        /// have any legal move and return false if figures dont have any legal moves
        /// </summary>
        [TestMethod]
        public void StartMove_ShouldReturnTrue()
        {
            int xKing = 5;
            int yKing = 5;
            int xRook = 4;
            int yRook = 7;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            board.AddFigure(board.GetCell(xKing, yKing), king);
            board.AddFigure(board.GetCell(xRook, yRook), new Rook(xRook, yRook, PlayerColor.Black));
            bool expected = true;

            // Act
            bool actual = board.StartMove(PlayerColor.White, board);


            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests method ClearHits
        /// </summary>
        [TestMethod]
        public void ClearHits_ShouldClearAllCellsFromProssibleHits()
        {
            // Arrange
            bool expected = true;
            int xKing = 5;
            int yKing = 5;
            int xRook = 4;
            int yRook = 7;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            board.AddFigure(board.GetCell(xKing, yKing), king);
            board.AddFigure(board.GetCell(xRook, yRook), new Rook(xRook, yRook, PlayerColor.Black));
            board.PrepareFigures(board);

            // Act
            board.ClearHits(board);
            bool actual = true;
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if (board.Board[i, j].possibleToHit.Count != 0)
                        actual = false;
                }
            }

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests method CalculateLegalMoves
        /// </summary>
        [TestMethod]
        public void CalculateLegalMoves_ShouldCalculateAllLegalMovesOfTheWhiteKing()
        {
            // Arrange
            List<(int, int)> expected = new List<(int, int)> { (6, 7) };
            int xKing = 7;
            int yKing = 7;
            int xRook = 0;
            int yRook = 6;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            board.AddFigure(board.GetCell(xKing, yKing), king);
            board.AddFigure(board.GetCell(xRook, yRook), new Rook(xRook, yRook, PlayerColor.Black));
            board.PrepareFigures(board);
            board.Color = PlayerColor.White;

            // Arrange
            board.CalculateLegalMoves(board);
            List<(int, int)> actual = board.WhiteKing.LegalMoves;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests method PrepareFigures
        /// </summary>
        [TestMethod]
        public void PrepareFigures_ShouldCalculateAllPossibleMovesOfTheWhiteKing()
        {
            // Arrange
            List<(int, int)> expected = new List<(int, int)> { (7, 6), (6, 7), (6, 6) };
            int xKing = 7;
            int yKing = 7;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            board.AddFigure(board.GetCell(xKing, yKing), king);
            board.Color = PlayerColor.White;
            List<(int, int)> actual = new List<(int, int)> { };

            // Arrange
            board.PrepareFigures(board);
            foreach (var move in board.WhiteKing.GetPossibleMoves)
            {
                actual.Add(move);
            }

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests method move in scenario where one figure move to capture another figure
        /// </summary>
        [TestMethod]
        public void Move_OneFigureMoveToAnotherCellWithAnotherFigure_MovedFigureShouldCapturePreviousFigureOnTheMovedCell()
        {
            // Arrange
            int xKing = 5;
            int yKing = 5;
            int xRook = 5;
            int yRook = 6;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            Figure expected = board.WhiteKing;
            board.AddFigure(board.GetCell(xKing, yKing), king);
            board.AddFigure(board.GetCell(xRook, yRook), new Rook(xRook, yRook, PlayerColor.Black));
            board.PrepareFigures(board);

            // Act
            board.Move(board.GetCell(xKing, yKing), board.GetCell(xRook, yRook), Promotion.Queen, board);
            Figure actual = board.GetCell(xRook, yRook).figure;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests method move in scenario where pawn is about to be promoted
        /// </summary>
        [TestMethod]
        public void Move_PawnMovesToTheEdgeOfTheBoard_ShouldPromotePawnToQueen()
        {
            // Arrange
            int xPawn = 6;
            int yPawn = 6;
            Pawn pawn = new Pawn(xPawn, yPawn, PlayerColor.White);
            bool expected = true;
            bool actual = false;
            board.AddFigure(board.GetCell(xPawn, yPawn), pawn);
            board.PrepareFigures(board);

            // Act
            board.Move(board.GetCell(xPawn, yPawn), board.GetCell(7, yPawn), Promotion.Queen, board);
            if (board.GetCell(7, yPawn).figure.GetType() == typeof(Queen))
                actual = true;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests method LegalMove in scenario where king is trying to make a step in check
        /// </summary>
        [TestMethod]
        public void LegalMove_KingTryingToStepInCheck_ShouldReturnFalse()
        {
            // Arrange
            bool expected = false;
            int xKing = 7;
            int yKing = 7;
            int xRook = 0;
            int yRook = 6;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            board.AddFigure(board.GetCell(xKing, yKing), king);
            board.AddFigure(board.GetCell(xRook, yRook), new Rook(xRook, yRook, PlayerColor.Black));
            board.PrepareFigures(board);
            board.Color = PlayerColor.White;

            // Act
            bool actual = board.LegalMove(7, 6, board.WhiteKing, board);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests method LegalMove where blocker of the king trying to escape from attacker and let his king to be in check
        /// </summary>
        [TestMethod]
        public void LegalMove_BlockerOfTheKingTryToEscapeAndPutKingInCheck_ShouldReturnFalse()
        {
            // Arrange
            bool expected = false;
            int xKing = 7;
            int yKing = 7;
            int xBlocker = 6;
            int yBlocker = 7;
            int xRook = 0;
            int yRook = 7;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            board.AddFigure(board.GetCell(xKing, yKing), king);
            board.AddFigure(board.GetCell(xRook, yRook), new Rook(xRook, yRook, PlayerColor.Black));
            board.AddFigure(board.GetCell(xBlocker, yBlocker), new Rook(xBlocker, yBlocker, PlayerColor.White));
            board.PrepareFigures(board);
            board.Color = PlayerColor.White;

            // Act
            bool actual = board.LegalMove(6, 4, board.GetCell(xBlocker, yBlocker).figure, board);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests method LegalMove in scenario where Blocker of his king trying to capture attacker
        /// </summary>
        [TestMethod]
        public void LegalMove_BlockerOfTheKingTryToCaptureAttacker_ShouldReturnTrue()
        {
            // Arrange
            bool expected = true;
            int xKing = 7;
            int yKing = 7;
            int xBlocker = 6;
            int yBlocker = 7;
            int xRook = 0;
            int yRook = 7;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            board.AddFigure(board.GetCell(xKing, yKing), king);
            board.AddFigure(board.GetCell(xRook, yRook), new Rook(xRook, yRook, PlayerColor.Black));
            board.AddFigure(board.GetCell(xBlocker, yBlocker), new Rook(xBlocker, yBlocker, PlayerColor.White));
            board.PrepareFigures(board);
            board.Color = PlayerColor.White;

            // Act
            bool actual = board.LegalMove(xRook, yRook, board.GetCell(xBlocker, yBlocker).figure, board);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// Tests CurrentKing method that returns current king
        /// </summary>
        [TestMethod]
        public void CurrentKing_ShouldReturnCurrentKingFigure()
        {
            // Arrange
            int xKing = 7;
            int yKing = 7;
            King king = new King(xKing, yKing, PlayerColor.White);
            board.WhiteKing = king;
            board.Color = PlayerColor.White;
            Figure expected = board.WhiteKing;

            // Act
            Figure actual = board.CurrentKing();

            // Assert
            Assert.AreEqual(expected, actual);

        }
    }
}