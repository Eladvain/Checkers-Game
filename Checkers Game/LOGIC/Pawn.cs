using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C20__Ex2
{
    public class Pawn
    {
        private Position m_currentPostion;
        private List<Position> m_nextCapturesOptionalPositions;
        private List<Position> m_nextSigleMoveOptionalPostions;
        private bool m_canCapture;
        private bool m_CanMove;
        private char m_MySign;
        private bool m_IsKing;
        private eDirection m_PawnMoveDirection;

        public Pawn(Position i_position, char i_sign)
        {
            m_nextCapturesOptionalPositions = new List<Position>();
            m_nextSigleMoveOptionalPostions = new List<Position>();
            m_currentPostion = i_position;
            m_MySign = i_sign;
            m_IsKing = false;
            m_canCapture = false;
            m_CanMove = false;
            updateSidesForPawnFronSign(i_sign);
        }

        public char MySign
        {
            get { return m_MySign; }
            set { m_MySign = value; }
        }

        public enum eDirection
        {
            UP,
            DOWN,
            BOTH // for kings
        }

        public List<Position> NextCapturesOptionalPositions
        {
            get { return m_nextCapturesOptionalPositions; }
            set { m_nextCapturesOptionalPositions = value; }
        }

        public List<Position> NextSigleMoveOptionalPostions
        {
            get { return m_nextSigleMoveOptionalPostions; }
            set { m_nextSigleMoveOptionalPostions = value; }
        }

        public Position CurrentPosition
        {
            get { return m_currentPostion; }
            set { m_currentPostion = value; }
        }

        public bool CanCapure
        {
            get { return m_canCapture; }
            set { m_canCapture = value; }
        }

        public bool CanMove
        {
            get { return m_CanMove; }
            set { m_CanMove = value; }
        }

        public bool IsKing
        {
            get { return m_IsKing; }
            set { m_IsKing = value; }
        }

        public eDirection PawnDirection
        {
            get { return m_PawnMoveDirection; }
            set { m_PawnMoveDirection = value; }
        }

        private void updateSidesForPawnFronSign(char i_PwanSign)
        {
            if (i_PwanSign == (char)Board.eCheckersOption.X)
            {
                this.m_PawnMoveDirection = eDirection.DOWN;
            }
            else if (i_PwanSign == (char)Board.eCheckersOption.O)
            {
                this.m_PawnMoveDirection = eDirection.UP;
            }
            else
            {
                this.m_PawnMoveDirection = eDirection.BOTH;
            }
        }
    }
}
