using ChessGame_v1.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1;

public class ChessBoard
{
    public static Piece[,] Pieces { get; set; } = new Piece[8, 8];
    public static Piece[,] TemporaryChessBoard { get; set; }
    public static readonly Dictionary<Position, Point> AllPositionsInTotalList = new Dictionary<Position, Point>();
    public static Image PossiblePositionImage { get; set; } = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\fieldAllowed.png");
    public static Piece SelectedPiece { get; set; } = null;
    public List<PictureBox> PossiblePositionsForNextMove { get; set; }

    private Form1 _form;
    public ChessBoard(Form1 form)
    {
        FillAllPositionsInTotalList();
        _form = form;
    }

    public static Piece[,] CreateTemporatyChessBoard(Piece[,] pieces)
    {
        var temporaryChessBoard = new Piece[8, 8];

        for (int i = 0; i < pieces.GetLength(0); i++)
        {
            for (int j = 0; j < pieces.GetLength(1); j++)
            {
                if (pieces[i, j] != null)
                {
                    temporaryChessBoard[i, j] = pieces[i, j].DeepCopy(pieces[i, j], pieces[i, j].Color);
                }
            }
        }

        return temporaryChessBoard;
    }
    private void FillAllPositionsInTotalList()
    {
        int onePositionDistance = 88;

        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                AllPositionsInTotalList.Add(new Position(i, j), new Point(j * onePositionDistance, i * onePositionDistance));
            }
        }
    }

    public static Point CalculatePointFromPosition(Position position)
    {
        var result = AllPositionsInTotalList.FirstOrDefault(x => x.Key.X == position.X && x.Key.Y == position.Y);
        var res2 = result.Value;
        int x = res2.X;
        int y = res2.Y;
        var newPoint = new Point(x, y);
        return newPoint;
    }
    public static bool IfAllPiecesUnselected()
    {
        bool ifAny = true;

        foreach (var item in Pieces)
        {
            if (item != null && item.IsActive)
            {
                ifAny = false;
                return ifAny;
            }
        }

        return ifAny;
    }

    public static Position CalculatePositionFromPoint(Point point)
    {
        foreach (var item in AllPositionsInTotalList)
        {
            if (item.Value == point)
            {
                return item.Key;
            }
        }

        throw new NullReferenceException();
    }

    public void SwitchSelectedPiece(object sender, EventArgs e)
    {
        var piece = (Piece)sender;
        var king = piece as King;

        if (IfAllPiecesUnselected())
        {
            if (piece.Color == Game.ColorHasMove)
            {
                piece.IsActive = true;
                SelectedPiece = piece;
                PossiblePositionsForNextMove = piece.PossiblePositionsForNextMove(Pieces);
                PossiblePositionsForNextMove.ForEach(x => x.MouseClick += Move);
                PossiblePositionsForNextMove.ForEach(_form.panel1.Controls.Add);
                PossiblePositionsForNextMove.ForEach(x => x.BringToFront());
                RemoveEventServiceAfterPieceSelected();

                if (king != null)
                {
                    if (!king.IsChecked)
                    {
                        piece.BackgroundImage = piece.PieceSelectedImagePath;
                    }
                }
                else
                {
                    piece.BackgroundImage = piece.PieceSelectedImagePath;
                }
            }
        }
        else
        {
            AddEventServiceWhenAllUnselected();
            piece.IsActive = false;

            if (king != null)
            {
                if (!king.IsChecked)
                {
                    piece.BackgroundImage = piece.PieceNotSelectedImagePath;
                }
            }
            else
            {
                piece.BackgroundImage = piece.PieceNotSelectedImagePath;
            }

            SelectedPiece = null;
            RemovePossiblePositions();
        }
    }

    public void RemovePossiblePositions()
    {
        PossiblePositionsForNextMove.ForEach(_form.panel1.Controls.Remove);
        PossiblePositionsForNextMove = null;
    }

    public void Move(object sender, EventArgs e)
    {
        var possiblePosition = (PictureBox)sender;
        var newPosition = CalculatePositionFromPoint(possiblePosition.Location);
        var oldPosition = SelectedPiece.ActualPosition;

        SelectedPiece.ChangePosition(newPosition, oldPosition, SelectedPiece, _form);

        RemovePossiblePositions();
        AddEventServiceWhenAllUnselected();
        SelectedPiece.IsActive = false;

        if (SelectedPiece is Pawn)
        {
            var tempSelected = (Pawn)SelectedPiece;
            tempSelected.IsBeforeFirstMove = false;
        }

        Position kingPosition = new Position();

        foreach (var item in Pieces)
        {
            if (item != null && item.Color == SelectedPiece.Color && item is King)
            {
                kingPosition = new Position(item.ActualPosition);
            }
        }

        var king = Pieces[kingPosition.X, kingPosition.Y] as King;
        var res = IsCheckedForEnemy();

        if (king.IsChecked)
        {
            king.BackgroundImage = king.PieceNotSelectedImagePath;
            king.IsChecked = false;
        }

        SelectedPiece = null;
        Game.SwitchPlayerForMove();
    }

    public void SetPiecesInStartPosition()
    {
        for (int i = 0; i <= 7; i++)
        {
            Pieces[1, i] = new Pawn(true)
            {
                ActualPosition = new Position(1, i),
            };

            Pieces[1, i].BackgroundImage = Pieces[1, i].PieceNotSelectedImagePath;
            Pieces[1, i].Location = CalculatePointFromPosition(Pieces[1, i].ActualPosition);
            _form.panel1.Controls.Add(Pieces[1, i]);
            Pieces[1, i].BringToFront();
            Pieces[1, i].MouseClick += SwitchSelectedPiece;
            Pieces[1, i].Tag = Pieces[1, i];
        }

        for (int i = 0; i <= 7; i++)
        {
            Pieces[6, i] = new Pawn(false)
            {
                ActualPosition = new Position(6, i),
            };

            Pieces[6, i].BackgroundImage = Pieces[6, i].PieceNotSelectedImagePath;
            Pieces[6, i].Location = CalculatePointFromPosition(Pieces[6, i].ActualPosition);
            _form.panel1.Controls.Add(Pieces[6, i]);
            Pieces[6, i].BringToFront();
            Pieces[6, i].MouseClick += SwitchSelectedPiece;
            Pieces[6, i].Tag = Pieces[6, i];
        }

        Pieces[0, 0] = new Rook(true)
        {
            ActualPosition = new Position(0, 0),
        };

        Pieces[0, 1] = new Knight(true)
        {
            ActualPosition = new Position(0, 1),
        };

        Pieces[0, 2] = new Bishop(true)
        {
            ActualPosition = new Position(0, 2),
        };

        Pieces[0, 3] = new Queen(true)
        {
            ActualPosition = new Position(0, 3),
        };

        Pieces[0, 4] = new King(true)
        {
            ActualPosition = new Position(0, 4),
        };

        Pieces[0, 5] = new Bishop(true)
        {
            ActualPosition = new Position(0, 5),
        };

        Pieces[0, 6] = new Knight(true)
        {
            ActualPosition = new Position(0, 6),
        };

        Pieces[0, 7] = new Rook(true)
        {
            ActualPosition = new Position(0, 7),
        };

        Pieces[7, 0] = new Rook(false)
        {
            ActualPosition = new Position(7, 0),
        };

        Pieces[7, 1] = new Knight(false)
        {
            ActualPosition = new Position(7, 1),
        };

        Pieces[7, 2] = new Bishop(false)
        {
            ActualPosition = new Position(7, 2),
        };

        Pieces[7, 3] = new Queen(false)
        {
            ActualPosition = new Position(7, 3),
        };

        Pieces[7, 4] = new King(false)
        {
            ActualPosition = new Position(7, 4),
        };

        Pieces[7, 5] = new Bishop(false)
        {
            ActualPosition = new Position(7, 5),
        };

        Pieces[7, 6] = new Knight(false)
        {
            ActualPosition = new Position(7, 6),
        };

        Pieces[7, 7] = new Rook(false)
        {
            ActualPosition = new Position(7, 7),
        };

        for (int i = 0; i <= 7; i++)
        {
            Pieces[0, i].BackgroundImage = Pieces[0, i].PieceNotSelectedImagePath;
            Pieces[0, i].Location = CalculatePointFromPosition(Pieces[0, i].ActualPosition);
            _form.panel1.Controls.Add(Pieces[0, i]);
            Pieces[0, i].BringToFront();
            Pieces[0, i].MouseClick += SwitchSelectedPiece;
            Pieces[0, i].Tag = Pieces[0, i];
        }

        for (int i = 0; i <= 7; i++)
        {
            Pieces[7, i].BackgroundImage = Pieces[7, i].PieceNotSelectedImagePath;
            Pieces[7, i].Location = CalculatePointFromPosition(Pieces[7, i].ActualPosition);
            _form.panel1.Controls.Add(Pieces[7, i]);
            Pieces[7, i].BringToFront();
            Pieces[7, i].MouseClick += SwitchSelectedPiece;
            Pieces[7, i].Tag = Pieces[7, i];
        }
    }

    public void RemoveEventServiceAfterPieceSelected()
    {
        foreach (var piece in Pieces)
        {
            if (piece != null && piece.IsActive == false)
            {
                piece.MouseClick -= SwitchSelectedPiece;
            }
        }
    }

    public void AddEventServiceWhenAllUnselected()
    {
        foreach (var piece in Pieces)
        {
            if (piece != null && piece.IsActive == false)
            {
                piece.MouseClick += SwitchSelectedPiece;
            }
        }
    }

    public Position KingPosition()
    {
        Position kingPosition = new Position();

        foreach (var item in Pieces)
        {
            if (item != null && item.Color == !SelectedPiece.Color && item is King)
            {
                kingPosition = new Position(item.ActualPosition);
                return kingPosition;
            }
        }

        return null;
    }

    public bool IsCheckedForEnemy()
    {
        Position kingPosition = KingPosition();

        // check positions upstream through the chessboard

        for (int i = kingPosition.X - 1; i >= 0; i--)
        {
            if (Pieces[i, kingPosition.Y] != null)
            {
                if (Pieces[i, kingPosition.Y].Color == !SelectedPiece.Color)
                {
                    break;
                }

                if (Pieces[i, kingPosition.Y] is Rook || Pieces[i, kingPosition.Y] is Queen)
                {
                    var enemyKing = Pieces[kingPosition.X, kingPosition.Y] as King;
                    enemyKing.Checked(!SelectedPiece.Color);
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
            if (Pieces[i, kingPosition.Y] != null)
            {
                if (Pieces[i, kingPosition.Y].Color == !SelectedPiece.Color)
                {
                    break;
                }

                if (Pieces[i, kingPosition.Y] is Rook || Pieces[i, kingPosition.Y] is Queen)
                {
                    var enemyKing = Pieces[kingPosition.X, kingPosition.Y] as King;
                    enemyKing.Checked(!SelectedPiece.Color);
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
            if (Pieces[kingPosition.X, i] != null)
            {
                if (Pieces[kingPosition.X, i].Color == !SelectedPiece.Color)
                {
                    break;
                }

                if (Pieces[kingPosition.X, i] is Rook || Pieces[kingPosition.X, i] is Queen)
                {
                    var enemyKing = Pieces[kingPosition.X, kingPosition.Y] as King;
                    enemyKing.Checked(!SelectedPiece.Color);
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
            if (Pieces[kingPosition.X, i] != null)
            {
                if (Pieces[kingPosition.X, i].Color == !SelectedPiece.Color)
                {
                    break;
                }

                if (Pieces[kingPosition.X, i] is Rook || Pieces[kingPosition.X, i] is Queen)
                {
                    var enemyKing = Pieces[kingPosition.X, kingPosition.Y] as King;
                    enemyKing.Checked(!SelectedPiece.Color);
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
            if (Pieces[X, Y] != null)
            {
                if (Pieces[X, Y].Color == !SelectedPiece.Color)
                {
                    break;
                }

                if (Pieces[X, Y] is Bishop || Pieces[X, Y] is Queen)
                {
                    var enemyKing = Pieces[kingPosition.X, kingPosition.Y] as King;
                    enemyKing.Checked(!SelectedPiece.Color);
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
            if (Pieces[X, Y] != null)
            {
                if (Pieces[X, Y].Color == !SelectedPiece.Color)
                {
                    break;
                }

                if (Pieces[X, Y] is Bishop || Pieces[X, Y] is Queen)
                {
                    var enemyKing = Pieces[kingPosition.X, kingPosition.Y] as King;
                    enemyKing.Checked(!SelectedPiece.Color);
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
            if (Pieces[X, Y] != null)
            {
                if (Pieces[X, Y].Color == !SelectedPiece.Color)
                {
                    break;
                }

                if (Pieces[X, Y] is Bishop || Pieces[X, Y] is Queen)
                {
                    var enemyKing = Pieces[kingPosition.X, kingPosition.Y] as King;
                    enemyKing.Checked(!SelectedPiece.Color);
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
            if (Pieces[X, Y] != null)
            {
                if (Pieces[X, Y].Color == !SelectedPiece.Color)
                {
                    break;
                }

                if (Pieces[X, Y] is Bishop || Pieces[X, Y] is Queen)
                {
                    var enemyKing = Pieces[kingPosition.X, kingPosition.Y] as King;
                    enemyKing.Checked(!SelectedPiece.Color);
                    return true;
                }
                else
                {
                    break;
                }
            }

            X--;
        }

        //var isCheckedByKnight = SelectedPiece.IsCheckedMyselfByKnight(kingPosition, Pieces);

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
                if (Pieces[item.X, item.Y] is Knight && Pieces[item.X, item.Y].Color != Pieces[kingPosition.X, kingPosition.Y].Color)
                {
                    var enemyKing = Pieces[kingPosition.X, kingPosition.Y] as King;
                    enemyKing.Checked(!SelectedPiece.Color);
                    return true;
                }
            }
        }

        return false;
    }
}
