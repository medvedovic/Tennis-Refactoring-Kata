using Tennis.TennisGame1Files.Contracts;
using Tennis.TennisGame1Files.States;

namespace Tennis.TennisGame1Files
{
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
}

