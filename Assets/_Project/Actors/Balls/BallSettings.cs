using DG.Tweening;
using UnityEngine;

namespace Actors.Balls
{
    public interface IBallSettings
    {
        float Speed { get; }
        float JumpForce { get; }
        float HostileChaseDistance { get; }
        float FriendlyAvoidDistance { get; }
        float JumpAnimationDuration { get; }
        Ease JumpAnimationEase { get; }
        float JumpAnimationScaleUp { get; }
        float EatAnimationDuration { get; }
        Ease EatAnimationEase { get; }
        float AteScale { get; }
    }

    [CreateAssetMenu]
    public class BallSettings : ScriptableObject, IBallSettings
    {
        public const float RotationBase = 1f;

        [Header("Movement")] [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _hostileChaseDistance;
        [SerializeField] private float _friendlyAvoidDistance;

        [Header("Animation")] [SerializeField] private float _jumpAnimationDuration;
        [SerializeField] private Ease _jumpAnimationEase;
        [SerializeField] private float _jumpAnimationScaleDown;
        [SerializeField] private float _eatAnimationDuration;
        [SerializeField] private Ease _eatAnimationEase;
        [SerializeField] private float _ateScale;
        
        public float Speed => _speed;
        public float JumpForce => _jumpForce;
        public float HostileChaseDistance => _hostileChaseDistance;
        public float FriendlyAvoidDistance => _friendlyAvoidDistance;
        public float JumpAnimationDuration => _jumpAnimationDuration;
        public Ease JumpAnimationEase => _jumpAnimationEase;
        public float JumpAnimationScaleUp => _jumpAnimationScaleDown;
        public float EatAnimationDuration => _eatAnimationDuration;
        public Ease EatAnimationEase => _eatAnimationEase;
        public float AteScale => _ateScale;
    }
}