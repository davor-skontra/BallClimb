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
                _handler.RotationForce.Subscribe()
            };
        }

        private void OnCollisionEnter(Collision other)
        {
            _handler.SetFloorContact(true);
        }

        private void OnCollisionExit(Collision other)
        {
            _handler.SetFloorContact(false);
        }

        private void Jump(float force)
        {
            _rigidbody.AddForce(Vector3.up * force);
        }

        private void Rotate(float force)
        {
            _rigidbody.AddTorque(Vector3.right * force);
        }
    }
}