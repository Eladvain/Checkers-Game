using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C20__Ex2
{
    public class Player
    {
        private readonly ePlayers r_HumanOrComputerPlayer;
        private string m_PlayerName;
        private Board.eCheckersOption m_playerSign;
        private ePlayerType m_PlayerType;
        private int m_Score;
        private List<Pawn> m_PlayerPawns;

        public enum ePlayers
        {
            Human,
            Computer
        }

        public List<Pawn> PlayerPawns
        {
            get { return m_PlayerPawns; }
            set { m_PlayerPawns = value; }
        }

        public enum ePlayerType
        {
            TopDownPlayerX = 'X',
            BottomUpPlayerO = 'O',
            KingOfTopDownPlayerX = 'K',
            KingOfBottomUpO = 'U'
        }

        public Player(string i_Name, ePlayerType i_PlayerTypeDiraction)
        {
            m_PlayerName = i_Name;
            r_HumanOrComputerPlayer = ePlayers.Human;
            m_PlayerType = i_PlayerTypeDiraction;
            m_PlayerPawns = new List<Pawn>();
        }

        public Player()
        {
            m_PlayerName = "Computer";
            r_HumanOrComputerPlayer = ePlayers.Computer;
            m_PlayerType = ePlayerType.BottomUpPlayerO;
            m_PlayerPawns = new List<Pawn>();
        }

        public Board.eCheckersOption PlayerSign
        {
            get { return m_playerSign; }
            set { m_playerSign = value; }
        }

        public string PlayerName
        {
            get { return m_PlayerName; }
            set { m_PlayerName = value; }
        }

        public ePlayers HumanOrComputerPlayer
        {
            get { return r_HumanOrComputerPlayer; }
        }

        public Player.ePlayerType PlayerType
        {
            get { return m_PlayerType; }
            set { m_PlayerType = value; }
        }

        public int NumOfPawnsOFCurrPlayer
        {
            get { return m_PlayerPawns.Count(); }
        }

        public int NumScores
        {
            get { return m_Score; }
            set { m_Score = value; }
        }
    }
}
