using System;
using AlkarInjector;
using AlkarInjector.Attributes;
using Core.Disposal;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Actors.Balls
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class BallBehaviour : AutoDisposableBehaviour
    {
        [SerializeField] private BallKind _ballKind;


        [InjectComponent] private Rigidbody _rigidbody;
        [InjectComponent] private Collider _collider;

        [Inject] private IBallSettings _ballSettings;

        private BallHandler _handler;
        private JumpDirectionProvider _jumpDirectionProvider;
        private Tween _scaleAnimation;

        public BallKind BallKind => _ballKind;

        protected override IDisposable[] Init()
        {
            Alkar.InjectMonoBehaviour(this);
            
            _handler = new BallHandler(_ballKind);
            _jumpDirectionProvider = new JumpDirectionProvider();
            
            return new IDisposable[]
            {
                _handler.JumpForce.Subscribe(Jump),
                _handler.RotationTorque.Subscribe(Rotate),
                _handler.ScaleAnimation.Subscribe(ScaleAnimation)
            };
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherBall = other.gameObject.GetComponent<BallBehaviour>();

            if (otherBall != null)
            {
                _handler.SetCollideWithOtherBall(otherBall._ballKind);
            }
        }

        private void OnCollisionStay(Collision other)
        {
            _jumpDirectionProvider.SetCollisionContacts(other);
            _handler?.SetFloorContact(true);
        }

        private void OnCollisionExit(Collision other)
        {
            _handler?.SetFloorContact(false);
        }

        private void Jump(float force)
        {
            var jumpDirection = _jumpDirectionProvider.GetJumpDirection(transform.position) * force;
            
            _rigidbody.AddForce(jumpDirection);

            var s = _ballSettings;
            
            _scaleAnimation?.Kill();
            
            _scaleAnimation = transform
                .DOScale(Vector3.one * s.JumpAnimationScaleUp, s.JumpAnimationDuration)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(s.JumpAnimationEase)
                .OnPlay(() => _collider.isTrigger = true)
                .OnComplete(() => _collider.isTrigger = false)
                .Play();

        }

        private void ScaleAnimation(float scaleTo)
        {
            var scale = Vector3.one * scaleTo;
            var s = _ballSettings;
            _scaleAnimation = transform
                .DOScale(scale, s.EatAnimationDuration)
                .OnComplete(() =>
                {
                    if (transform.localScale.magnitude < 0.1f)
                    {
                        Destroy(gameObject);
                    }
                })
                .SetEase(s.EatAnimationEase);
        }

        private void Rotate(Vector3 torque)
        {
            _rigidbody.AddTorque(torque);
        }

        public void Update()
        {
            _handler.UpdatePosition(transform.position);
        }
    }
}