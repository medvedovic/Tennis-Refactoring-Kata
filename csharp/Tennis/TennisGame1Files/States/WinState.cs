using System;
using Tennis.TennisGame1Files.Contracts;

namespace Tennis.TennisGame1Files.States
{
    internal class WinState : TennisGameStateBase, ITennisGameState
    {
        public WinState(ITennisGameStateContext context) : base(context) { }

        public string GetScore(CurrentScore score)
            => Math.Sign(score.GetScoreDifference()) > 0 ? "Win for player1" : "Win for player2";

        public void WonPoint(CurrentScore score)
        {
            // no-op game is already won
        }
    }
}

