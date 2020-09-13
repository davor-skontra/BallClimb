using System;

namespace Actors.Balls
{
    public class BallInputFactory
    {
        private readonly IBallInput _player;
        private readonly PlayerPositionService _positionService;
        private readonly IBallSettings _ballSettings;
        private readonly IBallInput _friendly;

        public BallInputFactory(IBallInput player, PlayerPositionService positionService, IBallSettings ballSettings)
        {
            _player = player;
            _positionService = positionService;
            _ballSettings = ballSettings;
        }
        
        public IBallInput Get(BallKind ballKind)
        {
            switch (ballKind)
            {
                case BallKind.Player:
                    return _player;
                case BallKind.Hostile:
                    return new AiInput(BallKind.Hostile, _positionService, _ballSettings);
                case BallKind.Friendly:
                    return new AiInput(BallKind.Friendly, _positionService, _ballSettings);;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ballKind), ballKind, null);
            }
        }
    }
}