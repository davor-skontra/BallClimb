using System;
using AlkarInjector;
using AlkarInjector.Attributes;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Actors.Balls
{
    public class BallHandler
    {
        [Inject] private BallInputFactory _ballInputFactory;
        [Inject] private IBallSettings _ballSettings;
        [Inject] private PlayerPositionService _playerPositionService;
        
        private IBallInput _input;
        private bool _touchingFloor;

        private Subject<Vector3> _position = new Subject<Vector3>();

        public BallHandler(BallKind ballKind)
        {
            Alkar.Inject(this);
            
            _input = _ballInputFactory.Get(ballKind);

            if (ballKind == BallKind.Player)
            {
                _playerPositionService.TrackPlayerHandler(this);
            }

            RotationTorque = _input
                .RawDirection
                .Select(
                    x => x * _ballSettings.Speed * Time.deltaTime
                )
                .Select(x => x * Vector3.forward);

            JumpForce = _input
                .Jump
                .Where(_ => _touchingFloor)
                .Select(_ => _ballSettings.JumpForce)
                .Select(x => x * Vector3.up);
        }

        public IObservable<Vector3> RotationTorque { get; }
        public IObservable<Vector3> JumpForce { get; }

        public IObservable<Vector3> Position => _position;

        public void SetFloorContact(bool contact)
        {
            _touchingFloor = contact;
        }

        public void UpdatePosition(Vector3 position)
        {
            _position.OnNext(position);
        }
    }
}