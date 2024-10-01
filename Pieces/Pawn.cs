﻿
namespace ChessGame_v1.Pieces;

public class Pawn : Piece
{
    public override Image PieceNotSelectedImagePath { get; set; }
    public override Image PieceSelectedImagePath { get; set; }
    public bool IsBeforeFirstMove { get; set; } = true;
    public Pawn(bool color) : base()
    {
        Color = color;

        if (Color)
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\pawn.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\pawn_selected.png");
        }
        else
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\pawn_white.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\pawn_white_selected.png");
        }
    }

    public override List<PictureBox> PossiblePositionsForNextMove(Piece[,] pieces)
    {
        List<PictureBox> pictureBoxes = new List<PictureBox>();

        int X = ActualPosition.X;
        int Y = ActualPosition.Y;

        if (Color == false)
        {
            if (X - 1 >= 0)
            {
                if (pieces[X - 1, Y] is null)
                {
                    CreateNewPositionHelper(X - 1, Y, pictureBoxes);
                    AdditionalPositionBeforeFirstMove(pictureBoxes, X - 2);
                }
            }

            if (X - 1 >= 0 && Y - 1 >= 0)
            {
                if (pieces[X - 1, Y - 1] is King && pieces[X - 1, Y - 1].Color == true)
                {

                }
                else if (pieces[X - 1, Y - 1] != null && pieces[X - 1, Y - 1].Color == true)
                {
                    CreateNewPositionHelper(X - 1, Y - 1, pictureBoxes);
                }
            }


            if (X - 1 >= 0 && Y + 1 <= 7)
            {
                if (pieces[X - 1, Y + 1] is King && pieces[X - 1, Y + 1].Color == true)
                {

                }
                else if (pieces[X - 1, Y + 1] != null && pieces[X - 1, Y + 1].Color == true)
                {
                    CreateNewPositionHelper(X - 1, Y + 1, pictureBoxes);
                }
            }

        }
        else
        {
            if (X + 1 <= 7)
            {
                if (pieces[X + 1, Y] is null)
                {
                    CreateNewPositionHelper(X + 1, Y, pictureBoxes);
                    AdditionalPositionBeforeFirstMove(pictureBoxes, X + 2);
                }
            }

            if (X + 1 <= 7 && Y - 1 >= 0)
            {
                if (pieces[X + 1, Y - 1] is King && pieces[X + 1, Y - 1].Color == false)
                {

                }
                else if (pieces[X + 1, Y - 1] != null && pieces[X + 1, Y - 1].Color == false)
                {
                    CreateNewPositionHelper(X + 1, Y - 1, pictureBoxes);
                }
            }
            
            if (X + 1 <= 7 && Y + 1 <= 7)
            {
                if (pieces[X + 1, Y + 1] is King && pieces[X + 1, Y + 1].Color == false)
                {

                }
                else if (pieces[X + 1, Y + 1] != null && pieces[X + 1, Y + 1].Color == false)
                {
                    CreateNewPositionHelper(X + 1, Y + 1, pictureBoxes);
                }
            }

        }

        return pictureBoxes;
    }

    private void AdditionalPositionBeforeFirstMove(List<PictureBox> pictureBoxes, int x)
    {
        if (IsBeforeFirstMove && ChessBoard.Pieces[x, ActualPosition.Y] is null)
        {
            CreateNewPositionHelper(x, ActualPosition.Y, pictureBoxes);
        }
    }

    public override Piece DeepCopy(Piece piece, bool color)
    {
        Piece newPawn = new Pawn(color)
        {
            Color = piece.Color,
            ActualPosition = piece.ActualPosition
        };

        return newPawn;
    }
}
