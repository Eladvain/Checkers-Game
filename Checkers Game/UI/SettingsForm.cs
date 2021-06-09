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
    public partial class SettingsForm : Form
    {
        private GameBoard m_BoardGameForm;
        private int m_Boardsize;

        private string FirstPlayerName
        {
            get
            {
                return this.textBoxPlayer1.Text;
            }
        }

        private string SecondPlayerName
        {
            get
            {
                return this.textBoxPlayer2.Text;
            }
        }

   
        Manager gameManagaer { get; }

        public SettingsForm()
        {
            InitializeComponent();
            
        }

       
        private void radioButtonBoardSize_CheckedChanged(object i_Sender, EventArgs i_E)
        {
            RadioButton boardSizeRadioButton = i_Sender as RadioButton;
            char size = boardSizeRadioButton.Text[0];
            if (size == '1')
            {
                this.m_Boardsize = (size - '0') + 9;
            }
            else
            {
                this.m_Boardsize = size - '0';
            }
           
           
        }
      
        private void checkBoxPlayer2_CheckedChanged(object i_Sender, EventArgs e)
        {
            this.textBoxPlayer2.Enabled = textBoxPlayer2.Enabled == true ? false : true;

            if (textBoxPlayer2.Enabled)
            {
                this.textBoxPlayer2.BackColor = Color.White;
                this.textBoxPlayer2.Text = string.Empty;
                
            }
            else
            {
                this.textBoxPlayer2.BackColor = System.Drawing.SystemColors.MenuBar;
                this.textBoxPlayer2.Text = "[Computer]";
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(this.textBoxPlayer1.Text) || string.IsNullOrEmpty(this.textBoxPlayer2.Text))
            {
                MessageBox.Show("You must fill the player field");

            }
            else
            {
                m_BoardGameForm = new GameBoard(FirstPlayerName, SecondPlayerName, m_Boardsize, checkBoxPlayer2.Checked);
                Hide();
                m_BoardGameForm.ShowDialog();
                Close();
            }   
        }

        private void textBoxPlayer1_TextChanged(object i_Sender, EventArgs e)
        {
            TextBox current = i_Sender as TextBox;
            String player1Name = current.Text;
            if (!isOnlyLetters(player1Name))
            {
                MessageBox.Show(this,
                    @"name is invalid!",
                    @"Name-Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                current.Clear();
            }
        }

        private static bool isOnlyLetters(string i_PotentialName)
        {
            bool ValidInput = true;

            foreach (char currentChar in i_PotentialName)
            {
                if (!char.IsLetter(currentChar))
                {
                    ValidInput = !ValidInput;
                    break;
                }
            }

            return ValidInput;
        }


    }
}
