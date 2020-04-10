namespace Tennis.TennisGame1Files.Contracts
{
    internal interface ITennisGameState
    {
        void WonPoint(CurrentScore score);
        string GetScore(CurrentScore score);
    }
}

