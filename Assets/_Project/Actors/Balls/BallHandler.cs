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
        private readonly BallKind _ballKind;
        private const float JumpThrottleSeconds = 0.1f;
        
        private Subject<float> _scaleAnimationSubject = new Subject<float>();

        [Inject] private BallInputFactory _ballInputFactory;
        [Inject] private IBallSettings _ballSettings;

        private IBallInput _input;
        private bool _touchingFloor;

        private Vector3 _position;

        public BallHandler(BallKind ballKind)
        {
            _ballKind = ballKind;
            Alkar.Inject(this);

            _input = _ballInputFactory.Get(ballKind);
            
            _input.TrackHandler(this);
            
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
                .ThrottleFirst(TimeSpan.FromSeconds(JumpThrottleSeconds));
            
        }

        public IObservable<Vector3> RotationTorque { get; }
        public IObservable<float> JumpForce { get; }

        public IObservable<float> ScaleAnimation => _scaleAnimationSubject;

        public Vector3 Position => _position;

        public void SetCollideWithOtherBall(BallKind otherBallKind)
        {
            
            switch (_ballKind)
            {
                case BallKind.Player:
                    if (otherBallKind == BallKind.Hostile)
                    {
                        _scaleAnimationSubject.OnNext(0f);
                    }
                    break;
                case BallKind.Hostile:
                    if (otherBallKind == BallKind.Friendly)
                    {
                        _scaleAnimationSubject.OnNext(0f);
                    }

                    if (otherBallKind == BallKind.Player)
                    {
                        _scaleAnimationSubject.OnNext(_ballSettings.AteScale);
                    }
                    break;
                case BallKind.Friendly:
                    if (otherBallKind == BallKind.Hostile)
                    {
                        _scaleAnimationSubject.OnNext(_ballSettings.AteScale);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(otherBallKind), otherBallKind, null);
            }
        }

        public void SetFloorContact(bool contact)
        {
            _touchingFloor = contact;
        }

        public void UpdatePosition(Vector3 position)
        {
            _position = position;
        }
    }
}