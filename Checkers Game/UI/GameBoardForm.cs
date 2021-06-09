using C20__Ex2.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C20__Ex2
{
    public partial class GameBoard : Form, AfterMove
    {
      
        private const int k_ButtonWidth = 50;
        private const int k_ButtonHeight = 50;

        private const int k_SartingButtonX = 20;
        private const int k_SartingButtonY = 40;

        private const int k_SideMargin = 20;
        private const int k_TopMargin = 100;

        private readonly Manager r_GameControler;

        private readonly char[,] m_charMatrix;
        private readonly ButtonTool[,] m_buttonMatrix;

        private ButtonTool m_FirstClicked;
        private ButtonTool m_SeconedClicked;
        private bool m_IsFirstClickOnButton = true;

        private Label m_LablePlayer1 = new Label();
        private Label m_LablePlayer2 = new Label();
        
        public GameBoard(
            string i_FirstPlayerName,
            string i_SecondPlayerName,
            int i_BoardSize,
            bool i_IsAgainstComputer)
        {
            InitializeComponent();
            m_buttonMatrix = new ButtonTool[i_BoardSize, i_BoardSize];

            r_GameControler = new Manager(i_FirstPlayerName, i_SecondPlayerName, i_BoardSize, i_IsAgainstComputer);
            r_GameControler.AfterMovement = this;
            m_charMatrix = r_GameControler.TheGame.Board.GameBoard;
            this.ClientSize = new Size((i_BoardSize * k_ButtonWidth) + 2*k_SideMargin, (i_BoardSize * k_ButtonHeight) + k_TopMargin);

            r_GameControler.PlayGame();
            initialBoardButtons();
            setPlayersLabels(i_FirstPlayerName,i_SecondPlayerName);
            initialPawnsOnButtons();
            MarkCurrentPlayerLabel(r_GameControler.TheGame.WhoPlayNow.PlayerName);
        }

        private void updateButtonMatrixFronCharMatrixBoard()
        {
            char[,] m_CharMtrixBoard = r_GameControler.TheGame.Board.GameBoard;

            int Boardsize = r_GameControler.TheGame.Board.Size;
            for (int i = 0; i < Boardsize; i++)
            {
                for (int j = 0; j < Boardsize; j++)
                {
                    if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j % 2 != 0))
                    {
                        m_buttonMatrix[i, j].BackColor = Color.Gray;
                    }
                    else
                    {
                        m_buttonMatrix[i, j].Text = (m_CharMtrixBoard[i, j]).ToString();
                        m_buttonMatrix[i, j].ButtonPosition.InPosition = (Board.eCheckersOption)m_CharMtrixBoard[i, j];
                        m_buttonMatrix[i, j].Enabled = true;
                        m_buttonMatrix[i, j].BackColor = Color.White;
                    }
                }
            }
            MarkCurrentPlayerLabel(r_GameControler.TheGame.WhoPlayNow.PlayerName);

        }

        private void initialPawnsOnButtons()
        {
            char[,] m_CharMtrixBoard = r_GameControler.TheGame.Board.GameBoard;

            int Boardsize = r_GameControler.TheGame.Board.Size; 
            for (int i = 0; i < Boardsize; i++)
            {
                for (int j = 0; j < Boardsize; j++)
                {
                    if (i % 2 != 0 && j % 2 == 0 && i < (Boardsize / 2) - 1)
                    {
                        m_buttonMatrix[i, j].Text = (m_CharMtrixBoard[i, j]).ToString();
                        m_buttonMatrix[i, j].ButtonPosition.InPosition = (Board.eCheckersOption)m_CharMtrixBoard[i, j];
                        m_buttonMatrix[i, j].Enabled = true;
                        m_buttonMatrix[i, j].BackColor = Color.White;
                    }
                    else if (i % 2 == 0 && j % 2 != 0 && i < (Boardsize / 2) - 1)
                    {
                        m_buttonMatrix[i, j].Text = (m_CharMtrixBoard[i, j]).ToString();
                        m_buttonMatrix[i, j].ButtonPosition.InPosition = (Board.eCheckersOption)m_CharMtrixBoard[i, j];
                        m_buttonMatrix[i, j].Enabled = true;
                        m_buttonMatrix[i, j].BackColor = Color.White;
                    }
                    else if (i % 2 == 0 && j % 2 != 0 && i > (Boardsize / 2))
                    {
                        m_buttonMatrix[i, j].Text = (m_CharMtrixBoard[i, j]).ToString();
                        m_buttonMatrix[i, j].ButtonPosition.InPosition = (Board.eCheckersOption)m_CharMtrixBoard[i, j];
                        m_buttonMatrix[i, j].Enabled = true;
                        m_buttonMatrix[i, j].BackColor = Color.White;
                    }
                    else if (i % 2 != 0 && j % 2 == 0 && i > (Boardsize / 2))
                    {
                        m_buttonMatrix[i, j].Text = (m_CharMtrixBoard[i, j]).ToString();
                        m_buttonMatrix[i, j].ButtonPosition.InPosition = (Board.eCheckersOption)m_CharMtrixBoard[i, j];
                        m_buttonMatrix[i, j].Enabled = true;
                        m_buttonMatrix[i, j].BackColor = Color.White;
                    }
                    else
                    {
                        m_buttonMatrix[i, j].BackColor = Color.Gray;
                        if((i == (Boardsize/2 -1)) || (i == Boardsize/2))
                        {
                            if ((i % 2 == 0 && j % 2 == 0) || (i % 2 != 0 && j %2 != 0))
                            {
                                m_buttonMatrix[i, j].BackColor = Color.Gray;
                                m_buttonMatrix[i, j].Text = (m_CharMtrixBoard[i, j]).ToString();
                            }
                            else
                            {
                                m_buttonMatrix[i, j].Text = (m_CharMtrixBoard[i, j]).ToString();
                                m_buttonMatrix[i, j].BackColor = Color.White;
                                m_buttonMatrix[i, j].Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void setPlayersLabels(string i_firstPlayerName, string i_secondPlayerName)
        {
            setPlayersLabelLocation();
            UpdateScoreBorad();
            this.Controls.Add(m_LablePlayer1);
            this.Controls.Add(m_LablePlayer2);
        }

        private void setPlayersLabelLocation()
        {
            ButtonTool middleButton = m_buttonMatrix[0, (r_GameControler.TheGame.Board.Size / 2) - 1];
            int buttomWeidth = middleButton.Width;
            int buttomHeiht = middleButton.Height / 2;

            Point middle = middleButton.Location;
            Point player1LableLocation = middle, player2LabelLocation = middle;

            player1LableLocation.Offset(-middleButton.Width, -buttomHeiht);
            player2LabelLocation.Offset( middleButton.Width, -middleButton.Height/2);
            m_LablePlayer1.Location = player1LableLocation;
            m_LablePlayer1.AutoSize = true;
            m_LablePlayer2.Location = player2LabelLocation;
            m_LablePlayer2.AutoSize = true;
        }

        public void UpdateScoreBorad()
        {
            m_LablePlayer1.Text = string.Format("{0}: {1} ",
                r_GameControler.TheGame.TopDownPlayer.PlayerName, r_GameControler.TheGame.TopDownPlayer.NumScores);

            m_LablePlayer2.Text = string.Format("{0}: {1} ",
                r_GameControler.TheGame.BottomUpPlayer.PlayerName, r_GameControler.TheGame.BottomUpPlayer.NumScores);
        }

        private void initialBoardButtons()
        {
            bool newLine = false, isFirstButton = true;


            ButtonTool lastButtonInMatrix = new ButtonTool();

            int boardSize = r_GameControler.TheGame.Board.Size;

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    ButtonTool currButton = new ButtonTool(j,i,Board.eCheckersOption.space);
                    currButton.Location = new Point(i, j);
                    setButtonLoaction(currButton, newLine, isFirstButton, lastButtonInMatrix);
                    initalizeButton(currButton);
                    m_buttonMatrix[i, j] = currButton;
                    this.Controls.Add(currButton);
                    newLine = false;
                    isFirstButton = false;
                    lastButtonInMatrix = currButton;
                }
                newLine = true;
            }
        }

        private void initalizeButton(Button i_CurrButton)
        {
            setButtonFigure(i_CurrButton);
            i_CurrButton.Enabled = false;
            i_CurrButton.Click += button_CLick;
        }

        private void setButtonFigure(Button i_CurrentButton)
        {
            i_CurrentButton.Height = k_ButtonHeight;
            i_CurrentButton.Width = k_ButtonWidth;
        }

        private void button_CLick(object sender, EventArgs e)
        {
            
            bool secondClickIsOnEmptyButton = false;
            ButtonTool currentGameButton = sender as ButtonTool;
            string error = null;

             if (m_IsFirstClickOnButton)  
             {
                FirstClickOnCurrPlayerButton(ref currentGameButton);
             }
             else 
             {
                SeconeClickOnCurrPlayerButtom(ref currentGameButton, ref secondClickIsOnEmptyButton);
                if (secondClickIsOnEmptyButton)
                {
                    r_GameControler.EachTurn(
                                            r_GameControler.TheGame.WhoPlayNow,
                                            m_FirstClicked.ButtonPosition,
                                            m_SeconedClicked.ButtonPosition,
                                            out error);
                    if(error!=null)
                    {
                        MessageBox.Show(error);
                    }
                    else
                    {
                        UpdateAfterPlayerMove();
                    }
                }
                else if (secondClickIsOnEmptyButton && m_IsFirstClickOnButton)
                {
                    MessageBox.Show("invalid move");
                }
                
            }  
        }

        public void UpdateAfterPlayerMove()
        {
            Player currPlayer = r_GameControler.TheGame.WhoPlayNow;
            string error;

            if (this.r_GameControler.CheckIfTheGameIsOver(currPlayer)) // check if the game over 
            {
                EndGame(currPlayer);
            }
            if (r_GameControler.GameMode == Game.eGameModes.HumanVsComputer &&
                r_GameControler.TheGame.WhoPlayNow == r_GameControler.TheGame.BottomUpPlayer)
            {
                updateButtonMatrixFronCharMatrixBoard();
                r_GameControler.EachTurn(currPlayer, m_FirstClicked.ButtonPosition, m_SeconedClicked.ButtonPosition , out error);
               
            }

            updateButtonMatrixFronCharMatrixBoard();
        }
        public void EndGame(Player i_currPlayer)
        {
            DialogResult result = MessageBox.Show(this,
                                string.Format("The game is over, {0} won!!\nAnother Round?", i_currPlayer.PlayerName),
                                "Damka",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                UpdateScoreBorad();
                playNewGame();
            }
            else
            {
                this.Close();
            }
        }

        private void playNewGame()
        {
            r_GameControler.PlayGame();
            updateButtonMatrixFronCharMatrixBoard();
        }

        public void SeconeClickOnCurrPlayerButtom(ref ButtonTool io_CurrGameButton, ref bool io_secondClickIsOnEmptyButton)
        {
            if (m_FirstClicked == io_CurrGameButton)
            {
                io_CurrGameButton.BackColor = Color.White;
                m_IsFirstClickOnButton = true;
            }
            else if (io_CurrGameButton.Text[0] == (char)Board.eCheckersOption.space)
            {
                if (r_GameControler.TheGame.CheckIfPlayerMustCaptureThisTurn())
                {
                    if (!r_GameControler.TheGame.CheckIfEndPositionIsRightAtCapture(r_GameControler.TheGame.WhoPlayNow, io_CurrGameButton.ButtonPosition))
                    {
                        MessageBox.Show("This pawn must eat");
                        m_FirstClicked.BackColor = Color.White;
                        m_IsFirstClickOnButton = true;
                        io_secondClickIsOnEmptyButton = false;
                    }
                    else
                    {
                        m_FirstClicked.BackColor = Color.White;
                        m_SeconedClicked = io_CurrGameButton;
                        m_IsFirstClickOnButton = true;
                        io_secondClickIsOnEmptyButton = true;
                    }
                }
                else
                {
                    m_FirstClicked.BackColor = Color.White;
                    m_SeconedClicked = io_CurrGameButton;
                    m_IsFirstClickOnButton = true;
                    io_secondClickIsOnEmptyButton = true;
                }

            }
            else if (!io_secondClickIsOnEmptyButton && !m_IsFirstClickOnButton)
            {
                MessageBox.Show("pawn can't move to catched position");
                m_FirstClicked.BackColor = Color.White;
                m_IsFirstClickOnButton = true;
            }
            
           
        }
                
        public void FirstClickOnCurrPlayerButton(ref ButtonTool io_CurrGameButton)
        {
            if (r_GameControler.TheGame.WhoPlayNow.PlayerSign == Board.eCheckersOption.O)
            {
                if (io_CurrGameButton.ButtonPosition.InPosition == Board.eCheckersOption.KingOfBottomUpO ||
                    io_CurrGameButton.ButtonPosition.InPosition == Board.eCheckersOption.O)
                {
                    FirstClickOnCurrPlayerButtonContinue(ref io_CurrGameButton);
                }
                else
                {
                    MessageBox.Show(string.Format("invalid player it's {0} turn", r_GameControler.TheGame.WhoPlayNow.PlayerName));
                }

            }
            else
            {
                if (io_CurrGameButton.ButtonPosition.InPosition == Board.eCheckersOption.KingOfTopDownX ||
                    io_CurrGameButton.ButtonPosition.InPosition == Board.eCheckersOption.X)
                {
                    FirstClickOnCurrPlayerButtonContinue(ref io_CurrGameButton);
                }
                else
                {
                    MessageBox.Show(string.Format("invalid player it's {0} turn", r_GameControler.TheGame.WhoPlayNow.PlayerName));
                }

            }
        }

        public void FirstClickOnCurrPlayerButtonContinue(ref ButtonTool io_CurrGameButton)
        {
            Player currPlayer = r_GameControler.TheGame.WhoPlayNow;
            Position currButtonPosition = io_CurrGameButton.ButtonPosition;
            bool validButtonStart = true;

            if (io_CurrGameButton.Enabled)
            {
                if (m_IsFirstClickOnButton)  
                {
                    if (r_GameControler.TheGame.CheckIfPlayerMustCaptureThisTurn())
                    {
                       if(!r_GameControler.TheGame.CheckIfMustCaptureStartPosition(ref currPlayer, ref currButtonPosition))
                        {
                            MessageBox.Show("You must play with Pwan that can capture");
                            validButtonStart = false;
                            
                        }

                    }

                    if (validButtonStart)
                    {
                        m_FirstClicked = io_CurrGameButton;
                        if (!string.IsNullOrEmpty(m_FirstClicked.Text))  
                        {
                            m_FirstClicked.BackColor = Color.LightBlue;
                            m_IsFirstClickOnButton = false;
                        }
                    }                                                   
                }              
            }
        }

        private void setButtonLoaction(ButtonTool i_CurrButton, bool i_NewLine, bool i_IsFirstButton, ButtonTool i_LastButtonInMatrix)
        {
            Point newLocation;

            if (i_IsFirstButton)
            {
                newLocation = new Point(k_SartingButtonX, k_SartingButtonY);
            }
            else
            {
                newLocation = i_LastButtonInMatrix.Location;
                if (!i_NewLine)
                {
                    newLocation.Offset(i_LastButtonInMatrix.Width, 0);
                }
                else
                {
                    newLocation.X = k_SartingButtonX;
                    newLocation.Offset(0, i_LastButtonInMatrix.Height);
                }
            }
            i_CurrButton.Location = newLocation;

        }
        public void MarkCurrentPlayerLabel(string i_CurrentPlayerName)
        {
            if (i_CurrentPlayerName == r_GameControler.TheGame.TopDownPlayer.PlayerName)
            {
                m_LablePlayer1.ForeColor = Color.Blue;
                m_LablePlayer2.ForeColor = Color.Black;
            }
            else if (i_CurrentPlayerName == r_GameControler.TheGame.BottomUpPlayer.PlayerName)
            {
                m_LablePlayer2.ForeColor = Color.Blue;
                m_LablePlayer1.ForeColor = Color.Black;
            }
        }

    }
}
