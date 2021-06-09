using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C20__Ex2
{
    public class Position
    {
        private int m_Row;
        private int m_Column;
        private Board.eCheckersOption m_InPosition;

        public static bool operator !=(Position position1, Position position2)
        {
            return !(position1 == position2);
        }

        public static bool operator ==(Position position1, Position position2)
        {
            return position1.Row == position2.Row && position1.Column == position2.Column;
        }

        public Position(int i_Column, int i_Row, Board.eCheckersOption i_InPosition)
        {
            m_Row = i_Row;
            m_Column = i_Column;
            m_InPosition = i_InPosition; // pawnOf / X / O kingX / kingO
        }

        public Position(int i_Column, int i_Row)
        {
            m_Row = i_Row;
            m_Column = i_Column;
            m_InPosition = Board.eCheckersOption.space; // pawnOf / X / O kingX / kingO
        }

        public static Position GetEnemyMiddlePosition(ref Position io_Startposition, ref Position io_EndPosition)
        {
            int rowPosition = (io_Startposition.Row + io_EndPosition.Row) / 2;
            int columnPosition = (io_Startposition.Column + io_EndPosition.Column) / 2;
            Position newPosition = new Position(columnPosition, rowPosition);
            return newPosition;
        }

        public void SetCharInPosition(char i_charToInsertInPosition)
        {
            if (i_charToInsertInPosition == (char)Board.eCheckersOption.X)
            {
                m_InPosition = Board.eCheckersOption.X;
            }
            else if (i_charToInsertInPosition == (char)Board.eCheckersOption.KingOfTopDownX)
            {
                m_InPosition = Board.eCheckersOption.KingOfTopDownX;
            }
            else if (i_charToInsertInPosition == (char)Board.eCheckersOption.O)
            {
                m_InPosition = Board.eCheckersOption.O;
            }
            else if (i_charToInsertInPosition == (char)Board.eCheckersOption.KingOfBottomUpO)
            {
                m_InPosition = Board.eCheckersOption.KingOfBottomUpO;
            }
        }

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public int Column
        {
            get { return m_Column; }
            set { m_Column = value; }
        }

        public Board.eCheckersOption InPosition
        {
            get { return m_InPosition; }
            set { m_InPosition = value; }
        }

        public override bool Equals(object position1)
        {
            return this.Column == ((Position)position1).Column &&
                this.Row == ((Position)position1).Row;
        }
    }
}
