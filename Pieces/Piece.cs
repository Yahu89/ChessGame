using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1.Pieces;

public abstract class Piece : PictureBox
{
    public bool Color { get; set; } // false - whithe, true - black
    public Position ActualPosition { get; set; }
    public bool Exists { get; set; } = true;
    public bool IsActive { get; set; } = false;
    public abstract Image PieceNotSelectedImagePath { get; set; }
    public abstract Image PieceSelectedImagePath { get; set; }
    public Piece()
    {
        BackColor = System.Drawing.Color.Transparent;
        Size = new Size(90, 90);
    }

    public abstract List<PictureBox> PossiblePositionsForNextMove(Piece[,] pieces);


}
