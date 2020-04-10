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
            string s;
            if ((p1 < 4 && p2 < 4) && (p1 + p2 < 6))
                return IsCurrentScoreSame()
                    ? $"{indexedTextualScore[p1]}-All"
                    : $"{indexedTextualScore[p1]}-{indexedTextualScore[p2]}";
            else
            {
                if (IsCurrentScoreSame())
                    return "Deuce";
                s = p1 > p2 ? p1N : p2N;
                return ((p1 - p2) * (p1 - p2) == 1) ? "Advantage " + s : "Win for " + s;
            }
        }

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

