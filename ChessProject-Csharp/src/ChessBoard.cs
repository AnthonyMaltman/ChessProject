using SolarWinds.MSP.Chess.Enums;
using SolarWinds.MSP.Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarWinds.MSP.Chess
{
    public class ChessBoard
    {
        private const int MaxBoardWidth = 7;
        private const int MaxBoardHeight = 7;
        private ICollection<ChessPiece> Pieces { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        private Player PlayersTurn { get; set; }

        public ChessBoard ()
        {
            Pieces = new List<ChessPiece>();
        }

        public Player WhosTurnIsIt() => PlayersTurn;

        public MoveResult Move(int fromX, int fromY, int toX, int toY, MovementType movementType)
        {
            if(IsLegalBoardPosition(fromX, fromY) &&
                IsLegalBoardPosition(toX, toY))
            {
                var piece = GetPiece(fromX, fromY);

                if (piece.Player == PlayersTurn)
                {
                    var moveResult = MoveResult.IlegalMove;
                    (Pieces, moveResult) = piece.Move(PlayersTurn, Pieces, movementType, toX, toY);

                    if (moveResult != MoveResult.IlegalMove)
                    {
                        if (PlayersTurn == Player1)
                            PlayersTurn = Player2;
                        else
                            PlayersTurn = Player1;
                    }

                    return moveResult;
                }
                   
                return MoveResult.NotYourPiece;
            }

            return MoveResult.NotAValidBoardPosition;
        }

        public void New(Player Player1Layout)
        {
            NewPLayer(Player1Layout);

            Pieces = new List<ChessPiece>();

            var gamePieces = PieceList.GetGamePieces;

            int counter = 0;
            for (int y = 0; y <= MaxBoardHeight; y++)
            {
                if (y >= 2 && y < 6)
                    y = 6; // incrament to move to other side of the board
                
                var player = y <= 2 && Player1Layout.Direction == Direction.Up 
                        ? Player1
                        : Player2;

                for (int x = 0; x <= MaxBoardWidth; x++)
                {
                    switch (gamePieces[counter])
                    {
                        case PieceType.Pawn:
                            Pieces.Add(new Pawn(player, x, y));
                            break;
                        default:
                            break;
                    };
                    
                    counter++;
                }
            }
        }

        private bool IsLegalBoardPosition(int xCoordinate, int yCoordinate) 
            => (xCoordinate >= 0 && yCoordinate >= 0) && (xCoordinate <= MaxBoardWidth && yCoordinate <= MaxBoardHeight);

        public ChessPiece GetPiece(int X, int Y)
        {
            var piece = Pieces.SingleOrDefault(x => x.GetChordinates() == (X, Y) && x.TakenBy == null);

            if (piece != null)
                return piece;

            throw new Exception($"No piece at position X,Y {X} {Y}");
        }

        public ICollection<ChessPiece> GetPieces() => Pieces;

        private void NewPLayer(Player Player1Layout)
        {
            Player1 = new Player();
            Player2 = new Player();

            if (Player1Layout.Color == PieceColor.White)
            {
                Player1.Color = PieceColor.White;
                Player2.Color = PieceColor.Black;
                PlayersTurn = Player1;
            }
            else
            {
                Player1.Color = PieceColor.Black;
                Player2.Color = PieceColor.White;
                PlayersTurn = Player2;
            }

            if (Player1Layout.Direction == Direction.Up)
            {
                Player1.Direction = Direction.Up;
                Player2.Direction = Direction.Down;
            }
            else
            {
                Player1.Direction = Direction.Down;
                Player2.Direction = Direction.Up;
            }
        }
    }
}
