using SolarWinds.MSP.Chess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarWinds.MSP.Chess.Pieces
{
    public class Pawn : ChessPiece
    {
        public Pawn(Player player, int xCoordinate, int yCoordinate) 
            : base(player, PieceType.Pawn, xCoordinate, yCoordinate)
        {
            
        }

        public override (ICollection<ChessPiece>, MoveResult moveResult) Move(Player playersTurn, ICollection<ChessPiece> pieces, MovementType movementType, int newX, int newY)
        {
            var allPieces = pieces.ToList();
            var moveResult = MoveResult.IlegalMove;

            if (IsLegalBoardMove(playersTurn, pieces, movementType, newX, newY))
            {
                if (movementType == MovementType.Move)
                {
                    moveResult = MoveResult.Moved;
                    NumberOfMoves++;
                }
                else
                {
                    var (updatedPieces, capturedPiece) = Capture(pieces, newX, newY);
                    
                    allPieces = updatedPieces.ToList();

                    if (capturedPiece)
                    {
                        moveResult = MoveResult.Captured;
                        NumberOfMoves++;
                    } 
                }

                var (pieceToTake, indexOfPieceTotake) = GetPiece(pieces, XCoordinate, YCoordinate);

                XCoordinate = newX;
                YCoordinate = newY;

                allPieces[indexOfPieceTotake] = this;
            }

            return (allPieces, moveResult);
        }        
        
        internal override bool IsLegalBoardMove(Player playersTurn, ICollection<ChessPiece> pieces, MovementType movementType, int newX, int newY)
        {
            if (Player != playersTurn)
                return false;

            var (pieceInNewPosition, indexOfPieceInNewPosition) = GetPiece(pieces, newX, newY);

            if (Player.Direction == Direction.Up)
            {
                if (movementType == MovementType.Move)
                {
                    if (pieceInNewPosition == null)
                    {
                        if (NumberOfMoves == 0)
                            return newX == XCoordinate && (newY == YCoordinate + 1 || newY == YCoordinate + 2);
                        else
                            return newX == XCoordinate && newY == YCoordinate + 1;
                    }
                }

                if (movementType == MovementType.Capture)
                {
                    if (pieceInNewPosition != null && pieceInNewPosition.Player != Player)
                    {
                        return (newX == XCoordinate - 1 || newX == XCoordinate + 1) && newY == YCoordinate + 1;
                    }
                }
            }
            else
            {
                if (movementType == MovementType.Move)
                {
                    if (pieceInNewPosition == null)
                    {
                        if (NumberOfMoves == 0)
                            return newX == XCoordinate && (newY == YCoordinate - 1 || newY == YCoordinate - 2);
                        else
                            return newX == XCoordinate && newY == YCoordinate - 1;
                    }
                }

                if (movementType == MovementType.Capture)
                {
                    if (pieceInNewPosition != null && pieceInNewPosition.Player != Player)
                    {
                        return (newX == XCoordinate - 1 || newX == XCoordinate + 1) && newY == YCoordinate - 1;
                    }
                }
            }

            return false;
        }

        private (ICollection<ChessPiece>, bool capturedPiece) Capture(ICollection<ChessPiece> pieces, int X, int Y)
        {
            var allPieces = pieces.ToList();
            var capturedPiece = false;

            var (pieceToTake, indexOfPieceTotake) = GetPiece(pieces, X, Y);

            if (pieceToTake != null && pieceToTake.Player != Player)
            {
                if (Player.Direction == Direction.Up)
                {
                    if ((X == XCoordinate - 1 || X == XCoordinate + 1) && Y == YCoordinate + 1)
                    {
                        capturedPiece = true;

                        TakePiece(allPieces, X, Y);
                    }
                }
                else
                {
                    if ((X == XCoordinate - 1 || X == XCoordinate + 1) && Y == YCoordinate - 1)
                    {
                        capturedPiece = true;

                        TakePiece(allPieces, X, Y);
                    }
                }
            }

            return (allPieces, capturedPiece);
        }

        private ICollection<ChessPiece> TakePiece(ICollection<ChessPiece> pieces, int X, int Y)
        {
            var allPieces = pieces.ToList();

            var (takenPiece, indexOfTakenPiece) = GetPiece(allPieces, X, Y);

            takenPiece = Capture(takenPiece);

            allPieces[indexOfTakenPiece] = takenPiece;

            return allPieces;
        }

        private (ChessPiece piece, int indexOfPiece) GetPiece(ICollection<ChessPiece> pieces, int X, int Y)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces.ToList()[i].GetChordinates() == (X, Y) && pieces.ToList()[i].HasBeenTaken() == false)
                {
                    return (pieces.ToList()[i], i);
                }
            }

            return (null, 0);
        }

        public override string ToString() 
            => CurrentPositionAsString();
      
        protected string CurrentPositionAsString() 
            =>  $"Current X: {XCoordinate}{Environment.NewLine}" +
                $"Current Y: {YCoordinate}{Environment.NewLine}" +
                $"Piece Color: {Player.Color}{Environment.NewLine}";
    }
}
