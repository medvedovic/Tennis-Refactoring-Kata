using Tennis.TennisGame1Files.Contracts;

namespace Tennis.TennisGame1Files.States
{
    internal abstract class TennisGameStateBase
    {
        protected readonly ITennisGameStateContext _context;

        protected TennisGameStateBase(ITennisGameStateContext context)
        {
            _context = context;
        }
    }
}
