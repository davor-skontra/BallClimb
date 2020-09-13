using System;

namespace Actors.Balls
{
    public class BallInputFactory
    {
        private readonly IBallInput _player;
        private readonly IBallInput _hostile;
        private readonly IBallInput _friendly;

        public BallInputFactory(IBallInput player, PlayerPositionService positionService)
        {
            _player = player;
        }
        
        public IBallInput Get(BallKind ballKind)
        {
            switch (ballKind)
            {
                case BallKind.Player:
                    return _player;
                case BallKind.Hostile:
                    return _hostile;
                case BallKind.Friendly:
                    return _friendly;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ballKind), ballKind, null);
            }
        }
    }
}