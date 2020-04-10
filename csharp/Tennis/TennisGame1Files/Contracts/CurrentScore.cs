namespace Tennis.TennisGame1Files.Contracts
{
    internal struct CurrentScore
    {
        public int playerOneScore;
        public int playerTwoScore;

        public bool IsSame() => playerOneScore == playerTwoScore;
        public int GetScoreDifference() => playerOneScore - playerTwoScore;
    }
}

