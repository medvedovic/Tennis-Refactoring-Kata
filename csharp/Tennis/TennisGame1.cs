using Tennis.TennisGame1Files;
using Tennis.TennisGame1Files.Contracts;

namespace Tennis
{

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

