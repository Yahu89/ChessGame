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
        foreach (var item in AllPositionsInTotalList.Values)
        {
            if (item == point)
            {
                return new Position(point.X, point.Y);
            }
        }

        throw new NullReferenceException();
    }

    public void SwitchSelectedPiece(object sender, EventArgs e)
    {
        var piece = (Piece)sender;

        if (IfAllPiecesUnselected(Pieces))
        {       
            piece.IsActive = true;
            piece.BackgroundImage = piece.PieceSelectedImagePath;
            SelectedPiece = piece;
            //GeneratePossiblePositions(piece);
            PossiblePositionsForNextMove = piece.PossiblePositionsForNextMove(Pieces);
            PossiblePositionsForNextMove.ForEach(x => x.MouseClick += Move);
            PossiblePositionsForNextMove.ForEach(_form.panel1.Controls.Add);
            PossiblePositionsForNextMove.ForEach(x => x.BringToFront());
            RemoveEventServiceAfterPieceSelected();
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
        PossiblePositionsForNextMove.Clear();
    }

    //public void GeneratePossiblePositions(Piece selectedPiece)
    //{
    //    PossiblePositionsForNextMove = new List<PictureBox>()
    //    {
    //        new PictureBox()
    //        {
    //            Location = CalculatePointFromPosition(new Position(1, 1)),
    //            BackgroundImage = PossiblePositionImage,
    //            BackgroundImageLayout = ImageLayout.Tile,
    //            Size = new Size(47, 47),
    //            BackColor = Color.Transparent,
    //        }
    //    };

    //    PossiblePositionsForNextMove.ForEach(x => x.MouseClick += Move);
    //    PossiblePositionsForNextMove.ForEach(_form.panel1.Controls.Add);
    //    PossiblePositionsForNextMove.ForEach(x => x.BringToFront());

    //}

    public void Move(object sender, EventArgs e)
    {
        var possiblePosition = (PictureBox)sender;
        var newPosition = CalculatePositionFromPoint(possiblePosition.Location);
        SelectedPiece.ActualPosition = newPosition;
        SelectedPiece.Location = possiblePosition.Location;
        SelectedPiece.BackgroundImage = SelectedPiece.PieceNotSelectedImagePath;       
        RemovePossiblePositions();
        AddEventServiceWhenAllUnselected();
        SelectedPiece.IsActive = false;
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
            //Color = true,
            ActualPosition = new Position(0, 0),
        };

        Pieces[0, 0].BackgroundImage = Pieces[0, 0].PieceNotSelectedImagePath;
        Pieces[0, 0].Location = CalculatePointFromPosition(Pieces[0, 0].ActualPosition);
        _form.panel1.Controls.Add(Pieces[0, 0]);
        Pieces[0, 0].BringToFront();
        Pieces[0, 0].MouseClick += SwitchSelectedPiece;
        Pieces[0, 0].Tag = Pieces[0, 0];

        // *********************************

        Pieces[0, 1] = new Knight(true)
        {
            //Color = true,
            ActualPosition = new Position(0, 1),
        };

        Pieces[0, 1].BackgroundImage = Pieces[0, 1].PieceNotSelectedImagePath;
        Pieces[0, 1].Location = CalculatePointFromPosition(Pieces[0, 1].ActualPosition);
        _form.panel1.Controls.Add(Pieces[0, 1]);
        Pieces[0, 1].BringToFront();
        Pieces[0, 1].MouseClick += SwitchSelectedPiece;
        Pieces[0, 1].Tag = Pieces[0, 1];

        // *********************************

        Pieces[0, 2] = new Bishop(true)
        {
            //Color = false,
            ActualPosition = new Position(0, 2),
        };

        Pieces[0, 2].BackgroundImage = Pieces[0, 2].PieceNotSelectedImagePath;
        Pieces[0, 2].Location = CalculatePointFromPosition(Pieces[0, 2].ActualPosition);
        _form.panel1.Controls.Add(Pieces[0, 2]);
        Pieces[0, 2].BringToFront();
        Pieces[0, 2].MouseClick += SwitchSelectedPiece;
        Pieces[0, 2].Tag = Pieces[0, 2];

        // *********************************

        Pieces[0, 3] = new Queen(true)
        {
            //Color = true,
            ActualPosition = new Position(0, 3),
        };

        Pieces[0, 3].BackgroundImage = Pieces[0, 3].PieceNotSelectedImagePath;
        Pieces[0, 3].Location = CalculatePointFromPosition(Pieces[0, 3].ActualPosition);
        _form.panel1.Controls.Add(Pieces[0, 3]);
        Pieces[0, 3].BringToFront();
        Pieces[0, 3].MouseClick += SwitchSelectedPiece;
        Pieces[0, 3].Tag = Pieces[0, 3];

        // *********************************

        Pieces[0, 4] = new King(true)
        {
            //Color = true,
            ActualPosition = new Position(0, 4),
        };

        Pieces[0, 4].BackgroundImage = Pieces[0, 4].PieceNotSelectedImagePath;
        Pieces[0, 4].Location = CalculatePointFromPosition(Pieces[0, 4].ActualPosition);
        _form.panel1.Controls.Add(Pieces[0, 4]);
        Pieces[0, 4].BringToFront();
        Pieces[0, 4].MouseClick += SwitchSelectedPiece;
        Pieces[0, 4].Tag = Pieces[0, 4];

        // *********************************

        Pieces[0, 5] = new Bishop(true)
        {
            //Color = true,
            ActualPosition = new Position(0, 5),
        };

        Pieces[0, 5].BackgroundImage = Pieces[0, 5].PieceNotSelectedImagePath;
        Pieces[0, 5].Location = CalculatePointFromPosition(Pieces[0, 5].ActualPosition);
        _form.panel1.Controls.Add(Pieces[0, 5]);
        Pieces[0, 5].BringToFront();
        Pieces[0, 5].MouseClick += SwitchSelectedPiece;
        Pieces[0, 5].Tag = Pieces[0, 5];

        // *********************************

        Pieces[0, 6] = new Knight(true)
        {
            //Color = true,
            ActualPosition = new Position(0, 6),
        };

        Pieces[0, 6].BackgroundImage = Pieces[0, 6].PieceNotSelectedImagePath;
        Pieces[0, 6].Location = CalculatePointFromPosition(Pieces[0, 6].ActualPosition);
        _form.panel1.Controls.Add(Pieces[0, 6]);
        Pieces[0, 6].BringToFront();
        Pieces[0, 6].MouseClick += SwitchSelectedPiece;
        Pieces[0, 6].Tag = Pieces[0, 6];

        // *********************************

        Pieces[0, 7] = new Rook(true)
        {
            //Color = true,
            ActualPosition = new Position(0, 7),
        };

        Pieces[0, 7].BackgroundImage = Pieces[0, 7].PieceNotSelectedImagePath;
        Pieces[0, 7].Location = CalculatePointFromPosition(Pieces[0, 7].ActualPosition);
        _form.panel1.Controls.Add(Pieces[0, 7]);
        Pieces[0, 7].BringToFront();
        Pieces[0, 7].MouseClick += SwitchSelectedPiece;
        Pieces[0, 7].Tag = Pieces[0, 7];

        Pieces[7, 0] = new Rook(false)
        {
            //Color = false,
            ActualPosition = new Position(7, 0),
        };

        Pieces[7, 0].BackgroundImage = Pieces[7, 0].PieceNotSelectedImagePath;
        Pieces[7, 0].Location = CalculatePointFromPosition(Pieces[7, 0].ActualPosition);
        _form.panel1.Controls.Add(Pieces[7, 0]);
        Pieces[7, 0].BringToFront();
        Pieces[7, 0].MouseClick += SwitchSelectedPiece;
        Pieces[7, 0].Tag = Pieces[7, 0];

        // *********************************

        Pieces[7, 1] = new Knight(false)
        {
            //Color = false,
            ActualPosition = new Position(7, 1),
        };

        Pieces[7, 1].BackgroundImage = Pieces[7, 1].PieceNotSelectedImagePath;
        Pieces[7, 1].Location = CalculatePointFromPosition(Pieces[7, 1].ActualPosition);
        _form.panel1.Controls.Add(Pieces[7, 1]);
        Pieces[7, 1].BringToFront();
        Pieces[7, 1].MouseClick += SwitchSelectedPiece;
        Pieces[7, 1].Tag = Pieces[7, 1];

        // *********************************

        Pieces[7, 2] = new Bishop(false)
        {
            //Color = false,
            ActualPosition = new Position(7, 2),
        };

        Pieces[7, 2].BackgroundImage = Pieces[7, 2].PieceNotSelectedImagePath;
        Pieces[7, 2].Location = CalculatePointFromPosition(Pieces[7, 2].ActualPosition);
        _form.panel1.Controls.Add(Pieces[7, 2]);
        Pieces[7, 2].BringToFront();
        Pieces[7, 2].MouseClick += SwitchSelectedPiece;
        Pieces[7, 2].Tag = Pieces[7, 2];

        // *********************************

        Pieces[7, 3] = new Queen(false)
        {
            //Color = false,
            ActualPosition = new Position(7, 3),
        };

        Pieces[7, 3].BackgroundImage = Pieces[7, 3].PieceNotSelectedImagePath;
        Pieces[7, 3].Location = CalculatePointFromPosition(Pieces[7, 3].ActualPosition);
        _form.panel1.Controls.Add(Pieces[7, 3]);
        Pieces[7, 3].BringToFront();
        Pieces[7, 3].MouseClick += SwitchSelectedPiece;
        Pieces[7, 3].Tag = Pieces[7, 3];

        // *********************************

        Pieces[7, 4] = new King(false)
        {
            //Color = false,
            ActualPosition = new Position(7, 4),
        };

        Pieces[7, 4].BackgroundImage = Pieces[7, 4].PieceNotSelectedImagePath;
        Pieces[7, 4].Location = CalculatePointFromPosition(Pieces[7, 4].ActualPosition);
        _form.panel1.Controls.Add(Pieces[7, 4]);
        Pieces[7, 4].BringToFront();
        Pieces[7, 4].MouseClick += SwitchSelectedPiece;
        Pieces[7, 4].Tag = Pieces[7, 4];

        // *********************************

        Pieces[7, 5] = new Bishop(false)
        {
            //Color = false,
            ActualPosition = new Position(7, 5),
        };

        Pieces[7, 5].BackgroundImage = Pieces[7, 5].PieceNotSelectedImagePath;
        Pieces[7, 5].Location = CalculatePointFromPosition(Pieces[7, 5].ActualPosition);
        _form.panel1.Controls.Add(Pieces[7, 5]);
        Pieces[7, 5].BringToFront();
        Pieces[7, 5].MouseClick += SwitchSelectedPiece;
        Pieces[7, 5].Tag = Pieces[7, 5];

        // *********************************

        Pieces[7, 6] = new Knight(false)
        {
            //Color = false,
            ActualPosition = new Position(7, 6),
        };

        Pieces[7, 6].BackgroundImage = Pieces[7, 6].PieceNotSelectedImagePath;
        Pieces[7, 6].Location = CalculatePointFromPosition(Pieces[7, 6].ActualPosition);
        _form.panel1.Controls.Add(Pieces[7, 6]);
        Pieces[7, 6].BringToFront();
        Pieces[7, 6].MouseClick += SwitchSelectedPiece;
        Pieces[7, 6].Tag = Pieces[7, 6];

        // *********************************

        Pieces[7, 7] = new Rook(false)
        {
            //Color = false,
            ActualPosition = new Position(7, 7),
        };

        Pieces[7, 7].BackgroundImage = Pieces[7, 7].PieceNotSelectedImagePath;
        Pieces[7, 7].Location = CalculatePointFromPosition(Pieces[7, 7].ActualPosition);
        _form.panel1.Controls.Add(Pieces[7, 7]);
        Pieces[7, 7].BringToFront();
        Pieces[7, 7].MouseClick += SwitchSelectedPiece;
        Pieces[7, 7].Tag = Pieces[7, 7];


        // ********************************* test only

        Pieces[4, 4] = new Rook(true)
        {
            //Color = false,
            ActualPosition = new Position(4, 4),
        };

        Pieces[4, 4].BackgroundImage = Pieces[4, 4].PieceNotSelectedImagePath;
        Pieces[4, 4].Location = CalculatePointFromPosition(Pieces[4, 4].ActualPosition);
        _form.panel1.Controls.Add(Pieces[4, 4]);
        Pieces[4, 4].BringToFront();
        Pieces[4, 4].MouseClick += SwitchSelectedPiece;
        Pieces[4, 4].Tag = Pieces[4, 4];
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
