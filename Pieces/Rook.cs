using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1.Pieces;

public class Rook : Piece
{
    public override Image PieceNotSelectedImagePath { get; set; } = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\pawn.png");
    public override Image PieceSelectedImagePath { get; set; } = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\pawn_selected.png");

    //public override Position ChangePosition(Piece piece, Position newPosition)
    //{
    //    if (IsPossibleToChangePosition(piece.ActualPosition, newPosition))
    //    {
    //        return newPosition;
    //    }

    //    return piece.ActualPosition;
    //}
    
    //public bool IsPossibleToChangePosition(Position currentPosition, Position newPosition)
    //{
    //    if (currentPosition.X == newPosition.X)
    //    {
    //        return true;
    //    }

    //    if (currentPosition.Y == newPosition.Y)
    //    {
    //        return true;
    //    }

    //    return false;
    //}
}
