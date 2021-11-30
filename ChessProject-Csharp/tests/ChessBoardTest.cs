using NUnit.Framework;
using SolarWinds.MSP.Chess.Enums;
using System;
using System.Linq;

namespace SolarWinds.MSP.Chess
{
    [TestFixture]
	public class ChessBoardTest
	{
		[Test]
		public void GetPiece_ReturnsValidCheckPiece_WhenValidPositionIsGiven()
		{
			// setup
			var player1Settings = new Player(Direction.Up, PieceColor.White);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);

			var result = chessBoard.Move(0, 1, 0, 2, MovementType.Move);
			
			var piece = chessBoard.GetPiece(0, 2);
			var pieces = chessBoard.GetPieces().ToList();

			// assert
			Assert.AreEqual(piece, pieces[0]);
		}

		[Test]
		public void GetPiece_ThrowsAnException_WhenInalidPositionIsGiven()
		{
			// setup
			var player1Settings = new Player(Direction.Up, PieceColor.White);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);

			var result = chessBoard.Move(0, 1, 0, 2, MovementType.Move);

			// assert
			Assert.Throws<Exception>(() => chessBoard.GetPiece(0, 4));
		}

		[Test]
		public void Move_ReturnsMoved_WhenValidPositionIsGiven()
		{
			// setup
			var player1Settings = new Player(Direction.Up, PieceColor.White);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);

			var result = chessBoard.Move(0, 1, 0, 2, MovementType.Move);

			// assert
			Assert.IsTrue(result == MoveResult.Moved);
		}

		[Test]
		public void Move_ReturnsNotAValidBoardPosition_WhenIlegalPositionIsGiven()
		{
			// setup
			var player1Settings = new Player(Direction.Up, PieceColor.White);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);

			var result = chessBoard.Move(0,1, -2,2, MovementType.Move);

			// assert
			Assert.IsTrue(result == MoveResult.NotAValidBoardPosition);
		}

		[Test]
		public void Move_ReturnsNotYourPiece_WhenYouTryToMoveAPieceNotBelongingToTheCurrentPlayer()
		{
			// setup
			var player1Settings = new Player(Direction.Up, PieceColor.White);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);

			var result = chessBoard.Move(0, 6, 0, 4, MovementType.Move);

			// assert
			Assert.IsTrue(result == MoveResult.NotYourPiece);
		}

		[Test]
		public void Move_ReturnsIlegalMove_WhenIlegalPositionIsGiven()
		{
			// setup
			var player1Settings = new Player(Direction.Up, PieceColor.White);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);

			var result = chessBoard.Move(0, 1, 1, 2, MovementType.Move);

			// assert
			Assert.IsTrue(result == MoveResult.IlegalMove);
		}

		[Test]
		public void Move_ReturnsCaptured_WhenAValidMoveResultsInACapture()
		{
			// setup
			var player1Settings = new Player(Direction.Up, PieceColor.White);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);

			var result = chessBoard.Move(0, 1, 0, 3, MovementType.Move);
			result = chessBoard.Move(1, 6, 1, 4, MovementType.Move);

			result = chessBoard.Move(0, 3, 1, 4, MovementType.Capture);

			// assert
			Assert.IsTrue(result == MoveResult.Captured);
		}

		[Test]
		public void Move_ReturnsCheck_WhenAValidMoveResultsInCheck()
		{
			// TODO - implement check check

			// setup

			// act

			// assert
			//Assert.IsTrue(result == MoveResult.Check);
		}

		[Test]
		public void Move_ReturnsCheckmate_WhenAValidMoveResultsInCheckmate()
		{
			// TODO - implement checkmate check

			// setup
			
			// act
			
			// assert
			//Assert.IsTrue(result == MoveResult.Checkmate);
		}

		[Test]
		public void New_SetsUpANewBoard()
		{
			// setup
			var player1Settings = new Player(Enums.Direction.Up, PieceColor.White);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);

			// assert

			//only pawns applied currently
			// TODO - update when adding new chess piece types
			Assert.AreEqual(chessBoard.GetPieces().Count, 16);
		}

        [Test]
		public void New_DoesNotDuplicatePawnPieces_WhenCalledMultipleTimes()
		{
			// setup
			var player1Settings = new Player(Enums.Direction.Up, PieceColor.White);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);
			chessBoard.New(player1Settings);
			chessBoard.New(player1Settings);
			chessBoard.New(player1Settings);

			// assert
			Assert.AreEqual(chessBoard.GetPieces().Count(x => x.GetPieceType() == PieceType.Pawn), 16);

			Assert.AreEqual(chessBoard.GetPieces().Count(x => x.GetPieceType() == PieceType.Pawn && x.GetPlayer().Color == PieceColor.White), 8);
			Assert.AreEqual(chessBoard.GetPieces().Count(x => x.GetPieceType() == PieceType.Pawn && x.GetPlayer().Color == PieceColor.Black), 8);
		}

		[Test]
		public void New_AddsCorrectNumberOfPawnPieces_WhenCalled()
		{
			// setup
			var player1Settings = new Player(Enums.Direction.Up, Enums.PieceColor.White);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);

			// assert
			Assert.AreEqual(chessBoard.GetPieces().Count(x => x.GetPieceType() == PieceType.Pawn), 16);

			Assert.AreEqual(chessBoard.GetPieces().Count(x => x.GetPieceType() == PieceType.Pawn && x.GetPlayer().Color == PieceColor.White), 8);
			Assert.AreEqual(chessBoard.GetPieces().Count(x => x.GetPieceType() == PieceType.Pawn && x.GetPlayer().Color == PieceColor.Black), 8);
		}

		[Test]
		public void New_AddsPawnTheCorrectPositions_WhenCalled()
		{
			// TODO - this unit test will break when new check piece types are added

			// setup
			var player1Settings = new Player(Enums.Direction.Up, Enums.PieceColor.White);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);

			// assert
			var pawns = chessBoard.GetPieces().ToList();

			var counter = 0;
			for (int y = 0; y <= 7; y++)
			{
				if (y == 1 || y == 6)
				{
					Assert.IsTrue(pawns[counter].GetChordinates().Y == y);

					for (int x = 0; x <= 7; x++)
					{
						Assert.IsTrue(pawns[counter].GetChordinates().X == x);

						counter++;
					}
				}
			}
		}

		[Test]
		public void New_SetsUpAThePlayersCorrectly_WhenCalled()
		{
			// setup
			var player1Settings = new Player(Enums.Direction.Up, PieceColor.White);
			var player2Settings = new Player(Enums.Direction.Down, PieceColor.Black);

			// act
			var chessBoard = new ChessBoard();
			chessBoard.New(player1Settings);

			// assert
			Assert.AreEqual(chessBoard.Player1.Color, player1Settings.Color);
			Assert.AreEqual(chessBoard.Player1.Direction, player1Settings.Direction);

			Assert.AreEqual(chessBoard.Player2.Color, player2Settings.Color);
			Assert.AreEqual(chessBoard.Player2.Direction, player2Settings.Direction);
		}
	}
}
