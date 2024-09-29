using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1.Pieces;

public abstract class Piece : PictureBox
{
    public bool Color { get; set; } // false - white, true - black
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

    public abstract Piece DeepCopy(Piece piece, bool color);

    public abstract List<PictureBox> PossiblePositionsForNextMove(Piece[,] pieces);
    public void ChangePosition(Position newPosition, Position oldPosition, Piece piece, Form1 form)
    {
        if (ChessBoard.Pieces[newPosition.X, newPosition.Y] == null)
        {
            ChessBoard.Pieces[newPosition.X, newPosition.Y] = piece;
            ChessBoard.Pieces[newPosition.X, newPosition.Y].ActualPosition = newPosition;
            ChessBoard.Pieces[newPosition.X, newPosition.Y].Location = ChessBoard.CalculatePointFromPosition(newPosition);
            ChessBoard.Pieces[newPosition.X, newPosition.Y].BackgroundImage = piece.PieceNotSelectedImagePath;
            ChessBoard.Pieces[oldPosition.X, oldPosition.Y] = null;
        }
        else
        {
            form.panel1.Controls.Remove(ChessBoard.Pieces[newPosition.X, newPosition.Y]);
            ChessBoard.Pieces[newPosition.X, newPosition.Y] = null;
            ChessBoard.Pieces[newPosition.X, newPosition.Y] = piece;
            ChessBoard.Pieces[newPosition.X, newPosition.Y].Color = piece.Color;
            ChessBoard.Pieces[newPosition.X, newPosition.Y].ActualPosition = newPosition;
            ChessBoard.Pieces[newPosition.X, newPosition.Y].Location = ChessBoard.CalculatePointFromPosition(newPosition);
            ChessBoard.Pieces[newPosition.X, newPosition.Y].BackgroundImage = piece.PieceNotSelectedImagePath;
            ChessBoard.Pieces[oldPosition.X, oldPosition.Y] = null;
        }
    }

    protected void CreateNewPositionHelper(int x, int y, List<PictureBox> pictureBoxes)
    {
        var possibleNewChessBoard = ChessBoard.CreateTemporatyPieces(ChessBoard.Pieces);
        possibleNewChessBoard[ActualPosition.X, ActualPosition.Y] = ChessBoard.Pieces[ActualPosition.X, ActualPosition.Y];
        possibleNewChessBoard[ActualPosition.X, ActualPosition.Y] = null;

        pictureBoxes.Add(new PictureBox()
        {
            BackgroundImage = ChessBoard.PossiblePositionImage,
            BackgroundImageLayout = ImageLayout.Tile,
            Location = ChessBoard.CalculatePointFromPosition(new Position(x, y)),
            Size = new Size(90, 90),
            BackColor = System.Drawing.Color.Transparent
        });
    }

    //public abstract bool IsCheckForMyself(bool color, Position oldPosition, Position newPosition);


}
