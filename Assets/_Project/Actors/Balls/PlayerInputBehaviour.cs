using System;
using UniRx;
using UnityEngine;

namespace Actors.Balls
{
    public class PlayerInputBehaviour : MonoBehaviour, IBallInput
    {
        private readonly Subject<float> _rawDirectionSubject = new Subject<float>();
        private readonly Subject<Unit> _jumpSubject = new Subject<Unit>();
        private PlayerPositionService _playerPositionService;

        private bool _left;
        private bool _right;

        public IObservable<float> RawDirection => Observable
            .EveryUpdate()
            .Select(_ => GetRotation());

        public IObservable<Unit> Jump => Observable
            .EveryUpdate()
            .Where(_ => _left && _right)
            .AsUnitObservable();

        public void Initialize(PlayerPositionService positionService)
        {
            _playerPositionService = positionService;
        }

        public void TrackHandler(BallHandler handler)
        {
            _playerPositionService.TrackPlayerHandler(handler);
        }

        public void Left(bool down)
        {
            _left = down;
        }

        public void Right(bool down)
        {
            _right = down;
        }

        private float GetRotation()
        {
            if (_left && !_right)
            {
                return BallSettings.RotationBase;
            }

            if (!_left && _right)
            {
                return -BallSettings.RotationBase;
            }

            return 0f;
        }
        
#if UNITY_EDITOR
        public void Update()
        {

            Left(Input.GetKey(KeyCode.LeftArrow));
            Right(Input.GetKey(KeyCode.RightArrow));
        }
#endif

    }
}