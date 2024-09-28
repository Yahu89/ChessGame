using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1.Pieces;

public class Knight : Piece
{
    public override Image PieceNotSelectedImagePath { get; set; }
    public override Image PieceSelectedImagePath { get; set; } 
    public Knight(bool color) : base()
    {
        Color = color;

        if (Color)
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\knight.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\knight_selected.png");
        }
        else
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\knight_white.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\knight_white_selected.png");
        }
    }

    public override List<PictureBox> PossiblePositionsForNextMove(Piece[,] pieces)
    {
        throw new NotImplementedException();
    }
}
