using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1.Pieces;

public class Rook : Piece
{
    public override Image PieceNotSelectedImagePath { get; set; }
    public override Image PieceSelectedImagePath { get; set; }
    public Rook(bool color) : base()
    {
        Color = color;

        if (Color)
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\rook.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\rook_selected.png");
        }
        else
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\rook_white.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\rook_white_selected.png");
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
                pictureBoxes.Add(new PictureBox()
                {
                    BackgroundImage = ChessBoard.PossiblePositionImage,
                    BackgroundImageLayout = ImageLayout.Tile,
                    Location = ChessBoard.CalculatePointFromPosition(new Position(i, ActualPosition.Y)),
                    Size = new Size(90, 90),
                    BackColor = System.Drawing.Color.Transparent
                });
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
                        pictureBoxes.Add(new PictureBox()
                        {
                            BackgroundImage = ChessBoard.PossiblePositionImage,
                            BackgroundImageLayout = ImageLayout.Tile,
                            Location = ChessBoard.CalculatePointFromPosition(new Position(i, ActualPosition.Y)),
                            Size = new Size(90, 90),
                            BackColor = System.Drawing.Color.Transparent
                        });

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
                pictureBoxes.Add(new PictureBox()
                {
                    BackgroundImage = ChessBoard.PossiblePositionImage,
                    BackgroundImageLayout = ImageLayout.Tile,
                    Location = ChessBoard.CalculatePointFromPosition(new Position(i, ActualPosition.Y)),
                    Size = new Size(90, 90),
                    BackColor = System.Drawing.Color.Transparent
                });
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
                        pictureBoxes.Add(new PictureBox()
                        {
                            BackgroundImage = ChessBoard.PossiblePositionImage,
                            BackgroundImageLayout = ImageLayout.Tile,
                            Location = ChessBoard.CalculatePointFromPosition(new Position(i, ActualPosition.Y)),
                            Size = new Size(90, 90),
                            BackColor = System.Drawing.Color.Transparent
                        });

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
                pictureBoxes.Add(new PictureBox()
                {
                    BackgroundImage = ChessBoard.PossiblePositionImage,
                    BackgroundImageLayout = ImageLayout.Tile,
                    Location = ChessBoard.CalculatePointFromPosition(new Position(ActualPosition.X, i)),
                    Size = new Size(90, 90),
                    BackColor = System.Drawing.Color.Transparent
                });
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
                        pictureBoxes.Add(new PictureBox()
                        {
                            BackgroundImage = ChessBoard.PossiblePositionImage,
                            BackgroundImageLayout = ImageLayout.Tile,
                            Location = ChessBoard.CalculatePointFromPosition(new Position(ActualPosition.X, i)),
                            Size = new Size(90, 90),
                            BackColor = System.Drawing.Color.Transparent
                        });

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
                pictureBoxes.Add(new PictureBox()
                {
                    BackgroundImage = ChessBoard.PossiblePositionImage,
                    BackgroundImageLayout = ImageLayout.Tile,
                    Location = ChessBoard.CalculatePointFromPosition(new Position(ActualPosition.X, i)),
                    Size = new Size(90, 90),
                    BackColor = System.Drawing.Color.Transparent
                });
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
                        pictureBoxes.Add(new PictureBox()
                        {
                            BackgroundImage = ChessBoard.PossiblePositionImage,
                            BackgroundImageLayout = ImageLayout.Tile,
                            Location = ChessBoard.CalculatePointFromPosition(new Position(ActualPosition.X, i)),
                            Size = new Size(90, 90),
                            BackColor = System.Drawing.Color.Transparent
                        });

                        break;
                    }
                }
            }
        }

        return pictureBoxes;
    }
}
