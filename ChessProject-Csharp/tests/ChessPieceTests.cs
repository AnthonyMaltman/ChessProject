
using NUnit.Framework;
using SolarWinds.MSP.Chess.Enums;
using System.Linq;

namespace SolarWinds.MSP.Chess
{
	[TestFixture]
	class ChessPieceTests
    {
        [Test]
        public void GetChordinates_ReturnsTheCorrectXAndYValues_WhenCalled()
        {
            // setup
            var player1Settings = new Player(Direction.Up, PieceColor.White);

            // act
            var chessBoard = new ChessBoard();
            chessBoard.New(player1Settings);

            var piece1 = chessBoard.GetPieces().First();
            var piece2 = chessBoard.GetPieces().ToList()[4];
            var piece3 = chessBoard.GetPieces().ToList()[10];

            var chords1 = piece1.GetChordinates();
            var chords2 = piece2.GetChordinates();
            var chords3 = piece3.GetChordinates();

            // assert
            Assert.IsTrue(chords1.X == 0);
            Assert.IsTrue(chords1.Y == 1);

            Assert.IsTrue(chords2.X == 4);
            Assert.IsTrue(chords2.Y == 1);

            Assert.IsTrue(chords3.X == 2);
            Assert.IsTrue(chords3.Y == 6);
        }

        [Test]
        public void HasBeenTaken_ReturnsFalse_WhenPieceIsNotCaptured()
        {
            // setup
            var player1Settings = new Player(Direction.Up, PieceColor.White);

            // act
            var chessBoard = new ChessBoard();
            chessBoard.New(player1Settings);

            var result = chessBoard.Move(0, 1, 0, 3, MovementType.Move);
            result = chessBoard.Move(1, 6, 1, 4, MovementType.Move);

            var piece = chessBoard.GetPiece(0, 3);

            // assert
            Assert.IsTrue(piece.HasBeenTaken() == false);
        }

        [Test]
        public void HasBeenTaken_ReturnsTrue_WhenPieceIsCaptured()
        {
            // setup
            var player1Settings = new Player(Direction.Up, PieceColor.White);

            // act
            var chessBoard = new ChessBoard();
            chessBoard.New(player1Settings);

            var result = chessBoard.Move(0, 1, 0, 3, MovementType.Move);
            result = chessBoard.Move(1, 6, 1, 4, MovementType.Move);
            result = chessBoard.Move(0, 3, 1, 4, MovementType.Capture);

            var piece = chessBoard.GetPiece(1, 4);

            // assert
            Assert.IsTrue(piece.GetTakenPieces().First().HasBeenTaken() == true);
        }

        [Test]
        public void TakenBy_ReturnsCorrectPiece_WhenPieceIsCaptured()
        {
            // setup
            var player1Settings = new Player(Direction.Up, PieceColor.White);

            // act
            var chessBoard = new ChessBoard();
            chessBoard.New(player1Settings);

            var result = chessBoard.Move(0, 1, 0, 3, MovementType.Move);
            result = chessBoard.Move(1, 6, 1, 4, MovementType.Move);

            result = chessBoard.Move(0, 3, 1, 4, MovementType.Capture);

            var piece = chessBoard.GetPiece(1, 4);

            // assert
            Assert.IsTrue(piece.GetTakenPieces().First().GetTakenBy() == piece);
        }

        [Test]
        public void Move_ReturnsIlegalMove_WhenYouMove_ToAPlaceOcupiedByAnotherOneOfYourPieces()
        {
            // setup
            var player1Settings = new Player(Direction.Up, PieceColor.White);

            // act
            var chessBoard = new ChessBoard();
            chessBoard.New(player1Settings);

            var result = chessBoard.Move(0, 1, 0, 2, MovementType.Move);
            
            result = chessBoard.Move(1, 6, 1, 5, MovementType.Move);

            result = chessBoard.Move(1, 1, 1, 2, MovementType.Capture);

            // assert
            Assert.IsTrue(result == MoveResult.IlegalMove);
        }

        [Test]
        public void GetPlayer_ReturnsTheCorrectPlayer()
        {
            // setup
            var player1Settings = new Player(Direction.Up, PieceColor.White);
            var player2Settings = new Player(Direction.Down, PieceColor.Black);

            // act
            var chessBoard = new ChessBoard();
            chessBoard.New(player1Settings);

            var piece1 = chessBoard.GetPiece(0, 1);
            var piece2 = chessBoard.GetPiece(7, 6);

            // assert
            Assert.AreEqual(piece1.GetPlayer().Color, player1Settings.Color);
            Assert.AreEqual(piece1.GetPlayer().Direction, player1Settings.Direction);

            Assert.AreEqual(piece2.GetPlayer().Color, player2Settings.Color);
            Assert.AreEqual(piece2.GetPlayer().Direction, player2Settings.Direction);
        }

        [Test]
        public void GetNumberOfMoves_ReturnsTheCorrectNumberOfMoves()
        {
            // setup
            var player1Settings = new Player(Direction.Up, PieceColor.White);

            // act
            var chessBoard = new ChessBoard();
            chessBoard.New(player1Settings);

            var result = chessBoard.Move(0, 1, 0, 3, MovementType.Move);

            result = chessBoard.Move(1, 6, 1, 4, MovementType.Move);

            result = chessBoard.Move(0, 3, 1, 4, MovementType.Capture);

            var piece = chessBoard.GetPiece(1, 4);

            // assert
            Assert.IsTrue(piece.GetNumberOfMoves() == 2);
        }

        [Test]
        public void Move_ReturnsIlegalMove_WhenYouTryAndCaptureAVacentPosition()
        {
            // setup
            var player1Settings = new Player(Direction.Up, PieceColor.White);

            // act
            var chessBoard = new ChessBoard();
            chessBoard.New(player1Settings);

            var result = chessBoard.Move(0, 1, 0, 3, MovementType.Move);

            result = chessBoard.Move(1, 6, 1, 5, MovementType.Move);

            result = chessBoard.Move(0, 3, 1, 4, MovementType.Capture);

            // assert
            Assert.IsTrue(result == MoveResult.IlegalMove);
        }
    }
}
