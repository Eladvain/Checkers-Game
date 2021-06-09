using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C20__Ex2
{
    public class Board
    {
        public const int k_Six = 6;
        public const int k_Eight = 8;
        public const int k_Ten = 10;

        private readonly int r_Size;
        private int m_NumOfX;
        private int m_NumOfO;
        private char[,] m_GameBoard;

        public Board(int i_Size)
        {
            r_Size = i_Size;
            GameBoard = new char[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (i % 2 != 0 && j % 2 == 0 && i < (Size / 2) - 1)
                    {
                        GameBoard[i, j] = (char)eCheckersOption.X;
                    }
                    else if (i % 2 == 0 && j % 2 != 0 && i < (Size / 2) - 1)
                    {
                        GameBoard[i, j] = (char)eCheckersOption.X;
                    }
                    else if (i % 2 == 0 && j % 2 != 0 && i > (Size / 2))
                    {
                        GameBoard[i, j] = (char)eCheckersOption.O;
                    }
                    else if (i % 2 != 0 && j % 2 == 0 && i > (Size / 2))
                    {
                        GameBoard[i, j] = (char)eCheckersOption.O;
                    }
                    else
                    {
                        GameBoard[i, j] = (char)eCheckersOption.space;

                    }
                }
            }

            m_NumOfX = m_NumOfO = ((Size / 2) * ((Size / 2) - 1)) * 2;
        }

        public enum eCheckersOption
        {
            X = 'X',
            O = 'O',
            space = ' ',
            KingOfTopDownX = 'K',
            KingOfBottomUpO = 'U',
        }

        public enum eSides
        {
            Left = 0,
            Right = 1
        }

        public enum eUpOrDown
        {
            Down = 0,
            Up = 1
        }

        public char GetSignFromPositionInBoard(ref Position i_Position)
        {
            if (IsInBounds(i_Position))
            {
                return m_GameBoard[i_Position.Row, i_Position.Column];
            }

            return 'z';
        }

        public char GetCell(int i_Row, int i_Column)
        {
            return GameBoard[i_Row, i_Column];
        }

        public char[,] GameBoard
        {
            get { return m_GameBoard; }
            set { m_GameBoard = value; }
        }

        public int Size
        {
            get { return r_Size; }
        }

        public int NumOfWhite
        {
            get { return m_NumOfX; }
            set { m_NumOfX = value; }
        }

        public int NumOfBlack
        {
            get { return m_NumOfO; }
            set { m_NumOfO = value; }
        }

        public bool IsInBounds(Position i_CheckPosition)
        {
            return i_CheckPosition.Row >= 0 && i_CheckPosition.Row < Size
                && i_CheckPosition.Column >= 0 && i_CheckPosition.Column < Size;
        }

        public bool IsCheckerEmpty(int i_Row, int i_Column)
        {
            return GameBoard[i_Row, i_Column] == (char)Board.eCheckersOption.space;
        }

        public void SetSignInPosition(Position i_Position, char i_CharToSet)
        {
            if (IsInBounds(i_Position))
            {
                m_GameBoard[i_Position.Row, i_Position.Column] = i_CharToSet;
            }
        }
    }
}
