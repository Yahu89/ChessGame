
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

        return pictureBoxes;
    }

    public override Piece DeepCopy(Piece piece, bool color)
    {
        Piece newRook = new Rook(color)
        {
            Color = piece.Color,
            ActualPosition = piece.ActualPosition 
        };

        return newRook;
    }

    //public bool IsCheckForMyself(bool color)
    //{
    //    if (color)
    //    {

    //    }
    //}
}
