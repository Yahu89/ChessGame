
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

    public override List<PictureBox> PossiblePositionsForNextMove(Piece[,] pieces)
    {
        List<PictureBox> pictureBoxes = new List<PictureBox>();

        // check positions upstream right through the chessboard

        int X = ActualPosition.X - 1;
        int Y = ActualPosition.Y + 1;

        for (; Y <= 7 && X >= 0; Y++)
        {            
            if (pieces[X, Y] is null)
            {
                var possibleNewChessBoard = ChessBoard.CreateTemporatyPieces(ChessBoard.Pieces);
                possibleNewChessBoard[X, Y] = pieces[ActualPosition.X, ActualPosition.Y];
                possibleNewChessBoard[ActualPosition.X, ActualPosition.Y] = null;

                pictureBoxes.Add(new PictureBox()
                {
                    BackgroundImage = ChessBoard.PossiblePositionImage,
                    BackgroundImageLayout = ImageLayout.Tile,
                    Location = ChessBoard.CalculatePointFromPosition(new Position(X, Y)),
                    Size = new Size(90, 90),
                    BackColor = System.Drawing.Color.Transparent
                });

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
                        pictureBoxes.Add(new PictureBox()
                        {
                            BackgroundImage = ChessBoard.PossiblePositionImage,
                            BackgroundImageLayout = ImageLayout.Tile,
                            Location = ChessBoard.CalculatePointFromPosition(new Position(X, Y)),
                            Size = new Size(90, 90),
                            BackColor = System.Drawing.Color.Transparent
                        });

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
                var possibleNewChessBoard = ChessBoard.CreateTemporatyPieces(ChessBoard.Pieces);
                possibleNewChessBoard[X, Y] = pieces[ActualPosition.X, ActualPosition.Y];
                possibleNewChessBoard[ActualPosition.X, ActualPosition.Y] = null;

                pictureBoxes.Add(new PictureBox()
                {
                    BackgroundImage = ChessBoard.PossiblePositionImage,
                    BackgroundImageLayout = ImageLayout.Tile,
                    Location = ChessBoard.CalculatePointFromPosition(new Position(X, Y)),
                    Size = new Size(90, 90),
                    BackColor = System.Drawing.Color.Transparent
                });

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
                        pictureBoxes.Add(new PictureBox()
                        {
                            BackgroundImage = ChessBoard.PossiblePositionImage,
                            BackgroundImageLayout = ImageLayout.Tile,
                            Location = ChessBoard.CalculatePointFromPosition(new Position(X, Y)),
                            Size = new Size(90, 90),
                            BackColor = System.Drawing.Color.Transparent
                        });

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
                var possibleNewChessBoard = ChessBoard.CreateTemporatyPieces(ChessBoard.Pieces);
                possibleNewChessBoard[X, Y] = pieces[ActualPosition.X, ActualPosition.Y];
                possibleNewChessBoard[ActualPosition.X, ActualPosition.Y] = null;

                pictureBoxes.Add(new PictureBox()
                {
                    BackgroundImage = ChessBoard.PossiblePositionImage,
                    BackgroundImageLayout = ImageLayout.Tile,
                    Location = ChessBoard.CalculatePointFromPosition(new Position(X, Y)),
                    Size = new Size(90, 90),
                    BackColor = System.Drawing.Color.Transparent
                });

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
                        pictureBoxes.Add(new PictureBox()
                        {
                            BackgroundImage = ChessBoard.PossiblePositionImage,
                            BackgroundImageLayout = ImageLayout.Tile,
                            Location = ChessBoard.CalculatePointFromPosition(new Position(X, Y)),
                            Size = new Size(90, 90),
                            BackColor = System.Drawing.Color.Transparent
                        });

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
                var possibleNewChessBoard = ChessBoard.CreateTemporatyPieces(ChessBoard.Pieces);
                possibleNewChessBoard[X, Y] = pieces[ActualPosition.X, ActualPosition.Y];
                possibleNewChessBoard[ActualPosition.X, ActualPosition.Y] = null;

                pictureBoxes.Add(new PictureBox()
                {
                    BackgroundImage = ChessBoard.PossiblePositionImage,
                    BackgroundImageLayout = ImageLayout.Tile,
                    Location = ChessBoard.CalculatePointFromPosition(new Position(X, Y)),
                    Size = new Size(90, 90),
                    BackColor = System.Drawing.Color.Transparent
                });

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
                        pictureBoxes.Add(new PictureBox()
                        {
                            BackgroundImage = ChessBoard.PossiblePositionImage,
                            BackgroundImageLayout = ImageLayout.Tile,
                            Location = ChessBoard.CalculatePointFromPosition(new Position(X, Y)),
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
