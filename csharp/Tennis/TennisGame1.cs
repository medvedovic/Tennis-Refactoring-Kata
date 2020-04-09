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
            _state = new DeuceState();
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
            // no-op now
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
        {
            string score = "";
            var tempScore = 0;
            if (m_score1 == m_score2)
            {
                return _context.GetScore(new CurrentScore { playerOneScore = m_score1, playerTwoScore = m_score2 });
            }
            else if (m_score1 >= 4 || m_score2 >= 4)
            {
                var minusResult = m_score1 - m_score2;
                if (minusResult == 1) score = "Advantage player1";
                else if (minusResult == -1) score = "Advantage player2";
                else if (minusResult >= 2) score = "Win for player1";
                else score = "Win for player2";
            }
            else
            {
                for (var i = 1; i < 3; i++)
                {
                    if (i == 1) tempScore = m_score1;
                    else { score += "-"; tempScore = m_score2; }
                    switch (tempScore)
                    {
                        case 0:
                            score += "Love";
                            break;
                        case 1:
                            score += "Fifteen";
                            break;
                        case 2:
                            score += "Thirty";
                            break;
                        case 3:
                            score += "Forty";
                            break;
                    }
                }
            }
            return score;
        }
    }
}

