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

    public override List<PictureBox> PossiblePositionsForNextMove(Piece[,] pieces)
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
                if (pieces[item.X, item.Y] is null)
                {
                    CreateNewPositionHelper(item.X, item.Y, pictureBoxes);
                }
                else
                {
                    if (pieces[item.X, item.Y].Color != Color)
                    {
                        if (!(pieces[item.X, item.Y] is King))
                        {
                            pictureBoxes.Add(new PictureBox()
                            {
                                BackgroundImage = ChessBoard.PossiblePositionImage,
                                BackgroundImageLayout = ImageLayout.Tile,
                                Location = ChessBoard.CalculatePointFromPosition(new Position(item.X, item.Y)),
                                Size = new Size(90, 90),
                                BackColor = System.Drawing.Color.Transparent
                            });
                        }
                    }
                }
            }
        }

        return pictureBoxes;       
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
