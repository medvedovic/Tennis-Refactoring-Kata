namespace Tennis.TennisGame1Files.Contracts
{
    internal interface ITennisGameStateContext
    {
        ITennisGameStateContext SetState(ITennisGameState newState);
        void WonPoint(CurrentScore score);
        string GetScore(CurrentScore score);
    }
}

