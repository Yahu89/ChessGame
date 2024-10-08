using ChessGame_v1.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1;

public class ChessBoard
{
    public static Piece[,] Pieces { get; set; } = new Piece[8, 8];
    public static Piece[,] TemporaryPieces { get; set; }
    public static Dictionary<Position, Point> AllPositionsInTotalList { get; set; } = new Dictionary<Position, Point>();
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
        TemporaryPieces = null;
        TemporaryPieces = new Piece[8, 8];

        for (int i = 0; i < pieces.GetLength(0); i++)
        {
            for (int j = 0; j < pieces.GetLength(1); j++)
            {
                if (pieces[i, j] != null)
                {
                    TemporaryPieces[i, j] = pieces[i, j].DeepCopy(pieces[i, j], pieces[i, j].Color);
                }             
            }
        }

        return TemporaryPieces;
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
        foreach (var p in AllPositionsInTotalList.Keys)
        {
            if (p.X == position.X &&  p.Y == position.Y)
            {
                return AllPositionsInTotalList[p];
            }
        }

        throw new NullReferenceException();
    }
    public static bool IfAllPiecesUnselected(Piece[,] pieces)
    {
        bool ifAny = true;

        foreach (var item in pieces)
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

        if (IfAllPiecesUnselected(Pieces))
        {       
            if (piece.Color == Game.ColorHasMove)
            {
                piece.IsActive = true;
                piece.BackgroundImage = piece.PieceSelectedImagePath;
                SelectedPiece = piece;
                PossiblePositionsForNextMove = piece.PossiblePositionsForNextMove(Pieces);
                PossiblePositionsForNextMove.ForEach(x => x.MouseClick += Move);
                PossiblePositionsForNextMove.ForEach(_form.panel1.Controls.Add);
                PossiblePositionsForNextMove.ForEach(x => x.BringToFront());
                RemoveEventServiceAfterPieceSelected();
            }           
        }
        else
        {
            AddEventServiceWhenAllUnselected();
            piece.IsActive = false;
            piece.BackgroundImage = piece.PieceNotSelectedImagePath;
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

        SelectedPiece = null;
        Game.SwitchPlayerForMove();
    }

    public void SetPiecesInStartPosition()
    {
        for (int i = 0; i <= 7;  i++)
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

        for (int i = 0; i <= 7;  i++)
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
}
