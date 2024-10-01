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
    public static bool ColorHasMove { get; set; } = false;
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

    public static void SwitchPlayerForMove()
    {
        ColorHasMove = !ColorHasMove;
    }
}
