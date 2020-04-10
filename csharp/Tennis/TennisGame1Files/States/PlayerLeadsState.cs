using System;
using Tennis.TennisGame1Files.Contracts;

namespace Tennis.TennisGame1Files.States
{
    internal class PlayerLeadsState : ITennisGameState
    {
        private readonly ITennisGameStateContext _context;

        public PlayerLeadsState(ITennisGameStateContext context)
        {
            _context = context;
        }

        public string GetScore(CurrentScore score)
            => $"{ConvertScoreToStringRepresentation(score.playerOneScore)}-{ConvertScoreToStringRepresentation(score.playerTwoScore)}";

        public void WonPoint(CurrentScore score)
        {
            if (score.IsSame())
            {
                _context.SetState(new DeuceState(_context));
            }
            else if (score.playerOneScore >= 4 || score.playerTwoScore >= 4)
            {
                if (Math.Abs(score.GetScoreDifference()) >= 2)
                    _context.SetState(new WinState(_context));
                else
                    _context.SetState(new AdvantageState(_context));
            }
        }

        private string ConvertScoreToStringRepresentation(int score)
        {
            switch (score)
            {
                case 0:
                    return "Love";
                case 1:
                    return "Fifteen";
                case 2:
                    return "Thirty";
                case 3:
                    return "Forty";
                default:
                    throw new ArgumentOutOfRangeException(nameof(score), "Encountered value is not valid for conversion");
            }
        }
    }
}

