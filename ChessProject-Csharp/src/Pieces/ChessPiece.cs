using SolarWinds.MSP.Chess.Enums;
using System.Collections.Generic;

namespace SolarWinds.MSP.Chess.Pieces
{
    public abstract class ChessPiece
    {
        internal int XCoordinate { get; set; }
        internal int YCoordinate { get; set; }
        internal Player Player { get; set; }
        internal PieceType PieceType { get; set; }
        internal int NumberOfMoves { get; set; }
        internal ICollection<ChessPiece> TakenPieces { get; set; }
        internal ChessPiece TakenBy { get; set; }

        public ChessPiece(Player player, PieceType pieceType, int xCoordinate, int yCoordinate)
        {
            Player = player;
            PieceType = pieceType;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            NumberOfMoves = 0;
            TakenPieces = new List<ChessPiece>();
            TakenBy = null;
        }

        public (int X, int Y) GetChordinates() => (XCoordinate, YCoordinate);
        public bool HasBeenTaken() => TakenBy != null;

        public ChessPiece Capture(ChessPiece takenPiece)
        {
            takenPiece.SetTakenBy(this);
            TakenPieces.Add(takenPiece);

            // Thake the piece off the board
            takenPiece.XCoordinate = 8;
            takenPiece.YCoordinate = 8;

            return takenPiece;
        }

        public void SetTakenBy(ChessPiece takenPiece) => TakenBy = takenPiece;
        public ChessPiece GetTakenBy() => TakenBy;
        public bool Taken() => TakenBy == null;
        public Player GetPlayer() => Player;
        public PieceType GetPieceType() => PieceType;
        public int GetNumberOfMoves() => NumberOfMoves;
        public ICollection<ChessPiece> GetTakenPieces() => TakenPieces;

        public abstract (ICollection<ChessPiece>, MoveResult moveResult) Move(Player playersTurn, ICollection<ChessPiece> pieces, MovementType movementType, int newX, int newY);
        internal abstract bool IsLegalBoardMove(Player playersTurn, ICollection<ChessPiece> pieces, MovementType movementType, int X, int Y);
    }
}
