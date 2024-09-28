using ChessGame_v1.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1;

public class Game
{
    public Form1 Form1 { get; set; }
    public ChessBoard ChessBoard { get; set; }
    //public Position PositionForNextMove { get; set; }
    public List<PictureBox> FieldsAllowedMarking { get; set; } = new List<PictureBox>();
    public Game(Form1 form)
    {
        Form1 = form;
        ChessBoard = new ChessBoard(Form1);
    }

    public void StartNewGame()
    {
        InitializePieces();
    }

    public void InitializePieces()
    {
        ChessBoard.SetPiecesInStartPosition();
    }

    public void SelectPiece(object sender, EventArgs e)
    {
        //if (IfAllPiecesUnselected(ChessBoard))
        //{
        //    PictureBox = sender as PictureBox;
        //    PieceBox = PictureBox.Tag as PieceBox;
        //    SwitchPieceSelection(PieceBox);
        //    return;
        //}

        var newPictureBox = sender as PictureBox;
        var newPieceBox = newPictureBox.Tag as PieceBox;

        if (newPieceBox.Piece.IsActive)
        {
            //SwitchPieceSelection(PieceBox);
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
        
        //PositionForNextMove = Game.CalculatePositionFromPoint(res);
        //ChangePosition(picBox, PositionForNextMove);

    }

    public void ChangePosition(PictureBox pictureBox, Position newPosition)
    {
        pictureBox.Location = new Point(newPosition.X, newPosition.Y);
        //pictureBox.Box.Location = new Point(newPosition.X, newPosition.Y);

    }
}
