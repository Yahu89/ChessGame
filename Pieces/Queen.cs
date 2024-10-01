using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1.Pieces;

public class Queen : Piece
{
    public override Image PieceNotSelectedImagePath { get; set; } 
    public override Image PieceSelectedImagePath { get; set; } 
    public Queen(bool color) : base() 
    {
        Color = color;

        if (Color)
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\queen.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\queen_selected.png");
        }
        else
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\queen_white.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\queen_white_selected.png");
        }
    }

    public override List<PictureBox> PossiblePositionsForNextMove(Piece[,] pieces)
    {
        List<PictureBox> pictureBoxes = new List<PictureBox>();

        // check positions upstream through the chessboard

        for (int i = ActualPosition.X - 1; i >= 0; i--)
        {
            if (pieces[i, ActualPosition.Y] is null)
            {
                CreateNewPositionHelper(i, ActualPosition.Y, pictureBoxes);
            }
            else
            {
                if (pieces[i, ActualPosition.Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (pieces[i, ActualPosition.Y] is King)
                    {
                        break;
                    }
                    else
                    {
                        CreateNewPositionHelper(i, ActualPosition.Y, pictureBoxes);
                        break;
                    }
                }
            }
        }

        // check positions downstream through the chessboard

        for (int i = ActualPosition.X + 1; i <= 7; i++)
        {
            if (pieces[i, ActualPosition.Y] is null)
            {
                CreateNewPositionHelper(i, ActualPosition.Y, pictureBoxes);
            }
            else
            {
                if (pieces[i, ActualPosition.Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (pieces[i, ActualPosition.Y] is King)
                    {
                        break;
                    }
                    else
                    {
                        CreateNewPositionHelper(i, ActualPosition.Y, pictureBoxes);
                        break;
                    }
                }
            }
        }

        // check positions turning right through the chessboard

        for (int i = ActualPosition.Y + 1; i <= 7; i++)
        {
            if (pieces[ActualPosition.X, i] is null)
            {
                CreateNewPositionHelper(ActualPosition.X, i, pictureBoxes);
            }
            else
            {
                if (pieces[ActualPosition.X, i].Color == Color)
                {
                    break;
                }
                else
                {
                    if (pieces[ActualPosition.X, i] is King)
                    {
                        break;
                    }
                    else
                    {
                        CreateNewPositionHelper(ActualPosition.X, i, pictureBoxes);
                        break;
                    }
                }
            }
        }

        // check positions turning left through the chessboard


        for (int i = ActualPosition.Y - 1; i >= 0; i--)
        {
            if (pieces[ActualPosition.X, i] is null)
            {
                CreateNewPositionHelper(ActualPosition.X, i, pictureBoxes);
            }
            else
            {
                if (pieces[ActualPosition.X, i].Color == Color)
                {
                    break;
                }
                else
                {
                    if (pieces[ActualPosition.X, i] is King)
                    {
                        break;
                    }
                    else
                    {
                        CreateNewPositionHelper(ActualPosition.X, i, pictureBoxes);
                        break;
                    }
                }
            }
        }

        // check positions upstream right through the chessboard

        int X = ActualPosition.X - 1;
        int Y = ActualPosition.Y + 1;

        for (; Y <= 7 && X >= 0; Y++)
        {
            if (pieces[X, Y] is null)
            {
                CreateNewPositionHelper(X, Y, pictureBoxes);
                X--;
            }
            else
            {
                if (pieces[X, Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (pieces[X, Y] is King)
                    {
                        break;
                    }
                    else
                    {
                        CreateNewPositionHelper(X, Y, pictureBoxes);
                        break;
                    }
                }
            }
        }

        // check positions downstream right through the chessboard

        X = ActualPosition.X + 1;
        Y = ActualPosition.Y + 1;

        for (; Y <= 7 && X <= 7; Y++)
        {
            if (pieces[X, Y] is null)
            {
                CreateNewPositionHelper(X, Y, pictureBoxes);
                X++;
            }
            else
            {
                if (pieces[X, Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (pieces[X, Y] is King)
                    {
                        break;
                    }
                    else
                    {
                        CreateNewPositionHelper(X, Y, pictureBoxes);
                        break;
                    }
                }
            }
        }

        // check positions downstream left through the chessboard

        X = ActualPosition.X + 1;
        Y = ActualPosition.Y - 1;

        for (; Y >= 0 && X <= 7; Y--)
        {
            if (pieces[X, Y] is null)
            {
                CreateNewPositionHelper(X, Y, pictureBoxes);
                X++;
            }
            else
            {
                if (pieces[X, Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (pieces[X, Y] is King)
                    {
                        break;
                    }
                    else
                    {
                        CreateNewPositionHelper(X, Y, pictureBoxes);
                        break;
                    }
                }
            }
        }

        // check positions upstream left through the chessboard

        X = ActualPosition.X - 1;
        Y = ActualPosition.Y - 1;

        for (; Y >= 0 && X >= 0; Y--)
        {
            if (pieces[X, Y] is null)
            {
                CreateNewPositionHelper(X, Y, pictureBoxes);
                X--;
            }
            else
            {
                if (pieces[X, Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (pieces[X, Y] is King)
                    {
                        break;
                    }
                    else
                    {
                        CreateNewPositionHelper(X, Y, pictureBoxes);
                        break;
                    }
                }
            }
        }

        return pictureBoxes;
    }


    public override Piece DeepCopy(Piece piece, bool color)
    {
        return new Queen(color);
    }
}
