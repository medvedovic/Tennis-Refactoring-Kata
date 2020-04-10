using System;

namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
        private int p1point;
        private int p2point;

        private string player1Name;
        private string player2Name;

        public TennisGame2(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            p1point = 0;
            this.player2Name = player2Name;
        }

        public string GetScore()
        {
            if (HasOnePlayerReachedAtLeastFourPoints() && IsScoreDifferenceAtLeastTwoPoints())
                return Math.Sign(p1point - p2point) > 0 ? "Win for player1" : "Win for player2";

            if (HasOnePlayerAdvantage())
                return Math.Sign(p1point - p2point) > 0 ? "Advantage player1" : "Advantage player2";

            if (IsScoreSame())
                return GetStringRepresentationForSameScore(p1point);

            return GetScoreForLeadingPlayer();
        }

        private bool IsScoreSame()
            => p1point == p2point;

        private string GetScoreForLeadingPlayer()
            => $"{ConvertScoreToString(p1point)}-{ConvertScoreToString(p2point)}";

        private bool HasOnePlayerAdvantage()
            => (p1point > p2point && p2point >= 3) || (p2point > p1point && p1point >= 3);

        private bool HasOnePlayerReachedAtLeastFourPoints()
            => p1point >= 4 || p2point >= 4;

        private bool IsScoreDifferenceAtLeastTwoPoints()
            => Math.Abs(p2point - p1point) >= 2;

        private string GetStringRepresentationForSameScore(int currentScore)
            => currentScore > 2 ? "Deuce" : $"{ConvertScoreToString(currentScore)}-All";

        private string ConvertScoreToString(int score)
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
                    throw new ArgumentOutOfRangeException(nameof(score), "Encountered invalid value for conversion");
            }
        }

        public void SetP1Score(int number)
        {
            for (int i = 0; i < number; i++)
            {
                P1Score();
            }
        }

        public void SetP2Score(int number)
        {
            for (var i = 0; i < number; i++)
            {
                P2Score();
            }
        }

        private void P1Score()
        {
            p1point++;
        }

        private void P2Score()
        {
            p2point++;
        }

        public void WonPoint(string player)
        {
            if (player == "player1")
                P1Score();
            else
                P2Score();
        }

    }
}

