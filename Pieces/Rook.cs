
using System.Windows.Forms;

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

    private List<PictureBox> PossiblePositionsWithoutCheckVerify()
    {
        List<PictureBox> pictureBoxes = new List<PictureBox>();

        // check positions upstream through the chessboard

        for (int i = ActualPosition.X - 1; i >= 0; i--)
        {
            if (ChessBoard.Pieces[i, ActualPosition.Y] is null)
            {
                CreateNewPositionHelper(i, ActualPosition.Y, pictureBoxes);
            }
            else
            {
                if (ChessBoard.Pieces[i, ActualPosition.Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (ChessBoard.Pieces[i, ActualPosition.Y] is King)
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
            if (ChessBoard.Pieces[i, ActualPosition.Y] is null)
            {
                CreateNewPositionHelper(i, ActualPosition.Y, pictureBoxes);
            }
            else
            {
                if (ChessBoard.Pieces[i, ActualPosition.Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (ChessBoard.Pieces[i, ActualPosition.Y] is King)
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
            if (ChessBoard.Pieces[ActualPosition.X, i] is null)
            {
                CreateNewPositionHelper(ActualPosition.X, i, pictureBoxes);
            }
            else
            {
                if (ChessBoard.Pieces[ActualPosition.X, i].Color == Color)
                {
                    break;
                }
                else
                {
                    if (ChessBoard.Pieces[ActualPosition.X, i] is King)
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
            if (ChessBoard.Pieces[ActualPosition.X, i] is null)
            {
                CreateNewPositionHelper(ActualPosition.X, i, pictureBoxes);
            }
            else
            {
                if (ChessBoard.Pieces[ActualPosition.X, i].Color == Color)
                {
                    break;
                }
                else
                {
                    if (ChessBoard.Pieces[ActualPosition.X, i] is King)
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
    }

    public override Piece DeepCopy(Piece piece, bool color)
    {
        Position newPosition = new Position();
        Point newPoint = new Point();

        newPosition.X = piece.ActualPosition.X;
        newPosition.Y = piece.ActualPosition.Y;
        newPoint.X = piece.Location.X;
        newPoint.Y = piece.Location.Y;

        Piece newRook = new Rook(color)
        {
            Color = piece.Color,
            ActualPosition = newPosition,
            Location = newPoint
        };

        return newRook;
    }
}
