using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1.Pieces;

public class King : Piece
{
    public override Image PieceNotSelectedImagePath { get; set; } 
    public override Image PieceSelectedImagePath { get; set; }
    public King(bool color) : base()
    {
        Color = color;

        if (Color)
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\king.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\king_selected.png");
        }
        else
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\king_white.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\king_white_selected.png");
        }
    }

    public override List<PictureBox> PossiblePositionsForNextMove(Piece[,] pieces)
    {
        throw new NotImplementedException();
    }

    //public override void ChangePosition(Position newPosition, Position oldPosition, Piece piece, Form1 form)
    //{
    //    throw new NotImplementedException();
    //}

    public override Piece DeepCopy(Piece piece, bool color)
    {
        return new King(color);
    }
}
