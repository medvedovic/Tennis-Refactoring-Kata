using Tennis.TennisGame1Files.Contracts;

namespace Tennis.TennisGame1Files.States
{
    internal class DeuceState : TennisGameStateBase, ITennisGameState
    {
        public DeuceState(ITennisGameStateContext context): base(context) { }

        public string GetScore(CurrentScore score)
        {
            // no need to check both values here
            switch (score.playerOneScore)
            {
                case 0:
                    return "Love-All";
                case 1:
                    return "Fifteen-All";
                case 2:
                    return "Thirty-All";
                default:
                    return "Deuce";
            }
        }

        public void WonPoint(CurrentScore score)
        {
            if (score.playerOneScore >= 4 || score.playerTwoScore >= 4)
            {
                _context.SetState(new AdvantageState(_context));
            }
            else if (!score.IsSame())
            {
                _context.SetState(new PlayerLeadsState(_context));
            }
        }
    }
}

