namespace ChessGame_v1
{
    public partial class Form1 : Form
    {
        public Game Game { get; set; }
        public Form1()
        {
            InitializeComponent();
            Game = new Game(this);
        }

        private void StartNewGameBtn_Click(object sender, EventArgs e)
        {
            Game.StartNewGame();
        }
    }
}
