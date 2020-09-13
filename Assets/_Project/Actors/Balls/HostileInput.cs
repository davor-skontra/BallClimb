using System;
using AlkarInjector;
using AlkarInjector.Attributes;
using UniRx;
using UnityEngine;

namespace Actors.Balls
{
    public class HostileInput : IBallInput
    {
        private Subject<float> RawDirectionSubject = new Subject<float>();

        private BallHandler _handler;

        private PlayerPositionService _playerPositionService;
        private IBallSettings _ballSettings;

        public HostileInput(PlayerPositionService playerPositionService, IBallSettings ballSettings)
        {
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

            return left ? BallSettings.RotationBase: -BallSettings.RotationBase;
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