using System;
using AlkarInjector;
using AlkarInjector.Attributes;
using Core.Disposal;
using UniRx;
using Unitilities;
using UnityEngine;

namespace Actors.Balls
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class BallBehaviour : AutoDisposableBehaviour
    {
        [SerializeField] private BallKind _ballKind;
        
        [InjectComponent] private Rigidbody _rigidbody;

        private BallHandler _handler;

        protected override IDisposable[] Init()
        {
            Alkar.InjectMonoBehaviour(this);
            
            _handler = new BallHandler(_ballKind);
            
            return new IDisposable[]
            {
                _handler.JumpForce.Subscribe(Jump),
                _handler.RotationTorque.Subscribe(Rotate)
            };
        }

        private void OnCollisionEnter(Collision other)
        {
            _handler?.SetFloorContact(true);
        }

        private void OnCollisionExit(Collision other)
        {
            _handler?.SetFloorContact(false);
        }

        private void Jump(Vector3 force)
        {
            _rigidbody.AddForce(force);
        }

        private void Rotate(Vector3 torque)
        {
            _rigidbody.AddTorque(torque);
        }
    }
}