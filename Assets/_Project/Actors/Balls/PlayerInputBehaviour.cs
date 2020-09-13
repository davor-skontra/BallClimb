using System;
using UniRx;
using UnityEngine;

namespace Actors.Balls
{
    public class PlayerInputBehaviour : MonoBehaviour, IBallInput
    {
        private const float RotationBase = 1f;
        private readonly Subject<float> _rawDirectionSubject = new Subject<float>();

        private readonly Subject<Unit> _jumpSubject = new Subject<Unit>();

        private bool _left;
        private bool _right;

        public IObservable<float> RawDirection => Observable
            .EveryUpdate()
            .Select(_ => GetRotation());

        public IObservable<Unit> Jump => Observable
            .EveryUpdate()
            .Where(_ => _left && _right)
            .AsUnitObservable();

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
                return RotationBase;
            }

            if (!_left && _right)
            {
                return -RotationBase;
            }

            return 0f;
        }
    }
}