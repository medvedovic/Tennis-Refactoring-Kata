using System;
using Tennis.TennisGame1Files.Contracts;

namespace Tennis.TennisGame1Files.States
{
    internal class AdvantageState : ITennisGameState
    {
        private readonly ITennisGameStateContext _context;

        public AdvantageState(ITennisGameStateContext context)
        {
            _context = context;
        }

        public string GetScore(CurrentScore score)
            => Math.Sign(score.GetScoreDifference()) > 0 ? "Advantage player1" : "Advantage player2";

        public void WonPoint(CurrentScore score)
        {
            if (score.IsSame())
            {
                _context.SetState(new DeuceState(_context));
            }
            else if (Math.Abs(score.GetScoreDifference()) >= 2)
            {
                _context.SetState(new WinState(_context));
            }
        }
    }
}

