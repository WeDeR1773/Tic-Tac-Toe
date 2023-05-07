using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        private List<Button> buttons = new List<Button>();
            
        private bool firstPlayerTurn = true;                                // if true - turn of X, if false - turn of O
        private int count = 0;                                              // count of turns
        private byte[,] gameState;                                          //for check, who wins

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.MediumAquamarine;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "TicTacToe";

            this.Paint += Form1_Paint;
            this.Load += Form1_Load;

            CreateBtnReset();
        }

        private void Form1_Paint(object sender, EventArgs e)
        {
            CreateGate();           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void ResetGame()
        {
            CageReset();
            CreateSpaces();
            CLearOfFigures();
        }

        public void CreateGate()
        {
            Graphics elementVertical;
            Point firstPointV = new Point(300, 50);
            Point secondPointV = new Point(300, 350);

            Graphics elementHorizontal;
            Point firstPointH = new Point(200, 150);
            Point secondPointH = new Point(500, 150);

            Pen Pen = new Pen(Color.Black, 2f);

            for (int i = 0; i < 2; i++)
            {
                elementVertical = CreateGraphics();
                elementVertical.DrawLine(Pen, firstPointV, secondPointV);
                firstPointV.X += 100;
                secondPointV.X += 100;
                elementVertical.Dispose();

                elementHorizontal = CreateGraphics();
                elementHorizontal.DrawLine(Pen, firstPointH, secondPointH);
                firstPointH.Y += 100;
                secondPointH.Y += 100;
                elementHorizontal.Dispose();
            }
            Pen.Dispose();
        }

        public void CreateSpaces()
        {
            Point position = new Point(241, 91);           

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    CreateBtnPlay(position, i, j);

                    position.X += 100;
                }
                position.Y += 100;
                position.X = 241;
            }
        }

        public void CreateBtnPlay(Point position, int x, int y)
        {
            Button btnPlay = new Button();
            btnPlay.Text = "";
            btnPlay.BackColor = Color.LightGray;
            btnPlay.ForeColor = Color.Gray;
            btnPlay.Location = new Point(position.X - 36 , position.Y  - 36);
            btnPlay.Size = new Size(90, 90);
            btnPlay.Click += BtnPlay_Click;
            btnPlay.Tag = $"{x} {y}";
            buttons.Add(btnPlay);

            this.Controls.Add(btnPlay);
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;           
            if (btn != null)
            {
                string[] coordinates = btn.Tag.ToString().Split(' ');
                int x = int.Parse(coordinates[0]);
                int y = int.Parse(coordinates[1]);

                btn.Dispose();
                buttons.Remove(btn);

                Draw(x, y, btn);
                
                int winner = CheckGameState();

                if (winner > 0)
                {
                    if (winner == 1)
                    {
                        MessageBox.Show("Winner: X");

                    }
                    else
                    {
                        MessageBox.Show("Winner: O");
                    }
                    CageReset();
                }
                else if (count == 9)
                {
                    MessageBox.Show("Draw");
                }
                
            }
        }

        private int CheckGameState()
        {
            for (int row = 0; row < gameState.GetLength(0); row++)
            {
                int currentPlayer = 0; 
                                
                for (int column = 0; column < gameState.GetLength(1); column++)
                {
                    if (gameState[row, column] != 0)
                    {
                        if (currentPlayer == 0)
                        {
                            currentPlayer = gameState[row, column];
                            continue;
                        }

                        
                        if (currentPlayer != gameState[row, column])
                        {
                            currentPlayer = 0;
                            break;
                        }
                        
                    }
                    else
                    {
                        currentPlayer = 0;
                        break;
                    }

                }
                if (currentPlayer != 0)
                {
                    return currentPlayer;
                }
                
            }

            for (int column = 0; column < gameState.GetLength(1); column++)              
            {
                int currentPlayer = 0;

                for (int row = 0; row < gameState.GetLength(0); row++)
                {
                    if (gameState[row, column] != 0)
                    {
                        if (currentPlayer == 0)
                        {
                            currentPlayer = gameState[row, column];
                            continue;
                        }

                        if (currentPlayer != gameState[row, column])
                        {
                            currentPlayer = 0;
                            break;
                        }
                    }
                    else
                    {
                        currentPlayer = 0;
                        break;
                    }

                }
                if (currentPlayer != 0)
                {
                    return currentPlayer;
                }
            }

            if (gameState[0, 0] != 0 && gameState[0, 0] == gameState[1, 1] && gameState[1, 1] == gameState[2, 2])
            {
                return gameState[0, 0];
            }

            if (gameState[0, 2] != 0 && gameState[0, 2] == gameState[1, 1] && gameState[1, 1] == gameState[2, 0])
            {
                return gameState[0, 2];
            }

            return 0;
        }

        private void CreateBtnReset()
        {
            Button btnReset = new Button();
            btnReset.Text = "Click to restart game";
            btnReset.BackColor = Color.LightGray;
            btnReset.ForeColor = Color.Red;
            btnReset.Location = new Point(30, 30);
            btnReset.Size = new Size(100, 50);
            btnReset.Click += Form1_Load;

            this.Controls.Add(btnReset);
        }

        private void CageReset()
        {
            foreach (Button btn in buttons)
            {
                btn.Dispose();
            }
            gameState = new byte[3, 3];
            firstPlayerTurn = true;
            
            count = 0;
        }

        private void DrawX(Button btn)
        {
            Graphics graphics1 = CreateGraphics();
            Graphics graphics2 = CreateGraphics();
            Pen Pen = new Pen(Color.Black, 2f);

            Point topLeft = new Point(btn.Location.X, btn.Location.Y);
            Point topRight = new Point(btn.Location.X + 90, btn.Location.Y);
            Point bottomLeft = new Point(btn.Location.X, btn.Location.Y + 90);
            Point bottomRight = new Point(btn.Location.X + 90, btn.Location.Y + 90);

            graphics1.DrawLine(Pen, topLeft, bottomRight);
            graphics2.DrawLine(Pen, bottomLeft, topRight);
            graphics1.Dispose();
            graphics2.Dispose();
            Pen.Dispose();
                       
        }

        private void DrawO(Button btn)
        {
            Graphics graphics = CreateGraphics();
            Pen Pen = new Pen(Color.Black, 2f);
            Rectangle rectangle = new Rectangle(btn.Location, new Size(90, 90));

            graphics.DrawEllipse(Pen, rectangle);
            graphics.Dispose();
            Pen.Dispose();

        }

        private void CLearOfFigures()
        {
            Graphics g = CreateGraphics();
            SolidBrush brush = new SolidBrush(Color.Aquamarine);
            Rectangle rectangle = new Rectangle(new Point(0, 0), this.Size);

            g.FillRectangle(brush, rectangle);
            g.Dispose();
            brush.Dispose();
        }

        private void Draw(int x, int y, Button btn)
        {
            if (firstPlayerTurn)
            {
                DrawX(btn);
                gameState[x, y] = 1;
            }
            else
            {
                DrawO(btn);
                gameState[x, y] = 2;
            }       
            firstPlayerTurn = !firstPlayerTurn;
            count++;
        }
    }
}