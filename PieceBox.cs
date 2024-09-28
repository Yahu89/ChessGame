using ChessGame_v1.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame_v1;

public class PieceBox
{
    public Position Position { get; set; }
    public Piece Piece { get; set; }
    public PictureBox Box { get; set; }
    public Position PositionForNextMove { get; set; }
    public Dictionary<Position, Point> ActualPosition { get; set; } = new Dictionary<Position, Point>();
    public Dictionary<Position, Point> PossiblePositionsForNextMove { get; set; } = new Dictionary<Position, Point>();
    public List<PictureBox> FieldsAllowedMarking { get; set; } = new List<PictureBox>();

    //public List<PictureBox> ShowFieldAllowed(Dictionary<Position, Point> possiblePositions)
    //{
    //    //FieldsAllowedMarking.Clear();

    //    foreach (var item in possiblePositions)
    //    {
    //        var newPictureBox = new PictureBox()
    //        {
    //            Location = item.Value,
    //            BackgroundImage = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\fieldAllowed.png"),
    //            BackgroundImageLayout = ImageLayout.Tile,
    //            Size = new Size(47, 47),
    //            BackColor = Color.Transparent,
    //            Tag = this
    //        };

    //        newPictureBox.BringToFront();
    //        newPictureBox.MouseClick += ClickPossibleField;
    //        FieldsAllowedMarking.Add(newPictureBox);
    //        //form.panel1.Controls.Add(newPictureBox);
    //    }

    //    return FieldsAllowedMarking;
    //}
    //public void ClickPossibleField(object sender, EventArgs e)
    //{
    //    var res = ((PictureBox)sender).Location;

    //    PositionForNextMove = Game.CalculatePositionFromPoint(res);
    //    ChangePosition(PositionForNextMove);

    //}

    public void RemoveFieldAllowed(Form1 form, List<PictureBox> pictureBoxes)
    {
        foreach (var pictureBox in pictureBoxes)
        {
            form.panel1.Controls.Remove(pictureBox);
        }
    }

    public Dictionary<Position, Point> GeneratePossiblePositions()
    {
        return new Dictionary<Position, Point>()
        {
            { new Position(4, 4), new Point(464, 458) },
            { new Position(5, 5), new Point(549, 548) }
        };
    }

    //public void ChangePosition(Position newPosition)
    //{
    //    Position = newPosition;
    //    Box.Location = new Point(newPosition.X, newPosition.Y);

    //}
}
