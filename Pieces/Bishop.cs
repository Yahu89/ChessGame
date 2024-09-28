using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1.Pieces;

public class Bishop : Piece
{
    public override Image PieceNotSelectedImagePath { get; set; } 
    public override Image PieceSelectedImagePath { get; set; }
    public Bishop(bool color) : base()
    {
        Color = color;

        if (Color)
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\bishop.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\bishop_selected.png");
        }
        else
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\bishop_white.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\bishop_white_selected.png");
        }
    }

    public override List<PictureBox> PossiblePositionsForNextMove(Piece[,] pieces)
    {
        throw new NotImplementedException();
    }
}
