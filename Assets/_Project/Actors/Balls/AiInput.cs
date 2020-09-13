using System;
using AlkarInjector;
using AlkarInjector.Attributes;
using UniRx;
using UnityEngine;

namespace Actors.Balls
{
    public class AiInput : IBallInput
    {
        private Subject<float> RawDirectionSubject = new Subject<float>();

        private BallHandler _handler;

        private readonly BallKind _ballKind;
        private PlayerPositionService _playerPositionService;
        private IBallSettings _ballSettings;

        public AiInput(BallKind ballKind, PlayerPositionService playerPositionService, IBallSettings ballSettings)
        {
            _ballKind = ballKind;
            _playerPositionService = playerPositionService;
            _ballSettings = ballSettings;
        }

        public void TrackHandler(BallHandler handler)
        {
            _handler = handler;
        }

        public IObservable<float> RawDirection => Observable
            .EveryUpdate()
            .Where(
                _ => CloseToPlayer()
            )
            .Select(_ => GetDirection());

        public IObservable<Unit> Jump { get; } = new Subject<Unit>(); // Hostile balls don't jump so this is ignored

        private float GetDirection()
        {
            if (NotInitialized)
            {
                return 0f;
            }

            var left = _playerPositionService.PlayerPosition.Value.x < _handler.Position.x;
            var hostileDirection = left ? BallSettings.RotationBase : -BallSettings.RotationBase;
            var direction = _ballKind == BallKind.Hostile ? hostileDirection : -hostileDirection;
            
            return direction;
        }

        private bool CloseToPlayer()
        {
            if (NotInitialized)
            {
                return false;
            }
            
            return Vector3.Distance(_handler.Position, _playerPositionService.PlayerPosition.Value) <
                   _ballSettings.HostileChaseDistance;
        }

        private bool NotInitialized => !_playerPositionService.PlayerPosition.HasValue || _handler == null;
    }
}