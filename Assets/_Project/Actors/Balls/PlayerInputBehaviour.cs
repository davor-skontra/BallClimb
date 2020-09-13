using System;
using UniRx;
using UnityEngine;

namespace Actors.Balls
{
    public class PlayerInputBehaviour: MonoBehaviour, IBallInput
    {
        private readonly Subject<float> _rawDirectionSubject = new Subject<float>();
        
        private readonly Subject<Unit> _jumpSubject = new Subject<Unit>();

        public IObservable<float> RawDirection => _rawDirectionSubject;

        public IObservable<Unit> Jump => _jumpSubject;

        public void ApplyJump()
        {
            _jumpSubject.OnNext(new Unit());
        }

        public void ApplyInput(float amount)
        {
            _rawDirectionSubject.OnNext(amount);
        }
    }
}