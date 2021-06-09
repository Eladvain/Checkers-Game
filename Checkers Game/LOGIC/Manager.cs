using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C20__Ex2
{
    public class Manager 
    {
        private Game.eGameModes m_GameMode;
        private Game m_TheGame;
        private AfterMove m_AfterMoveOfComputer;

        public Game.eGameModes GameMode
        {
            get { return m_GameMode; }
            set { m_GameMode = value; }
        }

        public Game TheGame
        {
            get { return m_TheGame; }
            set { m_TheGame = value; }
        }

        public AfterMove AfterMovement
        {
            set { m_AfterMoveOfComputer = value; }
        }


        public void NewGame()
        {
            PlayGame();
        }

        public Manager(string i_FirstPlayerName, string i_SecondePlayerName, int i_Size, bool i_AgainstHuman)
        {
            Player player1, player2;
            player1 = new Player(i_FirstPlayerName, Player.ePlayerType.TopDownPlayerX);
            if (i_AgainstHuman)
            {
                this.m_GameMode = Game.eGameModes.HumanVsHuman;
            }
            else
            {
                this.m_GameMode = Game.eGameModes.HumanVsComputer;
            }
            player2 = new Player(i_SecondePlayerName, Player.ePlayerType.BottomUpPlayerO);
            int size = i_Size;
            this.m_TheGame = new Game(player1, player2, size);
        }

        public void PlayGame()
        {
            TheGame.CleanBoard();
            TheGame.CurrentStatus = Game.eGameStatus.Running;
            TheGame.WhoPlayNow = TheGame.TopDownPlayer; // X play first
            TheGame.GetAllOptinalMovesOfOfAPlayer(TheGame.TopDownPlayer);
            TheGame.GetAllOptinalMovesOfOfAPlayer(TheGame.BottomUpPlayer);
        }

        public void ComputerPlay(ref Player io_ComputerPlayerNow)
        {
            bool canCApture = false;
            bool camSinglMove = false;
            bool captureOrNOt = false;
            Random rndom = new Random();
            int randomInt;
            Position middilePos = null;
            Position star = null, end = null;

            List<Pawn> computerMoves = TheGame.WhoPlayNow.PlayerPawns;
            foreach (Pawn currMovment in computerMoves)
            {
                star = currMovment.CurrentPosition;
                if (currMovment.CanCapure)
                {
                    List<Position> pawnMoves = currMovment.NextCapturesOptionalPositions;
                    randomInt = rndom.Next(pawnMoves.Count() - 1);
                    end = pawnMoves[randomInt];
                    canCApture = true;
                    middilePos = Position.GetEnemyMiddlePosition(ref star, ref end);
                    captureOrNOt = true;
                    m_TheGame.ExecuteMove(ref star, ref middilePos, ref end, captureOrNOt);
                    break;
                }
            }

            if (!canCApture)
            {
                List<Pawn> computerMoves2 = TheGame.WhoPlayNow.PlayerPawns;
                foreach (Pawn currMovment2 in computerMoves2)
                {
                    star = currMovment2.CurrentPosition;
                    List<Position> singlMovesPawns2 = currMovment2.NextSigleMoveOptionalPostions;
                    if(singlMovesPawns2.Count() > 0)
                    {
                        randomInt = rndom.Next(singlMovesPawns2.Count() - 1);
                        end = singlMovesPawns2[randomInt];
                        camSinglMove = true;
                        captureOrNOt = false;
                        m_TheGame.ExecuteMove(ref star, ref middilePos, ref end, captureOrNOt);;
                        break;
                    }
                }
            }
        }

        public bool CheckIfTheGameIsOver(Player i_PlayerNow)
        {
            bool isTheGameOver = false;
            m_TheGame.SwitchPlayer();

            if (m_TheGame.WhoPlayNow.PlayerPawns.Count() == 0)
            {
                m_TheGame.SwitchPlayer();
                isTheGameOver = true;
                UpdatesScoresAndAskIfContinue(m_TheGame.WhoPlayNow);
            }
            else
            {
                m_TheGame.SwitchPlayer();
            }

            return isTheGameOver;
        }

        public void EachTurn(Player i_Player, Position i_StatPosision, Position i_endPositin, out string o_Error)
        {
            Position enemyMiddlePosition = null;
            bool o_NomoveBack = false;
            bool moveNoCapture;
            bool moveWithCapure;
            bool legalCapture = false;
            bool capterExecuted = false;
            o_Error = null;

            TheGame.UpdateMoveForEachPawnOfPlayer();

            if (this.GameMode == Game.eGameModes.HumanVsComputer &&
             this.TheGame.WhoPlayNow == this.TheGame.BottomUpPlayer)
            {
                ComputerPlay(ref i_Player);
                TheGame.GetAllOptinalMovesOfOfAPlayer(i_Player);
                if (CheckIfTheGameIsOver(this.TheGame.WhoPlayNow))
                {
                    m_AfterMoveOfComputer.EndGame(this.TheGame.WhoPlayNow);
                }
                    this.m_TheGame.SwitchPlayer();
                    TheGame.UpdateMoveForEachPawnOfPlayer();
            }
            else
             {
                   secondHumanPlayer(ref i_Player,
                   out enemyMiddlePosition,
                   ref i_StatPosision,
                   ref i_endPositin,
                   out moveNoCapture,
                   out moveWithCapure,
                   out legalCapture,
                   out capterExecuted,
                   out o_NomoveBack,
                   out o_Error);

                    if (o_NomoveBack && (moveNoCapture || moveWithCapure))
                    {
                        if (!legalCapture && moveWithCapure)
                        {
                            o_Error = "pawn can't go back or invalid move, try again";
                        }
                        else if (!capterExecuted && !m_TheGame.MustCaptureAtNextTurn ||
                                (capterExecuted && !m_TheGame.MustCaptureAtNextTurn))
                        {
                          if (!this.CheckIfTheGameIsOver(i_Player))
                           {
                                m_TheGame.SwitchPlayer();
                                TheGame.UpdateMoveForEachPawnOfPlayer();

                           }

                        }
                    }
             }
         
        } //end of each turn

        private void secondHumanPlayer(ref Player io_Player,
               out Position o_enemyMiddlePosition,
               ref Position io_statPosision,
               ref Position io_endPositin,
               out bool o_moveNoCapture,
               out bool o_moveWithCapure,
               out bool o_legalCapture,
               out bool o_capterExecuted,
               out bool o_NomoveBack,
               out string o_Error)
        {
            
            checkAllValidationsUntilExecution(
                ref io_Player,
                out o_enemyMiddlePosition,
                ref io_statPosision,
                ref io_endPositin,
                out o_moveNoCapture,
                out o_moveWithCapure,
                out o_legalCapture,
                out o_capterExecuted,
                out o_NomoveBack,
                out o_Error);
            if(!o_legalCapture  && o_moveWithCapure)
            {
                o_Error = "pawn can't go back or invalid move, try again";
            }
            else if((o_moveNoCapture || o_moveWithCapure) && o_NomoveBack)
            {
                m_TheGame.ExecuteMove(ref io_statPosision, ref o_enemyMiddlePosition, ref io_endPositin, o_legalCapture);
            }

            TheGame.GetAllOptinalMovesOfOfAPlayer(TheGame.TopDownPlayer);
            TheGame.GetAllOptinalMovesOfOfAPlayer(TheGame.BottomUpPlayer);

            if (o_capterExecuted)
            {
                m_TheGame.UpdateMoveForEachPawnOfPlayer();
                if (TheGame.PlayerCanCaptureAgain(io_endPositin))
                {
                    m_TheGame.MustCaptureAtNextTurn = true;
                    m_TheGame.CaptureFromThisPosition = io_endPositin;
                }
                else
                {
                    m_TheGame.MustCaptureAtNextTurn = false;
                }
            }

        } // end of secondHumanPlayer

        private void checkAllValidationsUntilExecution(
            ref Player io_Player,
            out Position io_MiddlePosition,
            ref Position io_StatPosision,
            ref Position i_EndPositin,
            out bool o_MoveNoCapture,
               out bool o_MoveWithCapure,
               out bool o_LegalCapture,
               out bool o_CaptureExecuted,
               out bool o_NomoveBack,
               out string o_Error)
        {
            o_Error = null;
            io_MiddlePosition = null;
            o_MoveWithCapure = false;
            o_LegalCapture = false;
            o_CaptureExecuted = false;
            o_MoveNoCapture = false;

            o_NomoveBack = TheGame.CheckIfMoveBack(ref io_StatPosision, ref i_EndPositin, ref io_Player);
            m_TheGame.CheckIfCanMoveAndIfCanCapture(ref io_StatPosision, ref i_EndPositin, io_Player.PlayerType, out o_MoveNoCapture, out o_MoveWithCapure);
            if (!o_NomoveBack || (!o_MoveNoCapture && !o_MoveWithCapure))
            {
                o_Error = "pawn can't go back or invalid move, try again";
            }

            if (m_TheGame.MustCaptureAtNextTurn)
            {
                if (io_StatPosision != m_TheGame.CaptureFromThisPosition ||
                    !goodEndPositionChainCapture(i_EndPositin))
                {
                    o_Error = "you must move with the same pawn";
                }
            }

            if (o_MoveWithCapure)
            {
                io_MiddlePosition = Position.GetEnemyMiddlePosition(ref io_StatPosision, ref i_EndPositin);
                o_LegalCapture = CheckIfCaptureIsLegal(
                    ref io_StatPosision,
                    ref i_EndPositin,
                    ref io_Player,
                    ref io_MiddlePosition);

                if (o_LegalCapture)
                {
                    o_CaptureExecuted = true;
                }
                else
                {
                    o_Error = "pawn can't go back or invalid move, try again";
                }
            }
            else
            {
                o_CaptureExecuted = false;
            }

        }

        private bool goodEndPositionChainCapture(Position i_EndPosition)
        {
            bool goodEndPosition = false;
            List<Pawn> playerPawns = m_TheGame.WhoPlayNow.PlayerPawns;
            foreach (Pawn currPawn in playerPawns)
            {
                List<Position> pawnNextMoves = currPawn.NextCapturesOptionalPositions;
                foreach (Position currNextPosition in pawnNextMoves)
                {
                    if (currNextPosition == i_EndPosition)
                    {
                        goodEndPosition = true;
                        break;
                    }
                }
            }

            return goodEndPosition;
        }

        public bool CheckIfCaptureIsLegal(
            ref Position io_StartPosition,
            ref Position io_EndPosition,
            ref Player io_Player,
            ref Position io_MiddleEmnemyPos)
        {
            bool empyEndPosition = false;
            bool captureAnEnemy = false;

            empyEndPosition = m_TheGame.Board.IsCheckerEmpty(io_EndPosition.Row, io_EndPosition.Column);
            char getMiddleEnemyPositionSign = m_TheGame.Board.GetSignFromPositionInBoard(ref io_MiddleEmnemyPos);
            captureAnEnemy = isThereAnEnemtyPawn(ref io_Player, getMiddleEnemyPositionSign);

            return captureAnEnemy && empyEndPosition;
        }

        private bool isThereAnEnemtyPawn(ref Player io_PlayeNow, char i_MiddlePosEnemySign)
        {
            bool isEnemy = false;

            if (io_PlayeNow.PlayerType == Player.ePlayerType.TopDownPlayerX || io_PlayeNow.PlayerType == Player.ePlayerType.KingOfTopDownPlayerX)
            {
                isEnemy = i_MiddlePosEnemySign == (char)Board.eCheckersOption.O ||
                    i_MiddlePosEnemySign == (char)Board.eCheckersOption.KingOfBottomUpO;
            }
            else if (io_PlayeNow.PlayerType == Player.ePlayerType.BottomUpPlayerO || io_PlayeNow.PlayerType == Player.ePlayerType.KingOfBottomUpO)
            {
                isEnemy = i_MiddlePosEnemySign == (char)Board.eCheckersOption.X ||
                      i_MiddlePosEnemySign == (char)Board.eCheckersOption.KingOfTopDownX;
            }

            return isEnemy;
        }
 
        public void UpdatesScoresAndAskIfContinue(Player i_WinnerPlayer)
        { 
            int score = i_WinnerPlayer.NumScores;

            List<Pawn> pwansPlayer = i_WinnerPlayer.PlayerPawns;
            foreach (Pawn currPawnPlayer in pwansPlayer)
            {
                if (currPawnPlayer.IsKing)
                {
                    score += 4;
                }
                else
                {
                    score++;
                }
            }
            i_WinnerPlayer.NumScores += score;
       
        }
    }
}





