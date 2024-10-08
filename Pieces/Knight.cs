using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

    private List<PictureBox> PossiblePositionsWithoutCheckVerify()
    {
        List<PictureBox> pictureBoxes = new List<PictureBox>();

        int X = ActualPosition.X;
        int Y = ActualPosition.Y;

        Position[] possiblePositions = new Position[]
        {
            new Position() { X = X - 2, Y = Y - 1 },
            new Position() { X = X - 2, Y = Y + 1 },
            new Position() { X = X - 1, Y = Y + 2 },
            new Position() { X = X + 1, Y = Y + 2 },
            new Position() { X = X + 2, Y = Y + 1 },
            new Position() { X = X + 2, Y = Y - 1 },
            new Position() { X = X - 1, Y = Y - 2 },
            new Position() { X = X + 1, Y = Y - 2 },
        };

        foreach (var item in possiblePositions)
        {
            bool isNewPlaceWithinChessBoard = (item.X >= 0 && item.X <= 7) && (item.Y >= 0 && item.Y <= 7);

            if (isNewPlaceWithinChessBoard)
            {
                if (ChessBoard.Pieces[item.X, item.Y] is null)
                {
                    CreateNewPositionHelper(item.X, item.Y, pictureBoxes);
                }
                else
                {
                    if (ChessBoard.Pieces[item.X, item.Y].Color != Color)
                    {
                        if (!(ChessBoard.Pieces[item.X, item.Y] is King))
                        {
                            CreateNewPositionHelper(item.X, item.Y, pictureBoxes);
                        }
                    }
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
            var actualKingPosition = KingPosition(actualChessBoard, ChessBoard.SelectedPiece.Color);
            int x = ChessBoard.CalculatePositionFromPoint(new Point(item.Location.X, item.Location.Y)).X;
            int y = ChessBoard.CalculatePositionFromPoint(new Point(item.Location.X, item.Location.Y)).Y;

            actualChessBoard[x, y] = ChessBoard.SelectedPiece;
            actualChessBoard[ActualPosition.X, ActualPosition.Y] = null;

            var isCheckedMyself = IsCheckedMyself(actualChessBoard);

            if (!isCheckedMyself)
            {
                finalPossiblePositions.Add(item);
            }
        }

        return finalPossiblePositions;

        //List<PictureBox> pictureBoxes = new List<PictureBox>();

        //int X = ActualPosition.X;
        //int Y = ActualPosition.Y;

        //Position[] possiblePositions = new Position[]
        //{
        //    new Position() { X = X - 2, Y = Y - 1 },
        //    new Position() { X = X - 2, Y = Y + 1 },
        //    new Position() { X = X - 1, Y = Y + 2 },
        //    new Position() { X = X + 1, Y = Y + 2 },
        //    new Position() { X = X + 2, Y = Y + 1 },
        //    new Position() { X = X + 2, Y = Y - 1 },
        //    new Position() { X = X - 1, Y = Y - 2 },
        //    new Position() { X = X + 1, Y = Y - 2 },
        //};

        //foreach (var item in possiblePositions)
        //{
        //    bool isNewPlaceWithinChessBoard = (item.X >= 0 && item.X <= 7) && (item.Y >= 0 && item.Y <= 7);

        //    if (isNewPlaceWithinChessBoard)
        //    {
        //        if (pieces[item.X, item.Y] is null)
        //        {
        //            CreateNewPositionHelper(item.X, item.Y, pictureBoxes);
        //        }
        //        else
        //        {
        //            if (pieces[item.X, item.Y].Color != Color)
        //            {
        //                if (!(pieces[item.X, item.Y] is King))
        //                {
        //                    CreateNewPositionHelper(item.X, item.Y, pictureBoxes);
        //                }
        //            }
        //        }
        //    }
        //}

        //return pictureBoxes;       
    }

    public override Piece DeepCopy(Piece piece, bool color)
    {
        Piece newKnight = new Knight(color)
        {
            Color = piece.Color,
            ActualPosition = piece.ActualPosition
        };

        return newKnight;
    }
}
