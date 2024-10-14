using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        pictureBoxes.Add(new PictureBox()
        {
            BackgroundImage = ChessBoard.PossiblePositionImage,
            BackgroundImageLayout = ImageLayout.Tile,
            Location = ChessBoard.CalculatePointFromPosition(new Position(x, y)),
            Size = new Size(90, 90),
            BackColor = System.Drawing.Color.Transparent
        });
    }

    protected Position KingPosition(Piece[,] newChessBoard, bool color)
    {
        Position kingPosition = new Position();

        foreach (var item in newChessBoard)
        {
            if (item != null && item.Color == color && item is King)
            {
                kingPosition = item.ActualPosition;
                break;
            }
        }

        return kingPosition;
    }

    private bool IsCheckedMyselfForKnight(Position kingPosition, Piece[,] chessBoard)
    {
        List<Position> possibleKnightPositions = new List<Position>()
        {
            new Position(kingPosition.X - 2, kingPosition.Y - 1),
            new Position(kingPosition.X - 2, kingPosition.Y + 1),
            new Position(kingPosition.X - 1, kingPosition.Y + 2),
            new Position(kingPosition.X + 1, kingPosition.Y + 2),
            new Position(kingPosition.X + 2, kingPosition.Y - 1),
            new Position(kingPosition.X + 2, kingPosition.Y + 1),
            new Position(kingPosition.X - 1, kingPosition.Y - 2),
            new Position(kingPosition.X + 1, kingPosition.Y - 2)
        };

        foreach (var item in possibleKnightPositions)
        {
            if (item.X >= 0 && item.X <= 7 && item.Y >= 0 && item.Y <= 7)
            {
                if (chessBoard[item.X, item.Y] is Knight && chessBoard[item.X, item.Y].Color != ChessBoard.SelectedPiece.Color)
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected bool IsCheckedMyself(Piece[,] newChessBoard)
    {
        var kingPosition = KingPosition(newChessBoard, ChessBoard.SelectedPiece.Color);

        // check positions upstream through the chessboard

        for (int i = kingPosition.X - 1; i >= 0; i--)
        {
            if (newChessBoard[i, kingPosition.Y] != null)
            {
                if (newChessBoard[i, kingPosition.Y].Color == ChessBoard.SelectedPiece.Color)
                {
                    break;
                }
                else if (newChessBoard[i, kingPosition.Y] is Rook || newChessBoard[i, kingPosition.Y] is Queen)
                {
                    return true;
                }
                else
                {
                    break;
                }
            }
        }

        // check positions downstream through the chessboard

        for (int i = kingPosition.X + 1; i <= 7; i++)
        {
            if (newChessBoard[i, kingPosition.Y] != null)
            {
                if (newChessBoard[i, kingPosition.Y].Color == ChessBoard.SelectedPiece.Color)
                {
                    break;
                }

                if (newChessBoard[i, kingPosition.Y] is Rook || newChessBoard[i, kingPosition.Y] is Queen)
                {
                    return true;
                }
                else
                {
                    break;
                }
            }
        }

        // check positions turning right through the chessboard

        for (int i = kingPosition.Y + 1; i <= 7; i++)
        {
            if (newChessBoard[kingPosition.X, i] != null)
            {
                if (newChessBoard[kingPosition.X, i].Color == ChessBoard.SelectedPiece.Color)
                {
                    break;
                }
                else if (newChessBoard[kingPosition.X, i] is Rook || newChessBoard[kingPosition.X, i] is Queen)
                {
                    return true;
                }
                else
                {
                    break;
                }
            }
        }

        // check positions turning left through the chessboard


        for (int i = kingPosition.Y - 1; i >= 0; i--)
        {
            if (newChessBoard[kingPosition.X, i] != null)
            {
                if (newChessBoard[kingPosition.X, i].Color == ChessBoard.SelectedPiece.Color)
                {
                    break;
                }
                else if (newChessBoard[kingPosition.X, i] is Rook || newChessBoard[kingPosition.X, i] is Queen)
                {
                    return true;
                }
                else
                {
                    break;
                }
            }
        }

        // check positions upstream right through the chessboard

        int X = kingPosition.X - 1;
        int Y = kingPosition.Y + 1;

        for (; Y <= 7 && X >= 0; Y++)
        {
            //if (X >= kingPosition.X - 1)
            //{

            //}

            if (newChessBoard[X, Y] != null)
            {
                if (newChessBoard[X, Y].Color == ChessBoard.SelectedPiece.Color)
                {
                    break;
                }
                else if (newChessBoard[X, Y] is Bishop || newChessBoard[X, Y] is Queen)
                {
                    return true;
                }
                else
                {
                    break;
                }
            }

            X--;
        }

        // check positions downstream right through the chessboard

        X = kingPosition.X + 1;
        Y = kingPosition.Y + 1;

        for (; Y <= 7 && X <= 7; Y++)
        {
            if (newChessBoard[X, Y] != null)
            {
                if (newChessBoard[X, Y].Color == ChessBoard.SelectedPiece.Color)
                {
                    break;
                }
                else if (newChessBoard[X, Y] is Bishop || newChessBoard[X, Y] is Queen)
                {
                    return true;
                }
                else
                {
                    break;
                }
            }

            X++;
        }

        // check positions downstream left through the chessboard

        X = kingPosition.X + 1;
        Y = kingPosition.Y - 1;

        for (; Y >= 0 && X <= 7; Y--)
        {
            if (newChessBoard[X, Y] != null)
            {
                if (newChessBoard[X, Y].Color == ChessBoard.SelectedPiece.Color)
                {
                    break;
                }
                else if (newChessBoard[X, Y] is Bishop || newChessBoard[X, Y] is Queen)
                {
                    return true;
                }
                else
                {
                    break;
                }
            }

            X++;
        }

        // check positions upstream left through the chessboard

        X = kingPosition.X - 1;
        Y = kingPosition.Y - 1;

        for (; Y >= 0 && X >= 0; Y--)
        {
            if (newChessBoard[X, Y] != null)
            {
                if (newChessBoard[X, Y].Color == ChessBoard.SelectedPiece.Color)
                {
                    break;
                }
                else if (newChessBoard[X, Y] is Bishop || newChessBoard[X, Y] is Queen)
                {
                    return true;
                }
                else
                {
                    break;
                }
            }

            X--;
        }

        var isCheckedMyselfForKnight = IsCheckedMyselfForKnight(kingPosition, newChessBoard);


        return false || isCheckedMyselfForKnight;
    }



}
