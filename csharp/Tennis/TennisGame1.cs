using System;

namespace Tennis
{
    struct CurrentScore
    {
        public int playerOneScore;
        public int playerTwoScore;
    }
    internal interface ITennisGameStateContext
    {
        ITennisGameStateContext SetState(ITennisGameState newState);
        void WonPoint(CurrentScore score);
        string GetScore(CurrentScore score);
    }

    internal class TennisGameStateContext : ITennisGameStateContext
    {
        private ITennisGameState _state;

        public TennisGameStateContext()
        {
            _state = new DeuceState(this);
        }

        public string GetScore(CurrentScore score) => _state.GetScore(score);

        public ITennisGameStateContext SetState(ITennisGameState newState)
        {
            _state = newState;
            return this;
        }
        public void WonPoint(CurrentScore score)
        {
            _state.WonPoint(score);
        }
    }

    interface ITennisGameState
    {
        void WonPoint(CurrentScore score);
        string GetScore(CurrentScore score);
    }

    internal class DeuceState : ITennisGameState
    {
        private readonly ITennisGameStateContext _context;

        public DeuceState(ITennisGameStateContext context)
        {
            _context = context;
        }

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
            else if (score.playerOneScore != score.playerTwoScore)
            {
                _context.SetState(new PlayerLeadsState(_context));
            }
        }
    }

    internal class WinState : ITennisGameState
    {
        private readonly ITennisGameStateContext _context;

        public WinState(ITennisGameStateContext context)
        {
            _context = context;
        }

        public string GetScore(CurrentScore score)
        {
            if (Math.Sign(score.playerOneScore - score.playerTwoScore) > 0)
                return "Win for player1";
            else
                return "Win for player2";
        }

        public void WonPoint(CurrentScore score)
        {
            // no-op game is already won
        }
    }

    internal class AdvantageState : ITennisGameState
    {
        private readonly ITennisGameStateContext _context;

        public AdvantageState(ITennisGameStateContext context)
        {
            _context = context;
        }

        public string GetScore(CurrentScore score)
        {
            var currentScoreDiff = score.playerOneScore - score.playerTwoScore;
            if (currentScoreDiff == 1)
                return "Advantage player1";
            else
                return "Advantage player2";
        }

        public void WonPoint(CurrentScore score)
        {
            if (score.playerOneScore == score.playerTwoScore)
            {
                _context.SetState(new DeuceState(_context));
            }
            if (Math.Abs(score.playerOneScore - score.playerTwoScore) >= 2)
            {
                _context.SetState(new WinState(_context));
            }
        }
    }

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
            if (score.playerOneScore == score.playerTwoScore)
            {
                _context.SetState(new DeuceState(_context));
            }
            else if (score.playerOneScore >= 4 || score.playerTwoScore >= 4)
            {
                if (Math.Abs(score.playerOneScore - score.playerTwoScore) >= 2)
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

    class TennisGame1 : ITennisGame
    {
        private int m_score1 = 0;
        private int m_score2 = 0;
        private string player1Name;
        private string player2Name;
        private readonly ITennisGameStateContext _context;

        public TennisGame1(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
            _context = new TennisGameStateContext();
        }

        public void WonPoint(string playerName)
        {
            if (playerName == "player1")
                m_score1 += 1;
            else
                m_score2 += 1;
            // mind the order
            _context.WonPoint(new CurrentScore { playerOneScore = m_score1, playerTwoScore = m_score2 });
        }

        public string GetScore()
            => _context.GetScore(new CurrentScore { playerOneScore = m_score1, playerTwoScore = m_score2 });
    }
}

