using System;

namespace Tennis
{
    public class TennisGame3 : ITennisGame
    {
        private int player1Points;
        private int player2Points;
        private readonly string player1Name;
        private readonly string player2Name;
        private static readonly string[] indexedTextualScore = { "Love", "Fifteen", "Thirty", "Forty" };

        public TennisGame3(string player1Name, string player2Name)
        {
            this.player1Name = player1Name;
            this.player2Name = player2Name;
        }

        public string GetScore()
        {
            if (IsCurrentScoreSame())
                return IsDeuce() ? "Deuce" : $"{indexedTextualScore[player1Points]}-All";

            return HasAnyPlayerReachedFourPoints()
                ? GetTextualScoreForAdvantageOrWin()
                : $"{indexedTextualScore[player1Points]}-{indexedTextualScore[player2Points]}";
        }

        private string GetTextualScoreForAdvantageOrWin()
            => Math.Abs(GetCurrentScoreDifference()) == 1
            ? $"Advantage {GetLeadingPlayerName()}"
            : $"Win for {GetLeadingPlayerName()}";

        private int GetCurrentScoreDifference() => player1Points - player2Points;

        private bool HasAnyPlayerReachedFourPoints() => player1Points >= 4 || player2Points >= 4;

        private string GetLeadingPlayerName() => player1Points > player2Points ? player1Name : player2Name;

        private bool IsDeuce() => player1Points >= 3 && player2Points >= 3 && IsCurrentScoreSame();

        private bool IsCurrentScoreSame() => player1Points == player2Points;

        public void WonPoint(string playerName)
        {
            if (playerName == "player1")
                this.player1Points += 1;
            else
                this.player2Points += 1;
        }

    }
}

