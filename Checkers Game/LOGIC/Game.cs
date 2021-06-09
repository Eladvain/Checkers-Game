using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C20__Ex2
{
    public class Game
    {
        private readonly Player r_TopDownPlayer;
        private readonly Player r_BottomUpPlayer;

        private int m_SizeOfPositionArrays;
        private Board m_Board;
        private Board m_RestartBoard;

        private char m_CurrentSignPlayer;
        private eGameStatus m_CurrentStatus;
        private Player m_WhoPlayNow; // BottomUpPlayerO or TopDownPlayerX
        private bool m_MustCaptureAtNextTurn;
        private Position m_MustCaptureFromThisPosition;

        public enum eGameStatus
        {
            Running,
            Exit,
            Player1Won,
            Player2Won,
            Equality
        }

        public enum eGameModes
        {
            HumanVsHuman = 1,
            HumanVsComputer
        }

        public Game(Player i_Player1, Player i_Player2, int i_Size)
        {
            m_Board = new Board(i_Size);
            m_RestartBoard = new Board(i_Size);
            r_TopDownPlayer = i_Player1;
            TopDownPlayer.PlayerSign = Board.eCheckersOption.X;
            r_BottomUpPlayer = i_Player2;
            BottomUpPlayer.PlayerSign = Board.eCheckersOption.O;
            CurrentSignPlayer = (char)Board.eCheckersOption.X;
            m_WhoPlayNow = r_TopDownPlayer; // x stats      
            m_MustCaptureAtNextTurn = false;
            m_MustCaptureFromThisPosition = null;
        }

        public Board Board
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public Board RestartBoard
        {
            get { return m_RestartBoard; }
            set { m_RestartBoard = value; }
        }

        public int PositionArraysSize
        {
            get { return m_SizeOfPositionArrays; }
            set { m_SizeOfPositionArrays = value; }
        }

        public bool MustCaptureAtNextTurn
        {
            get { return m_MustCaptureAtNextTurn; }
            set { m_MustCaptureAtNextTurn = value; }
        }

        public Position CaptureFromThisPosition
        {
            get { return m_MustCaptureFromThisPosition; }
            set { m_MustCaptureFromThisPosition = value; }
        }

        public Player WhoPlayNow
        {
            get { return m_WhoPlayNow; }
            set { m_WhoPlayNow = value; }
        }

        public Player TopDownPlayer
        {
            get { return r_TopDownPlayer; }
        }

        public Player BottomUpPlayer
        {
            get { return r_BottomUpPlayer; }
        }

        public char CurrentSignPlayer
        {
            get { return m_CurrentSignPlayer; }
            set { m_CurrentSignPlayer = value; }
        }

        public eGameStatus CurrentStatus
        {
            get { return m_CurrentStatus; }
            set { m_CurrentStatus = value; }
        }

        public void CleanBoard()
        {
            int boradSize = this.Board.Size;
            for (int i = 0; i < boradSize; i++)
            {
                for (int j = 0; j < boradSize; j++)
                {
                    this.m_Board.GameBoard[i, j] = this.RestartBoard.GameBoard[i, j];
                }
            }
        }

        public void GetAllOptinalMovesOfOfAPlayer(Player io_CurrPlayer)
        {
            io_CurrPlayer.PlayerPawns.Clear(); 
            char[,] gameBoard = Board.GameBoard;
            int size = m_Board.Size;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (io_CurrPlayer.PlayerType == Player.ePlayerType.TopDownPlayerX)
                    {
                        if (gameBoard[i, j].Equals((char)Board.eCheckersOption.X)) 
                        {
                            io_CurrPlayer.PlayerPawns.Add(
                                new Pawn(
                                    new Position(j, i),
                                (char)Board.eCheckersOption.X));
                        }
                        else if (gameBoard[i, j].Equals((char)Board.eCheckersOption.KingOfTopDownX))
                        {
                            io_CurrPlayer.PlayerPawns.Add(
                                new Pawn(
                                    new Position(j, i), 
                                (char)Board.eCheckersOption.KingOfTopDownX));
                        }
                    }

                    if (io_CurrPlayer.PlayerType == Player.ePlayerType.BottomUpPlayerO)
                    {
                        if (gameBoard[i, j].Equals((char)Board.eCheckersOption.O)) 
                        {
                            io_CurrPlayer.PlayerPawns.Add(
                                new Pawn(
                                    new Position(j, i), 
                                (char)Board.eCheckersOption.O));
                        }
                        else if (gameBoard[i, j].Equals((char)Board.eCheckersOption.KingOfBottomUpO)) 
                        {
                            io_CurrPlayer.PlayerPawns.Add(
                                new Pawn(
                                    new Position(j, i),
                                (char)Board.eCheckersOption.KingOfBottomUpO));
                        }
                    }
                }
            }
        }

        public bool CheckIfMoveBack(ref Position io_StatPosision, ref Position io_EndPositin, ref Player i_PlayNow)
        {
            bool isValedMove = true;
            if(io_StatPosision.InPosition == Board.eCheckersOption.KingOfBottomUpO ||
                io_StatPosision.InPosition == Board.eCheckersOption.KingOfTopDownX)
            {
                isValedMove = true;
            }
            else if (i_PlayNow.PlayerType == Player.ePlayerType.TopDownPlayerX)
            {
                isValedMove = io_EndPositin.Row - io_StatPosision.Row > 0;
            }
            else if (i_PlayNow.PlayerType == Player.ePlayerType.BottomUpPlayerO)
            {
                isValedMove = io_StatPosision.Row - io_EndPositin.Row > 0;
            }

            return isValedMove;
        }

        public void ExecuteMove(
            ref Position io_StartPosition, 
            ref Position io_MiddlePosition, 
            ref Position io_EndPosition,
            bool i_LegalCapture)
        {
            jumpToNewPosition(ref io_StartPosition, ref io_EndPosition); 
            if (i_LegalCapture)
            {
                killEnemyPawnFromHisList(ref io_MiddlePosition); 
            }
        }

        private void killEnemyPawnFromHisList(ref Position i_EnemyPosition)
        {
            Player enemyToKill = GetEnemyPlayer();
            List<Pawn> enemyPawns = enemyToKill.PlayerPawns;

            foreach (Pawn currPawn in enemyPawns)
            {
                if (currPawn.CurrentPosition == i_EnemyPosition)
                {
                    m_Board.SetSignInPosition(i_EnemyPosition, (char)Board.eCheckersOption.space);
                    break;
                }
            }
        }

        public void jumpToNewPosition(ref Position io_StartPosition, ref Position io_EndPosition)
        {
            if (Board.IsInBounds(io_StartPosition))
            {
                char playerSign = m_Board.GetSignFromPositionInBoard(ref io_StartPosition);
                m_Board.SetSignInPosition(io_StartPosition, (char)Board.eCheckersOption.space);
                m_Board.SetSignInPosition(io_EndPosition, getSignIfKing(io_EndPosition, playerSign));
            }
        }

        private char getSignIfKing(Position i_NextPosition, char i_PlayerSign)
        {
            char charOFKing = i_PlayerSign;

            if (i_PlayerSign == (char)Board.eCheckersOption.O)
            {
                if (i_NextPosition.Row == 0)
                {
                    charOFKing = (char)Board.eCheckersOption.KingOfBottomUpO;
                    i_NextPosition.InPosition = Board.eCheckersOption.KingOfBottomUpO;
                }
            }
            else if (i_PlayerSign == (char)Board.eCheckersOption.X)
            {
                if (i_NextPosition.Row == Board.Size - 1)
                {
                    charOFKing = (char)Board.eCheckersOption.KingOfTopDownX;
                    i_NextPosition.InPosition = Board.eCheckersOption.KingOfTopDownX;
                }
            }

            return charOFKing;
        }

        public Player GetEnemyPlayer()
        {
            Player tempPlayer = null;
            if (m_WhoPlayNow.PlayerSign == Board.eCheckersOption.X ||
                m_WhoPlayNow.PlayerSign == Board.eCheckersOption.KingOfTopDownX)
            {
                tempPlayer = r_BottomUpPlayer;
            }
            else if (m_WhoPlayNow.PlayerSign == Board.eCheckersOption.O ||
                m_WhoPlayNow.PlayerSign == Board.eCheckersOption.KingOfBottomUpO)
            {
                tempPlayer = r_TopDownPlayer;
            }

            return tempPlayer;
        }

        public void CheckIfCanMoveAndIfCanCapture(
            ref Position io_StarPosition,
            ref Position io_EndOfosition,
            Player.ePlayerType i_CurrPlyerType,
            out bool o_MoveNoCapture,
            out bool o_MoveWithCapture)
        {
            int startRowPos = io_StarPosition.Row;
            int startColumnPos = io_StarPosition.Column;

            int endRowPos = io_EndOfosition.Row;
            int endColumn = io_EndOfosition.Column;

            o_MoveNoCapture = (Math.Abs(startRowPos - endRowPos) == 1) && (Math.Abs(startColumnPos - endColumn) == 1);
            o_MoveWithCapture = (Math.Abs(startRowPos - endRowPos) == 2) && (Math.Abs(startColumnPos - endColumn) == 2);
        }

        public Pawn getPlayerPawnFromHisPwansList(Position i_CurrPosition, ref bool io_FoundThePawn)
        {
            io_FoundThePawn = false;
            List<Pawn> pawnsPlayer = this.m_WhoPlayNow.PlayerPawns;
            Pawn wantedPwan = null;
            foreach (Pawn currPwan in pawnsPlayer)
            {
                if (currPwan.CurrentPosition == i_CurrPosition)
                {
                    wantedPwan = currPwan;
                    io_FoundThePawn = true;
                    break;
                }
            }

            return wantedPwan;
        }

        public bool AllMovesOptionForAnPwan(Position i_CurrPawnPosition)
        {
            bool foundPawn = false;
            Pawn currPwan = getPlayerPawnFromHisPwansList(i_CurrPawnPosition, ref foundPawn);
            if (foundPawn)
            {
                Position enemyX1 = new Position(
                    currPwan.CurrentPosition.Column + 1,
                    currPwan.CurrentPosition.Row + 1); // (+1 ,+1) 
                enemyX1.SetCharInPosition(Board.GetSignFromPositionInBoard(ref enemyX1));
                bool optionEnemyX1 = false;

                Position enemyX2 = new Position(
                    currPwan.CurrentPosition.Column - 1,
                    currPwan.CurrentPosition.Row + 1); // (-1 ,+1)
                enemyX2.SetCharInPosition(Board.GetSignFromPositionInBoard(ref enemyX2));
                bool optionEnemyX2 = false;

                Position enemyO1 = new Position(
                    currPwan.CurrentPosition.Column - 1,
                    currPwan.CurrentPosition.Row - 1); // (-1 ,-1)
                enemyO1.SetCharInPosition(Board.GetSignFromPositionInBoard(ref enemyO1));
                bool optionEnemyO1 = false;

                Position enemyO2 = new Position(
                    currPwan.CurrentPosition.Column + 1,
                    currPwan.CurrentPosition.Row - 1); // ( +1 ,-1)
                enemyO2.SetCharInPosition(Board.GetSignFromPositionInBoard(ref enemyO2));
                bool optionEnemyO2 = false;

                if (currPwan.PawnDirection == Pawn.eDirection.DOWN)
                {
                    optionEnemyX1 = (isCharOfOorX(enemyX1) == Player.ePlayerType.BottomUpPlayerO); 
                    optionEnemyX2 = (isCharOfOorX(enemyX2) == Player.ePlayerType.BottomUpPlayerO);
                    AddSignleMoveList(ref currPwan, enemyX1, enemyX2);
                    calcDeltaAndCheckIfEmptyEndOfCapturePosition(
                        currPwan,
                        i_CurrPawnPosition, 
                        enemyX1, 
                        enemyX2,
                        optionEnemyX1, 
                        optionEnemyX2);
                }
                else if (currPwan.PawnDirection == Pawn.eDirection.UP)
                {
                    optionEnemyO1 = (isCharOfOorX(enemyO1) == Player.ePlayerType.TopDownPlayerX);
                    optionEnemyO2 = (isCharOfOorX(enemyO2) == Player.ePlayerType.TopDownPlayerX);
                    AddSignleMoveList(ref currPwan, enemyO1, enemyO2);
                    calcDeltaAndCheckIfEmptyEndOfCapturePosition(
                        currPwan,
                        i_CurrPawnPosition,
                        enemyO1,
                        enemyO2,
                        optionEnemyO1,
                        optionEnemyO2);
                }
                else
                {
                    // kings of X and O 
                    AddSignleMoveList(ref currPwan, enemyX1, enemyX2);
                    AddSignleMoveList(ref currPwan, enemyO1, enemyO2);
                    calcDeltaAndCheckIfEmptyEndOfCapturePosition(
                        currPwan, 
                        i_CurrPawnPosition, 
                        enemyX1,
                        enemyX2,
                        optionEnemyX1, 
                        optionEnemyX2);
                    calcDeltaAndCheckIfEmptyEndOfCapturePosition(
                        currPwan, 
                        i_CurrPawnPosition,
                        enemyO1, 
                        enemyO2,
                        optionEnemyO1, 
                        optionEnemyO2);
                }

                foundPawn = currPwan.CanCapure;
            }

            return foundPawn;
        }

        public void AddSignleMoveList(ref Pawn io_CurrPawn, Position i_OptionalMove1, Position i_OptionalMove2)
        {
            if (i_OptionalMove1.InPosition == Board.eCheckersOption.space && Board.IsInBounds(i_OptionalMove1))
            {
                io_CurrPawn.NextSigleMoveOptionalPostions.Add(i_OptionalMove1);
            }

            if (i_OptionalMove2.InPosition == Board.eCheckersOption.space && Board.IsInBounds(i_OptionalMove2))
            {
                io_CurrPawn.NextSigleMoveOptionalPostions.Add(i_OptionalMove2);
            }
        }

        private void calcDeltaAndCheckIfEmptyEndOfCapturePosition(
            Pawn o_CurrPwan,
            Position i_CurrPosition,
            Position i_EnemyX1,
            Position i_EnemyX2,
            bool optionEnemyX1,
            bool optionEnemyX2)
        {
            if (optionEnemyX1 && Board.IsInBounds(i_EnemyX1))
            {
                calcDeltaAndUpdateListOfPawn(o_CurrPwan, i_CurrPosition, i_EnemyX1);
            }

            if (optionEnemyX2 && Board.IsInBounds(i_EnemyX2))
            {
                calcDeltaAndUpdateListOfPawn(o_CurrPwan, i_CurrPosition, i_EnemyX2);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="i_CurrPawn"></param>
        /// <param name="i_CurrPosition"></param>
        /// <param name="i_Enemy"></param>
        /// 
        private void calcDeltaAndUpdateListOfPawn(Pawn i_CurrPawn, Position i_CurrPosition, Position i_Enemy)
        {
            Position delta = new Position(i_Enemy.Column - i_CurrPosition.Column, i_Enemy.Row - i_CurrPosition.Row);

            Position possibleMoveAfterCapture = new Position(
                i_CurrPosition.Column + (delta.Column * 2),
                i_CurrPosition.Row + (delta.Row * 2));
            if (Board.IsInBounds(possibleMoveAfterCapture))
            {
                if (this.m_Board.IsCheckerEmpty(possibleMoveAfterCapture.Row, possibleMoveAfterCapture.Column))
                {
                    i_CurrPawn.NextCapturesOptionalPositions.Add(possibleMoveAfterCapture);
                    i_CurrPawn.CanCapure = true;
                    this.m_MustCaptureFromThisPosition = i_CurrPawn.CurrentPosition;
                    return;
                }
            }

            i_CurrPawn.CanCapure = false;
        }

        public bool PlayerCanCaptureAgain(Position i_StatPositionForNextTurn)
        {
            bool canCaptureAgain = false;
            List<Pawn> playerPwans = this.m_WhoPlayNow.PlayerPawns;
            foreach (Pawn currPawn in playerPwans)
            {
                if (currPawn.CurrentPosition == i_StatPositionForNextTurn)
                {
                    if (currPawn.CanCapure)
                    {
                        canCaptureAgain = true;
                        break;
                    }
                }
            }

            return canCaptureAgain;
        }

        private Player.ePlayerType isCharOfOorX(Position i_CurrPosition)
        {
            Player.ePlayerType playerType = 0;

            if (m_Board.GetSignFromPositionInBoard(ref i_CurrPosition) == (char)Board.eCheckersOption.O ||
                      m_Board.GetSignFromPositionInBoard(ref i_CurrPosition) == (char)Board.eCheckersOption.KingOfBottomUpO)
            {
                playerType = Player.ePlayerType.BottomUpPlayerO;
            }
            else if (m_Board.GetSignFromPositionInBoard(ref i_CurrPosition) == (char)Board.eCheckersOption.X ||
                     m_Board.GetSignFromPositionInBoard(ref i_CurrPosition) == (char)Board.eCheckersOption.KingOfTopDownX)
            {
                playerType = Player.ePlayerType.TopDownPlayerX;
            }

            return playerType;
        }

        public void SwitchPlayer()
        {
            if (WhoPlayNow.PlayerType == Player.ePlayerType.TopDownPlayerX)
            {
                this.WhoPlayNow = this.r_BottomUpPlayer;
            }
            else
            {
                this.WhoPlayNow = this.r_TopDownPlayer;
            }
        }

        public bool CheckIfCaptureAgainIsLegal(Position i_StartPosition, Position i_EndPosition, Player i_CurrPlayer)
        {
            bool isStartPosition = false;
            bool endposition = false;
            List<Pawn> playerPwans = i_CurrPlayer.PlayerPawns;

            isStartPosition = i_StartPosition == this.m_MustCaptureFromThisPosition;
            if (isStartPosition)
            {
                foreach (Pawn currPawn in playerPwans)
                {
                    if (currPawn.CurrentPosition == i_StartPosition)  
                    {
                        List<Position> allMovesOfCurrPawn = currPawn.NextCapturesOptionalPositions;
                        foreach (Position currPawnsEndPosition in allMovesOfCurrPawn)
                        {
                            if (currPawnsEndPosition == i_EndPosition) 
                            {
                                endposition = true;
                            }
                        }
                    }
                }
            }

            return isStartPosition && endposition;
        }

        public void UpdateMoveForEachPawnOfPlayer()
        {
            List<Pawn> listOfPwans = this.m_WhoPlayNow.PlayerPawns;
            foreach (Pawn currPawn in listOfPwans)
            {
                AllMovesOptionForAnPwan(currPawn.CurrentPosition);
            }
        }
        public bool CheckIfEndPositionIsRightAtCapture(Player i_CurrPlayer,  Position i_EndPosition)
        {
            bool isTheEndPosition = false;
            if (CheckIfPlayerMustCaptureThisTurn())
            {
                List<Pawn> playerPawns = i_CurrPlayer.PlayerPawns;
                foreach(Pawn currPawn in playerPawns)
                {
                    List<Position> posibilePositionAfterCapture = currPawn.NextCapturesOptionalPositions;
                    foreach(Position currPosition in posibilePositionAfterCapture)
                    {
                        if(currPosition == i_EndPosition)
                        {
                            isTheEndPosition = true;
                            break;
                        }
                    }
                }
            }

            return isTheEndPosition;
        }


        public  bool CheckIfPlayerMustCaptureThisTurn()
        {
            bool mustCaptureThisTurn = false;
            List<Pawn> playerPawns = this.WhoPlayNow.PlayerPawns;
            foreach (Pawn currPawn in playerPawns)
            {
                if (currPawn.NextCapturesOptionalPositions.Count() > 0)
                {
                    mustCaptureThisTurn = true;
                    break;
                }
            }
            return mustCaptureThisTurn;
        }

        public bool CheckIfMustCaptureStartPosition(ref Player io_CurrPlayer,ref Position io_StartPosition)
        {
            bool mustCaptureThisTurn = false;

            if (CheckIfPlayerMustCaptureThisTurn())
            {
                List<Pawn> playerPawns = this.WhoPlayNow.PlayerPawns;
                foreach (Pawn currPawn in playerPawns)
                {
                    if (currPawn.CurrentPosition == io_StartPosition)
                    {
                        if (currPawn.NextCapturesOptionalPositions.Count() > 0) 
                        {
                            mustCaptureThisTurn = true;
                            break;
                        }
                    }

                }
            }
           
            return mustCaptureThisTurn;
        }
    }
}
