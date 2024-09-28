using ChessGame_v1.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1;

public class Game
{
    public PieceBox[,] ChessBoard { get; set; }
    public Form1 Form1 { get; set; }
    public PictureBox PictureBox { get; set; }
    public PieceBox PieceBox { get; set; }
    public Position PositionForNextMove { get; set; }
    public List<PictureBox> FieldsAllowedMarking { get; set; } = new List<PictureBox>();
    public static Dictionary<Position, Point> AllPositionsInTotalList { get; set; } = new Dictionary<Position, Point>();
    public Game(Form1 form)
    {
        Form1 = form;
        FillAllPositionsInTotalList();
    }

    private void FillAllPositionsInTotalList()
    {
        AllPositionsInTotalList.Add(new Position(0, 0), new Point(50, 50));
        AllPositionsInTotalList.Add(new Position(1, 1), new Point(150, 150));
        AllPositionsInTotalList.Add(new Position(4, 4), new Point(364, 358));
        AllPositionsInTotalList.Add(new Position(5, 5), new Point(464, 458));
    }
    public static Point CalculatePointFromPosition(Position position)
    {
        return AllPositionsInTotalList[position];
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
    public void StartNewGame()
    {
        InitializePieces();
    }

    public void InitializePieces()
    {
        ChessBoard = new PieceBox[8, 8];

        // Pozycja figury [4, 4]

        ChessBoard[4, 4] = new PieceBox()
        {
            Position = new Position()
            {
                X = 4,
                Y = 4
            },
            Piece = new Rook(),
            Box = new PictureBox()
            {
                Location = new Point(364, 358),
                BackgroundImageLayout = ImageLayout.Tile,
                Size = new Size(66, 78),
                BackColor = Color.Transparent,        
            }
        };

        ChessBoard[4, 4].ActualPosition = new Dictionary<Position, Point>()
        {
            { ChessBoard[4, 4].Position, ChessBoard[4, 4].Box.Location }
        };

        ChessBoard[4, 4].Box.BackgroundImage = ChessBoard[4, 4].Piece.PieceNotSelectedImagePath;
        ChessBoard[4, 4].Box.Tag = ChessBoard[4, 4];
        ChessBoard[4, 4].Box.BringToFront();
        Form1.panel1.Controls.Add(ChessBoard[4, 4].Box);
        ChessBoard[4, 4].Box.MouseDown += SelectPiece;

        // Pozycja figury

        ChessBoard[1, 0] = new PieceBox()
        {
            Position = new Position()
            {
                X = 1,
                Y = 0
            },
            Piece = new Rook(),
            Box = new PictureBox()
            {
                Location = new Point(190, 170),
                BackgroundImageLayout = ImageLayout.Tile,
                Size = new Size(66, 78),
                BackColor = Color.Transparent,
            }
        };

        ChessBoard[1, 0].Box.Tag = ChessBoard[1, 0];
        ChessBoard[1, 0].Box.BackgroundImage = ChessBoard[1, 0].Piece.PieceNotSelectedImagePath;     
        ChessBoard[1, 0].Box.BringToFront();        
        Form1.panel1.Controls.Add(ChessBoard[1, 0].Box);      
        ChessBoard[1, 0].Box.MouseDown += SelectPiece;
    }

    public void SelectPiece(object sender, EventArgs e)
    {
        if (IfAllPiecesUnselected(ChessBoard))
        {
            PictureBox = sender as PictureBox;
            PieceBox = PictureBox.Tag as PieceBox;
            SwitchPieceSelection(PieceBox);
            return;
        }

        var newPictureBox = sender as PictureBox;
        var newPieceBox = newPictureBox.Tag as PieceBox;

        if (newPieceBox.Piece.IsActive)
        {
            SwitchPieceSelection(PieceBox);
        }
    }

    public void SwitchPieceSelection(PieceBox piece)
    {
        if (piece.Piece.IsActive)
        {
            piece.Piece.IsActive = false;
            piece.Box.BackgroundImage = piece.Piece.PieceNotSelectedImagePath;
            piece.RemoveFieldAllowed(Form1, piece.FieldsAllowedMarking);
        }
        else
        {
            piece.Piece.IsActive = true;
            piece.Box.BackgroundImage = piece.Piece.PieceSelectedImagePath;
            var possiblePositions = piece.GeneratePossiblePositions();
            FieldsAllowedMarking = ShowFieldAllowed(possiblePositions);
            //AddPictureBoxesToForm(FieldsAllowedMarking);
        }
    }

    public bool IfAllPiecesUnselected(PieceBox[,] box)
    {
        bool ifAny = true;

        foreach (var item in box)
        {
            if (item != null && item.Piece.IsActive)
            {
                ifAny = false;
                return ifAny;
            }
        }

        return ifAny;
    }

    //private void AddPictureBoxesToForm(List<PictureBox> pictureBoxes)
    //{
    //    foreach (var pictureBox in pictureBoxes)
    //    {
    //        //pictureBox.BackColor = Color.Transparent;         
    //        //pictureBox.BackgroundImage = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\fieldAllowed.png");
    //        //pictureBox.BackgroundImageLayout = ImageLayout.Tile;
    //        Form1.Controls.Add(pictureBox);
    //        //pictureBox.BackColor = Color.Transparent;
    //        pictureBox.BringToFront();
            
    //    }
    //}

    public List<PictureBox> ShowFieldAllowed(Dictionary<Position, Point> possiblePositions)
    {
        //FieldsAllowedMarking.Clear();

        foreach (var item in possiblePositions)
        {
            var newPictureBox = new PictureBox()
            {
                Location = item.Value,
                BackgroundImage = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\fieldAllowed.png"),
                BackgroundImageLayout = ImageLayout.Tile,
                Size = new Size(47, 47),
                BackColor = Color.Transparent,
                //Tag = this
            };

            newPictureBox.BringToFront();
            newPictureBox.MouseClick += ClickPossibleField;
            FieldsAllowedMarking.Add(newPictureBox);
            Form1.panel1.Controls.Add(newPictureBox);
        }

        return FieldsAllowedMarking;
    }

    public void ClickPossibleField(object sender, EventArgs e)
    {
        var picBox = (PictureBox)sender;
        var res = ((PictureBox)sender).Location;
        
        PositionForNextMove = Game.CalculatePositionFromPoint(res);
        ChangePosition(picBox, PositionForNextMove);

    }

    public void ChangePosition(PictureBox pictureBox, Position newPosition)
    {
        pictureBox.Location = new Point(newPosition.X, newPosition.Y);
        //pictureBox.Box.Location = new Point(newPosition.X, newPosition.Y);

    }
}
