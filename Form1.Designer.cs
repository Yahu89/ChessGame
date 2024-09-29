namespace ChessGame_v1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            StartNewGameBtn = new Button();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = Image.FromFile("C:\\Users\\Yahu\\source\\repos\\ChessGame_v1\\ChessGame_v1\\Images\\chessboard.png"); //(Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Location = new Point(94, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(705, 705);
            panel1.TabIndex = 0;
            // 
            // StartNewGameBtn
            // 
            StartNewGameBtn.Location = new Point(825, 12);
            StartNewGameBtn.Name = "StartNewGameBtn";
            StartNewGameBtn.Size = new Size(115, 23);
            StartNewGameBtn.TabIndex = 1;
            StartNewGameBtn.Text = "Start New Game";
            StartNewGameBtn.UseVisualStyleBackColor = true;
            StartNewGameBtn.Click += StartNewGameBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1205, 729);
            Controls.Add(StartNewGameBtn);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        public Panel panel1;
        private Button StartNewGameBtn;
    }
}
