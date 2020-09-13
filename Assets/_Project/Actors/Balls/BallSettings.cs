using UnityEngine;

namespace Actors.Balls
{
    public interface IBallSettings
    {
        float Speed { get; }
        float JumpForce { get; }
        float HostileChaseDistance { get; }
        float FriendlyAvoidDistance { get; }
    }

    [CreateAssetMenu]
    public class BallSettings : ScriptableObject, IBallSettings
    {
        public const float RotationBase = 1f;

        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _hostileChaseDistance;
        [SerializeField] private float _friendlyAvoidDistance;

        public float Speed => _speed;

        public float JumpForce => _jumpForce;
        public float HostileChaseDistance => _hostileChaseDistance;
        public float FriendlyAvoidDistance => _friendlyAvoidDistance;
    }
}