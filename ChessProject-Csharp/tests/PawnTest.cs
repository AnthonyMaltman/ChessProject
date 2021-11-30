using NUnit.Framework;
using SolarWinds.MSP.Chess.Enums;
using System.Linq;

namespace SolarWinds.MSP.Chess
{
	[TestFixture]
	public class PawnTest
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
        public void Move_ReturnsIlegalMove_WhenYouMoveAPieceBelongingToTheWrongPlayerInPlay()
        {
            // setup
            var player1Settings = new Player(Direction.Up, PieceColor.White);

            // act
            var chessBoard = new ChessBoard();
            chessBoard.New(player1Settings);

            chessBoard.Move(0, 1, 0, 3, MovementType.Move);
            chessBoard.Move(1, 6, 1, 4, MovementType.Move);
            chessBoard.Move(0, 3, 1, 4, MovementType.Capture);

            var allPieces = chessBoard.GetPieces();
            var playersTurn = chessBoard.WhosTurnIsIt();
            var piece = chessBoard.GetPiece(1, 4);

            var (aupdatedPieces, result) = piece.Move(playersTurn, allPieces, MovementType.Move, 1, 5);

            // assert
            Assert.IsTrue(result == MoveResult.IlegalMove);
        }

        [Test]
        public void Move_ReturnsMoved_WhenYouMoveAValidPiecey()
        {
            // setup
            var player1Settings = new Player(Direction.Up, PieceColor.White);

            // act
            var chessBoard = new ChessBoard();
            chessBoard.New(player1Settings);

            chessBoard.Move(0, 1, 0, 3, MovementType.Move);
            chessBoard.Move(1, 6, 1, 4, MovementType.Move);
            chessBoard.Move(0, 3, 1, 4, MovementType.Capture);

            var allPieces = chessBoard.GetPieces();
            var playersTurn = chessBoard.WhosTurnIsIt();
            var piece = chessBoard.GetPiece(2, 6);

            var (aupdatedPieces, result) = piece.Move(playersTurn, allPieces, MovementType.Move, 2, 4);

            // assert
            Assert.IsTrue(result == MoveResult.Moved);
        }

        [Test]
        public void Move_ReturnsIlegalMove_WhenMoveAPawnInTheWrongDirection()
        {
            // setup
            var player1Settings = new Player(Direction.Up, PieceColor.White);

            // act
            var chessBoard = new ChessBoard();
            chessBoard.New(player1Settings);

            chessBoard.Move(0, 1, 0, 3, MovementType.Move);
            chessBoard.Move(1, 6, 1, 4, MovementType.Move);
            chessBoard.Move(0, 3, 1, 4, MovementType.Capture);

            var allPieces = chessBoard.GetPieces();
            var playersTurn = chessBoard.WhosTurnIsIt();
            var piece = chessBoard.GetPiece(2, 6);

            var (aupdatedPieces, result) = piece.Move(playersTurn, allPieces, MovementType.Move, 2, 7);

            // assert
            Assert.IsTrue(result == MoveResult.IlegalMove);
        }
    }
}
