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

    private List<PictureBox> PossiblePositionsWithoutCheckVerify()
    {
        List<PictureBox> pictureBoxes = new List<PictureBox>();

        int x = KingPosition(ChessBoard.Pieces, ChessBoard.SelectedPiece.Color).X;
        int y = KingPosition(ChessBoard.Pieces, ChessBoard.SelectedPiece.Color).Y;

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

        List<Position> finalPossiblePositions = new List<Position>();

        foreach (var item in possiblePositions)
        {
            if (item.X >= 0 && item.X <= 7 && item.Y >= 0 && item.Y <= 7)
            {
                if (ChessBoard.Pieces[item.X, item.Y] == null)
                {
                    CreateNewPositionHelper(item.X, item.Y, pictureBoxes);
                    continue;
                }

                if (ChessBoard.Pieces[item.X, item.Y].Color == ChessBoard.SelectedPiece.Color)
                {
                    continue;
                }

                if (!(ChessBoard.Pieces[item.X, item.Y] is King))
                {
                    CreateNewPositionHelper(item.X, item.Y, pictureBoxes);
                }
            }
        }

        return pictureBoxes;
    }

    public override List<PictureBox> PossiblePositionsForNextMove(Piece[,] pieces)
    {
        var positionsWithoutCheckVerify = PossiblePositionsWithoutCheckVerify();
        var finalPossiblePositions = new List<PictureBox>();

        foreach (var item in positionsWithoutCheckVerify)
        {
            var actualChessBoard = ChessBoard.CreateTemporatyChessBoard(ChessBoard.Pieces);
            int x = ChessBoard.CalculatePositionFromPoint(new Point(item.Location.X, item.Location.Y)).X;
            int y = ChessBoard.CalculatePositionFromPoint(new Point(item.Location.X, item.Location.Y)).Y;

            var kingCopy = DeepCopy(ChessBoard.SelectedPiece, ChessBoard.SelectedPiece.Color);
            actualChessBoard[ActualPosition.X, ActualPosition.Y] = null;
            actualChessBoard[x, y] = kingCopy;

            actualChessBoard[x, y].ActualPosition.X = x;
            actualChessBoard[x, y].ActualPosition.Y = y;


            var isCheckedMyself = IsCheckedMyself(actualChessBoard);

            if (!isCheckedMyself)
            {
                finalPossiblePositions.Add(item);
            }
        }

        return finalPossiblePositions;
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
        Point newPoint = new Point();

        newPoint.X = piece.Location.X;
        newPoint.Y = piece.Location.Y;

        Piece newKing = new King(color)
        {
            Color = piece.Color,
            ActualPosition = new Position(piece.ActualPosition),
            Location = newPoint
        };

        return newKing;
    }
}
