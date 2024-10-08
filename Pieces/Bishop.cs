
namespace ChessGame_v1.Pieces;

public class Bishop : Piece
{
    public override Image PieceNotSelectedImagePath { get; set; } 
    public override Image PieceSelectedImagePath { get; set; }
    public Bishop(bool color) : base()
    {
        Color = color;

        if (Color)
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\bishop.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\bishop_selected.png");
        }
        else
        {
            PieceNotSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\bishop_white.png");
            PieceSelectedImagePath = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\bishop_white_selected.png");
        }
    }

    private List<PictureBox> PossiblePositionsWithoutCheckVerify()
    {
        List<PictureBox> pictureBoxes = new List<PictureBox>();

        // check positions upstream right through the chessboard

        int X = ActualPosition.X - 1;
        int Y = ActualPosition.Y + 1;

        for (; Y <= 7 && X >= 0; Y++)
        {
            if (ChessBoard.Pieces[X, Y] is null)
            {
                CreateNewPositionHelper(X, Y, pictureBoxes);
                X--;
            }
            else
            {
                if (ChessBoard.Pieces[X, Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (ChessBoard.Pieces[X, Y] is King)
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
            if (ChessBoard.Pieces[X, Y] is null)
            {
                CreateNewPositionHelper(X, Y, pictureBoxes);
                X++;
            }
            else
            {
                if (ChessBoard.Pieces[X, Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (ChessBoard.Pieces[X, Y] is King)
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
            if (ChessBoard.Pieces[X, Y] is null)
            {
                CreateNewPositionHelper(X, Y, pictureBoxes);
                X++;
            }
            else
            {
                if (ChessBoard.Pieces[X, Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (ChessBoard.Pieces[X, Y] is King)
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
            if (ChessBoard.Pieces[X, Y] is null)
            {
                CreateNewPositionHelper(X, Y, pictureBoxes);
                X--;
            }
            else
            {
                if (ChessBoard.Pieces[X, Y].Color == Color)
                {
                    break;
                }
                else
                {
                    if (ChessBoard.Pieces[X, Y] is King)
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
        Piece newBishop = new Bishop(color)
        {
            Color = piece.Color,
            ActualPosition = piece.ActualPosition
        };

        return newBishop;
    }
}
