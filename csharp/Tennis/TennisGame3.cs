namespace Tennis
{
    public class TennisGame3 : ITennisGame
    {
        private int p2;
        private int p1;
        private string p1N;
        private string p2N;
        private static readonly string[] indexedTextualScore = { "Love", "Fifteen", "Thirty", "Forty" };

        public TennisGame3(string player1Name, string player2Name)
        {
            this.p1N = player1Name;
            this.p2N = player2Name;
        }

        public string GetScore()
        {
            if (IsCurrentScoreSame())
                return IsDeuce() ? "Deuce" : $"{indexedTextualScore[p1]}-All";

            if (p1 >= 4 || p2 >= 4)
                return ((p1 - p2) * (p1 - p2) == 1) ? $"Advantage {GetLeadingPlayerName()}" : $"Win for {GetLeadingPlayerName()}";
            else
                return $"{indexedTextualScore[p1]}-{indexedTextualScore[p2]}";
        }

        private string GetLeadingPlayerName() => p1 > p2 ? p1N : p2N;

        private bool IsDeuce() => p1 >= 3 && p2 >= 3 && IsCurrentScoreSame();

        private bool IsCurrentScoreSame() => p1 == p2;

        public void WonPoint(string playerName)
        {
            if (playerName == "player1")
                this.p1 += 1;
            else
                this.p2 += 1;
        }

    }
}

