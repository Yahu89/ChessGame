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
        List<PictureBox> pictureBoxes = new List<PictureBox>();

        int x = ActualPosition.X;
        int y = ActualPosition.Y;

        List<Position> possiblePositions = new List<Position>()
        {
            new Position(x - 1, y),
            new Position(x - 1, y + 1),
            new Position(x, y + 1),
            new Position(x + 1, y + 1),
            new Position(x + 1, y),
            new Position(x + 1, y - 1),
            new Position(x, y - 1),
            new Position(x - 1, y - 1)
        };

        var newPossibleKingPositions = AreTwoKingsNextTo(pieces, possiblePositions);

        foreach (var item in newPossibleKingPositions)
        {
            if (item.X >= 0 && item.X <= 7 && item.Y >= 0 && item.Y <= 7)
            {
                if (pieces[item.X, item.Y] == null)
                {
                    CreateNewPositionHelper(item.X, item.Y, pictureBoxes);
                }
                else if (pieces[item.X, item.Y] is King && pieces[item.X, item.Y].Color != Color)
                {

                }
                else if (pieces[item.X, item.Y] != null && pieces[item.X, item.Y].Color != Color)
                {
                    CreateNewPositionHelper(item.X, item.Y, pictureBoxes);
                }

            }
        }

        return pictureBoxes;
    }

    private List<Position> AreTwoKingsNextTo(Piece[,] newChessBoard, List<Position> possibleKingPositions)
    {
        var actualEnemyKingPosition = KingPosition(newChessBoard, !ChessBoard.SelectedPiece.Color);

        int x = actualEnemyKingPosition.X;
        int y = actualEnemyKingPosition.Y;

        List<Position> newPossibleList = new List<Position>();

        foreach (var item in possibleKingPositions)
        {
            if (item.X >= 0 && item.X <= 7 && item.Y >= 0 && item.Y <= 7)
            {
                int resX = x - item.X;
                int resY = y - item.Y;

                if (!((Math.Abs(resX) == 0 || Math.Abs(resX) == 1) && (Math.Abs(resY) == 0 || Math.Abs(resY) == 1)))
                {
                    newPossibleList.Add(item);
                }
            }       
        }

        return newPossibleList;
    }

    public override Piece DeepCopy(Piece piece, bool color)
    {
        Piece newKing = new King(color)
        {
            Color = piece.Color,
            ActualPosition = piece.ActualPosition
        };

        return newKing;
    }
}
