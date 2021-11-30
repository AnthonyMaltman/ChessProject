using SolarWinds.MSP.Chess.Enums;

namespace SolarWinds.MSP.Chess
{
    public class Player
    {
        public Direction Direction { get; set; }
        public PieceColor Color { get; set; }

        public Player()
        {
        }

        public Player(Direction direction, PieceColor color)
        {
            Direction = direction;
            Color = color;
        }
    }
}
