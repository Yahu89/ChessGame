using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1.Pieces;

public abstract class Piece
{
    public bool Color { get; set; }
    public Position ActualPosition { get; set; }
    public bool Exists { get; set; } = true;
    public bool IsActive { get; set; } = false;
    public abstract Image PieceNotSelectedImagePath { get; set; }
    public abstract Image PieceSelectedImagePath { get; set; }
    //public List<Position> PossiblePositionsForNextMove { get; set; }
    //public abstract Position ChangePosition(Piece piece, Position newPosition);
}
